# BitBlazor - Component Documentation

Welcome to the complete documentation for BitBlazor, a Blazor component library based on [Bootstrap Italia](https://italia.github.io/bootstrap-italia/docs/come-iniziare/introduzione/).

## Overview

BitBlazor provides a comprehensive set of ready-to-use UI components that follow the design guidelines of the Bootstrap Italia system. Each component is designed to be accessible, performant, and easily customizable.

## Library Structure

The library is organized into two main namespaces:

- **`BitBlazor.Components`**: Core UI components
- **`BitBlazor.Utilities`**: Utilities and support components

## Available Components

### UI Components

#### [Alert](components/alert.md)
Alert system for contextual user feedback.
- Different alert types (primary, info, success, warning, danger)
- Support for dismissible alerts
- Event handling callbacks

#### [Badge](components/badge.md)
Small and adaptable labels for adding information.
- Solid and outline variants
- Support for rounded shapes
- Various colors available

#### [Button](components/button.md)
Interactive buttons with support for icons and different styles.
- Solid and outline variants
- Multiple sizes
- Support for icons with customizable positioning
- Disabled states

#### [ButtonBadge](components/button-badge.md)
Specialized badge for use within buttons.
- Automatic integration with parent button
- Automatic variant inversion
- Accessibility support

#### [Card](components/card.md)
Complete card system for organizing content.
- Different types (default, profile, banner)
- Inline and standard layouts
- Modular components (header, body, footer, etc.)
- Support for images and icons

### Utilities

#### [Icon](utilities/icon.md)
Icon system based on Bootstrap Italia.
- Wide range of available icons
- Multiple sizes and colors
- Customizable alignment
- Full accessibility support

## Common Enumerations

### Color
Used in many components to define colors:
- `Primary`: Theme primary color
- `Secondary`: Secondary color
- `Success`: Green to indicate success
- `Warning`: Yellow/orange for warnings
- `Danger`: Red for errors or dangerous actions

### Variant
Defines the display style:
- `Solid`: Full color fill
- `Outline`: Colored border only, transparent background

### Size
Standard sizes for components:
- `Mini`: Very small size
- `Small`: Small size
- `Default`: Standard size
- `Large`: Large size

### Ratio
Aspect ratios for media elements:
- `Ratio1x1`: Square (1:1)
- `Ratio4x3`: Traditional (4:3) 
- `Ratio16x9`: Widescreen (16:9)
- `Ratio21x9`: Ultra-wide (21:9)

For a complete list of all enumerations and their usage, see [Enumerations](enumerations.md).

## Design Principles

### Accessibility
All components follow WCAG accessibility guidelines:
- Screen reader support
- Keyboard navigation
- Appropriate color contrast
- Correct ARIA attributes

### Customization
Components support various customization methods:
- Parameters for appearance and behavior
- Custom CSS classes
- Style overrides via CSS

### Responsive Design
All components are designed to be responsive and work on:
- Desktop
- Tablet
- Mobile

## Bootstrap Italia

BitBlazor is based on [Bootstrap Italia](https://italia.github.io/bootstrap-italia/), the Bootstrap theme for Italian Public Administration websites. This ensures:

- Compliance with Italian government design guidelines
- Accessibility according to European standards
- Consistent and professional design system

## Installation and Configuration

For installation and configuration information, please refer to the project README.

## Contributing

BitBlazor is an open source project. Contributions, bug reports, and feature requests are welcome through the project's GitHub repository.

## Support

For questions, issues, or suggestions:
- Consult this documentation
- Check the usage examples
- Open an issue in the GitHub repository

## Compatibility

- **.NET**: 9.0+
- **Blazor**: Server and WebAssembly
- **Browser**: All modern browsers supported by Blazor

---

*This documentation is automatically generated and reflects the current state of the BitBlazor library.*
