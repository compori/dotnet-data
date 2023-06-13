using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Interface IParameterFactory
    /// </summary>
    public interface IParameterFactory
    {
        /// <summary>
        /// Creates the a database data parameter with specified name, type and value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="value">The value.</param>
        /// <returns>IDbDataParameter.</returns>
        IDbDataParameter Create(string name, DbType dbType, object value);
    }
}
