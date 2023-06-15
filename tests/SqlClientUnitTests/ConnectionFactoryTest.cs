using Compori.Data;
using Compori.Data.SqlClient;
using System;
using Xunit;

namespace ComporiTesting.Data.SqlClient
{
    public class ConnectionFactoryTest
    {
        [Fact]
        public void ConfigureFailedTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ConnectionFactory().Configure(null));
            Assert.Throws<ArgumentNullException>(() => new ConnectionFactory().Configure(""));

            Assert.Throws<ArgumentNullException>(() => new ConnectionFactory().Configure("Data Source=.;Integrated Security=True", null, null));
            Assert.Throws<ArgumentNullException>(() => new ConnectionFactory().Configure("Data Source=.;Integrated Security=True", "", null));
            Assert.Throws<ArgumentNullException>(() => new ConnectionFactory().Configure("Data Source=.;Integrated Security=True", "abc", null));
        }

        [Fact]
        public void ConfigureTest()
        {
            ConnectionFactory factory;
            string connectionString = "Data Source=.;Integrated Security=True";

            factory = new ConnectionFactory(); 
            factory.Configure(connectionString);
            Assert.Equal(connectionString, factory.ConnectionString);

            factory = new ConnectionFactory();
            factory.Configure(connectionString, "abc", "");
#if !NET35
            Assert.Equal(connectionString, factory.ConnectionString);
#else
            Assert.Equal(connectionString + ";User ID=abc;Password=", factory.ConnectionString);
#endif
            factory = new ConnectionFactory();
            factory.Configure(connectionString, "abc", "ABCTX");

#if !NET35
            Assert.Equal(connectionString, factory.ConnectionString);
#else
            Assert.Equal(connectionString + ";User ID=abc;Password=ABCTX", factory.ConnectionString);
#endif
        }

        [Fact]
        public void CreateTest()
        {
            IConnectionFactory sut;

            string connectionString = "Data Source=.;Integrated Security=True";

            sut = new ConnectionFactory();
            ((ConnectionFactory)sut).Configure(connectionString);
            Assert.IsAssignableFrom<IConnection>(sut.Create());

            // no integrated security with credentials.
            connectionString = "Data Source=.;";
            sut = new ConnectionFactory();
            ((ConnectionFactory)sut).Configure(connectionString, "abc", "ABCTX");
            Assert.IsAssignableFrom<IConnection>(sut.Create());

            sut = new ConnectionFactory();
            Assert.Throws<InvalidOperationException>(() => sut.Create());
        }
    }
}
