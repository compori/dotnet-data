using System;
using System.Reflection;

namespace Compori.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HydratorMappingAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ignore null value.
        /// </summary>
        /// <value><c>true</c> if ignore null value; otherwise, <c>false</c>.</value>
        public bool IgnoreNullValue { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether ignore not existing field.
        /// </summary>
        /// <value><c>true</c> if ignore not existing field; otherwise, <c>false</c>.</value>
        public bool IgnoreNotExistingField { get; set; } = true;

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>HydratorMappingAttribute.</returns>
        public static HydratorMappingAttribute GetAttribute(PropertyInfo propertyInfo)
        {
            return GetCustomAttribute(propertyInfo, typeof (HydratorMappingAttribute), true) as HydratorMappingAttribute;
        }
    }
}
