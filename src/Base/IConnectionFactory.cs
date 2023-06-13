namespace Compori.Data
{
    /// <summary>
    /// Interface to a connection factory intance.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns>IConnection.</returns>
        IConnection Create();
    }
}
