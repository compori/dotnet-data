using Compori.Data.Extensions;
using System;
using System.Data;
using System.Globalization;

namespace Compori.Data.Sqlite.Extensions
{
    /// <summary>
    /// Class ICommandParameterExtension.
    /// </summary>
    public static class IParameterizableCommandExtension
    {
        /// <summary>
        /// Creates the a database data parameter with specified name and datetime value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The datetime value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DateTime value)
        {
            return command.WithParameter(name, DbType.String, value.ToString("o", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and datetime value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The datetime value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DateTime? value)
        {
            if (value.HasValue)
            {
                return command.WithParameter(name, value.Value);
            }
            return command.WithParameter(name, DbType.String, null);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and datetimeoffset value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DateTimeOffset value)
        {
            return command.WithParameter(name, DbType.String, value.ToString("o", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and datetimeoffset value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DateTimeOffset? value)
        {
            if (value.HasValue)
            {
                return command.WithParameter(name, value.Value);
            }
            return command.WithParameter(name, DbType.String, null);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and guid value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The guid value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, Guid value)
        {
            return command.WithParameter(name, DbType.String, value.ToString("N", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and guid value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The guid value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, Guid? value)
        {
            if (value.HasValue)
            {
                return command.WithParameter(name, value.Value);
            }
            return command.WithParameter(name, DbType.String, null);
        }
    }
}
