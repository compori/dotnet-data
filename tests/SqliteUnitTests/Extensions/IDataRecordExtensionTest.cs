using Compori.Data.Sqlite.Extensions;
using Moq;
using System;
using System.Data;
using Xunit;

namespace ComporiTesting.Data.Sqlite.Extensions
{


    [Trait("Provider", "Sqlite")]
    public class IDataRecordExtensionTest
    {
        #region Testing GetGuid
        [Fact]
        public void GetGuidTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            Guid expect;

            mock = new Mock<IDataRecord>();
            expect = Guid.NewGuid();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns(expect.ToString("N"));

            Assert.Equal(expect, mock.Object.GetGuid(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }

        [Fact]
        public void GetGuidFailedTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            Guid expect;

            mock = new Mock<IDataRecord>();
            expect = Guid.NewGuid();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns("NOGUIDFORMAT");

            Assert.Throws<ArgumentException>(() => mock.Object.GetGuid(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());

            mock = new Mock<IDataRecord>();
            expect = Guid.NewGuid();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns<string>(null);

            Assert.Throws<ArgumentException>(() => mock.Object.GetGuid(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }
        #endregion

        #region Testing GetNullableGuid
        [Fact]
        public void GetNullableGuidTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            Guid expect;

            //
            // Behave like GetGuid
            //
            mock = new Mock<IDataRecord>();
            expect = Guid.NewGuid();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns(expect.ToString("N"));

            Assert.Equal(expect, mock.Object.GetNullableGuid(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with number
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetString(number)).Returns<string>(null);
            Assert.Null(mock.Object.GetNullableGuid(number));
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with name
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns<string>(null);

            Assert.Null(mock.Object.GetNullableGuid(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }

        [Fact]
        public void GetNullableGuidFailedTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;

            //
            // Nullable with number
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetString(number)).Returns("NOGUIDFORMAT");
            Assert.Throws<ArgumentException>(() => mock.Object.GetNullableGuid(number));
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with name
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns("NOGUIDFORMAT");
            Assert.Throws<ArgumentException>(() => mock.Object.GetNullableGuid(name));
            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }
        #endregion

        #region Testing GetDateTime
        [Fact]
        public void GetDateTimeTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            DateTime expect;

            mock = new Mock<IDataRecord>();
            expect = DateTime.Now;
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns(expect.ToString("o"));

            Assert.Equal(expect, mock.Object.GetDateTime(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }

        [Fact]
        public void GetDateTimeFailedTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            DateTime expect;

            mock = new Mock<IDataRecord>();
            expect = DateTime.Now;
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns("NOGUIDFORMAT");

            Assert.Throws<ArgumentException>(() => mock.Object.GetDateTime(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());

            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns<string>(null);

            Assert.Throws<ArgumentException>(() => mock.Object.GetDateTime(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }
        #endregion

        #region Testing GetNullableDateTime
        [Fact]
        public void GetNullableDateTimeTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            DateTime expect;

            //
            // Behave like GetDateTime
            //
            mock = new Mock<IDataRecord>();
            expect = DateTime.Now;
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns(expect.ToString("o"));

            Assert.Equal(expect, mock.Object.GetNullableDateTime(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with number
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetString(number)).Returns<string>(null);
            Assert.Null(mock.Object.GetNullableDateTime(number));
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with name
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns<string>(null);

            Assert.Null(mock.Object.GetNullableDateTime(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }

        [Fact]
        public void GetNullableDateTimeFailedTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;

            //
            // Nullable with number
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetString(number)).Returns("NODATETIMEFORMAT");
            Assert.Throws<ArgumentException>(() => mock.Object.GetNullableDateTime(number));
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with name
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns("NODATETIMEFORMAT");
            Assert.Throws<ArgumentException>(() => mock.Object.GetNullableDateTime(name));
            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }
        #endregion

        #region Testing GetDateTimeOffset
        [Fact]
        public void GetDateTimeOffsetTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            DateTimeOffset expect;

            //
            // with name
            //
            mock = new Mock<IDataRecord>();
            expect = DateTimeOffset.Now;
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns(expect.ToString("o"));

            Assert.Equal(expect, mock.Object.GetDateTimeOffset(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // with number
            //
            mock = new Mock<IDataRecord>();
            expect = DateTimeOffset.Now;
            mock.Setup(service => service.GetString(number)).Returns(expect.ToString("o"));
            Assert.Equal(expect, mock.Object.GetDateTimeOffset(number));
            mock.Verify(service => service.GetString(number), Times.Once());
        }

        [Fact]
        public void GetDateTimeOffsetFailedTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            DateTimeOffset expect;

            //
            // with name
            //
            mock = new Mock<IDataRecord>();
            expect = DateTimeOffset.Now;
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns("NODATETIMEOFFSET");
            Assert.Throws<ArgumentException>(() => mock.Object.GetDateTimeOffset(name));
            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());

            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns<string>(null);
            Assert.Throws<ArgumentException>(() => mock.Object.GetDateTimeOffset(name));
            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // with number
            //
            mock = new Mock<IDataRecord>();
            expect = DateTimeOffset.Now;
            mock.Setup(service => service.GetString(number)).Returns("NODATETIMEOFFSET");
            Assert.Throws<ArgumentException>(() => mock.Object.GetDateTimeOffset(number));
            mock.Verify(service => service.GetString(number), Times.Once());

            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetString(number)).Returns<string>(null);
            Assert.Throws<ArgumentException>(() => mock.Object.GetDateTimeOffset(number));
            mock.Verify(service => service.GetString(number), Times.Once());
        }
        #endregion

        #region Testing GetNullableDateTimeOffset
        [Fact]
        public void GetNullableDateTimeOffsetTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;
            DateTimeOffset expect;

            //
            // Behave like GetDateTime
            //
            mock = new Mock<IDataRecord>();
            expect = DateTimeOffset.Now;
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns(expect.ToString("o"));

            Assert.Equal(expect, mock.Object.GetNullableDateTimeOffset(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with number
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetString(number)).Returns<string>(null);
            Assert.Null(mock.Object.GetNullableDateTimeOffset(number));
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with name
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns<string>(null);

            Assert.Null(mock.Object.GetNullableDateTimeOffset(name));

            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }

        [Fact]
        public void GetNullableDateTimeOffsetFailedTest()
        {
            Mock<IDataRecord> mock;
            var name = "fieldName";
            var number = 2;

            //
            // Nullable with number
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetString(number)).Returns("NODATETIMEFORMAT");
            Assert.Throws<ArgumentException>(() => mock.Object.GetNullableDateTimeOffset(number));
            mock.Verify(service => service.GetString(number), Times.Once());

            //
            // Nullable with name
            //
            mock = new Mock<IDataRecord>();
            mock.Setup(service => service.GetOrdinal(name)).Returns(number);
            mock.Setup(service => service.GetString(number)).Returns("NODATETIMEFORMAT");
            Assert.Throws<ArgumentException>(() => mock.Object.GetNullableDateTimeOffset(name));
            mock.Verify(service => service.GetOrdinal(name), Times.Once());
            mock.Verify(service => service.GetString(number), Times.Once());
        }
        #endregion

    }
}
