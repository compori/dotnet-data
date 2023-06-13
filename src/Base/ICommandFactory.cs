using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Interface ICommandFactory
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Creates the a new command
        /// </summary>
        /// <returns>ICommand.</returns>
        ICommand CreateCommand();

        /// <summary>
        /// Creates the command and setzs its execution timeout in seconds.
        /// </summary>
        /// <param name="timeout">The timeout in seconds.</param>
        /// <returns>ICommand.</returns>
        ICommand CreateCommand(int timeout);
    }
}
