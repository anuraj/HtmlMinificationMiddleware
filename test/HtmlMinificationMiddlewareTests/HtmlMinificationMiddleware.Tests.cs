using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.TestHost;
using Xunit;
using DotnetThoughts.AspNet;
using System.Text.RegularExpressions;

namespace DotnetThoughts.AspNet.Tests
{
    public class HtmlMinificationMiddlewareTests
    {
        [Fact]
        public async Task MiddlewareShouldNotModifyNonHTMLContent()
        {
            var responseContent = "body { color:red; }";
            var server = TestServer.Create(app =>
            {
                app.UseHTMLMinification();
                app.Run(context =>
                {
                    context.Response.ContentType = "text/css";
                    context.Response.StatusCode = 200;
                    return context.Response.WriteAsync(responseContent);
                });
            });

            using (server)
            {
                var response = await server.CreateClient().GetAsync("/");
                var data = await response.Content.ReadAsStringAsync();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(data.Length == responseContent.Length);
            }
        }
        
        [Fact]
        public async Task MiddlewareShouldRemoveWhiteSpaceInHTML()
        {
            var responseContent = "<html><head><title>" +
            "        Hello</title>  </head><body>      "+
            "                            </body></html>";
            var server = TestServer.Create(app =>
            {
                app.UseHTMLMinification();
                app.Run(context =>
                {
                    context.Response.ContentType = "text/html";
                    context.Response.StatusCode = 200;
                    return context.Response.WriteAsync(responseContent);
                });
            });

            using (server)
            {
                var response = await server.CreateClient().GetAsync("/");
                var data = await response.Content.ReadAsStringAsync();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var expected = Regex.Replace(responseContent, 
                    @">\s+<", "><", RegexOptions.Compiled);
                Assert.True(data.Length == expected.Length);
            }
        }
    }
}