# Quick Reference - BitBlazor

This guide provides a quick overview of all BitBlazor components with basic examples.

## UI Components

### Alert
```razor
<!-- Success alert -->
<BitAlert Type="AlertType.Success">
    Operation completed!
</BitAlert>

<!-- Dismissible alert -->
<BitAlert Type="AlertType.Warning" Dismissible="true">
    Warning: please verify the data
</BitAlert>
```

### Badge
```razor
<!-- Standard badge -->
<BitBadge Text="New" BackgroundColor="Color.Primary" />

<!-- Rounded outline badge -->
<BitBadge Text="3" 
          BackgroundColor="Color.Danger" 
          Variant="Variant.Outline" 
          Rounded="true" />
```

### Button
```razor
<!-- Simple button -->
<BitButton Color="Color.Primary">Save</BitButton>

<!-- Button with icon -->
<BitButton Color="Color.Success" Icon="@Icons.ItCheck">
    Confirm
</BitButton>

<!-- Outline button -->
<BitButton Color="Color.Secondary" Variant="Variant.Outline">
    Cancel
</BitButton>
```

### ButtonBadge
```razor
<BitButton Color="Color.Primary">
    Messages
    <ButtonBadge Text="5" AdditionalText="unread messages" />
</BitButton>
```

### Card
```razor
<!-- Simple card -->
<BitCard>
    <CardTitle>
        <a href="#">Title</a>
    </CardTitle>
    <CardBody>
        <CardText>Card content</CardText>
    </CardBody>
</BitCard>

<!-- Card with image -->
<BitCard>
    <CardTitle>
        <a href="#">Title</a>
    </CardTitle>
    <CardImageWrapper>
        <CardImage Src="/image.jpg" Alt="Image" />
    </CardImageWrapper>
    <CardBody>
        <CardText>Description...</CardText>
    </CardBody>
</BitCard>
```

## Utilities

### Icon
```razor
<!-- Simple icon -->
<BitIcon IconName="@Icons.ItCheck" />

<!-- Colored and sized icon -->
<BitIcon IconName="@Icons.ItUser" 
         Color="IconColor.Primary" 
         Size="IconSize.Large" />
```

## Common Parameters

### Available Colors
```csharp
Color.Primary      // Primary blue
Color.Secondary    // Secondary gray  
Color.Success      // Success green
Color.Warning      // Warning yellow/orange
Color.Danger       // Danger red
```

### Available Variants
```csharp
Variant.Solid      // Solid fill
Variant.Outline    // Border only
```

### Available Sizes
```csharp
Size.Mini         // Very small
Size.Small        // Small
Size.Default      // Standard
Size.Large        // Large
```

## Combination Examples

### Button toolbar
```razor
<div class="btn-toolbar">
    <BitButton Color="Color.Primary" Icon="@Icons.ItPlus">
        New
    </BitButton>
    <BitButton Color="Color.Secondary" Variant="Variant.Outline">
        Edit
    </BitButton>
    <BitButton Color="Color.Danger" Icon="@Icons.ItTrash">
        Delete
    </BitButton>
</div>
```

### Notification system
```razor
<BitAlert Type="AlertType.Info" Dismissible="true">
    <BitIcon IconName="@Icons.ItInfo" Color="IconColor.Primary" />
    You have <BitBadge Text="3" BackgroundColor="Color.Primary" Rounded="true" /> 
    new notifications
</BitAlert>
```

### User profile card
```razor
<BitCard Type="CardType.Profile" Shadow="CardShadow.Medium">
    <CardProfileHeader>
        <ProfileName>John Doe</ProfileName>
        <ProfileType>Developer</ProfileType>
        <ProfileImage>
            <CardProfileIcon IconName="@Icons.ItUser"
                             IconColor="IconColor.Primary"
                             AriaHidden="true"/>
        </ProfileImage>
    </CardProfileHeader>
    <CardBody>
        <CardText>
            Senior developer with experience in .NET and Blazor
        </CardText>
    </CardBody>
    <CardFooter>
        <BitButton Color="Color.Primary" Size="Size.Small">
            Contact
        </BitButton>
    </CardFooter>
</BitCard>
```

### Form with validation
```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <!-- Form fields -->
    
    <div class="d-flex gap-2">
        <BitButton Type="ButtonType.Submit" 
                   Color="Color.Success" 
                   Icon="@Icons.ItCheck">
            Save
        </BitButton>
        
        <BitButton Type="ButtonType.Reset" 
                   Color="Color.Secondary" 
                   Variant="Variant.Outline">
            Reset
        </BitButton>
    </div>
</EditForm>
```

## Most Used Icons

```csharp
// Navigation
Icons.ItArrowLeft    // Left arrow
Icons.ItArrowRight   // Right arrow
Icons.ItArrowUp      // Up arrow
Icons.ItArrowDown    // Down arrow

// Actions
Icons.ItPlus         // Add
Icons.ItMinus        // Remove
Icons.ItCheck        // Confirm
Icons.ItClose        // Close
Icons.ItSave         // Save
Icons.ItTrash        // Delete

// Interface
Icons.ItUser         // User
Icons.ItMail         // Email
Icons.ItPhone        // Phone
Icons.ItHome         // Home
Icons.ItSettings     // Settings
Icons.ItSearch       // Search

// Status
Icons.ItInfo         // Information
Icons.ItWarning      // Warning
Icons.ItDanger       // Danger
Icons.ItSuccess      // Success
```

## Custom CSS

All components support additional CSS classes:

```razor
<!-- Add custom classes -->
<BitButton Color="Color.Primary" CssClass="my-custom-button">
    Custom
</BitButton>

<BitIcon IconName="@Icons.ItStar" CssClass="rotating-icon" />

<BitCard CssClass="border-start border-primary border-4">
    <CardBody>Custom border</CardBody>
</BitCard>
```

## Accessibility

All components include accessibility support:

```razor
<!-- Button with aria-label -->
<BitButton Color="Color.Danger" Icon="@Icons.ItTrash" OnClick="Delete">
    <span class="visually-hidden">Delete item</span>
</BitButton>

<!-- Icon with title for screen reader -->
<BitIcon IconName="@Icons.ItInfo" 
         Title="Additional information" 
         Role="img" />
```

## Bootstrap Italia

All components use Bootstrap Italia CSS classes:

- Official Italian design system
- WCAG guidelines compliance
- Integrated responsive design
- Support for custom themes

For more details on each component, consult the specific documentation in the `components/` or `utilities/` folders.
