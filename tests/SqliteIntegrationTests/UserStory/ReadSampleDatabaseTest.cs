using Compori.Data.Extensions;
using Compori.Data.Sqlite.Extensions;
using Xunit;

namespace ComporiTesting.Data.Sqlite.UserStory
{
    [Trait("Provider", "Sqlite"), Collection("Database collection")]
    public class ReadSampleDatabaseTest
    {
        /// <summary>
        /// The fixture
        /// </summary>
        protected readonly DatabaseFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadSampleDatabaseTest"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public ReadSampleDatabaseTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact()]
        public void TestReadAdamsEmployee()
        {
            using (var connection = this.fixture.Factory.Create())
            {
                var result = connection
                    .CreateCommand()
                    .WithQuery("SELECT FirstName FROM employees WHERE LastName=@LastName")
                    .WithParameter("@LastName", "Adams")
                    .ExecuteScalar<string>();
                Assert.Equal("Andrew", result);
            }
        }
    }
}
