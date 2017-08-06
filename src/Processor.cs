using Markdig;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebOptimizer.Markdown
{
    /// <summary>
    /// Compiles Sass files
    /// </summary>
    /// <seealso cref="WebOptimizer.IProcessor" />
    public class Processor : IProcessor
    {
        private MarkdownPipeline _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        public Processor()
        {
            _options = new MarkdownPipelineBuilder()
                .UseDiagrams()
                .UseAdvancedExtensions()
                .UseYamlFrontMatter()
                .Build();
        }

        /// <summary>
        /// Gets the custom key that should be used when calculating the memory cache key.
        /// </summary>
        public string CacheKey(HttpContext context) => string.Empty;


        /// <summary>
        /// Executes the processor on the specified configuration.
        /// </summary>
        public Task ExecuteAsync(IAssetContext context)
        {
            var content = new Dictionary<string, byte[]>();                

            foreach (string route in context.Content.Keys)
            {
                string input = context.Content[route].AsString()
                    .Trim(new char[] { '\uFEFF', '\u200B' }); // Removes the BOM

                var result = Markdig.Markdown.ToHtml(input, _options);

                content[route] = result.AsByteArray();
            }

            context.Content = content;

            return Task.CompletedTask;
        }
    }
}
