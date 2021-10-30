namespace OpenMod.BaGet.Protocol;

public interface IClientFactory
{
    IServiceIndexClient GetServiceIndexClient();

    ISearchClient GetSearchClient();
}
