namespace Compori.Data
{
    public class ConnectionException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ConnectionException(string message) : base(message) { }
    }
}
