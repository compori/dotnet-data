using Compori.Data;
using Compori.Data.SqlClient.Extensions;
using Moq;
using Xunit;

namespace ComporiTesting.Data.SqlClient.Extensions
{
    public class ICommandExtensionTests
    {
        [Fact]
        public void TestSelectOne()
        {
            Mock<ICommand> mock;
            Mock<ICommand> mockExecute;

            var table = "MyTable";
            var field = "MyField";
            var where = "MyFilter";
            int expect = 123;

            mock = new Mock<ICommand>();
            mockExecute = new Mock<ICommand>();
            mock.Setup(service => service.WithQuery($"SELECT TOP 1 {field} FROM {table} WHERE {where}")).Returns(mockExecute.Object);
            mockExecute.Setup(service => service.ExecuteScalar<int>()).Returns(expect);

            Assert.Equal(expect, mock.Object.SelectOne<int>(table, field, where));

            mock.Verify(service => service.WithQuery($"SELECT TOP 1 {field} FROM {table} WHERE {where}"), Times.Once());
            mockExecute.Verify(service => service.ExecuteScalar<int>(), Times.Once());
        }

        [Fact]
        public void TestSelectOneOrDefault()
        {
            Mock<ICommand> mock;
            Mock<ICommand> mockExecute;

            var table = "MyTable";
            var field = "MyField";
            var where = "MyFilter";
            int expect = 123;

            mock = new Mock<ICommand>();
            mockExecute = new Mock<ICommand>();
            mock.Setup(service => service.WithQuery($"SELECT TOP 1 {field} FROM {table} WHERE {where}")).Returns(mockExecute.Object);
            mockExecute.Setup(service => service.ExecuteScalarOrDefault<int>()).Returns(expect);

            Assert.Equal(expect, mock.Object.SelectOneOrDefault<int>(table, field, where));

            mock.Verify(service => service.WithQuery($"SELECT TOP 1 {field} FROM {table} WHERE {where}"), Times.Once());
            mockExecute.Verify(service => service.ExecuteScalarOrDefault<int>(), Times.Once());
        }
    }
}
