---
$attribute: CustomPage("Welcome")
---

# Welcome to BitBlazor!

BitBlazor is a UI library that provides accessible, reusable Blazor components styled with [Bootstrap Italia](https://italia.github.io/bootstrap-italia/docs).  
The goal is to offer a comprehensive set of components for .NET 9 applications, following accessibility best practices and the official Bootstrap Italia design system.

## Features

- Accessible Blazor components
- Styled with Bootstrap Italia
- .NET 9+ support
- Comprehensive documentation

## Getting started

### Installation & Prerequisites

The library requires **.NET 9+** and can be installed via [NuGet](https://www.nuget.org/packages/BitBlazor):

```bash
dotnet add package BitBlazor
```

### Add the Bootstrap Italia CSS

Once the package is installed, simply add the reference to the Bootstrap Italia CSS included in the head section of your project:

```html
<head>
    <!-- other css imports -->
    <link rel="stylesheet" href="_content/BitBlazor/bootstrap-italia/css/bootstrap-italia.min.css" />
</head>
```

Additional informations can be found at the [GitHub repository of the project](https://github.com/albx/bitblazor).

The repository includes all the documentation, which can be found in the [docs](https://github.com/albx/bitblazor/blob/main/docs/README.md) folder.

## License

The project is released under [MIT](https://github.com/albx/bitblazor/blob/main/LICENSE) license.