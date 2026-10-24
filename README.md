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
    <!-- optional: include Bootstrap Italia default fonts (Titillium Web, Lora, Roboto Mono) -->
    <link rel="stylesheet" href="_content/BitBlazor/css/fonts.css" />
</head>
```

### JavaScript

No JavaScript setup is required. Components that need Bootstrap Italia JavaScript (currently `BitTooltip` and `BitPopover`) load the bundled copy on demand, only when first rendered.

- **Render modes**: works in interactive render modes (Server, WebAssembly, Auto). In static SSR pages the components render their content but no tooltip/popover is attached, because it requires JS interop.
- **Supported version**: Bootstrap Italia `2.18.x`, the version shipped with the package. The JavaScript and the CSS must be the same version.
- **Bring your own copy**: if the page already provides Bootstrap Italia JavaScript (`window.bootstrap`), BitBlazor reuses it instead of loading its own. Any version other than the supported one logs a `console.warn` and is used at your own risk. The host `<script>` must be classic and synchronous (no `defer`, `async` or `type="module"`) and placed **before** `blazor.web.js` / `blazor.webassembly.js`, in `App.razor` or `index.html` (not in a page or layout).
- **Content Security Policy**: the bundle is loaded with a dynamic `import()`, so `script-src` must allow `'self'` (or `'strict-dynamic'`). Nonce-only policies block it; in that case load the bundle yourself with a nonced `<script>` as described above.

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
