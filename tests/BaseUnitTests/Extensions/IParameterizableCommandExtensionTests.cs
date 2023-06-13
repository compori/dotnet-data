using Compori.Data;
using Compori.Data.Extensions;
using Moq;
using System;
using System.Data;
using Xunit;

namespace ComporiTesting.Data.Extensions
{
    public class IParameterizableCommandExtensionTests
    {
        #region Testing Int32
        [Fact]
        public void TestWithInt32Parameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            int expect;

            expect = 123;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.Int32, expect), Times.Once());
        }

        [Fact]
        public void TestWithNullableInt32Parameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";

            int? expect;
            expect = 123;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.Int32, expect), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (int?)null);
            factoryMock.Verify(service => service.Create(name, DbType.Int32, null), Times.Once());
        }
        #endregion

        #region Testing Int64
        [Fact]
        public void TestWithInt64Parameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            long expect;

            expect = 123;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.Int64, expect), Times.Once());
        }

        [Fact]
        public void TestWithNullableInt64Parameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";

            long? expect;
            expect = 123;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.Int64, expect), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (long?)null);
            factoryMock.Verify(service => service.Create(name, DbType.Int64, null), Times.Once());
        }
        #endregion

        #region Testing Double
        [Fact]
        public void TestWithDoubleParameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            double expect;

            expect = 123.456;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.Double, expect), Times.Once());
        }

        [Fact]
        public void TestWithNullableDoubleParameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";

            double? expect;
            expect = 123.456;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.Double, expect), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (double?)null);
            factoryMock.Verify(service => service.Create(name, DbType.Double, null), Times.Once());
        }
        #endregion

        #region Testing String
        [Fact]
        public void TestWithStringParameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            string expect;

            expect = "mystring";
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.String, expect), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (string)null);
            factoryMock.Verify(service => service.Create(name, DbType.String, null), Times.Once());
        }
        #endregion

        #region Testing Bytes
        [Fact]
        public void TestWithBytesParameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            byte[] expect;

            expect = new byte[] { 0, 1, 2 };
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.Binary, expect), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (byte[])null);
            factoryMock.Verify(service => service.Create(name, DbType.Binary, null), Times.Once());
        }
        #endregion

        #region Testing DateTime
        [Fact]
        public void TestWithDateTimeParameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            DateTime expect;

            expect = new DateTime(2019,01,12,22,21,11);
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.DateTime, expect), Times.Once());
        }

        [Fact]
        public void TestWithNullableDateTimeParameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";

            DateTime? expect;
            expect = new DateTime(2019, 01, 12, 22, 21, 11);
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);

            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.DateTime, expect), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (DateTime?)null);
            factoryMock.Verify(service => service.Create(name, DbType.DateTime, null), Times.Once());
        }
        #endregion

        #region Testing DateTimeOffset
        [Fact]
        public void TestWithDateTimeOffsetParameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            DateTimeOffset expect;

            expect = new DateTimeOffset(2019, 01, 12, 22, 21, 11, new TimeSpan(1,0,0));
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.DateTimeOffset, expect), Times.Once());
        }

        [Fact]
        public void TestWithNullableDateTimeOffsetParameter()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";

            DateTimeOffset? expect;
            expect = new DateTimeOffset(2019, 01, 12, 22, 21, 11, new TimeSpan(1, 0, 0));
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);

            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.DateTimeOffset, expect), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (DateTimeOffset?)null);
            factoryMock.Verify(service => service.Create(name, DbType.DateTimeOffset, null), Times.Once());
        }
        #endregion
    }
}
