namespace OpenMod.Plugins.Utils;

public class Cache<T>
{
    private readonly TimeSpan _lifetime;
    private readonly Func<CancellationToken, Task<T>> _reloadFunc;

    private T? _value;
    private DateTime _reloadTime;

    public Cache(TimeSpan lifetime, Func<CancellationToken, Task<T>> reloadFunc)
    {
        _lifetime = lifetime;
        _reloadFunc = reloadFunc;
    }

    public async Task<T> GetValueAsync(CancellationToken cancellationToken = default)
    {
        if (_value == null || _reloadTime.Add(_lifetime) < DateTime.UtcNow)
        {
            _value = await _reloadFunc(cancellationToken);
            _reloadTime = DateTime.UtcNow;
        }

        return _value;
    }
}
