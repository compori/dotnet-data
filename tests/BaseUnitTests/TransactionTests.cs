using Compori.Data;
using Moq;
using System;
using System.Data;
using Xunit;

namespace ComporiTesting.Data
{
    public class TransactionTests
    {
        [Fact()]
        public void TestGetTransaction()
        {
            var parameterMock = new Mock<IParameterFactory>();
            var mock = new Mock<IDbTransaction>();

            ITransaction sut = new Transaction(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Same(mock.Object, sut.Transaction);
        }

        [Fact()]
        public void TestCreateCommand()
        {
            var parameterMock = new Mock<IParameterFactory>();
            var mock = new Mock<IDbTransaction>();
            var connectionMock = new Mock<IDbConnection>();
            var commandMock = new Mock<IDbCommand>();

            ICommand actual;
            mock.Setup(service => service.Connection).Returns(connectionMock.Object);
            connectionMock.Setup(service => service.CreateCommand()).Returns(commandMock.Object);

            // Setup command property "Transaction" setter;
            IDbTransaction dbTransaction = null;
            commandMock.SetupSet(p => p.Transaction = It.IsAny<IDbTransaction>()).Callback<IDbTransaction>(value => dbTransaction = value);
            ITransaction sut = new Transaction(mock.Object, new Mock<IParameterFactory>().Object);

            actual = sut.CreateCommand();

            Assert.Same(commandMock.Object, actual.Command);
            Assert.Same(mock.Object, dbTransaction);

            mock.Verify(service => service.Connection, Times.Once);
            connectionMock.Verify(service => service.CreateCommand(), Times.Once);
        }

        [Fact()]
        public void TestCreateCommandWithTimeout()
        {
            var parameterMock = new Mock<IParameterFactory>();
            var mock = new Mock<IDbTransaction>();
            var connectionMock = new Mock<IDbConnection>();
            var commandMock = new Mock<IDbCommand>();
            var expectedTimeout = 123;

            ICommand actual;
            mock.Setup(service => service.Connection).Returns(connectionMock.Object);
            connectionMock.Setup(service => service.CreateCommand()).Returns(commandMock.Object);
            commandMock.Setup(service => service.CommandTimeout).Returns(30);

            // Setup command property "Transaction" setter;
            IDbTransaction dbTransaction = null;
            int actualTimeout = 0;
            commandMock.SetupSet(p => p.Transaction = It.IsAny<IDbTransaction>()).Callback<IDbTransaction>(value => dbTransaction = value);
            commandMock.SetupSet(p => p.CommandTimeout = It.IsAny<int>()).Callback<int>(value => actualTimeout = value);
            ITransaction sut = new Transaction(mock.Object, new Mock<IParameterFactory>().Object);

            actual = sut.CreateCommand(expectedTimeout);

            Assert.Same(commandMock.Object, actual.Command);
            Assert.Same(mock.Object, dbTransaction);
            Assert.Equal(expectedTimeout, actualTimeout);

            mock.Verify(service => service.Connection, Times.Once);
            connectionMock.Verify(service => service.CreateCommand(), Times.Once);

            // double check!
            commandMock.VerifySet(service => service.CommandTimeout = expectedTimeout);
        }

        [Fact()]
        public void TestCommit()
        {
            var parameterMock = new Mock<IParameterFactory>();
            var mock = new Mock<IDbTransaction>();

            ITransaction sut = new Transaction(mock.Object, new Mock<IParameterFactory>().Object);
            sut.Commit();
            mock.Verify(service => service.Commit(), Times.Once);
        }

        [Fact()]
        public void TestRollback()
        {
            var parameterMock = new Mock<IParameterFactory>();
            var mock = new Mock<IDbTransaction>();

            ITransaction sut = new Transaction(mock.Object, new Mock<IParameterFactory>().Object);
            sut.Rollback();
            mock.Verify(service => service.Rollback(), Times.Once);
        }

        [Fact()]
        public void TestDispose()
        {
            var mock = new Mock<IDbTransaction>();
            ITransaction sut;
            using (sut = new Transaction(mock.Object, new Mock<IParameterFactory>().Object))
            {
            }
            mock.Verify(service => service.Dispose(), Times.Once);

            mock = new Mock<IDbTransaction>();
            sut = new Transaction(mock.Object, new Mock<IParameterFactory>().Object);
            sut.Dispose();

            Assert.Null(sut.Transaction);
            Assert.Throws<ObjectDisposedException>(() => sut.Commit());
            Assert.Throws<ObjectDisposedException>(() => sut.Rollback());
        }

        [Fact()]
        public void TestDisposeWithCommands()
        {

            var mockConnection = new Mock<IDbConnection>();

            // CreateCommand will return this command mock
            var commandMock = new Mock<IDbCommand>();
            var command = commandMock.Object;
            mockConnection.Setup(service => service.State).Returns(ConnectionState.Open);
            mockConnection.Setup(service => service.CreateCommand()).Returns(command);

            var mockTransaction = new Mock<IDbTransaction>();
            mockTransaction.Setup(service => service.Connection).Returns(mockConnection.Object);

            ITransaction sut;
            using (sut = new Transaction(mockTransaction.Object, new Mock<IParameterFactory>().Object))
            {
                // create a command, that should be disposed whenn conection is disposed.
                sut.CreateCommand();
            }
            mockTransaction.Verify(service => service.Dispose(), Times.Once);
            commandMock.Verify(service => service.Dispose(), Times.Once);
        }
    }
}
