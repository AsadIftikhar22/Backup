using EPiServer.Core;
using Salam.Cms.Web.Features.Common.ViewModels;

namespace Salam.Cms.UnitTests.Features.Components.Common
{
    [TestFixture]
    public class BlockViewModelTests
    {
        private Mock<BlockData> _mockBlock;

        [SetUp]
        public void Setup()
        {
            _mockBlock = new Mock<BlockData>();
        }

        [Test]
        public void Constructor_SetsCurrentBlock()
        {
            // Arrange & Act
            var viewModel = new TestBlockViewModel(_mockBlock.Object);

            // Assert
            Assert.That(viewModel.CurrentBlock, Is.EqualTo(_mockBlock.Object));
        }

        [Test]
        public void IsInEditMode_DefaultsToFalse_InTestEnvironment()
        {
            // Arrange & Act
            var viewModel = new TestBlockViewModel(_mockBlock.Object);

            // Assert - Our fix in BlockViewModel should make this default to false in test environment
            Assert.That(viewModel.IsInEditMode, Is.False);
        }

        // Test implementation of BlockViewModel for testing
        private class TestBlockViewModel : BlockViewModel<BlockData>
        {
            public TestBlockViewModel(BlockData currentBlock)
                : base(currentBlock)
            {
            }
        }
    }
}