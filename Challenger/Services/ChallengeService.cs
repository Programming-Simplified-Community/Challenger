using Markdig;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Challenger.Services;

public record ChallengeNavItem(string Title, string Category, string File);

public class ChallengeService
{
    private static List<ChallengeNavItem> s_Items = new();
    private static Random s_Random = new();
    private readonly HttpClient _client;
    private static MarkdownPipeline s_Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UsePipeTables()
        .UseBootstrap()
        .UseFooters()
        .UseCitations()
        .UseAutoIdentifiers()
        .UseAutoLinks()
        .UseMediaLinks()
        .UseListExtras()
        .UseCustomContainers()
        .UseEmojiAndSmiley()
        .UseDiagrams().Build();

    public ChallengeService(HttpClient client)
    {
        _client = client;
        Task.Run(async () => await FetchChallengeList());
    }

    private async Task FetchChallengeList()
    {
        var list = await _client.GetFromJsonAsync<ChallengeNavItem[]>("challenges.json");
        s_Items = list.ToList();
    }

    /// <summary>
    /// Get a random bunch of challenges from our challenges.json file. This is meant to serve
    /// as a way to showcase a random set of challenges on the home page... or perhaps other places too
    /// </summary>
    /// <param name="count">Number of challenges to display</param>
    /// <returns>Navigatable challenges from challenges.json</returns>
    public async Task<List<ChallengeNavItem>> GetRandomChallenges(int count = 3)
    {
        if (!s_Items.Any())
            await FetchChallengeList();

        HashSet<int> selectedIndices = new();

        count = Math.Min(count, s_Items.Count);
        for(int i = 0; i < count; i++)
        {
            int attempts = 20;
            int index = s_Random.Next(0, s_Items.Count);
            
            while (attempts > 0 && selectedIndices.Contains(index))
            {
                index = s_Random.Next(0, s_Items.Count);
                attempts--;
            }

            selectedIndices.Add(index);
        }

        return selectedIndices.Select(x => s_Items[x]).ToList();
    }

    public async Task<List<ChallengeNavItem>> GetChallenges()
    {
        if (!s_Items.Any())
            await FetchChallengeList();

        return s_Items;
    }
    
    /// <summary>
    /// Retrieve a challenge via filename from wwwroot
    /// </summary>
    /// <param name="file">Name of file to retrieve</param>
    /// <returns>File contents, or empty if failed</returns>
    public async Task<MarkupString> GetChallengeData(string file)
    {
        try
        {
            var data = await _client.GetStringAsync($"challenges/{file}");

            if (!data.Contains("```"))
                return new(Markdown.ToHtml(data, s_Pipeline));

            var output = string.Empty;
            int codeBlockStart;

            /*
                We are utilizing HighlightJs for rendering codeblocks in our templates.
                The markdown renderer does not convert these for us unfortunately so
                we must parse it ourselves.

                ```language
                some awesome code goes in here
                ```

                Based on the above format we need to go by ``` as our markers.
                So long as there is a ``` in the file we have a codeblock!
             */

            while ((codeBlockStart = data.IndexOf("```")) > 0)
            {
                // Get everything from before the code block up until this point
                output += data[..codeBlockStart];

                // reduce the markdown scope
                data = data[codeBlockStart..];

                // get the language associated with this code block
                var langEnd = data.IndexOf('\n');
                var language = data[3..langEnd];
                data = data[(langEnd + 1)..];

                // retrieve the code block ending
                var codeBlockEnd = data.IndexOf("```");
                var contents = data[..codeBlockEnd];
                output += $"<pre><code class=\"language-{language}\">{contents}</code></pre>";

                // reduce scope of markdown text
                data = data[(codeBlockEnd + 3)..];
            }

            output += data;

            return new(Markdown.ToHtml(output, s_Pipeline));
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return new(string.Empty);
        }
    }
}
