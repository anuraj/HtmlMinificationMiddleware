# HTML Minification middleware for ASP.NET Core

Minification refers to the process of removing unnecessary or redundant data without affecting how the resource is processed by the browser - e.g. code comments and formatting, removing unused code, using shorter variable and function names, and so on. This repository contains source code of an ASP.NET Core middleware which helps to minify HTML.

How to use HTML Minification middleware for ASP.NET Core
--------------------------------
* Include HtmlMinification Middleware middleware in the project.json file.
```Javascript
{
    "version": "1.0.0-*",
    "webroot": "wwwroot",
    "dependencies": {
        "HtmlMinificationMiddleware": "1.0.0"
    }
}
```
* Modify the startup.cs - configure to enable HTML minification.
```Javascript
public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
{
    loggerFactory.AddConsole();
    app.UseIISPlatformHandler();
    app.UseHTMLMinification();
    app.UseDeveloperExceptionPage();
    app.UseMvcWithDefaultRoute();
}
```
* Done. Now you can browse the URL.

### Excluding URLs from minification.

You can exclude certain pages from minification by using the exclude filter option.

```Javascript
app.UseHTMLMinification("(w*)Page*");
```

Appveyor Build Status : [![Build status](https://ci.appveyor.com/api/projects/status/pyltm6fuc9qo8xkq?svg=true)](https://ci.appveyor.com/project/anuraj/htmlminificationmiddleware)