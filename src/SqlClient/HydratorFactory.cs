namespace Compori.Data.SqlClient
{
    public class HydratorFactory : IHydratorFactory
    {
        /// <summary>
        /// Creates a new instance of an hydrator for a type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IHydrator&lt;T&gt;.</returns>
        public IHydrator<T> Create<T>() where T : class
        {
            return new Hydrator<T>();
        }
    }
}
