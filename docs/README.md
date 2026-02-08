# BitBlazor - Component Documentation

Welcome to the complete documentation for BitBlazor, a Blazor component library based on [Bootstrap Italia](https://italia.github.io/bootstrap-italia/docs/come-iniziare/introduzione/).

## Overview

BitBlazor provides a comprehensive set of ready-to-use UI components that follow the design guidelines of the Bootstrap Italia system. Each component is designed to be accessible, performant, and easily customizable.

## Library Structure

The library is organized into these namespaces:

- **`BitBlazor.Components`**: Core UI components
- **`BitBlazor.Form`**: Form components with validation support
- **`BitBlazor.Utilities`**: Utilities and support components

## Available Components

### UI Components

#### [Alert](components/alert.md)
Alert system for contextual user feedback.
- Different alert types (primary, info, success, warning, danger)
- Support for dismissible alerts
- Event handling callbacks

#### [Avatar](components/avatar.md)
User avatar component for displaying profile images, icons, or initials.
- Support for images, icons, or text initials
- User status and presence indicators
- Clickable links and extra descriptive text
- Can be used standalone or within avatar groups

#### [Badge](components/badge.md)
Small and adaptable labels for adding information.
- Solid and outline variants
- Support for rounded shapes
- Various colors available

#### [Breadcrumb](components/breadcrumb.md)
Breadcrumb navigation component to display the current location within a hierarchy.
- Support for the customization of items and the separator
- Accessibility support

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

### Form Components

BitBlazor provides a comprehensive set of form components that integrate seamlessly with ASP.NET Core Blazor's form system and Bootstrap Italia styling.

#### [Form Components Overview](form/form-components.md)
Complete guide to form components, including validation, accessibility, and best practices.

#### [BitTextField](form/text-field.md)
Single-line text input field component.
- Support for different input types (text, email, tel, url)
- Input groups with prepend/append content
- Multiple sizes and states
- Built-in validation support

#### [BitPasswordField](form/password-field.md)
Password input field with show/hide functionality.
- Toggle password visibility
- Secure input handling
- Accessibility-compliant design
- Validation integration

#### [BitTextAreaField](form/text-area-field.md)
Multi-line text input field component.
- Configurable row height
- Auto-sizing capabilities
- Character count guidance
- Form validation support

#### [BitNumberField](form/number-field.md)
Numeric input field component with increment/decrement controls.
- Support for multiple numeric types (int, long, short, float, double, decimal)
- Built-in increment/decrement buttons
- Min/max value constraints
- Custom step values and symbol content

#### [BitSelectField](form/select-field.md)
Dropdown select input field component.
- Generic type support for any value type
- Support for option grouping
- Disabled options and groups
- Form validation integration
- Accessible and responsive

#### [BitCheckbox](form/checkbox.md)
Checkbox component for binary choices.
- Boolean value selection
- Inline and grouped layouts
- Form validation support
- Disabled states and accessibility

#### [BitRadio](form/radio.md)
Radio button component for single-choice selection from a set of options.
- Group-based value selection
- Support for any data type
- Inline and grouped layouts
- Form validation support
- Disabled states and accessibility

#### [BitDatepicker](form/datepicker.md)
Date input field component for selecting dates.
- Support for DateTime and DateOnly types
- Native browser date picker UI
- Form validation integration
- Accessible and responsive

#### [BitTimepicker](form/timepicker.md)
Time input field component for selecting times.
- Support for TimeOnly type
- Native browser time picker UI
- Form validation integration
- Accessible and responsive

#### [BitToggle](form/toggle.md)
Toggle switch component for binary on/off selection.
- Visual indicator for on/off state
- Inline and grouped layouts
- Form validation support
- Modern alternative to checkboxes

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
