# HTML Minification middleware for ASP.NET Core

Minification refers to the process of removing unnecessary or redundant data without affecting how the resource is processed by the browser - e.g. code comments and formatting, removing unused code, using shorter variable and function names, and so on. This repository contains source code of an ASP.NET Core middleware which helps to minify HTML.

How to use HTML Minification middleware for ASP.NET Core
--------------------------------
* Include HtmlMinification Middleware middleware in the project file.
```Xml
<ItemGroup>
  <PackageReference Include="HtmlMinificationMiddleware" Version="3.1.0" />
</ItemGroup>
```
* Modify the startup.cs - configure to enable HTML minification.
```Javascript
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    /* Code removed for brevity. */
    app.UseHTMLMinification();
    app.UseStaticFiles();
    /* Code removed for brevity. */
}
```
* Done. Now you can browse the URL.

### Excluding URLs from minification.

You can exclude certain pages from minification by using the exclude filter option.

```Javascript
app.UseHTMLMinification("(w*)Page*");
```

### For ASP.NET MVC Core 1.1+

In ASP.NET Core MVC 1.1 onwards, you can use middlewares as Filters, so instead of using the Minification options, you can use `MiddlewareFilter` attribute, either in Controller or in specific action methods.

```Javascript
[MiddlewareFilter(typeof(HtmlMinificationPipeline))]
public class HomeController : Controller
{
}
```

or 

```Javascript
public class HomeController : Controller
{
    [MiddlewareFilter(typeof(HtmlMinificationPipeline))]
    public IActionResult Index()
    {
        return View();
    }
}
```

[![Build Status](https://dev.azure.com/anurajp/HtmlMinificationMiddleware/_apis/build/status/HtmlMinificationMiddleware-CI?branchName=master)](https://dev.azure.com/anurajp/HtmlMinificationMiddleware/_build/latest?definitionId=6&branchName=master)