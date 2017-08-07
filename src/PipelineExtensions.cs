using System.Collections.Generic;

namespace WebOptimizer.Markdown
{
    /// <summary>
    /// Extensions methods for registrating the Sass compiler on the Asset Pipeline.
    /// </summary>
    public static class PipelineExtensions
    {
        /// <summary>
        /// Compile markdown files on the asset pipeline.
        /// </summary>
        public static IAsset CompileMarkdown(this IAsset asset)
        {
            asset.Processors.Add(new Processor());
            return asset;
        }

        /// <summary>
        /// Compile markdown files on the asset pipeline.
        /// </summary>
        public static IEnumerable<IAsset> CompileMarkdown(this IEnumerable<IAsset> assets)
        {
            var list = new List<IAsset>();

            foreach (IAsset asset in assets)
            {
                list.Add(asset.CompileMarkdown());
            }

            return list;
        }

        /// <summary>
        /// Compile markdown files on the asset pipeline.
        /// </summary>
        /// <param name="pipeline">The asset pipeline.</param>
        /// <param name="route">The route where the compiled markdown file will be available from.</param>
        /// <param name="sourceFiles">The path to the markdown source files to compile.</param>
        public static IAsset AddMarkdownBundle(this IAssetPipeline pipeline, string route, params string[] sourceFiles)
        {
            return pipeline.AddBundle(route, "text/html; charset=UTF-8", sourceFiles)
                           .CompileMarkdown()
                           .Concatenate();
        }

        /// <summary>
        /// Compiles markdown files into HTML and makes them servable in the browser.
        /// </summary>
        /// <param name="pipeline">The asset pipeline.</param>
        public static IEnumerable<IAsset> CompileMarkdownFiles(this IAssetPipeline pipeline)
        {
            return pipeline.AddFiles("text/html; charset=UTF-8", "**/*.md")
                           .CompileMarkdown();
        }

        /// <summary>
        /// Compiles the specified markdown files into HTML and makes them servable in the browser.
        /// </summary>
        /// <param name="pipeline">The pipeline object.</param>
        /// <param name="sourceFiles">A list of relative file names of the sources to compile.</param>
        public static IEnumerable<IAsset> CompileMarkdownFiles(this IAssetPipeline pipeline, params string[] sourceFiles)
        {
            return pipeline.AddFiles("text/html; charset=UTF-8", sourceFiles)
                           .CompileMarkdown();
        }
    }
}
