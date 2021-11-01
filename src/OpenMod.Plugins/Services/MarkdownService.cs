using Markdig;

namespace OpenMod.Plugins.Services;

public class MarkdownService : IMarkdownService
{
    private static readonly MarkdownPipeline _markdownPipeline;

    static MarkdownService()
    {
        _markdownPipeline = new MarkdownPipelineBuilder()
            .UseGridTables()
            .UsePipeTables()
            .UseListExtras()
            .UseTaskLists()
            .UseEmojiAndSmiley()
            .UseAutoLinks()
            .UseReferralLinks("noopener noreferrer nofollow")
            .DisableHtml() // block inline html
            .Build();
    }

    public string ToHtml(string markdown)
    {
        return Markdig.Markdown.ToHtml(markdown, _markdownPipeline);
    }
}
