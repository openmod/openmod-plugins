namespace OpenMod.Plugins.Services;

public class GitHubRepository : IGitHubRepository
{
    private readonly HttpClient _httpClient;

    public GitHubRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> GetReadmeAsync(string repoUri, string branch, CancellationToken cancellationToken = default)
    {
        if (!repoUri.StartsWith("https://github.com/"))
        {
            throw new ArgumentException("Invalid GitHub repository url", nameof(repoUri));
        }

        repoUri = repoUri.Replace("https://github.com/", "https://raw.githubusercontent.com/");

        const string gitExtension = ".git";

        if (repoUri.EndsWith(gitExtension))
        {
            repoUri = repoUri[..^gitExtension.Length];
        }

        if (!repoUri.EndsWith('/'))
        {
            repoUri += '/';
        }

        var readmeUrl = repoUri + branch + "/README.md";
        var request = new HttpRequestMessage(HttpMethod.Get, readmeUrl);

        var responce = await _httpClient.SendAsync(request, cancellationToken);

        if (!responce.IsSuccessStatusCode)
        {
            return null;
        }

        return await responce.Content.ReadAsStringAsync(cancellationToken);
    }
}
