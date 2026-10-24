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

    if (!window.bootstrap?.Popover) {
        throw new Error('[BitPopover] Bootstrap Italia Popover plugin is not available.');
    }
}

function clean(options) {
    return Object.fromEntries(
        Object.entries(options ?? {}).filter(([, v]) => v !== null && v !== undefined));
}

export const BitPopover = {
    /**
     * Creates a Bootstrap Italia Popover on the first child of the wrapper.
     * @param {HTMLElement} wrapper - The <bit-popover> wrapper element.
     * @param {string|null} title - The popover header text (optional).
     * @param {string} content - The popover body text.
     * @param {object} options - Popover options (Bootstrap JS API).
     * @returns {Promise<{ dispose: () => void } | null>} A handle bound to the created instance.
     */
    async create(wrapper, title, content, options) {
        const target = wrapper?.firstElementChild;
        if (!target) {
            console.warn('[BitPopover] No child element found for popover.');
            return null;
        }

        await ensureBootstrap();

        const settings = { ...clean(options), content };
        if (title) settings.title = title;
        const instance = bootstrap.Popover.getOrCreateInstance(target, settings);

        return {
            dispose: () => instance.dispose()
        };
    }
};
