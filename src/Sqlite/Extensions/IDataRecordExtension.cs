using Compori.Data.Extensions;
using System;
using System.Data;
using System.Globalization;

namespace Compori.Data.Sqlite.Extensions
{
    /// <summary>
    /// Class IDataRecordExtension.
    /// </summary>
    public static class IDataRecordExtension
    {
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>Guid.</returns>
        /// <exception cref="ArgumentException">Could not convert result value to Guid.</exception>
        public static Guid GetGuid(this IDataRecord record, string name)
        {
            if (!Guid.TryParseExact(record.GetString(name), "N", out Guid result))
            {
                throw new ArgumentException("Could not convert result value to Guid.", nameof(name));
            }
            return result;
        }

        /// <summary>
        /// Gets the nullable unique identifier.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="column">The column.</param>
        /// <returns>System.Nullable&lt;Guid&gt;.</returns>
        /// <exception cref="ArgumentException">Could not convert result value to Guid.</exception>
        public static Guid? GetNullableGuid(this IDataRecord record, int column)
        {
            var value = record.GetString(column);
            if(value == null)
            {
                return null;
            }
            if (!Guid.TryParseExact(value, "N", out Guid result))
            {
                throw new ArgumentException("Could not convert result value to Guid.", nameof(column));
            }
            return result;
        }

        /// <summary>
        /// Gets the nullable unique identifier.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Nullable&lt;Guid&gt;.</returns>
        public static Guid? GetNullableGuid(this IDataRecord record, string name)
        {
            return record.GetNullableGuid(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>DateTime.</returns>
        /// <exception cref="ArgumentException">Could not convert result value to DateTime.</exception>
        public static DateTime GetDateTime(this IDataRecord record, string name)
        {
            if (!DateTime.TryParseExact(record.GetString(name), "o", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                throw new ArgumentException("Could not convert result value to DateTime.", nameof(name));
            }
            return result;
        }

        /// <summary>
        /// Gets the nullable date time.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="column">The column.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        /// <exception cref="ArgumentException">Could not convert result value to DateTime.</exception>
        public static DateTime? GetNullableDateTime(this IDataRecord record, int column)
        {
            var value = record.GetString(column);
            if (value == null)
            {
                return null;
            }
            if (!DateTime.TryParseExact(value, "o", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                throw new ArgumentException("Could not convert result value to DateTime.", nameof(column));
            }
            return result;
        }

        /// <summary>
        /// Gets the nullable date time.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? GetNullableDateTime(this IDataRecord record, string name)
        {
            return record.GetNullableDateTime(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the date time offset.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="column">The column.</param>
        /// <returns>DateTimeOffset.</returns>
        /// <exception cref="ArgumentException">Could not convert result value to DateTimeOffset.</exception>
        public static DateTimeOffset GetDateTimeOffset(this IDataRecord record, int column)
        {
            if (!DateTimeOffset.TryParseExact(record.GetString(column), "o", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset result))
            {
                throw new ArgumentException("Could not convert result value to DateTimeOffset.", nameof(column));
            }
            return result;
        }

        /// <summary>
        /// Gets the date time offset.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>DateTimeOffset.</returns>
        /// <exception cref="ArgumentException">Could not convert result value to DateTimeOffset.</exception>
        public static DateTimeOffset GetDateTimeOffset(this IDataRecord record, string name)
        {
            return record.GetDateTimeOffset(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable date time offset.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="column">The column.</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        /// <exception cref="ArgumentException">Could not convert result value to DateTimeOffset.</exception>
        public static DateTimeOffset? GetNullableDateTimeOffset(this IDataRecord record, int column)
        {
            var value = record.GetString(column);
            if (value == null)
            {
                return null;
            }
            if (!DateTimeOffset.TryParseExact(value, "o", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset result))
            {
                throw new ArgumentException("Could not convert result value to DateTimeOffset.", nameof(column));
            }
            return result;
        }

        /// <summary>
        /// Gets the nullable date time offset.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        public static DateTimeOffset? GetNullableDateTimeOffset(this IDataRecord record, string name)
        {
            return record.GetNullableDateTimeOffset(record.GetOrdinal(name));
        }
    }
}
