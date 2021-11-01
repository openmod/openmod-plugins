using Microsoft.JSInterop;

namespace OpenMod.Plugins.Services
{
    public class ClipboardService : IClipboardService
    {
        private readonly IJSRuntime _jsRuntime;

        public ClipboardService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> CopyToClipboardAsync(string text)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
