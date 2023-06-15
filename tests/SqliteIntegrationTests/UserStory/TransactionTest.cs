using Compori.Data.Extensions;
using Compori.Data.Sqlite.Extensions;
using Microsoft.Data.Sqlite;
using Xunit;

namespace ComporiTesting.Data.Sqlite.UserStory
{

    /// <summary>
    /// Class TransactionTest.
    /// https://www.sqlite.org/isolation.html
    /// </summary>
    [Trait("Provider", "Sqlite"), Collection("Database collection")]

    public class TransactionTest
    {
        /// <summary>
        /// The fixture
        /// </summary>
        protected readonly DatabaseFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionTest"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public TransactionTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact()]
        public void TestLockTimeout()
        {
            using (var connection = this.fixture.Factory.Create())
            {
                var transaction = connection.BeginTransaction();

                //
                // Read a datarecord within a transaction
                //
                var result = transaction
                    .CreateCommand()
                    .WithQuery("SELECT GenreId FROM genres WHERE Name=@Name;")
                    .WithParameter("@Name", "Rock")
                    .ExecuteScalar<long>();
                Assert.Equal(1, result);
                transaction
                    .CreateCommand()
                    .WithQuery("INSERT INTO genres (Name) VALUES (@Name);")
                    .WithParameter("@Name", "MyGenre")
                    .Execute();

                var connection2 = this.fixture.Factory.Create();

                //
                // Try to insert Read a datarecord without transaction
                //
                var ex = Assert.Throws<SqliteException>(() => connection2
                    .CreateCommand(1)
                    .WithQuery("INSERT INTO genres (Name) VALUES (@Name);")
                    .WithParameter("@Name", "MyGenre 2")
                    .Execute());
                Assert.Equal(5, ex.SqliteErrorCode);
                Assert.Equal("SQLite Error 5: 'database is locked'.", ex.Message);
                transaction.Commit();
            }
        }

        //[Fact()]
        //public void LockTest()
        //{
        //    using (var connection = this.fixture.Factory.Create())
        //    {
        //        var trans = connection.BeginTransaction(IsolationLevel.ReadCommitted);

        //        //
        //        // Read a datarecord within a transaction
        //        //
        //        var result = trans
        //            .CreateCommand()
        //            .WithQuery("SELECT GenreId FROM genres WHERE Name=@Name;")
        //            .WithParameter("@Name", "Rock")
        //            .ExecuteScalar<long>();
        //        Assert.Equal(1, result);
        //        trans
        //            .CreateCommand()
        //            .WithQuery("INSERT INTO genres (Name) VALUES (@Name);")
        //            .WithParameter("@Name", "Rock 2")
        //            .Execute();

        //        // wait 10 seconds and commit;
        //        var t = Task.Run(() => { Task.Delay(new TimeSpan(0, 0, 5)).Wait(); trans.Commit(); });

        //        var connection2 = this.fixture.Factory.Create();
        //        var command = connection2.CreateCommand();
        //        command.Command.CommandTimeout = 2;

        //        //
        //        // Read a datarecord within without transaction
        //        //
        //        result = connection2
        //            .CreateCommand()
        //            .WithQuery("SELECT GenreId FROM genres WHERE Name=@Name;")
        //            .WithParameter("@Name", "Rock")
        //            .ExecuteScalar<long>();
        //        Assert.Equal(1, result);

        //        connection2
        //            .CreateCommand()
        //            .WithQuery("INSERT INTO genres (Name) VALUES (@Name);")
        //            .WithParameter("@Name", "Rock 2")
        //            .Execute();

        //        t.Wait();
        //    }
        //}
    }
}
