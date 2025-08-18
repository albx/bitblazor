# BitAlert

The `BitAlert` component represents an [alert using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/alert/).

## Namespace

```csharp
BitBlazor.Components
```

## Description

The Alert component provides contextual feedback for user actions with a handful of flexible and available alert messages.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Type` | `AlertType` | ✓ | - | The type of alert to display |
| `ChildContent` | `RenderFragment` | ✓ | - | The main content of the alert |
| `Title` | `string?` | ✗ | `null` | The title of the alert |
| `Dismissible` | `bool` | ✗ | `false` | Indicates if the alert can be closed |
| `CloseButtonAriaLabel` | `string?` | ✗ | `"close this alert"` | The aria-label attribute for the close button |
| `OnClose` | `EventCallback` | ✗ | - | Callback called before the alert is closed |
| `OnClosed` | `EventCallback` | ✗ | - | Callback called after the alert is closed |

## AlertType Enumeration

| Value | Description |
|-------|-------------|
| `Primary` | Primary alert |
| `Info` | Informational alert |
| `Success` | Success alert |
| `Warning` | Warning alert |
| `Danger` | Danger alert |

## Usage Examples

### Simple alert

```razor
<BitAlert Type="AlertType.Success">
    Operation completed successfully!
</BitAlert>
```

### Alert with title

```razor
<BitAlert Type="AlertType.Warning" Title="Warning">
    Please verify the entered data before proceeding.
</BitAlert>
```

### Dismissible alert

```razor
<BitAlert Type="AlertType.Info" 
          Dismissible="true" 
          OnClosed="HandleAlertClosed">
    This message can be closed by the user.
</BitAlert>
```

### Alert with event handling

```razor
<BitAlert Type="AlertType.Danger" 
          Dismissible="true"
          OnClose="HandleBeforeClose"
          OnClosed="HandleAfterClose">
    Error occurred during request processing.
</BitAlert>

@code {
    private async Task HandleBeforeClose()
    {
        // Logic executed before closing
        Console.WriteLine("Alert is about to be closed");
    }

    private async Task HandleAfterClose()
    {
        // Logic executed after closing
        Console.WriteLine("Alert has been closed");
    }
}
```

## Notes

- If the alert is dismissible and no `CloseButtonAriaLabel` is provided, the value "close this alert" is automatically used to ensure accessibility.
- The alert supports fade animations when closed (if dismissible).
- The component uses the ARIA "alert" role to improve accessibility.

## Generated CSS Classes

The component generates the following CSS classes based on parameters:

- `alert`: Base class
- `alert-{type}`: Type-specific class (e.g., `alert-primary`, `alert-success`)
- `alert-dismissible`: Added when `Dismissible` is `true`
- `fade`: Added for animation when dismissible
- `show`: Added when the alert is visible (removed during closing)
