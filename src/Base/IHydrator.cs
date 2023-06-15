using System;
using System.Data;
using System.Linq.Expressions;

namespace Compori.Data
{
    public interface IHydrator<T>
    {
        /// <summary>
        /// Creates a in memory object where data will be hydrated into.
        /// </summary>
        /// <returns>T.</returns>
        T Create();

        /// <summary>
        /// Hydrates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="record">The record.</param>
        /// <returns>T.</returns>
        T Hydrate(T obj, IDataRecord record);

        /// <summary>
        /// Hydrates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>T.</returns>
        T Hydrate(IDataRecord record);
    }
}
