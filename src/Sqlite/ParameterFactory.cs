using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace Compori.Data.Sqlite
{
    /// <summary>
    /// Class ParameterFactory.
    /// </summary>
    /// <seealso cref="IParameterFactory" />
    public class ParameterFactory : IParameterFactory
    {
        /// <summary>
        /// Creates the a database data parameter with specified name, type and value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="value">The value.</param>
        /// <returns>IDbDataParameter.</returns>
        /// <exception cref="InvalidCastException">Could not determine sqlite database type.</exception>
        private static IDbDataParameter Create(string name, DbType dbType, object value)
        {
            SqliteType sqliteDbType = SqliteType.Blob;

            switch (dbType)
            {
                case DbType.AnsiString:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.AnsiStringFixedLength:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.Binary:
                    sqliteDbType = SqliteType.Blob;
                    break;
                case DbType.Boolean:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.Byte:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.Currency:
                    sqliteDbType = SqliteType.Real;
                    break;
                case DbType.Date:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.DateTime:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.DateTime2:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.DateTimeOffset:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.Decimal:
                    sqliteDbType = SqliteType.Real;
                    break;
                case DbType.Double:
                    sqliteDbType = SqliteType.Real;
                    break;
                case DbType.Guid:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.Int16:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.Int32:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.Int64:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.Object:
                    sqliteDbType = SqliteType.Blob;
                    break;
                case DbType.SByte:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.Single:
                    sqliteDbType = SqliteType.Real;
                    break;
                case DbType.String:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.StringFixedLength:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.Time:
                    sqliteDbType = SqliteType.Text;
                    break;
                case DbType.UInt16:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.UInt32:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.UInt64:
                    sqliteDbType = SqliteType.Integer;
                    break;
                case DbType.VarNumeric:
                    sqliteDbType = SqliteType.Real;
                    break;
                case DbType.Xml:
                    sqliteDbType = SqliteType.Text;
                    break;
            }

            //
            // create the parameter
            //
            return new SqliteParameter(name, sqliteDbType)
            {
                Value = (value ?? DBNull.Value),
                IsNullable = (value == null)
            };
        }

        #region IParameterFactory Implementation

        /// <summary>
        /// Creates the a database data parameter with specified name, type and value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="value">The value.</param>
        /// <returns>IDbDataParameter.</returns>
        IDbDataParameter IParameterFactory.Create(string name, DbType dbType, object value)
        {
            return Create(name, dbType, value);
        }

        #endregion
    }
}
