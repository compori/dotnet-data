using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Interface defines a parameterizable command
    /// </summary>
    public interface IParameterizableCommand
    {
        /// <summary>
        /// Gets the parameter factory.
        /// </summary>
        /// <value>The parameter factory.</value>
        IParameterFactory ParameterFactory { get; }

        /// <summary>
        /// Adds a value to the end of the ParameterCollection.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>ICommand.</returns>
        ICommand WithParameter(IDataParameter parameter);
    }
}
