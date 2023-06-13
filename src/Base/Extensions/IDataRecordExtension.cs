using System;
using System.Data;

namespace Compori.Data.Extensions
{
    /// <summary>
    /// Class IDataRecordExtension.
    /// </summary>
    public static class IDataRecordExtension
    {
        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Int32.</returns>
        public static int GetInt32(this IDataRecord record, string name)
        {
            return record.GetInt32(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Int64.</returns>
        public static long GetInt64(this IDataRecord record, string name)
        {
            return record.GetInt64(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Double.</returns>
        public static double GetDouble(this IDataRecord record, string name)
        {
            return record.GetDouble(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetDateTime(this IDataRecord record, string name)
        {
            return record.GetDateTime(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        public static string GetString(this IDataRecord record, string name)
        {
            return record.GetString(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable string.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="column">The column.</param>
        /// <returns>System.String.</returns>
        public static string GetNullableString(this IDataRecord record, int column)
        {
            return !record.IsDBNull(column) ? record.GetString(column) : null;
        }

        /// <summary>
        /// Gets the nullable string.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        public static string GetNullableString(this IDataRecord record, string name)
        {
            return record.GetNullableString(record.GetOrdinal(name));
        }
    }
}
