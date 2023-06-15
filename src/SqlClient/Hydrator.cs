using System.Reflection;

namespace Compori.Data.SqlClient
{
    public class Hydrator<T> : Data.Hydrator<T> where T : class
    {
        /// <summary>
        /// Return the hydrator mapping for a property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>Compori.Data.HydatorMapping.</returns>
        protected override Data.HydatorMapping OnCreateMapping(PropertyInfo propertyInfo)
        {
            return new HydatorMapping(propertyInfo);
        }
    }
}
