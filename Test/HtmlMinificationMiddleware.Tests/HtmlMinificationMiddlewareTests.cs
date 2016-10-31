using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace DotnetThoughts.AspNetCore.Tests
{
    public class HtmlMinificationMiddlewareTests
    {
        [Fact]
        public async Task MiddlewareShouldRemoveWhiteSpaceInHTML()
        {
            var responseContent = "<html><head><title>" +
            "        Hello</title>  </head><body>      "+
            "                            </body></html>";
            var builder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseHTMLMinification();
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync(responseContent);
                    });
                });

            var server = new TestServer(builder); 

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
        
        [Fact]
        public async Task MiddlewareShouldNotModifyNonHTMLContent()
        {
            var responseContent = "body { color:red; }";
            var builder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseHTMLMinification();
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "text/css";
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync(responseContent);
                    });
                });

            var server = new TestServer(builder);            

            using (server)
            {
                var response = await server.CreateClient().GetAsync("/");
                var data = await response.Content.ReadAsStringAsync();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(data.Length == responseContent.Length);
            }
        }
    }
}
