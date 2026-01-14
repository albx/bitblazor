# BitAvatar

The `BitAvatar` component represents an [avatar using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/avatar/).

## Namespace

```csharp
BitBlazor.Components
```


## Description

The BitAvatar component displays a user avatar using an image, icon, or initials. It can show user status and presence indicators, extra descriptive text, and supports clickable links. The component is accessible, customizable, and can be used standalone or within an avatar group.

## Parameters

| Name                        | Type                          | Required | Default | Description                                                        |
|-----------------------------|-------------------------------|----------|---------|--------------------------------------------------------------------|
| `Text`                      | `string?`                     | ✓        | -       | The full name or label for the avatar.                             |
| `TextShort`                 | `string?`                     | ✗        | -       | Short name or initials to display if no image or icon is provided. |
| `Image`                     | `string?`                      | ✗        | -       | URL of the avatar image.                                           |
| `Icon`                      | `string?`                      | ✗        | -       | Name of the icon to display.                                       |
| `IconColor`                 | `Color`                       | ✗        | `Color.Default` | Color of the icon.                                                 |
| `BackgroundColor`           | `Color`                       | ✗        | -       | Background color of the avatar.                                    |
| `Size`                      | `Size`                        | ✗        | `Size.Default`  | Size of the avatar (e.g., Small, Medium, Large).                   |
| `Link`                      | `string'`                     | ✗        | -       | URL to navigate when the avatar or extra text is clicked.          |
| `ExtraText`                 | `string?`                     | ✗        | -       | Additional descriptive text below the avatar.                      |
| `UserStatus`                | `UserStatus`                  | ✗        | `UserStatus.None` | User status.                                              |
| `UserStatusIcon`            | `string?`                     | ✗        | -       | Icon for user status.                                              |
| `UserStatusIconColor`       | `IconColor`                   | ✗        | `IconColor.White` | Color for user status icon.                                        |
| `UserStatusDescription`     | `string?`                     | ✗        | -       | Description for user status (for accessibility).                   |
| `PresenceStatus`            | `PresenceStatus`              | ✗        | `PresenceStatus.None` | Icon for presence status.                                          |
| `PresenceStatusIcon`        | `string?`                     | ✗        | -       | Icon for presence status.                                          |
| `PresenceStatusIconColor`   | `IconColor`                   | ✗        | `IconColor.White` | Color for presence status icon.                                    |
| `PresenceStatusDescription` | `string?`                     | ✗        | -       | Description for presence status (for accessibility).               |
| `CssClass`                  | `string?`                     | ✗        | -       | Custom CSS class for the avatar.                                   |
| `AdditionalAttributes`      | `Dictionary<string, object>`  | ✗        | -       | Additional HTML attributes.                                        |


When used in a group, the avatar is rendered as a `BitAvatarGroupItem` and has a limited subset of parameters available:

| Name                        | Type                          | Required | Default | Description                                                        |
|-----------------------------|-------------------------------|----------|---------|--------------------------------------------------------------------|
| `Text`                      | `string?`                     | ✓        | -       | The full name or label for the avatar.                             |
| `TextShort`                 | `string?`                     | ✗        | -       | Short name or initials to display if no image or icon is provided. |
| `Image`                     | `string?`                     | ✗        | -       | URL of the avatar image.                                           |
| `Icon`                      | `string?`                     | ✗        | -       | Name of the icon to display.                                       |
| `IconColor`                 | `Color`                       | ✗        | `Color.Default` | Color of the icon.                                                 |
| `BackgroundColor`           | `Color`                       | ✗        | -       | Background color of the avatar.                                    |
| `Size`                      | `Size`                        | ✗        | `Size.Default`  | Size of the avatar (e.g., Small, Medium, Large).                   |
| `Link`                      | `string'`                     | ✗        | -       | URL to navigate when the avatar or extra text is clicked.          |
| `CssClass`                  | `string?`                     | ✗        | -       | Custom CSS class for the avatar.                                   |
| `AdditionalAttributes`      | `Dictionary<string, object>`  | ✗        | -       | Additional HTML attributes.                                        |

## UserStatus Enumeration

The `UserStatus` enumeration in `BitBlazor.Components` defines the possible user status values that can be displayed within a BitAvatar component. This status is typically shown as a badge or indicator on the avatar, helping to visually communicate the user's current state.

### Members

| Value      | Description                                 |
|------------|---------------------------------------------|
| `None`     | No user status provided.                    |
| `Approved` | The user has been approved.                 |
| `Declined` | The user has been declined.                 |
| `Notified` | The user has been notified.                 |

## PresenceStatus Enumeration

The `PresenceStatus` enumeration in `BitBlazor.Components` defines the possible presence status values that can be displayed within a BitAvatar component. This status is typically shown as a badge or indicator on the avatar, helping to visually communicate the user's current availability or activity.

### Members

| Value    | Description                                              |
|----------|----------------------------------------------------------|
| `None`   | No presence status provided.                             |
| `Active` | The user is active.                                      |
| `Busy`   | The user is busy.                                        |
| `Hidden` | The presence status of the user is hidden or private.    |


## Usage Examples

### Basic avatar

```razor
<BitAvatar Text="Gioacchino Romani" />
```

### Avatar with image

```razor
<BitAvatar 
	Text="Gioacchino Romani" 
	Image="https://randomuser.me/api/portraits/men/43.jpg" />

```

### Avatar with icon

```razor
<BitAvatar 
	Icon="@Icons.ItSearch" 
	IconColor="IconColor.Success" 
	Text="Search" />
```

### Avatar with link

```razor
<BitAvatar 
	Icon="@Icons.ItSearch" 
	IconColor="IconColor.Success" 
	Link="#"
	Text="Search" />
```

### Avatar with extra text

```razor
<BitAvatar 
	Image="https://randomuser.me/api/portraits/women/33.jpg" 
	Text="Giulia Neri" 
	ExtraText="Lorem ipsum dolor" />

```

### Avatar with presence status

```razor
<BitAvatar 
	Image="https://randomuser.me/api/portraits/women/32.jpg" 
	Text="Ludovica Galli" 
	PresenceStatus="PresenceStatus.Busy" 
	PresenceStatusIcon="@Icons.ItMinus" 
	PresenceStatusDescription="Presence: Busy" />
```

### Avatar with user status

```razor
<BitAvatar 
	Image="https://randomuser.me/api/portraits/women/32.jpg" 
	Text="Ludovica Galli" 
	UserStatus="UserStatus.Approved" 
	UserStatusIcon="@Icons.ItCheck" 
	UserStatusDescription="Status: approved" />
```

### Avatar group

```razor
 <BitAvatarGroup>
    <BitAvatar Image="https://randomuser.me/api/portraits/men/43.jpg" Text="Mario Rossi" Link="#" />
    <BitAvatarGroupItem Text="Arianna Gallo" Link="#" />
    <BitAvatarGroupItem Text="Sara Ghione" />
    <BitAvatarGroupItem Text="Antonio Esposito" Icon="@Icons.ItUser" />
</BitAvatarGroup>
```


## Accessibility

- Uses `aria-hidden` and visually hidden text to ensure avatars are accessible to screen readers.
- Status and presence indicators include descriptions for assistive technologies.

## Customization

- Override CSS classes using the `CssClass` parameter.
- Set custom background and icon colors.
- Extend with additional attributes using `AdditionalAttributes`.

## Generated CSS Classes

The component generates the following CSS classes based on parameters:

- `avatar`: Base class
- `avatar-{background}`: Background color class (e.g., `avatar-primary`, `avatar-green`)
- `size-{size}`: Size-specific class (e.g., `size-md`, `size-lg`)
- `avatar-icon`: Added when displaying an icon
- `avatar-image`: Added when displaying an image
- `avatar-text`: Added when displaying text/initials
- `avatar-status`: Added when displaying user status
- `avatar-presence`: Added when displaying presence status
- `avatar-wrapper`: Used when extra text or status is present

## Notes

- When used in a group, the avatar is rendered as a `BitAvatarGroupItem`.
- If both `Image` and `Icon` are provided, the icon takes precedence.
- If neither `Image` nor `Icon` is provided, initials or short text are displayed.
- Avatar tooltip is actually not supported