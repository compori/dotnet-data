using System;
using System.Data;
using System.Data.SqlClient;

namespace Compori.Data.SqlClient
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
        protected IDbDataParameter Create(string name, DbType dbType, object value)
        {
            SqlDbType sqlDbType = SqlDbType.Variant;

            switch (dbType)
            {
                case DbType.AnsiString:
                    sqlDbType = SqlDbType.VarChar;
                    break;
                case DbType.AnsiStringFixedLength:
                    sqlDbType = SqlDbType.Char;
                    break;
                case DbType.Binary:
                    sqlDbType = SqlDbType.VarBinary;
                    break;
                case DbType.Boolean:
                    sqlDbType = SqlDbType.Bit;
                    break;
                case DbType.Byte:
                    sqlDbType = SqlDbType.TinyInt;
                    break;
                case DbType.Currency:
                    sqlDbType = SqlDbType.Money;
                    break;
                case DbType.Date:
                    sqlDbType = SqlDbType.Date;
                    break;
                case DbType.DateTime:
                    sqlDbType = SqlDbType.DateTime;
                    break;
                case DbType.DateTime2:
                    sqlDbType = SqlDbType.DateTime2;
                    break;
                case DbType.DateTimeOffset:
                    sqlDbType = SqlDbType.DateTimeOffset;
                    break;
                case DbType.Decimal:
                    sqlDbType = SqlDbType.Decimal;
                    break;
                case DbType.Double:
                    sqlDbType = SqlDbType.Float;
                    break;
                case DbType.Guid:
                    sqlDbType = SqlDbType.UniqueIdentifier;
                    break;
                case DbType.Int16:
                    sqlDbType = SqlDbType.SmallInt;
                    break;
                case DbType.Int32:
                    sqlDbType = SqlDbType.Int;
                    break;
                case DbType.Int64:
                    sqlDbType = SqlDbType.BigInt;
                    break;
                case DbType.Object:
                    sqlDbType = SqlDbType.Variant;
                    break;
                case DbType.SByte:
                    throw new ArgumentException("Could not convert DbType.SByte to SqlDbType.");
                case DbType.Single:
                    sqlDbType = SqlDbType.Real;
                    break;
                case DbType.String:
                    sqlDbType = SqlDbType.NVarChar;
                    break;
                case DbType.StringFixedLength:
                    sqlDbType = SqlDbType.NChar;
                    break;
                case DbType.Time:
                    sqlDbType = SqlDbType.Time;
                    break;
                case DbType.UInt16:
                    throw new ArgumentException("Could not convert DbType.UInt16 to SqlDbType.");
                case DbType.UInt32:
                    throw new ArgumentException("Could not convert DbType.UInt32 to SqlDbType.");
                case DbType.UInt64:
                    throw new ArgumentException("Could not convert DbType.UInt64 to SqlDbType.");
                case DbType.VarNumeric:
                    throw new ArgumentException("Could not convert DbType.VarNumeric to SqlDbType.");
                case DbType.Xml:
                    sqlDbType = SqlDbType.Xml;
                    break;
            }

            //
            // Create parameter
            //
            return new SqlParameter(name, sqlDbType)
            {
                Value = (value ?? DBNull.Value),
                IsNullable = (value == null)

            };
        }

        #region IDbDataParameter Implementation

        /// <summary>
        /// Creates the a database data parameter with specified name, type and value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="value">The value.</param>
        /// <returns>IDbDataParameter.</returns>
        IDbDataParameter IParameterFactory.Create(string name, DbType dbType, object value)
        {
            return this.Create(name, dbType, value);
        }

        #endregion
    }
}
