using System;

namespace Compori.Data
{
    /// <summary>
    /// Interface that supports a query capable command
    /// </summary>
    public interface IQueryCapableCommand
    {

        /// <summary>
        /// Sets the Transact-SQL statement, table name or stored procedure to execute at the data source.
        /// </summary>
        /// <param name="query">The Transact-SQL statement or stored procedure to execute.</param>
        /// <returns>ICommand.</returns>
        ICommand WithQuery(string query);
    }
}
