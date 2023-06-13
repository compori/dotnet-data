using System;
using System.Collections.Generic;
using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Class Connection.
    /// </summary>
    /// <seealso cref="IConnection" />
    public class Connection : IConnection
    {
        /// <summary>
        /// The connection
        /// </summary>
        protected IDbConnection connection;

        /// <summary>
        /// The parameter factory
        /// </summary>
        protected IParameterFactory parameterFactory;

        /// <summary>
        /// A list of created transactions.
        /// </summary>
        private List<ITransaction> transactions;

        /// <summary>
        /// A list of created commands.
        /// </summary>
        private List<ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="parameterFactory">The parameter factory.</param>
        public Connection(IDbConnection connection, IParameterFactory parameterFactory)
        {
            this.connection = connection;
            this.parameterFactory = parameterFactory;
            this.commands = new List<ICommand>();
            this.transactions = new List<ITransaction>();
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        IDbConnection IConnection.Connection => this.connection;

        /// <summary>
        /// Ensures the connection is open.
        /// </summary>
        protected void EnsureConnectionIsOpened()
        {
            Guard.AssertObjectIsNotDisposed(this);

            // connection already open
            if (this.connection.State == ConnectionState.Open)
            {
                return;
            }

            // open connection
            this.connection.Open();
            if (this.connection.State != ConnectionState.Open)
            {
                throw new ConnectionException("Connection could not be opened.");
            }
        }

        /// <summary>
        /// Creates the command and setzs its execution timeout in seconds.
        /// </summary>
        /// <param name="timeout">The timeout in seconds.</param>
        /// <returns>ICommand.</returns>
        protected ICommand CreateCommand(int timeout)
        {
            Guard.AssertObjectIsNotDisposed(this);

            this.EnsureConnectionIsOpened();
            
            var internalCommand = this.connection.CreateCommand();
            internalCommand.CommandTimeout = timeout;
            
            var command = new Command(internalCommand, this.parameterFactory);
            this.commands.Add(command);
            
            return command;
        }

        /// <summary>
        /// Begins the transaction with a given isolation level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>ITransaction.</returns>
        protected ITransaction BeginTransaction(IsolationLevel level)
        {
            Guard.AssertObjectIsNotDisposed(this);

            this.EnsureConnectionIsOpened();
            var transaction = new Transaction(this.connection.BeginTransaction(level), this.parameterFactory);
            this.transactions.Add(transaction);
            return transaction;
        }

        /// <summary>
        /// Begins the transaction with providers default isolation level.
        /// </summary>
        /// <returns>ITransaction.</returns>
        protected ITransaction BeginTransaction()
        {
            Guard.AssertObjectIsNotDisposed(this);

            this.EnsureConnectionIsOpened();
            var transaction = new Transaction(this.connection.BeginTransaction(), this.parameterFactory);
            this.transactions.Add(transaction);
            return transaction;
        }

        /// <summary>
        /// Closes the underlying connection instance.
        /// </summary>
        protected void Close()
        {
            if(this.connection == null)
            {
                return;
            }

            this.connection.Close();
        }

        #region IConnection Implementation

        /// <summary>
        /// Closes the underlying connection instance.
        /// </summary>
        void IConnection.Close()
        {
            this.Close();
        }

        /// <summary>
        /// Creates the a new command
        /// </summary>
        /// <returns>ICommand.</returns>
        ICommand ICommandFactory.CreateCommand()
        {
            return this.CreateCommand(30);
        }

        /// <summary>
        /// Creates the command and setzs its execution timeout in seconds.
        /// </summary>
        /// <param name="timeout">The timeout in seconds.</param>
        /// <returns>ICommand.</returns>
        ICommand ICommandFactory.CreateCommand(int timeout)
        {
            return this.CreateCommand(timeout);
        }

        #endregion

        #region IConnection Implementation

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <returns>ITransaction.</returns>
        ITransaction IConnection.BeginTransaction()
        {
            return this.BeginTransaction();
        }

        /// <summary>
        /// Begins the transaction with a given isolation level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>ITransaction.</returns>
        ITransaction IConnection.BeginTransaction(IsolationLevel level)
        {
            return this.BeginTransaction(level);
        }

        #endregion

        #region IDisposable Support

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value><c>true</c> if this instance is disposed; otherwise, <c>false</c>.</value>        
        bool IDisposalState.IsDisposed => this.disposedValue;

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c>
        /// to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach(var command in this.commands)
                    {
                        command.Dispose();
                    }
                    this.commands.Clear();
                    foreach (var transaction in this.transactions)
                    {
                        transaction.Dispose();
                    }
                    this.transactions.Clear();

                    if (this.connection != null)
                    {
                        this.connection.Close();
                        this.connection.Dispose();
                        this.connection = null;
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
