using System;
using System.Data;

namespace Compori.Data.Extensions
{
    /// <summary>
    /// Class ICommandParameterExtension.
    /// </summary>
    public static class IParameterizableCommandExtension
    {
        /// <summary>
        /// Adds a value to the end of the SqlParameterCollection.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="dbType">Type of parameter.</param>
        /// <param name="value">The value to be added. Use DBNull.Value or null, to indicate a null value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DbType dbType, object value)
        {
            return command.WithParameter(command.ParameterFactory.Create(name, dbType, value));
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and boolean/bit value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The bool value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, bool value)
        {
            return command.WithParameter(name, DbType.Boolean, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and nullable boolean/bit value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The bool value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, bool? value)
        {
            return command.WithParameter(name, DbType.Boolean, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and byte value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The byte value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, byte value)
        {
            return command.WithParameter(name, DbType.Byte, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and nullable byte value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The byte value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, byte? value)
        {
            return command.WithParameter(name, DbType.Byte, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and int value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The int value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, int value)
        {
            return command.WithParameter(name, DbType.Int32, value);
        }

        /// <summary>
        /// Withes the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, int? value)
        {
            return command.WithParameter(name, DbType.Int32, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and long value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The long value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, long value)
        {
            return command.WithParameter(name, DbType.Int64, value);
        }

        /// <summary>
        /// Withes the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, long? value)
        {
            return command.WithParameter(name, DbType.Int64, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and double value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The double value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, double value)
        {
            return command.WithParameter(name, DbType.Double, value);
        }

        /// <summary>
        /// Withes the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, double? value)
        {
            return command.WithParameter(name, DbType.Double, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and string value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The string value.</param>
        /// <returns>IDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, string value)
        {
            return command.WithParameter(name, DbType.String, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and binary value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The guid value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, byte[] value)
        {
            return command.WithParameter(name, DbType.Binary, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and double value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The DateTime value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DateTime value)
        {
            return command.WithParameter(name, DbType.DateTime, value);
        }

        /// <summary>
        /// Withes the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The DateTime value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DateTime? value)
        {
            return command.WithParameter(name, DbType.DateTime, value);
        }

        /// <summary>
        /// Creates the a database data parameter with specified name and double value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The DateTime value.</param>
        /// <returns>IDbDataParameter.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DateTimeOffset value)
        {
            return command.WithParameter(name, DbType.DateTimeOffset, value);
        }

        /// <summary>
        /// Withes the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The DateTime value.</param>
        /// <returns>ICommand.</returns>
        public static ICommand WithParameter(this IParameterizableCommand command, string name, DateTimeOffset? value)
        {
            return command.WithParameter(name, DbType.DateTimeOffset, value);
        }
    }
}
