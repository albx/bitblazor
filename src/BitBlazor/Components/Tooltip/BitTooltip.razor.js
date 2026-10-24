const BUNDLE_URL = '../../bootstrap-italia/js/bootstrap-italia.bundle.min.js';
const SUPPORTED_VERSION = '2.18'; // major.minor of the shipped bundle

let versionChecked = false;

async function ensureBootstrap() {
    if (!window.bootstrap) {
        // Not provided by the host page: load the library's own copy.
        await import(BUNDLE_URL);
    } else if (!versionChecked) {
        // Provided by the host page: reuse it, but flag unsupported versions.
        const detected = window.BOOTSTRAP_ITALIA_VERSION;
        if (!detected?.startsWith(`${SUPPORTED_VERSION}.`)) {
            console.warn(
                `[BitBlazor] Bootstrap Italia ${detected ?? '(unknown)'} detected; ` +
                `supported version is ${SUPPORTED_VERSION}.x. Use at your own risk.`);
        }
    }
    versionChecked = true;

    if (!window.bootstrap?.Tooltip) {
        throw new Error('[BitTooltip] Bootstrap Italia Tooltip plugin is not available.');
    }
}

function clean(options) {
    return Object.fromEntries(
        Object.entries(options ?? {}).filter(([, v]) => v !== null && v !== undefined));
}

export const BitTooltip = {
    /**
     * Creates a Bootstrap Italia Tooltip on the first child of the wrapper.
     * @param {HTMLElement} wrapper - The <bit-tooltip> wrapper element.
     * @param {string} title - The tooltip text.
     * @param {object} options - Tooltip options (Bootstrap JS API).
     * @returns {Promise<{ dispose: () => void } | null>} A handle bound to the created instance.
     */
    async create(wrapper, title, options) {
        const target = wrapper?.firstElementChild;
        if (!target) {
            console.warn('[BitTooltip] No child element found for tooltip.');
            return null;
        }

        await ensureBootstrap();

        target.setAttribute('title', title);
        const instance = bootstrap.Tooltip.getOrCreateInstance(target, clean(options));

        return {
            dispose: () => instance.dispose()
        };
    }
};
