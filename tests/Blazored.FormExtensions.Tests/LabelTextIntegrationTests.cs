using Blazored.FormExtensions.Tests.Models;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Blazored.FormExtensions.Tests
{
    public sealed class LabelTextIntegrationTests : TestContext
    {
        private readonly Mock<IStringLocalizer> _stringLocalizerMock;
        private readonly Person _model = new Person();

        public LabelTextIntegrationTests()
        {
            _stringLocalizerMock = new Mock<IStringLocalizer>();
            _stringLocalizerMock.Setup(l => l[It.IsAny<string>()]).Returns(new LocalizedString("name", "my_value"));

            Services.AddSingleton(_stringLocalizerMock.Object);
        }

        [Fact]
        public void Renders_Label_ForPropertyWithDisplayAttributeAndAdditionalAttributes()
        {
            // Act
            var component = RenderComponent<LabelText<string>>(builder =>
            {
                builder
                    .Add(c => c.For, () => _model.First)
                    .AddUnmatched("class", "c");
            });

            // Assert
            Assert.Equal("<label class=\"c\">my_value</label>", component.Find("label").OuterHtml);

            // Verify
            _stringLocalizerMock.Verify(localizer => localizer["Person_First"], Times.Once);
        }
    }
}