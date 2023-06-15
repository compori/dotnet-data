using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Compori.Data
{
    public class Hydrator<T> : IHydrator<T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is configured.
        /// </summary>
        /// <value><c>true</c> if this instance is configured; otherwise, <c>false</c>.</value>
        public bool IsConfigured { get; private set; }

        /// <summary>
        /// Gets the type of the instance.
        /// </summary>
        /// <value>The type of the instance.</value>
        public Type InstanceType { get; private set; }

        /// <summary>
        /// Gets the create instance function.
        /// </summary>
        /// <value>The create instance.</value>
        public Func<T> CreateInstance { get; private set; }

        /// <summary>
        /// Returns the mappings.
        /// </summary>
        /// <value>The mappings.</value>
        public ReadOnlyCollection<HydatorMapping> Mappings => this._propertyMapping?.AsReadOnly();

        /// <summary>
        /// The property mapping
        /// </summary>
        private readonly List<HydatorMapping> _propertyMapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hydrator{T}"/> class.
        /// </summary>
        public Hydrator()
        {
            this.InstanceType = typeof(T);
            this.IsConfigured = false;
            this.CreateInstance = null;
            this._propertyMapping = new List<HydatorMapping>();
        }

        /// <summary>
        /// Called when configure instances.
        /// </summary>
        /// <returns>Func&lt;T&gt;.</returns>
        /// <exception cref="HydratorException">Hydrating type must be a class.</exception>
        /// <exception cref="HydratorException">Hydrating type have a must a parameterless public constructor.</exception>
        protected virtual Func<T> OnConfigureInstance()
        {
            if (!this.InstanceType.IsClass || !(this.InstanceType.IsPublic || this.InstanceType.IsNestedPublic))
            {
                throw new HydratorException("Hydrating type must be a class.");
            }
            var constructor = this.InstanceType.GetConstructors().FirstOrDefault(v => v.IsPublic && v.GetParameters().Length == 0);
            if (!constructor.IsPublic)
            {
                throw new HydratorException("Hydrating type have a must a parameterless public constructor.");
            }

            return () => Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Configures the instance.
        /// </summary>
        /// <exception cref="HydratorException">No instance creation function definied.</exception>
        private void ConfigureInstance()
        {
            this.CreateInstance = this.OnConfigureInstance();
            if (this.CreateInstance == null)
            {
                throw new HydratorException("No instance creation function definied.");
            }
        }

        /// <summary>
        /// Return the hydrator mapping for a property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>Compori.Data.HydatorMapping.</returns>
        protected virtual HydatorMapping OnCreateMapping(PropertyInfo propertyInfo)
        {
            return new HydatorMapping(propertyInfo);
        }

        /// <summary>
        /// Called when mapping is configuring.
        /// </summary>
        private void ConfigureMapping()
        {
            // potential properties top write to
            var properties = this.InstanceType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(v => v.CanWrite)
                .ToList();

            this._propertyMapping.Clear();
            foreach (var property in properties)
            {
                this._propertyMapping.Add(this.OnCreateMapping(property));
            }
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        private void Configure()
        {
            if (this.IsConfigured)
            {
                return;
            }

            this.ConfigureInstance();
            this.ConfigureMapping();

            this.IsConfigured = true;
        }

        /// <summary>
        /// Creates a in memory object where data will be hydrated into.
        /// </summary>
        /// <returns>T.</returns>
        public T Create()
        {
            this.Configure();

            return this.CreateInstance();
        }

        /// <summary>
        /// Hydrates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>T.</returns>
        public T Hydrate(IDataRecord record)
        {
            return this.Hydrate(this.Create(), record);
        }

        /// <summary>
        /// Hydrates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="record">The record.</param>
        /// <returns>T.</returns>
        public T Hydrate(T obj, IDataRecord record)
        {
            this.Configure();

            foreach (var mapping in this._propertyMapping)
            {
                mapping.Hydrate(record, obj);
            }
            
            return obj;
        }
    }
}
