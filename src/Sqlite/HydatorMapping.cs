using System;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Compori.Data.Sqlite
{
    public class HydatorMapping : Data.HydatorMapping
    {
        public HydatorMapping(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        /// <summary>
        /// Converting a string to a datetime value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>DateTime.</returns>
        private static DateTime ConvertStringToDateTime(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            // 2003-10-17 00:00:00
            if (DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateValue))
            {
                return dateValue;
            }
            return default;
        }

        /// <summary>
        /// Called when convert function is not set und must be created.
        /// Can be overriden in order to adjust behaviour.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="value">The value.</param>
        protected override void OnCreateConvertFunc(IDataRecord record, object value)
        {
            if (this.PropertyType.IsAssignableFrom(typeof(DateTime)))
            {
                if (value is string)
                {
                    this.ConvertFunc = (v) => ConvertStringToDateTime(v as string);
                }
            }
            else
            {
                base.OnCreateConvertFunc(record, value);
            }
        }
    }
}
