using Compori.Data;
using Compori.Data.Sqlite.Extensions;
using Moq;
using System;
using System.Data;
using Xunit;

namespace ComporiTesting.Data.Sqlite.Extensions
{

    [Trait("Provider", "Sqlite")]
    public class IParameterizableCommandExtensionTest
    {
        #region Testing DateTime
        [Fact]
        public void WithDateTimeParameterTest()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            DateTime expect;

            expect = DateTime.Now;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.String, expect.ToString("o")), Times.Once());
        }

        [Fact]
        public void WithNullableDateTimeParameterTest()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";

            DateTime? expect;
            expect = DateTime.Now;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.String, expect.Value.ToString("o")), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (DateTime?)null);
            factoryMock.Verify(service => service.Create(name, DbType.String, null), Times.Once());
        }
        #endregion

        #region Testing DateTimeOffset
        [Fact]
        public void WithDateTimeOffsetParameterTest()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            DateTimeOffset expect;

            expect = DateTimeOffset.Now;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.String, expect.ToString("o")), Times.Once());
        }

        [Fact]
        public void WithNullableDateTimeOffsetParameterTest()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";

            DateTimeOffset? expect;
            expect = DateTimeOffset.Now;
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.String, expect.Value.ToString("o")), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (DateTimeOffset?)null);
            factoryMock.Verify(service => service.Create(name, DbType.String, null), Times.Once());
        }
        #endregion

        #region Testing Guid
        [Fact]
        public void WithGuidParameterTest()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            Guid expect;

            expect = Guid.NewGuid();
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.String, expect.ToString("N")), Times.Once());
        }

        [Fact]
        public void WithNullableGuidParameterTest()
        {
            Mock<IParameterizableCommand> mock;
            Mock<IParameterFactory> factoryMock;
            var name = "fieldName";
            Guid? expect;

            expect = Guid.NewGuid();
            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, expect);
            factoryMock.Verify(service => service.Create(name, DbType.String, expect.Value.ToString("N")), Times.Once());

            mock = new Mock<IParameterizableCommand>();
            factoryMock = new Mock<IParameterFactory>();
            mock.Setup(service => service.ParameterFactory).Returns(factoryMock.Object);
            mock.Object.WithParameter(name, (Guid?)null);
            factoryMock.Verify(service => service.Create(name, DbType.String, null), Times.Once());
        }
        #endregion
    }
}
