namespace OpenMod.Plugins.Services;

public interface IClipboardService
{
    Task<bool> CopyToClipboardAsync(string text);
}
