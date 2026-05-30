# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.3.0] - 2026-05-30

### Added

#### Components
- [x] **Modal** - BitModal component for accessible modal dialogs with no JavaScript interop required (#87)
  - [x] ModalSize, ModalPosition, ModalType, ModalBackdrop enumerations
- [x] **Pagination** - BitPagination component for navigating large data sets split across multiple pages (#90)
  - [x] BitPageItem - Individual page item sub-component
  - [x] PaginationViewMode, PaginationAlignment enumerations
  - [x] PaginationState record struct for Simple mode template context
  - [x] Supports both interactive and static SSR render modes

#### Documentation
- [x] Modal component documentation
- [x] Pagination component documentation

### Changed
- [x] Updated NuGet packages
- [x] Updated Bootstrap Italia to version 2.18.1

## [0.2.3] - 2026-02-28

### Added

#### Components
- [x] **Breadcrumb** - BitBreadcrumb component for navigation breadcrumbs (#7)

#### Form Components
- [x] **SelectField** - BitSelectField component for dropdown selection with form integration (#38)
  - [x] BitSelectItem - Individual selectable option component
  - [x] BitSelectItemGroup - Option group component

#### Documentation
- [x] SelectField documentation
- [x] Breadcrumb documentation

### Changed
- [x] Updated Bootstrap Italia to version 2.17.5
- [x] Updated Bootstrap Italia to version 2.17.1 (#73)

### Fixed
- [x] Fixed validation classes in form components (#59)
- [x] CI/CD pipeline now correctly skips bitblazor-stories GHA in forked repositories (#75)

## [0.2.2] - 2026-01-16

### Added

#### Form Components
- [x] **Toggle** - BitToggle component for toggle/switch input with ToggleViewMode enumeration
- [x] **Checkbox** - BitCheckbox component for checkbox selection
- [x] **Radio** - Radio button components including:
  - [x] BitRadio - Individual radio button component
  - [x] BitRadioGroup - Group multiple radio buttons together

#### Documentation
- [x] Toggle documentation
- [x] Checkbox documentation
- [x] Radio documentation

## [0.2.1] - 2025-11-21

### Added

#### Form Components
- [x] **Datepicker** - BitDatepicker component for date selection
- [x] **Timepicker** - BitTimepicker component for time selection

#### Documentation
- [x] Datepicker documentation
- [x] Timepicker documentation

## [0.2.0] - 2025-10-24

### Added

#### Components
- [x] **Avatar** - Avatar component system including:
  - [x] BitAvatar - Main avatar component for displaying user profile pictures
  - [x] BitAvatarGroup - Group multiple avatars together
  - [x] BitAvatarGroupItem - Individual avatar item within a group
  - [x] PresenceStatus enumeration - Online, offline, busy status indicators
  - [x] UserStatus enumeration - User status types

#### Form Components
- [x] **TextField** - BitTextField component with TextFieldType enumeration for various input types
- [x] **PasswordField** - BitPasswordField component for secure password input
- [x] **NumberField** - BitNumberField component for numeric input with validation
- [x] **TextAreaField** - BitTextAreaField component for multi-line text input
- [x] BitFormComponentBase - Base class for all form components
- [x] BitInputFieldBase - Base class for input field components

#### Documentation
- [x] Avatar component documentation
- [x] Form components documentation
- [x] TextField documentation
- [x] PasswordField documentation
- [x] NumberField documentation
- [x] TextAreaField documentation

## [0.1.0] - 2025-08-19

### Added

#### Components
- [x] **Alert** - BitAlert component with AlertType enumeration
- [x] **Badge** - BitBadge component for displaying small status indicators
- [x] **Button** - BitButton component with ButtonBadge, ButtonType, and IconPosition support
- [x] **Card** - Comprehensive BitCard component system including:
  - [x] BitCard (main card component)
  - [x] CardBannerIcon
  - [x] CardBody
  - [x] CardDate
  - [x] CardFooter
  - [x] CardImage
  - [x] CardImageWrapper
  - [x] CardProfileHeader
  - [x] CardProfileIcon
  - [x] CardSignature
  - [x] CardSubtitle
  - [x] CardText
  - [x] CardTitle
  - [x] CardTitleIcon
  - [x] InlineCardContent

#### Core Features
- [x] BitComponentBase - Base class for all components
- [x] CssClassBuilder - Utility for building CSS classes
- [x] Color enumeration
- [x] Size enumeration
- [x] Variant enumeration
- [x] Typography enumeration
- [x] Ratio enumeration

#### Utilities
- [x] **Icon** - BitIcon component for displaying Bootstrap Italia SVG icons
  - [x] IconSize, IconColor, IconAlignment enumerations
  - [x] Icons static class with all Bootstrap Italia icon names

#### Infrastructure
- [x] .NET 9 support
- [x] Bootstrap Italia styling integration
- [x] Comprehensive documentation structure
- [x] Test project setup
- [x] Stories project for component showcasing

### Initial Release
This is the initial release of BitBlazor, providing a foundation of accessible Blazor components styled with Bootstrap Italia.
