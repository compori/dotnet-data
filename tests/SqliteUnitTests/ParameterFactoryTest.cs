using Compori.Data;
using Compori.Data.Sqlite;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace ComporiTesting.Data.Sqlite
{
    [Trait("Provider", "Sqlite")]
    public class ParameterFactoryTest
    {
        [Fact]
        public void CreateTest()
        {
            IParameterFactory sut;
            DbType dbType;
            SqliteType sqliteType;
            SqliteParameter parameter;
            // IDbDataParameter parameter;
            string name;
            sut = new ParameterFactory();

            // AnsiString
            name = "parameter";
            dbType = DbType.AnsiString;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);
            Assert.Equal(DbType.String, parameter.DbType);

            // AnsiStringFixedLength
            name = "parameter";
            dbType = DbType.AnsiStringFixedLength;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);
            Assert.Equal(DbType.String, parameter.DbType);

            // Binary
            name = "parameter";
            dbType = DbType.Binary;
            sqliteType = SqliteType.Blob;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Boolean
            name = "parameter";
            dbType = DbType.Boolean;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Byte
            name = "parameter";
            dbType = DbType.Byte;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Currency
            name = "parameter";
            dbType = DbType.Currency;
            sqliteType = SqliteType.Real;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Date
            name = "parameter";
            dbType = DbType.Date;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // DateTime
            name = "parameter";
            dbType = DbType.DateTime;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // DateTime Null
            name = "parameter";
            dbType = DbType.DateTime;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, null) as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);
            Assert.Equal(DBNull.Value, parameter.Value);

            // DateTime2
            name = "parameter";
            dbType = DbType.DateTime2;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // DateTimeOffset
            name = "parameter";
            dbType = DbType.DateTimeOffset;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Decimal
            name = "parameter";
            dbType = DbType.Decimal;
            sqliteType = SqliteType.Real;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Double
            name = "parameter";
            dbType = DbType.Double;
            sqliteType = SqliteType.Real;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Guid
            name = "parameter";
            dbType = DbType.Guid;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Int16
            name = "parameter";
            dbType = DbType.Int16;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Int32
            name = "parameter";
            dbType = DbType.Int32;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Int64
            name = "parameter";
            dbType = DbType.Int64;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Object
            name = "parameter";
            dbType = DbType.Object;
            sqliteType = SqliteType.Blob;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // SByte
            name = "parameter";
            dbType = DbType.SByte;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Single
            name = "parameter";
            dbType = DbType.Single;
            sqliteType = SqliteType.Real;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // String
            name = "parameter";
            dbType = DbType.String;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // StringFixedLength
            name = "parameter";
            dbType = DbType.StringFixedLength;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // String
            name = "parameter";
            dbType = DbType.Time;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // UInt16
            name = "parameter";
            dbType = DbType.UInt16;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // UInt32
            name = "parameter";
            dbType = DbType.UInt32;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // UInt64
            name = "parameter";
            dbType = DbType.UInt64;
            sqliteType = SqliteType.Integer;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // VarNumeric
            name = "parameter";
            dbType = DbType.VarNumeric;
            sqliteType = SqliteType.Real;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);

            // Xml
            name = "parameter";
            dbType = DbType.Xml;
            sqliteType = SqliteType.Text;
            parameter = sut.Create(name, dbType, "somevalue") as SqliteParameter;
            Assert.Equal(name, parameter.ParameterName);
            Assert.Equal(sqliteType, parameter.SqliteType);
        }
    }
}
