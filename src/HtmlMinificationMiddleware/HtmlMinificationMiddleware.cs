namespace DotnetThoughts.AspNet
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using System.IO;
    using System.Threading.Tasks;
    using System.Text;
    using System.Text.RegularExpressions;

    public class HtmlMinificationMiddleware
    {
        private RequestDelegate _next;
        public HtmlMinificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var stream = context.Response.Body;
            using (var buffer = new MemoryStream())
            {
                context.Response.Body = buffer;
                await _next(context);
                var isHtml = context.Response.ContentType?.ToLower().Contains("text/html");
                if (context.Response.StatusCode == 200 && isHtml.GetValueOrDefault())
                {
                    buffer.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(buffer))
                    {
                        string responseBody = await reader.ReadToEndAsync();
                        responseBody = Regex.Replace(responseBody, @">\s+<", "><", RegexOptions.Compiled);
                        using (var memoryStream = new MemoryStream())
                        {
                            var bytes = Encoding.UTF8.GetBytes(responseBody);
                            memoryStream.Write(bytes, 0, bytes.Length);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            await memoryStream.CopyToAsync(stream, bytes.Length);
                        }
                    }
                }
            }
        }
    }
}