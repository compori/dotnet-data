using Compori.Data;
using Compori.Data.Sqlite;
using System;
using System.IO;

namespace ComporiTesting.Data.Sqlite
{
    public class DatabaseFixture : IDisposable
    {
        /// <summary>
        /// Gets the file name of database.
        /// </summary>
        /// <value>The file.</value>
        public string File { get; private set; }

        /// <summary>
        /// Gets the folder name of database.
        /// </summary>
        /// <value>The folder.</value>
        public string Folder { get; private set; }

        /// <summary>
        /// Gets the path of database.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the connection factory.
        /// </summary>
        /// <value>The factory.</value>
        public IConnectionFactory Factory { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseFixture"/> class.
        /// </summary>
        public DatabaseFixture()
        {
            this.Folder = System.IO.Path.GetTempPath();
            this.File = Guid.NewGuid().ToString("N") + ".db";
            this.Path = System.IO.Path.Combine(this.Folder, this.File);

            //
            // Prepare sample database
            //
            this.Factory = new ConnectionFactory().Configure(this.File, this.Folder);

            using (var connection = this.Factory.Create())
            {
                this.CreateSampleSchema(connection);
                this.CreateSampleData(connection);
            }
        }

        /// <summary>
        /// Creates the sample data.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public void CreateSampleData(IConnection connection)
        {
            connection
                .CreateCommand()
                .WithQuery(this.GetEmbeddedResourceText("SqliteSample.txt"))
                .Execute();
        }

        /// <summary>
        /// Creates the schema of the testing database.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public void CreateSmallSampleData(IConnection connection)
        {
            connection
                .CreateCommand()
                .WithQuery(this.GetEmbeddedResourceText("SqliteSampleSmall.txt"))
                .Execute();
        }

        /// <summary>
        /// Creates the schema of the testing database.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public void CreateSampleSchema(IConnection connection)
        {
            connection
                .CreateCommand()
                .WithQuery(this.GetEmbeddedResourceText("SqliteSampleSchema.txt"))
                .Execute();
        }

        /// <summary>
        /// Gets an embedded resource text.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        public string GetEmbeddedResourceText(string name)
        {
            // var names = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var source = "ComporiTesting.Data.Sqlite.Resources." + name;
            using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(source))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        #region IDisposable Support

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        System.IO.File.Delete(this.Path);
                    }
                    catch (Exception)
                    {
                    }
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
