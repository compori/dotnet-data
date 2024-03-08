using System;
using System.Collections.Generic;
using System.Data;

namespace Compori.Data.Extensions
{
    /// <summary>
    /// Class ICommandExtension.
    /// </summary>
    public static class ICommandExtension
    {
        /// <summary>
        /// Executes a Transact-SQL statement against the connection and assume the number of rows affected to be 1
        /// otherwise will throw a <see cref="ExecuteSingleCommandException" /> with the message.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="ExecuteSingleCommandException"></exception>
        public static void ExecuteSingle(this ICommand command, string message)
        {
            if (command.Execute() != 1)
            {
                throw new ExecuteSingleCommandException(message);
            }
        }

        /// <summary>
        /// Reads the result and creates a <see cref="IList{T}"/> using <paramref name="hydrate"/> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public static IList<T> ReadList<T>(this ICommand command) where T : class
        {
            return new List<T>(command.Read<T>());
        }

        /// <summary>
        /// Reads the result and creates a <see cref="IList{T}"/> using <paramref name="hydrate"/> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <param name="hydrate">The hydrate.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public static IList<T> ReadList<T>(this ICommand command, Func<IDataRecord, T> hydrate)
        {
            return new List<T>(command.Read(hydrate));
        }        
    }
}
