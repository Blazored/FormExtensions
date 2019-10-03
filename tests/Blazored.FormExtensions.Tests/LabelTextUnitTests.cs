using Blazored.FormExtensions.Tests.Helpers;
using Blazored.FormExtensions.Tests.Models;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Blazored.FormExtensions.Tests
{
    public class LabelTextUnitTests
    {
        private readonly Person _model = new Person();

        private readonly LabelTextMock _sut;

        public LabelTextUnitTests()
        {
            _sut = new LabelTextMock();
        }

        [Fact]
        public void BuildRenderTree_ForPropertyWithDisplayAttribute_Should_CreateCorrectFrames()
        {
            // Arrange
            _sut.For = () => _model.First;

            // Act
            var renderTreeBuilder = new RenderTreeBuilder();
            _sut.BuildRenderTreeMock(renderTreeBuilder);

            // Assert
            var frames = renderTreeBuilder.GetFrames().Array;
            AssertFrame.Element(frames[0], "label", 2);
            AssertFrame.Text(frames[1], "Person_First");
        }

        [Fact]
        public void BuildRenderTree_ForPropertyWithDisplayAttributeAndStringLocalizer_Should_CreateCorrectFramesUsingStringLocalizer()
        {
            // Arrange
            var stringLocalizerMock = new Mock<IStringLocalizer>();
            stringLocalizerMock.Setup(l => l[It.IsAny<string>()]).Returns(new LocalizedString("name", "value"));

            _sut.StringLocalizerMock = stringLocalizerMock.Object;
            _sut.For = () => _model.First;

            // Act
            var renderTreeBuilder = new RenderTreeBuilder();
            _sut.BuildRenderTreeMock(renderTreeBuilder);

            // Assert
            var frames = renderTreeBuilder.GetFrames().Array;
            AssertFrame.Element(frames[0], "label", 2);
            AssertFrame.Text(frames[1], "value");

            // Verify
            stringLocalizerMock.Verify(localizer => localizer["Person_First"], Times.Once);
        }
    }
}
