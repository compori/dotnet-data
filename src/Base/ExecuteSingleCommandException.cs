namespace Compori.Data
{
    public class ExecuteSingleCommandException : ExecuteCommandException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteSingleCommandException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ExecuteSingleCommandException(string message) : base(message) { }
    }
}
