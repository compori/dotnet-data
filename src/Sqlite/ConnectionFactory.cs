using Microsoft.Data.Sqlite;
using System.IO;

namespace Compori.Data.Sqlite
{
    /// <summary>
    /// Class ConnectionFactory.
    /// </summary>
    public class ConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// Initializes static members of the <see cref="ConnectionFactory"/> class.
        /// </summary>
        static ConnectionFactory()
        {
            SQLitePCL.Batteries_V2.Init();
        }

        /// <summary>
        /// The connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Gets the Hydratorfactory.
        /// </summary>
        /// <value>The hydrator factory.</value>
        public IHydratorFactory HydratorFactory { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory"/> class and setup an in memory database.
        /// </summary>
        public ConnectionFactory() 
        {
            this.Configure(false);
        }

        /// <summary>
        /// Configures the instance of the <see cref="ConnectionFactory" /> class and setup an in memory database and defines, if this database is sharable in current process.
        /// </summary>
        /// <param name="shareInProcess">if set to <c>true</c> [share in process].</param>
        /// <param name="hydratorFactory">The hydrator factory.</param>
        /// <returns>IConnectionFactory.</returns>
        public IConnectionFactory Configure(bool shareInProcess, HydratorFactory hydratorFactory = null)
        {
            return this.Configure(
                new SqliteConnectionStringBuilder()
                {
                    Mode = SqliteOpenMode.Memory,
                    DataSource = ":memory:",
                    Cache = shareInProcess ? SqliteCacheMode.Shared : SqliteCacheMode.Private
                }.ToString(), 
                hydratorFactory
            );
        }

        /// <summary>
        /// Configures the instance of the <see cref="ConnectionFactory" /> class class with a folder and file name.
        /// If database not exists during creating a connection, it will be created.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="pooling">Whether pool connections on the database or not. The may leed to file locks even no connection is used.</param>
        /// <param name="hydratorFactory">The hydrator factory.</param>
        /// <returns>IConnectionFactory.</returns>
        public IConnectionFactory Configure(string file, string folder, bool pooling = true, HydratorFactory hydratorFactory = null)
        {
            return this.Configure(file, folder, SqliteOpenMode.ReadWriteCreate, pooling, hydratorFactory); 
        }

        /// <summary>
        /// Configures the instance of the <see cref="ConnectionFactory" /> class with a folder and file name.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="mode">The open mode for sqlite database file.</param>
        /// <param name="pooling">Whether pool connections on the database or not. The may leed to file locks even no connection is used.</param>
        /// <param name="hydratorFactory">The hydrator factory.</param>
        /// <returns>IConnectionFactory.</returns>
        public IConnectionFactory Configure(string file, string folder, SqliteOpenMode mode, bool pooling = true, HydratorFactory hydratorFactory = null) 
        {
            return this.Configure(
                new SqliteConnectionStringBuilder()
                {
                    DataSource = Path.Combine(folder, file),
                    Mode = mode,
                    Pooling = pooling
                }.ToString(), hydratorFactory);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory" /> class with a specific connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="hydratorFactory">The hydrator factory.</param>
        /// <returns>IConnectionFactory.</returns>
        public IConnectionFactory Configure(string connectionString, HydratorFactory hydratorFactory = null)
        {
            this.ConnectionString = connectionString;
            this.HydratorFactory = hydratorFactory ?? new HydratorFactory();
            return this;
        }

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns>IConnection.</returns>
        IConnection IConnectionFactory.Create()
        {
            var sqliteConnection = new SqliteConnection(this.ConnectionString);
            return new Connection(sqliteConnection, new ParameterFactory(), this.HydratorFactory ?? new HydratorFactory());
        }
    }
}
