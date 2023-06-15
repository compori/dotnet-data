using System;
using System.Data;
using System.Reflection;

namespace Compori.Data
{
    public class HydatorMapping
    {
        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <value>The property information.</value>
        public PropertyInfo PropertyInfo { get; private set; }

        /// <summary>
        /// Gets the property typ.
        /// </summary>
        /// <value>The property typ.</value>
        public Type PropertyType { get; private set; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; private set; }

        /// <summary>
        /// Gets the ordinal.
        /// </summary>
        /// <value>The ordinal.</value>
        public int Ordinal { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the field exists in record set.
        /// </summary>
        /// <value><c>true</c> if field exists; otherwise, <c>false</c>.</value>
        public bool? FieldExists { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance can be null.
        /// </summary>
        /// <value><c>true</c> if this instance can be null; otherwise, <c>false</c>.</value>
        public bool CanBeNull { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether ignore null value.
        /// </summary>
        /// <value><c>true</c> if ignore null value; otherwise, <c>false</c>.</value>
        public bool IgnoreNullValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ignore not existing field.
        /// </summary>
        /// <value><c>true</c> if ignore not existing field; otherwise, <c>false</c>.</value>
        public bool IgnoreNotExistingField { get; set; }

        /// <summary>
        /// Gets or sets the read function.
        /// </summary>
        /// <value>The read function.</value>
        protected Func<IDataRecord, int, object> ReadFunc { get; set; }

        /// <summary>
        /// Gets or sets the Converting function.
        /// </summary>
        /// <value>The Converting function.</value>
        protected Func<object, object> ConvertFunc { get; set; }

        /// <summary>
        /// Gets or sets the write function.
        /// </summary>
        /// <value>The write function.</value>
        protected Action<object, object> WriteFunc { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HydatorMapping"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        public HydatorMapping(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            this.PropertyName = propertyInfo.Name;

            var attributeValue = HydratorMappingAttribute.GetAttribute(propertyInfo);
            if (attributeValue != null)
            {
                this.FieldName = attributeValue.FieldName ?? propertyInfo.Name;
                this.IgnoreNullValue = attributeValue.IgnoreNullValue;
                this.IgnoreNotExistingField = attributeValue.IgnoreNotExistingField;
            }
            else
            {
                this.FieldName = propertyInfo.Name;
                this.IgnoreNullValue = true;
                this.IgnoreNotExistingField = true;
            }

            this.Ordinal = -1;
            this.FieldExists = null;

            var type = this.PropertyInfo.PropertyType;
            var underlyingType = Nullable.GetUnderlyingType(type);

            this.CanBeNull = !type.IsValueType || (underlyingType != null);
            this.PropertyType = underlyingType ?? type;

            this.ReadFunc = null;
            this.ConvertFunc = null;
            this.WriteFunc = null;
        }

        /// <summary>
        /// Gets the ordinal number for the data record field.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>System.Int32.</returns>
        public int GetOrdinal(IDataRecord record)
        {
            if (this.Ordinal >= 0)
            {
                return this.Ordinal;
            }
            try
            {
                this.Ordinal = record.GetOrdinal(this.FieldName);
                return this.Ordinal;
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }

        /// <summary>
        /// Called when read function is not set und must be created.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns>System.Object.</returns>
        protected virtual void OnCreateReadFunc(IDataRecord record, int ordinal)
        {
            this.ReadFunc = (r, o) => r.GetValue(o);
        }

        /// <summary>
        /// Called when <see cref="Read(IDataRecord, out bool)"/> is called.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns>System.Object.</returns>
        protected virtual object OnRead(IDataRecord record, int ordinal)
        {
            if (this.ReadFunc == null)
            {
                this.OnCreateReadFunc(record, ordinal);
                if (this.ReadFunc == null)
                {
                    throw new HydratorException($"No read function for hydrating property {this.PropertyName} for field {this.FieldName} is set.");
                }
            }
            return this.ReadFunc(record, ordinal);
        }

        /// <summary>
        /// Reads the value from record for a property.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="ignore">if set to <c>true</c> the return value should be ignored.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="HydratorException">Result field {this.FieldName} does not exists.</exception>
        /// <exception cref="HydratorException">Hydrating property {this.PropertyName} for field {this.FieldName} can not be null.</exception>
        public object Read(IDataRecord record, out bool ignore)
        {
            Guard.AssertArgumentIsNotNull(record, nameof(record));

            // Field exists?
            var ordinal = !this.FieldExists.HasValue || this.FieldExists.Value
                ? this.GetOrdinal(record)
                : -1;
            ignore = false;
            if (ordinal < 0)
            {
                this.FieldExists = false;
                if (this.IgnoreNotExistingField)
                {
                    ignore = true;
                    return null;
                }
                throw new HydratorException($"Result field {this.FieldName} does not exists.");
            }
            this.FieldExists = true;

            // Field value is null?
            var isNull = record.IsDBNull(ordinal);
            if (isNull)
            {
                if (this.IgnoreNullValue)
                {
                    ignore = true;
                    return null;
                }
                if (!this.CanBeNull)
                {
                    throw new HydratorException($"Hydrating property {this.PropertyName} for field {this.FieldName} can not be null.");
                }
                return null;
            }

            return this.OnRead(record, ordinal);
        }

        /// <summary>
        /// Called when write function is not set und must be created.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Object.</returns>
        protected virtual void OnCreateWriteFunc(object data, object value)
        {
            this.WriteFunc = (o, v) => this.PropertyInfo.SetValue(o, v, null);
        }

        /// <summary>
        /// Sets the value for a property in a object. Can be overriden in order to adjust behaviour.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="HydratorException">No write function for hydrating property {this.PropertyName} for field {this.FieldName} is set.</exception>
        protected virtual void OnWrite(object data, object value)
        {
            if (this.WriteFunc == null)
            {
                this.OnCreateWriteFunc(data, value);
                if (this.WriteFunc == null)
                {
                    throw new HydratorException($"No write function for hydrating property {this.PropertyName} for field {this.FieldName} is set.");
                }
            }
            this.WriteFunc(data, value);
        }

        /// <summary>
        /// Sets the value for a property in a object
        /// </summary>
        /// <param name="data">The data object.</param>
        /// <param name="value">The value.</param>
        public void Write(object data, object value)
        {
            this.OnWrite(data, value);
        }

        /// <summary>
        /// Called when convert function is not set und must be created.
        /// Can be overriden in order to adjust behaviour.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="data">The data.</param>
        /// <param name="value">The value.</param>
        protected virtual void OnCreateConvertFunc(IDataRecord record, object value)
        {
            this.ConvertFunc = v => v;
        }

        /// <summary>
        /// Converts the value for a property.
        /// Can be overriden in order to adjust behaviour.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="HydratorException">No convert function for hydrating property {this.PropertyName} for field {this.FieldName} is set.</exception>
        protected virtual object OnConvert(IDataRecord record, object value)
        {
            if (this.ConvertFunc == null)
            {
                this.OnCreateConvertFunc(record, value);
                if (this.ConvertFunc == null)
                {
                    throw new HydratorException($"No convert function for hydrating property {this.PropertyName} for field {this.FieldName} is set.");
                }
            }
            return this.ConvertFunc(value);
        }

        /// <summary>
        /// Sets the value for a property in a object
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Object.</returns>
        public object Convert(IDataRecord record, object value)
        {
            return this.OnConvert(record, value);
        }

        /// <summary>
        /// Hydrates the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="obj">The object.</param>
        public void Hydrate(IDataRecord record, object obj)
        {
            // read
            var value = this.Read(record, out var ignore);
            if (ignore)
            {
                return;
            }

            // convert
            value = this.Convert(record, value);

            // write
            this.Write(obj, value);
        }
    }
}
