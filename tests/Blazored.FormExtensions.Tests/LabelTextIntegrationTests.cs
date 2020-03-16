using System;
using System.Linq.Expressions;
using Blazored.FormExtensions.Tests.Models;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Blazored.FormExtensions.Tests
{
    public sealed class LabelTextIntegrationTests : ComponentTestFixture
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
            // Arrange
            Expression<Func<string>> For = () => _model.First;
            var parameters = new[] { ComponentParameter.CreateParameter("For", For), ComponentParameter.CreateParameter("class", "c") };

            // Act
            var component = RenderComponent<LabelText<string>>(parameters);

            // Assert
            Assert.Equal("<label class=\"c\">my_value</label>", component.Find("label").OuterHtml);

            // Verify
            _stringLocalizerMock.Verify(localizer => localizer["Person_First"], Times.Once);
        }
    }
}