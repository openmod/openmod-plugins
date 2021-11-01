namespace OpenMod.Plugins.Services;

public interface IGitHubRepository
{
    Task<string?> GetReadmeAsync(string repoUri, string branch, CancellationToken cancellationToken = default);
}
