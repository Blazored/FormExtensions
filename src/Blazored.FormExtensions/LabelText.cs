using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;

namespace Blazored.FormExtensions
{
    /// <summary>
    /// Displays a label for a property using the DisplayAttribute DataAnnotation.
    /// </summary>
    public class LabelText<TValue> : ComponentBase
    {
        /// <summary>
        /// The IStringLocalizer which is used to translate the label.
        /// </summary>
        [Inject]
        private IStringLocalizer Localizer { get; set; }

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created <c>label</c> element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// Specifies the field for which the label should be displayed.
        /// </summary>
        [Parameter]
        public Expression<Func<TValue>> For { get; set; }

        /// <inheritdoc />
        protected override void OnParametersSet()
        {
            if (For == null)
            {
                throw new InvalidOperationException($"{GetType()} requires a value for the {nameof(For)} parameter.");
            }

            if (!(For.Body is MemberExpression))
            {
                throw new InvalidOperationException($"{GetType()} should define a MemberExpression for the {nameof(For)} parameter.");
            }
        }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "label");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddContent(2, GetLabelText());
            builder.CloseElement();
        }

        private string GetLabelText()
        {
            var memberExpression = For.Body as MemberExpression;
            var property = memberExpression.Member;

            var displayProperty = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            string displayPropertyName = displayProperty?.Name ?? property.Name;

            return Localizer?[displayPropertyName] ?? displayPropertyName;
        }
    }
}