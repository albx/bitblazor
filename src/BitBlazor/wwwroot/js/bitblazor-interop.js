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
 * Initialises a Bootstrap Italia Popover on the supplied element.
 * @param {HTMLElement} element
 */
export async function initPopover(element) {
    if (!element) return;
    await ensureBootstrapItalia();
    if (window.bootstrap?.Popover) {
        bootstrap.Popover.getOrCreateInstance(element);
    }
}

/**
 * Disposes the Bootstrap Italia Popover attached to the supplied element.
 * @param {HTMLElement} element
 */
export function disposePopover(element) {
    if (!element || !window.bootstrap?.Popover) return;
    bootstrap.Popover.getInstance(element)?.dispose();
}
