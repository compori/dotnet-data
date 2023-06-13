namespace Compori.Data
{
    public class ExecuteCommandException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteCommandException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ExecuteCommandException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteCommandException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ExecuteCommandException(string message, System.Exception innerException) : base(message, innerException) {}
    }
}
