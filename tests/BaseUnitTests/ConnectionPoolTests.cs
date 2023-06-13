using Compori.Data;
using Moq;
using System;
using Xunit;

namespace ComporiTesting.Data
{
    public class ConnectionPoolTests
    {
        [Fact()]
        public void RegisterTest()
        {
            var sut = new ConnectionPool();

            var sk1 = Guid.NewGuid().ToString();
            var sk2 = Guid.NewGuid().ToString();

            IConnectionFactory connectionFactory1 = new Mock<IConnectionFactory>().Object;
            IConnectionFactory connectionFactory2 = new Mock<IConnectionFactory>().Object;

            sut.Register(sk1, connectionFactory1);
            sut.Register(sk2, connectionFactory2);
            
            sut.Register(sk1, connectionFactory2);
        }

        [Fact()]
        public void UnregisterTest()
        {
            var sut = new ConnectionPool();

            var sk1 = Guid.NewGuid().ToString();
            var sk2 = Guid.NewGuid().ToString();

            IConnectionFactory connectionFactory1 = new Mock<IConnectionFactory>().Object;
            IConnectionFactory connectionFactory2 = new Mock<IConnectionFactory>().Object;

            sut.Register(sk1, connectionFactory1);
            sut.Register(sk2, connectionFactory2);

            sut.Register(sk1, connectionFactory2);


            sut.Unregister(sk1);
            sut.Unregister(sk2);
        }

        [Fact()]
        public void CreateTest()
        {
            var sut = new ConnectionPool();

            var mockConnectionFactory = new Mock<IConnectionFactory>();
            var mockConnection = new Mock<IConnection>();
            var connection = mockConnection.Object;
            mockConnectionFactory.Setup(service => service.Create()).Returns(connection);

            var sk1 = Guid.NewGuid().ToString();
            sut.Register(sk1, mockConnectionFactory.Object);

            var actual = sut.Create(sk1);
            Assert.Same(connection, actual);

            var sk2 = Guid.NewGuid().ToString();
            Assert.Throws<ConnectionPoolException>(() => sut.Create(sk2));
        }
    }
}
