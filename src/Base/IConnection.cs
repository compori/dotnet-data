using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Interface IConnection
    /// </summary>
    public interface IConnection : IDisposalState, ICommandFactory
    {
        /// <summary>
        /// Gets the underlying connection interface.
        /// </summary>
        /// <value>The connection.</value>
        IDbConnection Connection { get; }

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <returns>ITransaction.</returns>
        ITransaction BeginTransaction();

        /// <summary>
        /// Begins the transaction with a given isolation level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>ITransaction.</returns>
        ITransaction BeginTransaction(IsolationLevel level);

        /// <summary>
        /// Closes the underlying connection instance.
        /// </summary>
        void Close();

    }
}
