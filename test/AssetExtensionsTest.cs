using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WebOptimizer.Markdown.Test
{
    public class AssetExtensionsTest
    {
        [Fact]
        public void Compile_single_Success()
        {
            var asset = GenerateAssets(1).First();

            asset.CompileMarkdown();

            Assert.Equal(1, asset.Processors.Count);
            Assert.True(asset.Processors.Any(p => p is Processor));
        }

        [Fact]
        public void Compile_Multiple_Success()
        {
            var assets = GenerateAssets(5).ToArray();

            assets.CompileMarkdown();

            foreach (IAsset asset in assets)
            {
                Assert.Equal(1, asset.Processors.Count);
                Assert.True(asset.Processors.Any(p => p is Processor));
            }
        }

        private IEnumerable<IAsset> GenerateAssets(int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                var asset = new Mock<IAsset>();
                asset.SetupGet(a => a.Processors)
                     .Returns(new List<IProcessor>());

                yield return asset.Object;
            }
        }
    }
}
