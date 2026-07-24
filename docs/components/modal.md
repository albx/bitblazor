# BitModal

The `BitModal` component provides a [modal dialog using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/modale/).

## Namespace

```csharp
BitBlazor.Components
```

## Description

The Modal component renders an accessible dialog that overlays the page. Visibility is controlled entirely through Blazor state using the `IsVisible` / `@bind-IsVisible` pattern — no JavaScript interop is required.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `IsVisible` | `bool` | ✗ | `false` | Controls whether the modal is displayed. Use `@bind-IsVisible` for two-way binding. |
| `IsVisibleChanged` | `EventCallback<bool>` | ✗ | - | Callback invoked when the modal requests a visibility change (close). Wired automatically by `@bind-IsVisible`. |
| `BodyContent` | `RenderFragment` | ✓ | - | The main body content of the modal. |
| `Title` | `string?` | ✗ | `null` | Text displayed in the header as an `h2` element. When set, the modal uses `aria-labelledby` for accessibility. |
| `TitleId` | `string?` | ✗ | `null` | Override for the `id` attribute of the title `h2` element. Defaults to `"{modal-id}-title"`. |
| `AriaLabel` | `string?` | ✗ | `null` | Sets the `aria-label` on the modal element. Use when `Title` is not set. |
| `AriaDescribedById` | `string?` | ✗ | `null` | Sets the `aria-describedby` attribute, pointing to a description element inside the modal. |
| `HeaderContent` | `RenderFragment?` | ✗ | `null` | Fully custom header content, replacing the default `Title`-based header. |
| `FooterContent` | `RenderFragment?` | ✗ | `null` | Footer content. When `null`, no footer element is rendered. |
| `ShowCloseButton` | `bool` | ✗ | `true` | Whether to show the close (×) button in the header. |
| `CloseButtonAriaLabel` | `string` | ✗ | `"Chiudi"` | The `aria-label` for the close button. |
| `Size` | `ModalSize` | ✗ | `Default` | The size of the modal dialog. |
| `Position` | `ModalPosition` | ✗ | `Default` | The positioning of the modal dialog. |
| `Type` | `ModalType` | ✗ | `Default` | The visual type variant of the modal. |
| `Backdrop` | `ModalBackdrop` | ✗ | `Default` | Controls backdrop visibility and whether clicking it closes the modal. |
| `ScrollableContent` | `bool` | ✗ | `false` | Enables internal body scrolling, keeping header and footer always visible. |
| `FooterShadow` | `bool` | ✗ | `false` | Adds a top shadow to the footer element (`modal-footer-shadow`). |
| `Fade` | `bool` | ✗ | `true` | Enables the CSS fade animation. Set to `false` for instant show/hide. |
| `OnClose` | `EventCallback` | ✗ | - | Callback invoked just before the modal is closed. |

## Enumerations

### ModalSize

| Value | CSS class on `.modal-dialog` | Description |
|-------|------------------------------|-------------|
| `Default` | *(none)* | Standard size |
| `Small` | `modal-sm` | Small dialog |
| `Large` | `modal-lg` | Large dialog |
| `ExtraLarge` | `modal-xl` | Extra-large dialog |

### ModalPosition

| Value | Effect | Description |
|-------|--------|-------------|
| `Default` | *(none)* | Top-center (default Bootstrap Italia) |
| `Centered` | `modal-dialog-centered` on dialog | Vertically centered |
| `Left` | `modal-dialog-left` on dialog + `it-dialog-scrollable` on modal | Full-height, left-aligned |
| `Right` | `modal-dialog-right` on dialog + `it-dialog-scrollable` on modal | Full-height, right-aligned |

### ModalType

| Value | CSS class on `.modal` | Description |
|-------|----------------------|-------------|
| `Default` | *(none)* | Standard modal |
| `Alert` | `alert-modal` | Alert modal, used with an icon in the header |
| `LinkList` | `it-dialog-link-list` | Optimised for navigation link lists |
| `Popconfirm` | `popconfirm-modal` | Compact confirmation dialog |

### ModalBackdrop

| Value | Description |
|-------|-------------|
| `Default` | Backdrop is shown; clicking it closes the modal |
| `Static` | Backdrop is shown; clicking it does **not** close the modal |
| `None` | No backdrop is rendered |

## Usage Examples

### Basic modal with two-way binding

```razor
<button class="btn btn-primary" @onclick="() => isOpen = true">
    Apri la modale
</button>

<BitModal @bind-IsVisible="isOpen"
          Id="example-modal"
          Title="Intestazione modale"
          AriaDescribedById="example-modal-desc">
    <BodyContent>
        <p id="example-modal-desc">Descrizione scopo della modale.</p>
        <p>Contenuto della modale.</p>
    </BodyContent>
    <FooterContent>
        <button class="btn btn-outline-primary btn-sm" type="button" @onclick="() => isOpen = false">Annulla</button>
        <button class="btn btn-primary btn-sm" type="button" @onclick="() => isOpen = false">Conferma</button>
    </FooterContent>
</BitModal>

@code {
    private bool isOpen = false;
}
```

### Modal without header

```razor
<BitModal @bind-IsVisible="isOpen"
          AriaLabel="Finestra di dialogo"
          ShowCloseButton="false">
    <BodyContent>
        <p>Questa modale non ha intestazione.</p>
    </BodyContent>
    <FooterContent>
        <button class="btn btn-primary btn-sm" @onclick="() => isOpen = false">Chiudi</button>
    </FooterContent>
</BitModal>
```

### Centered modal with scrollable content

```razor
<BitModal @bind-IsVisible="isOpen"
          Title="Contenuto lungo"
          Position="ModalPosition.Centered"
          ScrollableContent="true"
          FooterShadow="true">
    <BodyContent>
        @* Long content that scrolls inside the modal body *@
        @for (int i = 1; i <= 20; i++)
        {
            <p>Paragrafo @i lorem ipsum dolor sit amet.</p>
        }
    </BodyContent>
    <FooterContent>
        <button class="btn btn-primary btn-sm" @onclick="() => isOpen = false">OK</button>
    </FooterContent>
</BitModal>
```

### Static backdrop (does not close on click)

```razor
<BitModal @bind-IsVisible="isOpen"
          Title="Conferma obbligatoria"
          Backdrop="ModalBackdrop.Static"
          ShowCloseButton="false">
    <BodyContent>
        <p>Devi fare una scelta per continuare.</p>
    </BodyContent>
    <FooterContent>
        <button class="btn btn-outline-primary btn-sm" @onclick="() => isOpen = false">Annulla</button>
        <button class="btn btn-primary btn-sm" @onclick="ConfirmAsync">Conferma</button>
    </FooterContent>
</BitModal>
```

### Alert modal with icon

```razor
<BitModal @bind-IsVisible="isOpen"
          Type="ModalType.Alert"
          AriaLabel="Avviso importante">
    <HeaderContent>
        <div class="d-flex align-items-center">
            <BitIcon IconName="@Icons.ItWarningCircle" CssClass="me-2" />
            <h2 class="modal-title h5 mb-0">Avviso importante</h2>
        </div>
    </HeaderContent>
    <BodyContent>
        <p>Il documento verrà eliminato definitivamente.</p>
    </BodyContent>
    <FooterContent>
        <button class="btn btn-outline-primary btn-sm" @onclick="() => isOpen = false">Annulla</button>
        <button class="btn btn-primary btn-sm" @onclick="DeleteAsync">Elimina</button>
    </FooterContent>
</BitModal>
```

### Popconfirm modal

```razor
<BitModal @bind-IsVisible="isOpen"
          Type="ModalType.Popconfirm"
          AriaLabel="Sei sicuro?"
          ShowCloseButton="false">
    <BodyContent>
        <p>Questa operazione non può essere annullata.</p>
    </BodyContent>
    <FooterContent>
        <button class="btn btn-outline-primary btn-sm" @onclick="() => isOpen = false">No</button>
        <button class="btn btn-primary btn-sm" @onclick="ProceedAsync">Sì, procedi</button>
    </FooterContent>
</BitModal>
```

## Accessibility

- The modal element always carries `role="dialog"` and `aria-modal="true"`.
- The dialog element carries `role="document"`.
- When `Title` is set (and `HeaderContent` is not overriding the header), `aria-labelledby` points automatically to the title `h2` element.
- When there is no `Title`, set `AriaLabel` to provide an accessible name for screen readers.
- Use `AriaDescribedById` to reference a descriptive paragraph inside the `BodyContent` for a richer screen-reader experience.
- The close button always has an `aria-label` (defaults to `"Chiudi"`). Customise it with `CloseButtonAriaLabel`.

## References

- [Bootstrap Italia — Finestre modali](https://italia.github.io/bootstrap-italia/docs/componenti/modale/)
- [WAI-ARIA Authoring Practices — Dialog Modal](https://www.w3.org/TR/wai-aria-practices/#dialog_modal)
