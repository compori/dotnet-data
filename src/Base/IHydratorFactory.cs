namespace Compori.Data
{
    public interface IHydratorFactory
    {
        /// <summary>
        /// Creates a new instance of an hydrator for a type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IHydrator&lt;T&gt;.</returns>
        IHydrator<T> Create<T>() where T : class;
    }
}
