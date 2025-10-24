# BitBlazor

[![Tests](https://github.com/albx/bitblazor/actions/workflows/bitblazor-testrunner.yml/badge.svg)](https://github.com/albx/bitblazor/actions/workflows/bitblazor-testrunner.yml) [![CodeQL](https://github.com/albx/bitblazor/actions/workflows/bitblazor-codeql.yml/badge.svg)](https://github.com/albx/bitblazor/actions/workflows/bitblazor-codeql.yml)


BitBlazor is a UI library that provides accessible, reusable Blazor components styled with [Bootstrap Italia](https://italia.github.io/bootstrap-italia/docs).  
The goal is to offer a comprehensive set of components for .NET 9 applications, following accessibility best practices and the official Bootstrap Italia design system.

## Features

- Accessible Blazor components
- Styled with Bootstrap Italia
- .NET 9+ support
- Comprehensive documentation

## Installation & Prerequisites

Requires **.NET 9+**.

Install via [NuGet](https://www.nuget.org/packages/BitBlazor):

```bash
dotnet add package BitBlazor
```

## Getting started

Once the package is installed, simply add the reference to the Bootstrap Italia CSS included in the head section of your project:

```html
<head>
    <!-- other css imports -->
    <link rel="stylesheet" href="_content/BitBlazor/bootstrap-italia/css/bootstrap-italia.min.css" />
</head>
```

## Documentation

Comprehensive documentation can be found in the [docs](https://github.com/albx/bitblazor/blob/main/docs/README.md) folder.

A [Storybook](https://storybook.js.org/) for the library is available [here](https://bitblazor-stories-gwc2hdexede9cwgf.italynorth-01.azurewebsites.net).

The storybook website is made using the project [Blazing Story](https://github.com/jsakamoto/BlazingStory) by @jsakamoto.

## Roadmap

For the development plan and component list, see [ROADMAP.md](https://github.com/albx/bitblazor/blob/main/ROADMAP.md).

See the [CHANGELOG.md](https://github.com/albx/bitblazor/blob/main/CHANGELOG.md) file to keep track of all the project's releases.

## Contributing

Contributions are always welcome!

If you have any issues, feedback, or feature requests, please see the [CONTRIBUTING](https://github.com/albx/bitblazor/blob/main/CONTRIBUTING.md) file.


## License

The project is released under [MIT](https://github.com/albx/bitblazor/blob/main/LICENSE) license.
