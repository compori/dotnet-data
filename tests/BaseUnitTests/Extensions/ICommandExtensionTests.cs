using Compori.Data;
using Compori.Data.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace ComporiTesting.Data.Extensions
{
    public class ICommandExtensionTests
    {
        [Fact]
        public void TestExecuteSingle()
        {
            Mock<ICommand> mock;

            var message = "MyMessage";

            mock = new Mock<ICommand>();
            mock.Setup(service => service.Execute()).Returns(1);
            mock.Object.ExecuteSingle(message);
            mock.Verify(service => service.Execute(), Times.Once());

            mock = new Mock<ICommand>();
            mock.Setup(service => service.Execute()).Returns(0);
            var ex = Assert.Throws<ExecuteSingleCommandException>(() => mock.Object.ExecuteSingle(message));
            Assert.Equal(message, ex.Message);
            mock.Verify(service => service.Execute(), Times.Once());
        }

        [Fact]
        public void TestReadList()
        {
            Mock<ICommand> mock;
            Func<IDataReader, string> func = delegate (IDataReader reader) { return null; };

            mock = new Mock<ICommand>();
            mock.Setup(service => service.Read<string>(func)).Returns(new List<string>());
            Assert.Empty(mock.Object.ReadList<string>(func));
            mock.Verify(service => service.Read<string>(func), Times.Once());
        }
        
        /*
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
        */
    }
}
