namespace DotnetThoughts.AspNet
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;

    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseHTMLMinification(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HtmlMinificationMiddleware>();
        }
    }
}