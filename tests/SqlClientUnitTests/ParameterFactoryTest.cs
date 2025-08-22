using Compori.Data;
using Compori.Data.SqlClient;
using System;
using System.Data;
#if NET35
using System.Data.SqlClient;
#else
using Microsoft.Data.SqlClient;
#endif
using Xunit;

namespace ComporiTesting.Data.SqlClient
{
    public class ParameterFactoryTest
    {
        [Fact]
        public void CreateTest()
        {
            IParameterFactory sut;
            DbType dbType;
            SqlDbType sqlDbType;
            SqlParameter parameter;

            string name;

            sut = new ParameterFactory();

            // AnsiString
            name = "parameter";
            dbType = DbType.AnsiString;
            sqlDbType = SqlDbType.VarChar;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(DbType.AnsiString, parameter.DbType);

            // AnsiStringFixedLength
            name = "parameter";
            dbType = DbType.AnsiStringFixedLength;
            sqlDbType = SqlDbType.Char;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(DbType.AnsiStringFixedLength, parameter.DbType);

            // Binary
            name = "parameter";
            dbType = DbType.Binary;
            sqlDbType = SqlDbType.VarBinary;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(DbType.Binary, parameter.DbType);

            // Boolean
            name = "parameter";
            dbType = DbType.Boolean;
            sqlDbType = SqlDbType.Bit;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(DbType.Boolean, parameter.DbType);

            // Byte
            name = "parameter";
            dbType = DbType.Byte;
            sqlDbType = SqlDbType.TinyInt;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(DbType.Byte, parameter.DbType);

            // Currency
            name = "parameter";
            dbType = DbType.Currency;
            sqlDbType = SqlDbType.Money;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(DbType.Currency, parameter.DbType);

            // Date
            name = "parameter";
            dbType = DbType.Date;
            sqlDbType = SqlDbType.Date;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);


            // DateTime
            name = "parameter";
            dbType = DbType.DateTime;
            sqlDbType = SqlDbType.DateTime;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // DateTime Null
            name = "parameter";
            dbType = DbType.DateTime;
            sqlDbType = SqlDbType.DateTime;
            parameter = sut.Create(name, dbType, null) as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(DBNull.Value, parameter.Value);
            Assert.Equal(dbType, parameter.DbType);

            // DateTime2
            name = "parameter";
            dbType = DbType.DateTime2;
            sqlDbType = SqlDbType.DateTime2;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // DateTimeOffset
            name = "parameter";
            dbType = DbType.DateTimeOffset;
            sqlDbType = SqlDbType.DateTimeOffset;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // Decimal
            name = "parameter";
            dbType = DbType.Decimal;
            sqlDbType = SqlDbType.Decimal;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // Double
            name = "parameter";
            dbType = DbType.Double;
            sqlDbType = SqlDbType.Float;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // Guid
            name = "parameter";
            dbType = DbType.Guid;
            sqlDbType = SqlDbType.UniqueIdentifier;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // Int16
            name = "parameter";
            dbType = DbType.Int16;
            sqlDbType = SqlDbType.SmallInt;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // Int32
            name = "parameter";
            dbType = DbType.Int32;
            sqlDbType = SqlDbType.Int;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // Int64
            name = "parameter";
            dbType = DbType.Int64;
            sqlDbType = SqlDbType.BigInt;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // Object
            name = "parameter";
            dbType = DbType.Object;
            sqlDbType = SqlDbType.Variant;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // SByte
            name = "parameter";
            dbType = DbType.SByte;
            sqlDbType = SqlDbType.SmallInt;
            Assert.Throws<ArgumentException>(() => parameter = sut.Create(name, dbType, "somevalue") as SqlParameter);

            // Single
            name = "parameter";
            dbType = DbType.Single;
            sqlDbType = SqlDbType.Real;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // String
            name = "parameter";
            dbType = DbType.String;
            sqlDbType = SqlDbType.NVarChar;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // StringFixedLength
            name = "parameter";
            dbType = DbType.StringFixedLength;
            sqlDbType = SqlDbType.NChar;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // String
            name = "parameter";
            dbType = DbType.Time;
            sqlDbType = SqlDbType.Time;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

            // UInt16
            name = "parameter";
            dbType = DbType.UInt16;
            sqlDbType = SqlDbType.VarChar;
            Assert.Throws<ArgumentException>(() => parameter = sut.Create(name, dbType, "somevalue") as SqlParameter);

            // UInt32
            name = "parameter";
            dbType = DbType.UInt32;
            sqlDbType = SqlDbType.VarChar;
            Assert.Throws<ArgumentException>(() => parameter = sut.Create(name, dbType, "somevalue") as SqlParameter);

            // UInt64
            name = "parameter";
            dbType = DbType.UInt64;
            sqlDbType = SqlDbType.VarChar;
            Assert.Throws<ArgumentException>(() => parameter = sut.Create(name, dbType, "somevalue") as SqlParameter);

            // VarNumeric
            name = "parameter";
            dbType = DbType.VarNumeric;
            sqlDbType = SqlDbType.Real;
            Assert.Throws<ArgumentException>(() => parameter = sut.Create(name, dbType, "somevalue") as SqlParameter);

            // Xml
            name = "parameter";
            dbType = DbType.Xml;
            sqlDbType = SqlDbType.Xml;
            parameter = sut.Create(name, dbType, "somevalue") as SqlParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqlDbType, parameter.SqlDbType);
            Assert.Equal(dbType, parameter.DbType);

        }
    }
}
