using Compori.Data;
using Moq;
using System;
using System.Data;
using Xunit;

namespace ComporiTesting.Data
{
    public class ConnectionTests
    {
        [Fact()]
        public void TestGetConnection()
        {
            var parameterMock = new Mock<IParameterFactory>();
            var mock = new Mock<IDbConnection>();

            IConnection sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            Assert.Same(mock.Object, sut.Connection);
        }

        [Fact()]
        public void TestCreateCommand()
        {
            var mock = new Mock<IDbConnection>();
            IDbCommand command = new Mock<IDbCommand>().Object;
            IConnection sut;
            ICommand actual;

            // first case cannot open connection
            mock.Setup(service => service.State).Returns(ConnectionState.Closed);
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            Assert.Throws<ConnectionException>(() => sut.CreateCommand());
            mock.Verify(service => service.Open(), Times.Once);
            mock.Verify(service => service.State, Times.Exactly(2));

            // connection already open
            mock = new Mock<IDbConnection>();
            mock.Setup(service => service.State).Returns(ConnectionState.Open);
            mock.Setup(service => service.CreateCommand()).Returns(command);
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            actual = sut.CreateCommand();
            Assert.NotNull(actual);
            Assert.IsType<Command>(actual);

            mock.Verify(service => service.State, Times.Once);
            mock.Verify(service => service.CreateCommand(), Times.Once);

            // connection not open, but after open command
            mock = new Mock<IDbConnection>();
            mock.Setup(service => service.State).Returns(ConnectionState.Closed);
            mock.Setup(service => service.Open())
                .Callback(() => mock.Setup(service => service.State).Returns(ConnectionState.Open));
            mock.Setup(service => service.CreateCommand()).Returns(command);
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            actual = sut.CreateCommand();
            Assert.NotNull(actual);
            Assert.IsType<Command>(actual);
            Assert.Same(command, actual.Command);
            mock.Verify(service => service.Open(), Times.Once);
            mock.Verify(service => service.State, Times.Exactly(2));
            mock.Verify(service => service.CreateCommand(), Times.Once);
        }

        [Fact()]
        public void TestCreateCommandTimeout()
        {
            var mock = new Mock<IDbConnection>();
            Mock<IDbCommand> commandMock;
            IDbCommand command;
            IConnection sut;
            ICommand actual;
            var expectedTimeout = 123;
            int actualTimeout = 0;

            // first case cannot open connection
            commandMock = new Mock<IDbCommand>();
            command = commandMock.Object;
            mock.Setup(service => service.State).Returns(ConnectionState.Closed);
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            Assert.Throws<ConnectionException>(() => sut.CreateCommand(expectedTimeout));
            mock.Verify(service => service.Open(), Times.Once);
            mock.Verify(service => service.State, Times.Exactly(2));

            // connection already open
            mock = new Mock<IDbConnection>();
            commandMock = new Mock<IDbCommand>();
            command = commandMock.Object;
            mock.Setup(service => service.State).Returns(ConnectionState.Open);
            mock.Setup(service => service.CreateCommand()).Returns(command);
            commandMock.SetupSet(p => p.CommandTimeout = It.IsAny<int>()).Callback<int>(value => actualTimeout = value);

            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            actual = sut.CreateCommand(expectedTimeout);
            Assert.NotNull(actual);
            Assert.IsType<Command>(actual);
            Assert.Equal(expectedTimeout, actualTimeout);

            mock.Verify(service => service.State, Times.Once);
            mock.Verify(service => service.CreateCommand(), Times.Once);
            commandMock.VerifySet(service => service.CommandTimeout = expectedTimeout);

            // connection not open, but after open command
            mock = new Mock<IDbConnection>();
            commandMock = new Mock<IDbCommand>();
            command = commandMock.Object;
            mock.Setup(service => service.State).Returns(ConnectionState.Closed);
            mock.Setup(service => service.Open())
                .Callback(() => mock.Setup(service => service.State).Returns(ConnectionState.Open));
            mock.Setup(service => service.CreateCommand()).Returns(command);
            commandMock.SetupSet(p => p.CommandTimeout = It.IsAny<int>()).Callback<int>(value => actualTimeout = value);

            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);

            actual = sut.CreateCommand(expectedTimeout);

            Assert.NotNull(actual);
            Assert.IsType<Command>(actual);
            Assert.Same(command, actual.Command);
            Assert.Equal(expectedTimeout, actualTimeout);

            mock.Verify(service => service.Open(), Times.Once);
            mock.Verify(service => service.State, Times.Exactly(2));
            mock.Verify(service => service.CreateCommand(), Times.Once);
            commandMock.VerifySet(service => service.CommandTimeout = expectedTimeout);
        }

        [Fact()]
        public void TestBeginTransaction()
        {
            var mock = new Mock<IDbConnection>();
            IDbTransaction transaction = new Mock<IDbTransaction>().Object;
            IConnection sut;
            ITransaction actual;

            // first case cannot open connection
            mock.Setup(service => service.State).Returns(ConnectionState.Closed);
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            Assert.Throws<ConnectionException>(() => sut.BeginTransaction());
            mock.Verify(service => service.Open(), Times.Once);
            mock.Verify(service => service.State, Times.Exactly(2));

            // connection already open
            mock = new Mock<IDbConnection>();
            mock.Setup(service => service.State).Returns(ConnectionState.Open);
            mock.Setup(service => service.BeginTransaction()).Returns(transaction);
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            actual = sut.BeginTransaction();
            Assert.NotNull(actual);
            Assert.IsType<Transaction>(actual);

            mock.Verify(service => service.State, Times.Once);
            mock.Verify(service => service.BeginTransaction(), Times.Once);

            // connection not open, but after open command
            mock = new Mock<IDbConnection>();
            mock.Setup(service => service.State).Returns(ConnectionState.Closed);
            mock.Setup(service => service.Open())
                .Callback(() => mock.Setup(service => service.State).Returns(ConnectionState.Open));
            mock.Setup(service => service.BeginTransaction()).Returns(transaction);
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            actual = sut.BeginTransaction();
            Assert.NotNull(actual);
            Assert.IsType<Transaction>(actual);
            Assert.Same(transaction, actual.Transaction);
            mock.Verify(service => service.Open(), Times.Once);
            mock.Verify(service => service.State, Times.Exactly(2));
            mock.Verify(service => service.BeginTransaction(), Times.Once);

            // begin transaction with another isolation level
            mock = new Mock<IDbConnection>();
            mock.Setup(service => service.State).Returns(ConnectionState.Open);
            mock.Setup(service => service.BeginTransaction(IsolationLevel.ReadUncommitted)).Returns(transaction);
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            actual = sut.BeginTransaction(IsolationLevel.ReadUncommitted);
            Assert.NotNull(actual);
            Assert.IsType<Transaction>(actual);

            mock.Verify(service => service.State, Times.Once);
            mock.Verify(service => service.BeginTransaction(IsolationLevel.ReadUncommitted), Times.Once);
        }

        [Fact()]
        public void TestDispose()
        {
            var mock = new Mock<IDbConnection>();
            IConnection sut;
            using (sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object))
            {
            }
            mock.Verify(service => service.Dispose(), Times.Once);

            mock = new Mock<IDbConnection>();
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            sut.Dispose();

            Assert.Null(sut.Connection);
            Assert.Throws<ObjectDisposedException>(() => sut.CreateCommand());
        }

        [Fact()]
        public void TestDisposeWithCommands()
        {
            var mock = new Mock<IDbConnection>();

            // CreateCommand will return this command mock
            var commandMock = new Mock<IDbCommand>();
            var command = commandMock.Object;
            mock.Setup(service => service.State).Returns(ConnectionState.Open);
            mock.Setup(service => service.CreateCommand()).Returns(command);

            IConnection sut;
            using (sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object))
            {
                // create a command, that should be disposed whenn conection is disposed.
                sut.CreateCommand();
            }
            mock.Verify(service => service.Dispose(), Times.Once);
            commandMock.Verify(service => service.Dispose(), Times.Once);
        }

        [Fact()]
        public void TestDisposeWithTransactions()
        {
            var mock = new Mock<IDbConnection>();

            // CreateCommand will return this command mock
            var transactionMock = new Mock<IDbTransaction>();            
            var transaction = transactionMock.Object;
            mock.Setup(service => service.State).Returns(ConnectionState.Open);
            mock.Setup(service => service.BeginTransaction()).Returns(transaction);

            IConnection sut;
            using (sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object))
            {
                // create a transaction, that should be disposed whenn conection is disposed.
                sut.BeginTransaction();
            }
            mock.Verify(service => service.Dispose(), Times.Once);
            transactionMock.Verify(service => service.Dispose(), Times.Once);
        }

        [Fact()]
        public void TestClose()
        {
            Mock<IDbConnection> mock;
            IConnection sut;

            //
            // Close will be executed once.
            //
            mock = new Mock<IDbConnection>();
            using (sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object))
            {
                sut.Close();
            }
            // direct close and in dispose
            mock.Verify(service => service.Close(), Times.Exactly(2));

            //
            // Close will be executed as often es called.
            //
            mock = new Mock<IDbConnection>();
            using (sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object))
            {
                sut.Close();
                sut.Close();
                sut.Close();
                sut.Close();
            }
            // direct close and in dispose
            mock.Verify(service => service.Close(), Times.Exactly(5));

            //
            // Close will be executed once in dispose after that the connection is gone
            // and direct call will be returned immediately.
            //
            mock = new Mock<IDbConnection>();
            sut = new Connection(mock.Object, new Mock<IParameterFactory>().Object, new Mock<IHydratorFactory>().Object);
            sut.Dispose();
            sut.Close();
            mock.Verify(service => service.Close(), Times.Once);
        }
    }
}
