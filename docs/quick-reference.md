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
        <CardImage ImageSrc="/image.jpg" ImageAlt="Image" />
    </CardImageWrapper>
    <CardBody>
        <CardText>Description...</CardText>
    </CardBody>
</BitCard>
```

## Form Components

### BitTextField
```razor
<!-- Basic text field -->
<BitTextField Label="Full Name" @bind-Value="model.Name" />

<!-- Email field -->
<BitTextField Label="Email" 
              Type="TextFieldType.Email" 
              @bind-Value="model.Email" />

<!-- With input group -->
<BitTextField Label="Website" Type="TextFieldType.Url" @bind-Value="model.Website">
    <PrependContent>
        <span class="input-group-text">https://</span>
    </PrependContent>
</BitTextField>
```

### BitPasswordField
```razor
<!-- Basic password field -->
<BitPasswordField Label="Password" @bind-Value="model.Password" />

<!-- With validation -->
<BitPasswordField Label="New Password" 
                  @bind-Value="model.Password"
                  For="@(() => model.Password)">
    <AdditionalText>
        Must be at least 8 characters with uppercase, lowercase, number, and special character.
    </AdditionalText>
</BitPasswordField>
```

### BitTextAreaField
```razor
<!-- Basic text area -->
<BitTextAreaField Label="Comments" 
                  Rows="4" 
                  @bind-Value="model.Comments" />

<!-- With character count -->
<BitTextAreaField Label="Description" 
                  Rows="6" 
                  @bind-Value="model.Description"
                  AdditionalTextId="desc-help">
    <AdditionalText>
        Maximum 500 characters. Currently: @(model.Description?.Length ?? 0) characters.
    </AdditionalText>
</BitTextAreaField>
```

### BitNumberField
```razor
<!-- Basic number field -->
<BitNumberField Label="Age" @bind-Value="model.Age" />

<!-- With min/max and step -->
<BitNumberField Label="Price" 
                @bind-Value="model.Price"
                Min="0"
                Max="999.99m"
                Step="0.01m">
    <SymbolContent>€</SymbolContent>
</BitNumberField>

<!-- Adaptive sizing -->
<BitNumberField Label="Quantity" 
                @bind-Value="model.Quantity"
                Adaptive="true"
                Min="1"
                Step="1" />
```

### BitCheckbox
```razor
<!-- Basic checkbox -->
<BitCheckbox Label="I agree to the terms" @bind-Value="model.AcceptedTerms" />

<!-- Inline checkboxes -->
<BitCheckbox Label="Option 1" Inline="true" @bind-Value="option1" />
<BitCheckbox Label="Option 2" Inline="true" @bind-Value="option2" />

<!-- Grouped checkbox -->
<BitCheckbox Label="Right aligned" Grouped="true" @bind-Value="grouped" />

<!-- With validation -->
<BitCheckbox Label="I accept the privacy policy" 
             @bind-Value="model.AcceptedPrivacy"
             For="@(() => model.AcceptedPrivacy)" />
```

### BitToggle
```razor
<!-- Basic toggle -->
<BitToggle Label="Enable notifications" @bind-Value="model.NotificationsEnabled" />

<!-- Grouped toggle (right aligned) -->
<BitToggle Label="Dark mode" 
           ViewMode="ToggleViewMode.Grouped" 
           @bind-Value="darkMode" />

<!-- With validation -->
<BitToggle Label="Accept terms of service" 
           @bind-Value="model.AcceptedTerms"
           For="@(() => model.AcceptedTerms)" />

<!-- With additional text -->
<BitToggle Label="Enable analytics"
           @bind-Value="model.AnalyticsEnabled"
           AdditionalTextId="helper-text">
    <AdditionalText>
        Help us improve by sharing anonymous usage data
    </AdditionalText>
</BitToggle>
```

### BitRadio
```razor
<!-- Basic radio group -->
<BitRadioGroup @bind-Value="model.SelectedOption">
    <BitRadio Label="Option 1" Value="1" />
    <BitRadio Label="Option 2" Value="2" />
    <BitRadio Label="Option 3" Value="3" />
</BitRadioGroup>

<!-- Inline radio buttons -->
<BitRadioGroup @bind-Value="model.Size" Inline="true">
    <BitRadio Label="Small" Value="Size.Small" />
    <BitRadio Label="Medium" Value="Size.Medium" />
    <BitRadio Label="Large" Value="Size.Large" />
</BitRadioGroup>

<!-- With validation -->
<BitRadioGroup @bind-Value="model.PreferredContact"
               For="@(() => model.PreferredContact)">
    <BitRadio Label="Email" Value="ContactMethod.Email" />
    <BitRadio Label="Phone" Value="ContactMethod.Phone" />
    <BitRadio Label="SMS" Value="ContactMethod.SMS" />
</BitRadioGroup>

<!-- Grouped radio buttons -->
<BitRadioGroup @bind-Value="model.Agreement" Grouped="true">
    <BitRadio Label="I agree" Value="true" />
    <BitRadio Label="I do not agree" Value="false" />
</BitRadioGroup>
```

### BitTimepicker
```razor
<!-- Basic time picker -->
<BitTimepicker Label="Meeting Time" @bind-Value="model.MeetingTime" />

<!-- With validation -->
<BitTimepicker Label="Appointment Time" 
               @bind-Value="model.AppointmentTime"
               For="@(() => model.AppointmentTime)" />
```

### BitDatepicker
```razor
<!-- Basic date picker -->
<BitDatepicker Label="Birth Date" @bind-Value="model.BirthDate" />

<!-- With validation -->
<BitDatepicker Label="Event Date" 
               @bind-Value="model.EventDate"
               For="@(() => model.EventDate)" />
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
    <DataAnnotationsValidator />
    
    <div class="row">
        <div class="col-md-6">
            <BitTextField Label="First Name" 
                          @bind-Value="model.FirstName"
                          For="@(() => model.FirstName)" />
        </div>
        <div class="col-md-6">
            <BitTextField Label="Last Name" 
                          @bind-Value="model.LastName"
                          For="@(() => model.LastName)" />
        </div>
        <div class="col-12">
            <BitTextField Label="Email" 
                          Type="TextFieldType.Email"
                          @bind-Value="model.Email"
                          For="@(() => model.Email)" />
        </div>
        <div class="col-12">
            <BitPasswordField Label="Password" 
                              @bind-Value="model.Password"
                              For="@(() => model.Password)" />
        </div>
        <div class="col-md-6">
            <BitNumberField Label="Age" 
                            @bind-Value="model.Age"
                            For="@(() => model.Age)"
                            Min="13"
                            Max="120" />
        </div>
        <div class="col-md-6">
            <BitTextField Label="Phone" 
                          Type="TextFieldType.Tel"
                          @bind-Value="model.Phone"
                          For="@(() => model.Phone)" />
        </div>
        <div class="col-md-6">
            <BitDatepicker Label="Birth Date" 
                           @bind-Value="model.BirthDate"
                           For="@(() => model.BirthDate)" />
        </div>
        <div class="col-md-6">
            <BitTimepicker Label="Preferred Contact Time" 
                           @bind-Value="model.PreferredTime"
                           For="@(() => model.PreferredTime)" />
        </div>
        <div class="col-12">
            <BitTextAreaField Label="Bio" 
                              Rows="4"
                              @bind-Value="model.Bio"
                              Placeholder="Tell us about yourself..." />
        </div>
        <div class="col-12">
            <BitCheckbox Label="I agree to the terms and conditions" 
                         @bind-Value="model.AcceptedTerms"
                         For="@(() => model.AcceptedTerms)" />
        </div>
        <div class="col-12">
            <BitRadioGroup @bind-Value="model.PreferredContact"
                           For="@(() => model.PreferredContact)">
                <BitRadio Label="Contact me by email" Value="ContactMethod.Email" />
                <BitRadio Label="Contact me by phone" Value="ContactMethod.Phone" />
            </BitRadioGroup>
        </div>
    </div>
    
    <ValidationSummary />
    
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

@code {
    private UserModel model = new();
    
    private class UserModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [Range(13, 120)]
        public int Age { get; set; }
        
        [Phone]
        public string? Phone { get; set; }
        
        [Required]
        public DateOnly BirthDate { get; set; }
        
        public TimeOnly? PreferredTime { get; set; }
        
        public string? Bio { get; set; }
        
        public bool AcceptedTerms { get; set; }
        
        [Required]
        public ContactMethod PreferredContact { get; set; }
    }
    
    private enum ContactMethod
    {
        Email,
        Phone
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
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
