# A Markdown compiler for ASP.NET Core

[![Build status](https://ci.appveyor.com/api/projects/status/6mx0r3dvvje0h529?svg=true)](https://ci.appveyor.com/project/madskristensen/weboptimizer-markdown)
[![NuGet](https://img.shields.io/nuget/dt/LigerShark.WebOptimizer.Markdown.svg)](https://nuget.org/packages/LigerShark.WebOptimizer.Markdown/)

This package compiles markdown files into HTML by hooking into the [LigerShark.WebOptimizer](https://github.com/ligershark/WebOptimizer) pipeline.


You can reference any markdown file directly in the browser and a compiled and HTML document will be served. To set that up, do this:

```c#
services.AddWebOptimizer(pipeline =>
{
    pipeline.CompileMarkdownFiles();
});
```

Or if you just want to parse specific markdown files, do this:

```c#
services.AddWebOptimizer(pipeline =>
{
    pipeline.CompileMarkdownFiles("/path/file1.md", "/path/file2.md");
});
```