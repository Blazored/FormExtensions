using System;
using System.Reflection;

namespace Blazored.FormExtensions.Tests.Helpers
{
    public static class TestUtils
    {
        /// <summary>
        /// Sets a _private_ Property Value from a given Object.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is set</param>
        /// <param name="propertyName">Property name as string.</param>
        /// <param name="value">Value to set.</param>
        public static void SetPrivatePropertyValue<T>(this object obj, string propertyName, T value)
        {
            Type t = obj.GetType();
            PropertyInfo propertyInfo = null;
            while (propertyInfo == null && t != null)
            {
                propertyInfo = t.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }

            if (propertyInfo == null)
            {
                throw new ArgumentOutOfRangeException(nameof(propertyName), $"Private property {propertyName} was not found in Type {obj.GetType().FullName}");
            }

            propertyInfo.SetValue(obj, value);
        }
    }
}