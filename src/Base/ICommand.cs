using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Interface defines a command that is parameterizable and executable
    /// </summary>
    public interface ICommand : IParameterizableCommand, IExecutableCommand, IQueryCapableCommand, IDisposalState
    {
        /// <summary>
        /// Gets the underlying command interface.
        /// </summary>
        /// <value>The command.</value>
        IDbCommand Command { get; }
    }
}
