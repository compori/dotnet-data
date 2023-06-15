using System;
using System.Collections.Generic;
using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Interface define an executable command.
    /// </summary>
    public interface IExecutableCommand
    {
        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int Execute();

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first column of the first row in the result set, a null reference if the result set is empty or exception. Returns a maximum of 2033 characters. </returns>
        T ExecuteScalar<T>();

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty. Returns a maximum of 2033 characters. </returns>
        T ExecuteScalarOrDefault<T>();

        /// <summary>
        /// Reads the result and creates a <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> Read<T>() where T : class;

        /// <summary>
        /// Reads result by using a specified hydrator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hydrator">The hydrator.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> Read<T>(IHydrator<T> hydrator);

        /// <summary>
        /// Reads the result and creates a <see cref="IEnumerable{T}"/> using <paramref name="hydrate"/> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hydrate">The hydration function.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> Read<T>(Func<IDataRecord, T> hydrate);

        /// <summary>
        /// Reads the result and creates an object of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        T ReadFirstOrDefault<T>() where T : class;

        /// <summary>
        /// Reads result by using a specified hydrator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hydrator">The hydrator.</param>
        /// <returns>Returns a the object T.</returns>
        T ReadFirstOrDefault<T>(IHydrator<T> hydrator);

        /// <summary>
        /// Reads the result and creates an object of <typeparamref name="T"/> using <paramref name="hydrate"/> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hydrate">The hydration function.</param>
        /// <returns>Returns a the object T.</returns>
        T ReadFirstOrDefault<T>(Func<IDataRecord, T> hydrate);
    }
}
