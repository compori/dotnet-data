using System;
using System.Collections.Generic;
using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Class Transaction.
    /// </summary>
    /// <seealso cref="ITransaction" />
    public class Transaction : ITransaction
    {
        /// <summary>
        /// The transaction
        /// </summary>
        protected IDbTransaction transaction;

        /// <summary>
        /// The parameter factory
        /// </summary>
        protected IParameterFactory parameterFactory;

        /// <summary>
        /// The hydrator factory
        /// </summary>
        protected IHydratorFactory hydratorFactory;

        /// <summary>
        /// A list of created commands.
        /// </summary>
        private readonly List<ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction" /> class.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="parameterFactory">The parameter factory.</param>
        /// <param name="hydratorFactory">The hydrator factory.</param>
        public Transaction(IDbTransaction transaction, IParameterFactory parameterFactory, IHydratorFactory hydratorFactory)
        {
            this.transaction = transaction;
            this.parameterFactory = parameterFactory;
            this.commands = new List<ICommand>();
            this.hydratorFactory = hydratorFactory;
        }

        /// <summary>
        /// Creates the command and setzs its execution timeout in seconds.
        /// </summary>
        /// <param name="timeout">The timeout in seconds.</param>
        /// <returns>ICommand.</returns>
        protected ICommand CreateCommand(int timeout)
        {
            Guard.AssertObjectIsNotDisposed(this);

            var internalCommand = this.transaction.Connection.CreateCommand();
            internalCommand.CommandTimeout = timeout;
            internalCommand.Transaction = this.transaction;

            var command = new Command(internalCommand, this.parameterFactory, this.hydratorFactory);
            this.commands.Add(command);
            return command;
        }

        #region ITransaction Implementation

        /// <summary>
        /// Gets the underlying transaction interface.
        /// </summary>
        /// <value>The transaction.</value>
        IDbTransaction ITransaction.Transaction => this.transaction;

        /// <summary>
        /// Commits this transaction.
        /// </summary>
        void ITransaction.Commit()
        {
            Guard.AssertObjectIsNotDisposed(this);
            this.transaction.Commit();
        }

        /// <summary>
        /// Rollbacks this transaction.
        /// </summary>
        void ITransaction.Rollback()
        {
            Guard.AssertObjectIsNotDisposed(this);
            this.transaction.Rollback();
        }

        #endregion

        #region ICommandFactory Implementation

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

        #region IDisposable Implementation

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value><c>true</c> if this instance is disposed; otherwise, <c>false</c>.</value>        
        public bool IsDisposed => this.disposedValue;

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
                    if (this.transaction != null)
                    {
                        this.transaction.Dispose();
                        this.transaction = null;
                    }
                    foreach (var command in this.commands)
                    {
                        command.Dispose();
                    }
                    this.commands.Clear();
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        #endregion
    }
}
