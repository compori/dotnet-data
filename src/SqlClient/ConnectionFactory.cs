using Compori.StringExtensions;
using System;
using System.Data.SqlClient;
#if !NET35
using System.Security;
#endif

namespace Compori.Data.SqlClient
{
    /// <summary>
    /// Class ConnectionFactory.
    /// </summary>
    public class ConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// The connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Gets the hydrator factory.
        /// </summary>
        /// <value>The hydrator factory.</value>
        public IHydratorFactory HydratorFactory { get; private set; }

#if !NET35
        /// <summary>
        /// The SQL credential
        /// </summary>
        private SqlCredential _sqlCredential;
#endif
        /// <summary>
        /// Configures the instance.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="hydratorFactory">The hydrator factory.</param>
        public void Configure(string connectionString, HydratorFactory hydratorFactory = null)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(connectionString, nameof(connectionString));
            this.ConnectionString = connectionString;
#if !NET35
            this._sqlCredential = null;
#endif
            this.HydratorFactory = hydratorFactory ?? new HydratorFactory();
        }

        /// <summary>
        /// Configures the instance.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="hydratorFactory">The hydrator factory.</param>
        public void Configure(string connectionString, string user, string password, HydratorFactory hydratorFactory = null)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(user, nameof(user));
            Guard.AssertArgumentIsNotNull(password, nameof(password));
            this.Configure(connectionString, hydratorFactory);
#if NET35
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                UserID = user,
                Password = password
            };
            this.ConnectionString = builder.ToString();
#else
            var securePassword = new SecureString();
            foreach (var character in password.ToCharArray())
            {
                securePassword.AppendChar(character);
            }
            securePassword.MakeReadOnly();
            this._sqlCredential = new SqlCredential(user, securePassword);
#endif
        }

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns>IConnection.</returns>
        protected IConnection Create()
        {
            if (this.ConnectionString.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException("Connection string is not set.");
            }
#if !NET35
            if (this._sqlCredential != null)
            {
                return new Connection(
                    new SqlConnection(this.ConnectionString, this._sqlCredential), 
                    new ParameterFactory(), 
                    this.HydratorFactory ?? new HydratorFactory());
            }
#endif
            return new Connection(
                new SqlConnection(this.ConnectionString), 
                new ParameterFactory(), 
                this.HydratorFactory ?? new HydratorFactory());
        }

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns>IConnection.</returns>
        IConnection IConnectionFactory.Create()
        {
            return this.Create();
        }
    }
}
