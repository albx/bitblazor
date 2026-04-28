// Ensures the Bootstrap Italia bundle (which includes Popper.js) is loaded
// exactly once, even when multiple components initialise concurrently.
let _loadPromise = null;

async function ensureBootstrapItalia() {
    if (window.bootstrap?.Tooltip) return;

    if (!_loadPromise) {
        _loadPromise = new Promise((resolve, reject) => {
            // Avoid inserting the script twice (e.g. after hot-reload).
            if (document.querySelector('script[data-bitblazor-bi]')) {
                resolve();
                return;
            }
            const script = document.createElement('script');
            script.src = '_content/BitBlazor/bootstrap-italia/js/bootstrap-italia.bundle.min.js';
            script.setAttribute('data-bitblazor-bi', '');
            script.onload = resolve;
            script.onerror = () => reject(new Error('Failed to load bootstrap-italia bundle'));
            document.head.appendChild(script);
        });
    }

    await _loadPromise;
}

/**
 * Initialises a Bootstrap Italia Tooltip on the supplied element.
 * @param {HTMLElement} element
 */
export async function initTooltip(element) {
    if (!element) return;
    await ensureBootstrapItalia();
    if (window.bootstrap?.Tooltip) {
        bootstrap.Tooltip.getOrCreateInstance(element);
    }
}

/**
 * Disposes the Bootstrap Italia Tooltip attached to the supplied element.
 * @param {HTMLElement} element
 */
export function disposeTooltip(element) {
    if (!element || !window.bootstrap?.Tooltip) return;
    bootstrap.Tooltip.getInstance(element)?.dispose();
}

/**
 * Initialises a Bootstrap Italia Tooltip on the first child element
 * of the given wrapper element.
 *
 * @param {HTMLElement} wrapper  - The <bit-tooltip> wrapper element.
 * @param {string}      title    - The tooltip text.
 * @param {object}      options  - Tooltip options (matches Bootstrap JS API).
 */
export async function initTooltipOnFirstChild(wrapper, title, options) {
    const target = wrapper.firstElementChild;
    if (!target) {
        console.warn('[BitTooltip] No child element found for tooltip.');
        return;
    }

    target.setAttribute('title', title);

    await ensureBootstrapItalia();
    if (window.bootstrap?.Tooltip) {
        const cleaned = Object.fromEntries(
            Object.entries(options).filter(([, v]) => v !== null && v !== undefined)
        );
        bootstrap.Tooltip.getOrCreateInstance(target, cleaned);
    }
}

/**
 * Disposes the Bootstrap Italia Tooltip instance from the first child
 * element of the given wrapper.
 *
 * @param {HTMLElement} wrapper
 */
export function disposeTooltipOnFirstChild(wrapper) {
    const target = wrapper.firstElementChild;
    if (!target || !window.bootstrap?.Tooltip) return;
    bootstrap.Tooltip.getInstance(target)?.dispose();
}

/**
 * Initialises a Bootstrap Italia Popover on the first child element
 * of the given wrapper element.
 *
 * @param {HTMLElement} wrapper  - The <bit-popover> wrapper element.
 * @param {string|null} title    - The popover header text (optional).
 * @param {string}      content  - The popover body text.
 * @param {object}      options  - Popover options (matches Bootstrap JS API).
 */
export async function initPopoverOnFirstChild(wrapper, title, content, options) {
    const target = wrapper.firstElementChild;
    if (!target) {
        console.warn('[BitPopover] No child element found for popover.');
        return;
    }

    await ensureBootstrapItalia();
    if (window.bootstrap?.Popover) {
        const cleaned = Object.fromEntries(
            Object.entries(options).filter(([, v]) => v !== null && v !== undefined)
        );
        const opts = { ...cleaned, content };
        if (title) opts.title = title;
        bootstrap.Popover.getOrCreateInstance(target, opts);
    }
}

/**
 * Disposes the Bootstrap Italia Popover instance from the first child
 * element of the given wrapper.
 *
 * @param {HTMLElement} wrapper
 */
export function disposePopoverOnFirstChild(wrapper) {
    const target = wrapper.firstElementChild;
    if (!target || !window.bootstrap?.Popover) return;
    bootstrap.Popover.getInstance(target)?.dispose();
}
