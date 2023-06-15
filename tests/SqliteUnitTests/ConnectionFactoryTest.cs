using Compori.Data;
using Compori.Data.Sqlite;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using Xunit;

namespace ComporiTesting.Data.Sqlite
{
    [Trait("Provider", "Sqlite")]
    public class ConnectionFactoryTest
    {
        /// <summary>
        /// Setups the test tables.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected void SetupTables(IConnection connection)
        {
            var command = connection.CreateCommand();
            command
                .WithQuery(@"CREATE TABLE A(ID INTEGER, BID INTEGER); CREATE TABLE B(ID INTEGER, MYVAL VARCHAR);")
                .Execute();
            command
                .WithQuery(@"INSERT INTO A (ID, BID) VALUES(2, 1); INSERT INTO B (ID, MYVAL) VALUES(1,'TEST');")
                .Execute();
        }

        /// <summary>
        /// Tears down tables.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected void TearDownTables(IConnection connection)
        {
            var command = connection.CreateCommand();
            command
                .WithQuery("DROP TABLE A")
                .Execute();
            command
                .WithQuery("DROP TABLE B")
                .Execute();
        }

        /// <summary>
        /// Asserts the command exceutions.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected void AssertCommandExecutions(IConnection connection)
        {
            var command = connection.CreateCommand();
            Assert.Equal(1, command.WithQuery(@"SELECT BID FROM A").ExecuteScalar<long>());
            Assert.Equal("TEST", command.WithQuery(@"SELECT MYVAL FROM B").ExecuteScalar<string>());
        }

        [Fact]
        public void TestCommandsInPrivateMemory()
        {
            IConnectionFactory sut = new ConnectionFactory();

            var connection = sut.Create();
            this.SetupTables(connection);
            this.AssertCommandExecutions(connection);

            //
            // Second connection do not hav access to database from first connection
            //
            var connection2 = sut.Create();

            SqliteException ex;
            ex = Assert.Throws<SqliteException>(() => connection2.CreateCommand().WithQuery(@"SELECT BID FROM A").ExecuteScalar<long>());
            Assert.Equal(1, ex.SqliteErrorCode);
            Assert.Equal("SQLite Error 1: 'no such table: A'.", ex.Message);
            ex = Assert.Throws<SqliteException>(() => connection2.CreateCommand().WithQuery(@"SELECT MYVAL FROM B").ExecuteScalar<string>());
            Assert.Equal(1, ex.SqliteErrorCode);
            Assert.Equal("SQLite Error 1: 'no such table: B'.", ex.Message);
        }

        [Fact]
        public void TestCommandsInSharedMemory()
        {
            IConnectionFactory sut = new ConnectionFactory().Configure(true);
            var connection = sut.Create();
            try
            {
                this.SetupTables(connection);
                this.AssertCommandExecutions(connection);

                // second connection could read memory database in shared access.
                var connection2 = sut.Create();
                this.AssertCommandExecutions(connection2);
            }
            finally
            {
                this.TearDownTables(connection);
            }
        }

        [Fact]
        public void TestCommandsInTempFile()
        {
            var folder = Path.GetTempPath();
            var file = Guid.NewGuid().ToString("N") + ".db";
            var path = Path.Combine(folder, file);

            IConnectionFactory sut = new ConnectionFactory().Configure(file, folder, false);
            try
            {
                using (var connection = sut.Create())
                {
                    this.SetupTables(connection);
                    this.AssertCommandExecutions(connection);

                    // second connection could read memory database in shared access.
                    var connection2 = sut.Create();
                    this.AssertCommandExecutions(connection2);

                    // dispose otherwise file will not be deleted.
                    connection2.Dispose();
                }
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}
