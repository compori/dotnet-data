using System.Reflection;

namespace Compori.Data.SqlClient
{
    public class HydatorMapping : Data.HydatorMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydatorMapping"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        public HydatorMapping(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }
    }
}
