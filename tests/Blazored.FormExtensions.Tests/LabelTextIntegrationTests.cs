using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blazored.FormExtensions.Tests.Models;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Blazored.FormExtensions.Tests
{
    public class LabelTextIntegrationTests
    {
        private readonly TestHost _host = new TestHost();
        private readonly Mock<IStringLocalizer> _stringLocalizerMock;
        private readonly Person _model = new Person();

        public LabelTextIntegrationTests()
        {
            _stringLocalizerMock = new Mock<IStringLocalizer>();
            _stringLocalizerMock.Setup(l => l[It.IsAny<string>()]).Returns(new LocalizedString("name", "my_value"));

            _host.AddService(_stringLocalizerMock.Object);
        }

        [Fact]
        public void Renders_Label_ForPropertyWithDisplayAttributeAndAdditionalAttributes()
        {
            // Arrange
            Expression<Func<string>> For = () => _model.First;
            var parameters = new Dictionary<string, object> { { "For", For }, { "class", "c" } };

            // Act
            var component = _host.AddComponent<LabelText<string>>(parameters);

            // Assert
            Assert.Equal("<label class=\"c\">my_value</label>", component.Find("label").OuterHtml);

            // Verify
            _stringLocalizerMock.Verify(localizer => localizer["Person_First"], Times.Once);
        }
    }
}
