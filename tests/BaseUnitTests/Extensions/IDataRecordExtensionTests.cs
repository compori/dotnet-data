using Moq;
using System;
using System.Data;
using Xunit;
using Compori.Data.Extensions;

namespace ComporiTesting.Data.Extensions
{
    public class IDataRecordExtensionTests
    {
        [Fact]
        public void TestGetInt32()
        {
            var mock = new Mock<IDataRecord>();
            var fieldName = "fieldName";
            var fieldNumber = 2;
            Int32 result = 123;

            mock.Setup(service => service.GetOrdinal(fieldName)).Returns(fieldNumber);
            mock.Setup(service => service.GetInt32(fieldNumber)).Returns(result);

            Assert.Equal(result, mock.Object.GetInt32(fieldName));

            mock.Verify(service => service.GetOrdinal(fieldName), Times.Once());
            mock.Verify(service => service.GetInt32(fieldNumber), Times.Once());
        }

        [Fact]
        public void TestGetInt64()
        {
            var mock = new Mock<IDataRecord>();
            var fieldName = "fieldName";
            var fieldNumber = 2;
            Int64 result = 123;

            mock.Setup(service => service.GetOrdinal(fieldName)).Returns(fieldNumber);
            mock.Setup(service => service.GetInt64(fieldNumber)).Returns(result);

            Assert.Equal(result, mock.Object.GetInt64(fieldName));

            mock.Verify(service => service.GetOrdinal(fieldName), Times.Once());
            mock.Verify(service => service.GetInt64(fieldNumber), Times.Once());
        }

        [Fact]
        public void TestGetDouble()
        {
            var mock = new Mock<IDataRecord>();
            var fieldName = "fieldName";
            var fieldNumber = 2;
            double result = 123.123;

            mock.Setup(service => service.GetOrdinal(fieldName)).Returns(fieldNumber);
            mock.Setup(service => service.GetDouble(fieldNumber)).Returns(result);

            Assert.Equal(result, mock.Object.GetDouble(fieldName));

            mock.Verify(service => service.GetOrdinal(fieldName), Times.Once());
            mock.Verify(service => service.GetDouble(fieldNumber), Times.Once());
        }

        [Fact]
        public void TestGetDateTime()
        {
            var mock = new Mock<IDataRecord>();
            var fieldName = "fieldName";
            var fieldNumber = 2;
            DateTime result = DateTime.Now;

            mock.Setup(service => service.GetOrdinal(fieldName)).Returns(fieldNumber);
            mock.Setup(service => service.GetDateTime(fieldNumber)).Returns(result);

            Assert.Equal(result, mock.Object.GetDateTime(fieldName));

            mock.Verify(service => service.GetOrdinal(fieldName), Times.Once());
            mock.Verify(service => service.GetDateTime(fieldNumber), Times.Once());
        }

        [Fact]
        public void TestGetString()
        {
            var mock = new Mock<IDataRecord>();
            var fieldName = "fieldName";
            var fieldNumber = 2;
            string result = "some string";

            mock.Setup(service => service.GetOrdinal(fieldName)).Returns(fieldNumber);
            mock.Setup(service => service.GetString(fieldNumber)).Returns(result);

            Assert.Equal(result, mock.Object.GetString(fieldName));

            mock.Verify(service => service.GetOrdinal(fieldName), Times.Once());
            mock.Verify(service => service.GetString(fieldNumber), Times.Once());
        }

        [Fact]
        public void TestGetNullalbeString()
        {
            var mock = new Mock<IDataRecord>();
            var fieldName = "fieldName";
            var fieldNumber = 2;
            string result = "some string";

            mock.Setup(service => service.IsDBNull(fieldNumber)).Returns(false);
            mock.Setup(service => service.GetOrdinal(fieldName)).Returns(fieldNumber);
            mock.Setup(service => service.GetString(fieldNumber)).Returns(result);

            Assert.Equal(result, mock.Object.GetNullableString(fieldName));

            mock.Verify(service => service.GetOrdinal(fieldName), Times.Once());
            mock.Verify(service => service.GetString(fieldNumber), Times.Once());
            mock.Verify(service => service.IsDBNull(fieldNumber), Times.Once());

            mock = new Mock<IDataRecord>();
            fieldName = "fieldName";
            fieldNumber = 2;
            result = null;

            mock.Setup(service => service.IsDBNull(fieldNumber)).Returns(true);
            mock.Setup(service => service.GetOrdinal(fieldName)).Returns(fieldNumber);
            mock.Setup(service => service.GetString(fieldNumber)).Returns(result);

            Assert.Equal(result, mock.Object.GetNullableString(fieldName));

            mock.Verify(service => service.GetOrdinal(fieldName), Times.Once());
            mock.Verify(service => service.GetString(fieldNumber), Times.Never());
            mock.Verify(service => service.IsDBNull(fieldNumber), Times.Once());
        }
    }
}
