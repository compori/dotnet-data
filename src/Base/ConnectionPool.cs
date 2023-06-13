using System;
using System.Collections.Generic;

namespace Compori.Data
{
    public class ConnectionPool
    {
        /// <summary>
        /// Dictionary for connectionfactories
        /// </summary>
        protected Dictionary<string, IConnectionFactory> factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        public ConnectionPool() 
        { 
            this.factories = new Dictionary<string, IConnectionFactory>();
        } 

        /// <summary>
        /// Registers a connection factory with a service key.
        /// </summary>
        /// <param name="serviceKey">The service key</param>
        /// <param name="connectionFactory">The connection factory</param>
        public void Register(string serviceKey, IConnectionFactory connectionFactory)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(serviceKey, nameof(serviceKey)); 
            Guard.AssertArgumentIsNotNull(connectionFactory, nameof(connectionFactory));

            if(factories.ContainsKey(serviceKey))
            {
                this.factories[serviceKey] = connectionFactory;
            }
            else
            {
                this.factories.Add(serviceKey, connectionFactory);
            }
        }

        /// <summary>
        /// Unregister a connection factory.
        /// </summary>
        /// <param name="serviceKey">The service key</param>
        public void Unregister(string serviceKey) 
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(serviceKey, nameof(serviceKey));
            
            if (factories.ContainsKey(serviceKey))
            {
                this.factories.Remove(serviceKey);
            }
        }

        /// <summary>
        /// Creates a new connection from given service key.
        /// </summary>
        /// <param name="serviceKey">The service key</param>
        /// <returns>IConnection.</returns>
        /// <exception cref="Compori.Data.ConnectionException">Unkown connection factory for service key.</exception>
        public IConnection Create(string serviceKey)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(serviceKey, nameof(serviceKey));

            if (!factories.ContainsKey(serviceKey))
            {
                throw new ConnectionPoolException("Unkown connection factory for service key.");
            }
            return this.factories[serviceKey].Create();
        }
    }
}
