namespace Compori.Data
{
    public class ConnectionPoolException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionPoolException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ConnectionPoolException(string message) : base(message) { }  
    }
}
