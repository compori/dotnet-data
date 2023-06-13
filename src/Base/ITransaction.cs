using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Interface ITransaction
    /// </summary>
    /// <seealso cref="ICommandFactory" />
    public interface ITransaction : IDisposalState, ICommandFactory
    {
        /// <summary>
        /// Gets the underlying transaction interface.
        /// </summary>
        /// <value>The transaction.</value>
        IDbTransaction Transaction { get; }

        /// <summary>
        /// Commits this transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks this transaction.
        /// </summary>
        void Rollback();
    }
}
