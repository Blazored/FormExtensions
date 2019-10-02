using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;

namespace Blazored.FormExtensions.Tests.Helpers
{
    public class LabelTextMock : LabelText<string>
    {
        public IStringLocalizer StringLocalizerMock
        {
            set => this.SetPrivatePropertyValue("Localizer", value);
        }

        public void BuildRenderTreeMock(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
        }
    }
}