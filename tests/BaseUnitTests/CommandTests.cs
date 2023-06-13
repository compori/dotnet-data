using Moq;
using System;
using System.Data;
using Xunit;
using System.Collections.Generic;
using Compori.Data;

namespace ComporiTesting.Data
{
    public class CommandTests
    {
        #region IExecutableCommand Testing
        [Fact()]
        public void TextExecute()
        {
            Mock<IDbCommand> mock = new Mock<IDbCommand>();
            int expectedResult;
            ICommand sut;

            mock = new Mock<IDbCommand>();
            expectedResult = 1;
            mock.Setup(service => service.ExecuteNonQuery()).Returns(expectedResult);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Equal(expectedResult, sut.Execute());
            mock.Verify(service => service.ExecuteNonQuery(), Times.Once());
        }

        [Fact()]
        public void TestExecuteScalarOfInt32()
        {
            Mock<IDbCommand> mock = new Mock<IDbCommand>();
            int expectedResult;
            ICommand sut;

            mock = new Mock<IDbCommand>();
            expectedResult = 1;
            mock.Setup(service => service.ExecuteScalar()).Returns(expectedResult);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Equal(expectedResult, sut.ExecuteScalar<int>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // Return value from command is null
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns(null);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Throws<ExecuteCommandException>( ()=> sut.ExecuteScalar<int>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // But use a nullable works fine.
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns(null);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Null(sut.ExecuteScalar<int?>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // Return value is can not be casted
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns("ABC");
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Throws<InvalidCastException>(() => sut.ExecuteScalar<int>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());
        }

        [Fact()]
        public void TestExecuteScalarOfString()
        {
            Mock<IDbCommand> mock = new Mock<IDbCommand>();
            string expectedResult;
            ICommand sut;

            mock = new Mock<IDbCommand>();
            expectedResult = "Hello";
            mock.Setup(service => service.ExecuteScalar()).Returns(expectedResult);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Equal(expectedResult, sut.ExecuteScalar<string>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // Return use a null value for refernece type works fine.
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns(null);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Null(sut.ExecuteScalar<string>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // Try to cast from datetime result to string must throw an exception
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns(DateTime.Now);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Throws<InvalidCastException>(() => sut.ExecuteScalar<string>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());
        }

        [Fact()]
        public void TestExecuteScalarOrDefaultOfInt32()
        {
            Mock<IDbCommand> mock = new Mock<IDbCommand>();
            int expectedResult;
            ICommand sut;

            mock = new Mock<IDbCommand>();
            expectedResult = 1;
            mock.Setup(service => service.ExecuteScalar()).Returns(expectedResult);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Equal(expectedResult, sut.ExecuteScalarOrDefault<int>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // Return value from command is null
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns(null);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Equal(0, sut.ExecuteScalarOrDefault<int>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // But use a nullable works fine.
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns(null);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Null(sut.ExecuteScalarOrDefault<int?>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // Return value is can not be casted
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns("ABC");
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Throws<InvalidCastException>(() => sut.ExecuteScalarOrDefault<int>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());
        }

        [Fact()]
        public void TestExecuteScalarOrDefaultOfString()
        {
            Mock<IDbCommand> mock = new Mock<IDbCommand>();
            string expectedResult;
            ICommand sut;

            mock = new Mock<IDbCommand>();
            expectedResult = "Hello";
            mock.Setup(service => service.ExecuteScalar()).Returns(expectedResult);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Equal(expectedResult, sut.ExecuteScalarOrDefault<string>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // Return use a null value for refernece type works fine.
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns(null);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Null(sut.ExecuteScalarOrDefault<string>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());

            //
            // Try to cast from datetime result to string must throw an exception
            //
            mock = new Mock<IDbCommand>();
            mock.Setup(service => service.ExecuteScalar()).Returns(DateTime.Now);
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Throws<InvalidCastException>(() => sut.ExecuteScalarOrDefault<string>());
            mock.Verify(service => service.ExecuteScalar(), Times.Once());
        }

        [Fact()]
        public void TestReadOfString()
        {
            Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            ICommand sut;
            IEnumerable<string> expect;
            IEnumerable<string> actual;

            //
            // No data found meaning empty enumeration
            //
            mockCommand = new Mock<IDbCommand>();
            mockReader = new Mock<IDataReader>();

            mockReader.Setup(service => service.Read()).Returns(false);
            mockCommand.Setup(service => service.ExecuteReader()).Returns(mockReader.Object);
            sut = new Command(mockCommand.Object, new Mock<IParameterFactory>().Object);
            actual = sut.Read(reader => reader.GetString(0));
            Assert.Empty(actual);

            mockReader.Verify(service => service.GetString(0), Times.Never());
            mockReader.Verify(service => service.Read(), Times.Once());
            mockCommand.Verify(service => service.ExecuteReader(), Times.Once());

            //
            // return an enumeration
            //
            expect = new string[] { "1", "2", "3", "4" };

            mockCommand = new Mock<IDbCommand>();
            mockReader = new Mock<IDataReader>();
            int recordIndex = 0;
            int recordCount = 4;
            mockReader.Setup(service => service.Read()).Returns(() => { recordIndex++; return recordIndex <= recordCount; } );
            mockReader.Setup(service => service.GetString(0)).Returns(() => recordIndex.ToString());
            mockCommand.Setup(service => service.ExecuteReader()).Returns(mockReader.Object);
            sut = new Command(mockCommand.Object, new Mock<IParameterFactory>().Object);
            actual = new List<string>(sut.Read(reader => reader.GetString(0))).ToArray();
            foreach (var i in actual)
            {
                Assert.Contains(i, expect);
            }
            Assert.Equal(expect, actual);

            mockReader.Verify(service => service.GetString(0), Times.Exactly(recordCount));
            mockReader.Verify(service => service.Read(), Times.Exactly(recordCount + 1));
            mockCommand.Verify(service => service.ExecuteReader(), Times.Once());
        }

        [Fact()]
        public void TestReadFirstOrDefaultOfString()
        {
            Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            ICommand sut;
            string expect;

            //
            // No data found
            //
            mockCommand = new Mock<IDbCommand>();
            mockReader = new Mock<IDataReader>();

            mockReader.Setup(service => service.Read()).Returns(false);
            mockCommand.Setup(service => service.ExecuteReader()).Returns(mockReader.Object);
            sut = new Command(mockCommand.Object, new Mock<IParameterFactory>().Object);
            Assert.Null(sut.ReadFirstOrDefault(reader => reader.GetString(0)));

            mockReader.Verify(service => service.GetString(0), Times.Never());
            mockReader.Verify(service => service.Read(), Times.Once());
            mockCommand.Verify(service => service.ExecuteReader(), Times.Once());


            //
            // Result is null
            //
            mockCommand = new Mock<IDbCommand>();
            mockReader = new Mock<IDataReader>();

            mockReader.Setup(service => service.Read()).Returns(false);
            mockReader.Setup(service => service.GetString(0)).Returns<string>(null);
            mockCommand.Setup(service => service.ExecuteReader()).Returns(mockReader.Object);
            sut = new Command(mockCommand.Object, new Mock<IParameterFactory>().Object);
            Assert.Null(sut.ReadFirstOrDefault(reader => reader.GetString(0)));

            mockReader.Verify(service => service.GetString(0), Times.Never());
            mockReader.Verify(service => service.Read(), Times.Once());
            mockCommand.Verify(service => service.ExecuteReader(), Times.Once());

            //
            // Result is null
            //
            expect = null;
            mockCommand = new Mock<IDbCommand>();
            mockReader = new Mock<IDataReader>();
            mockReader.Setup(service => service.Read()).Returns(true);
            mockReader.Setup(service => service.GetString(0)).Returns(expect);
            mockCommand.Setup(service => service.ExecuteReader()).Returns(mockReader.Object);
            sut = new Command(mockCommand.Object, new Mock<IParameterFactory>().Object);
            Assert.Equal(expect, sut.ReadFirstOrDefault(reader => reader.GetString(0)));

            mockReader.Verify(service => service.GetString(0), Times.Once());
            mockReader.Verify(service => service.Read(), Times.Once());
            mockCommand.Verify(service => service.ExecuteReader(), Times.Once());

            //
            // Result is something
            //
            expect = "ABC";
            mockCommand = new Mock<IDbCommand>();
            mockReader = new Mock<IDataReader>();
            mockReader.Setup(service => service.Read()).Returns(true);
            mockReader.Setup(service => service.GetString(0)).Returns(expect);
            mockCommand.Setup(service => service.ExecuteReader()).Returns(mockReader.Object);
            sut = new Command(mockCommand.Object, new Mock<IParameterFactory>().Object);
            Assert.Equal(expect, sut.ReadFirstOrDefault(reader => reader.GetString(0)));

            mockReader.Verify(service => service.GetString(0), Times.Once());
            mockReader.Verify(service => service.Read(), Times.Once());
            mockCommand.Verify(service => service.ExecuteReader(), Times.Once());
        }

        #endregion

        #region ICommand Testing
        [Fact()]
        public void TestGetCommand()
        {
            var mock = new Mock<IDbCommand>();

            ICommand sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            Assert.Same(mock.Object, sut.Command);
        }
        #endregion

        #region IQueryCapableCommand Testing
        [Fact()]
        public void TestWithQuery()
        {
            var parameterMock = new Mock<IParameterFactory>();
            var mock = new Mock<IDbCommand>();

            ICommand sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            string commandText = null;
            string expect = "SELECT 1";
            mock.SetupSet(p => p.CommandText = It.IsAny<string>()).Callback<string>(value => commandText = value);
            sut.WithQuery(expect);

            Assert.Same(expect, commandText);
        }
        #endregion

        #region IParameterizableCommand Testing

        [Fact()]
        public void TestGetParameterFactory()
        {
            var parameterFactoryMock = new Mock<IParameterFactory>();
            var mock = new Mock<IDbCommand>();

            ICommand sut = new Command(mock.Object, parameterFactoryMock.Object);
            Assert.Same(parameterFactoryMock.Object, sut.ParameterFactory);
        }

        [Fact()]
        public void TestWithParameter()
        {
            var mock = new Mock<IDbCommand>();
            var parameterMock = new Mock<IDataParameter>();
            var parameterCollectionMock = new Mock<IDataParameterCollection>();

            mock.Setup(service => service.Parameters).Returns(parameterCollectionMock.Object);

            ICommand sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            sut.WithParameter(parameterMock.Object);

            // call add once with the parameter object
            parameterCollectionMock.Verify(service => service.Add(parameterMock.Object), Times.Once);
        }
        #endregion

        #region IDisposable Testing
        [Fact()]
        public void TestDispose()
        {
            var mock = new Mock<IDbCommand>();
            var parameterMock = new Mock<IDataParameter>();
            ICommand sut;
            using (sut = new Command(mock.Object, new Mock<IParameterFactory>().Object))
            {
            }
            mock.Verify(service => service.Dispose(), Times.Once);

            mock = new Mock<IDbCommand>();
            sut = new Command(mock.Object, new Mock<IParameterFactory>().Object);
            sut.Dispose();

            Assert.Null(sut.Command);
            Assert.Null(sut.ParameterFactory);

            Assert.Throws<ObjectDisposedException>(() => sut.WithQuery("SELECT 1"));
            Assert.Throws<ObjectDisposedException>(() => sut.WithParameter(parameterMock.Object));
            Assert.Throws<ObjectDisposedException>(() => sut.Execute());
        }
        #endregion
    }
}
