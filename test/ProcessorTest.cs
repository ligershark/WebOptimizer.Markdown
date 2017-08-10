using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WebOptimizer.Markdown.Test
{
    public class ProcessorTest
    {
        [Fact]
        public async Task Compile_Success()
        {
            var processor = new Processor();
            var pipeline = new Mock<IAssetPipeline>().SetupAllProperties();
            var context = new Mock<IAssetContext>().SetupAllProperties();
            context.Object.Content = new Dictionary<string, byte[]> {
                { "/file.md", "# foo".AsByteArray() },
            };

            context.Setup(s => s.HttpContext.RequestServices.GetService(typeof(IAssetPipeline)))
                   .Returns(pipeline.Object);

            await processor.ExecuteAsync(context.Object);
            var result = context.Object.Content.First().Value;

            Assert.Equal("<h1 id=\"foo\">foo</h1>\n", result.AsString());
        }
    }
}
