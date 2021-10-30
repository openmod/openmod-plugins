namespace OpenMod.BaGet.Protocol;

public interface IServiceIndexClient
{
    Task<ServiceIndexResponse> GetAsync(CancellationToken cancellationToken = default);
}
