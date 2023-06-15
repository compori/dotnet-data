using Compori.Data;
using Compori.Data.Extensions;
using System;
using System.Linq;
using Xunit;

namespace ComporiTesting.Data.Sqlite.UserStory
{
    [Trait("Provider", "Sqlite"), Collection("Database collection")]
    public class ReadWithoutHydrateTest
    {
        /// <summary>
        /// The fixture
        /// </summary>
        protected readonly DatabaseFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadWithoutHydrateTest"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public ReadWithoutHydrateTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        public class Employee
        {
            public long EmployeeId { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Title { get; set; }
            public long ReportsTo { get; set; }
            public DateTime BirthDate { get; set; }
            
            [HydratorMapping(FieldName = "Email")]
            public string BusinessEmail { get; set; }
        }

        /*
         5,'Johnson','Steve','Sales Support Agent',2,'1965-03-03 00:00:00','2003-10-17 00:00:00','7727B 41 Ave','Calgary','AB','Canada','T3B 1Y7','1 (780) 836-9987','1 (780) 836-9543','steve@chinookcorp.com'
`EmployeeId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`LastName`	NVARCHAR ( 20 ) NOT NULL,
	`FirstName`	NVARCHAR ( 20 ) NOT NULL,
	`Title`	NVARCHAR ( 30 ),
	`ReportsTo`	INTEGER,
	`BirthDate`	DATETIME,         
         */
        [Fact()]
        public void TestReadJohnsonEmployee()
        {
            using (var connection = this.fixture.Factory.Create())
            {
                var result = connection
                    .CreateCommand()
                    .WithQuery("SELECT EmployeeId, LastName, FirstName, Title, ReportsTo, BirthDate, Email FROM employees WHERE EmployeeId=@EmployeeId")
                    .WithParameter("@EmployeeId", 5)
                    .ReadFirstOrDefault<Employee>();

                Assert.Equal(5, result.EmployeeId);
                Assert.Equal("Steve", result.FirstName);
                Assert.Equal("Johnson", result.LastName);
                Assert.Equal("Sales Support Agent", result.Title);
                Assert.Equal("steve@chinookcorp.com", result.BusinessEmail);                
                Assert.Equal(2, result.ReportsTo);
                Assert.Equal(new DateTime(1965, 3, 3).Date, result.BirthDate.Date);
            }
        }

        [Fact()]
        public void TestReadEmployees()
        {
            using (var connection = this.fixture.Factory.Create())
            {
                var result = connection
                    .CreateCommand()
                    .WithQuery("SELECT EmployeeId, LastName, FirstName, Title, ReportsTo, BirthDate FROM employees")
                    .Read<Employee>();

                Assert.NotEmpty(result);
            }
        }

        [Fact()]
        public void TestReadEmployeesFailure()
        {
            using (var connection = this.fixture.Factory.Create())
            {
                var result = connection
                    .CreateCommand()
                    .WithQuery("SELECT EmployeeId, LastName, FirstName, Title, ReportsTo, BirthDate FROM employees")
                    .Read<Customer>();
                // Result is not empty but its record
                Assert.NotEmpty(result);
                foreach(var value in result)
                {
                    Assert.Equal(0, value.CustomerId);
                    Assert.Null(value.Phone);
                }
            }
        }


        public class Customer
        {
            public long CustomerId { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }
        }

        [Fact()]
        public void TestReadHansenCustomer()
        {
            using (var connection = this.fixture.Factory.Create())
            {
                var result = connection
                    .CreateCommand()
                    .WithQuery("SELECT CustomerId, LastName, FirstName, Phone, Fax, Email FROM customers WHERE CustomerId=@CustomerId")
                    .WithParameter("@CustomerId", 4)
                    .ReadFirstOrDefault<Customer>();

                Assert.Equal(4, result.CustomerId);
                Assert.Equal("Bjørn", result.FirstName);
                Assert.Equal("Hansen", result.LastName);
                Assert.Equal("+47 22 44 22 22", result.Phone);
                Assert.Null(result.Fax);
                Assert.Equal("bjorn.hansen@yahoo.no", result.Email);
            }
        }

        [Fact()]
        public void TestReadCustomers()
        {
            using (var connection = this.fixture.Factory.Create())
            {
                var result = connection
                    .CreateCommand()
                    .WithQuery("SELECT CustomerId, LastName, FirstName, Phone, Fax, Email FROM customers")
                    .Read<Customer>();

                Assert.NotEmpty(result);
            }
        }
    }
}