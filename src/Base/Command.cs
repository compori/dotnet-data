using System;
using System.Collections.Generic;
using System.Data;

namespace Compori.Data
{
    /// <summary>
    /// Class Command.
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// The database command object
        /// </summary>
        protected IDbCommand command;

        /// <summary>
        /// The parameter factory
        /// </summary>
        protected IParameterFactory parameterFactory;

        /// <summary>
        /// Prevents a default instance of the <see cref="Command"/> class from being created.
        /// </summary>
        public Command(IDbCommand command, IParameterFactory parameterFactory)
        {
            this.parameterFactory = parameterFactory;
            this.command = command;
        }

        /// <summary>
        /// Sets the Transact-SQL statement, table name or stored procedure to execute at the data source.
        /// </summary>
        /// <param name="query">The Transact-SQL statement or stored procedure to execute.</param>
        /// <returns>ICommand.</returns>
        protected ICommand WithQuery(string query)
        {
            Guard.AssertObjectIsNotDisposed(this);
            Guard.AssertArgumentIsNotNull(query, nameof(query));

            this.command.CommandText = query;
            return this;
        }

        /// <summary>
        /// Adds a value to the end of the ParameterCollection.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>ICommand.</returns>
        protected ICommand WithParameter(IDataParameter parameter)
        {
            Guard.AssertArgumentIsNotNull(parameter, nameof(parameter));
            Guard.AssertObjectIsNotDisposed(this);

            this.command.Parameters.Add(parameter);
            return this;
        }

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected int Execute()
        {
            Guard.AssertObjectIsNotDisposed(this);

            var result = this.command.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <returns>System.Object.</returns>
        protected object ExecuteScalar()
        {
            Guard.AssertObjectIsNotDisposed(this);

            var result = this.command.ExecuteScalar();
            return result;
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first column of the first row in the result set, a null reference if the result set is empty or exception. Returns a maximum of 2033 characters.</returns>
        /// <exception cref="ExecuteCommandException">Null could not be casted to specified type.</exception>
        protected T ExecuteScalar<T>()
        {
            try
            {
                return (T)this.ExecuteScalar();
            }
            catch (InvalidCastException)
            {
                throw;
            }
            catch (NullReferenceException ex)
            {
                throw new ExecuteCommandException("Null could not be casted to specified type.", ex);
            }
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query.
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.
        /// Returns a maximum of 2033 characters.</returns>
        protected T ExecuteScalarOrDefault<T>()
        {
            var result = this.ExecuteScalar();

            // if result is null return types default value.
            if(result == null)
            {
                return default(T);
            }
            try
            {
                // try to cast
                return (T)result;
            }
            catch (InvalidCastException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads the result and creates a <see cref="IEnumerable{T}" /> using <paramref name="hydrate" /> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hydrate">The build function.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        protected IEnumerable<T> Read<T>(Func<IDataReader, T> hydrate)
        {
            Guard.AssertObjectIsNotDisposed(this);

            using (var reader = this.command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return hydrate(reader);
                }
            }
        }

        /// <summary>
        /// Reads the result and creates an object of <typeparamref name="T" /> using <paramref name="hydrate" /> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hydrate">The build.</param>
        /// <returns>Returns a the object T.</returns>
        protected T ReadFirstOrDefault<T>(Func<IDataReader, T> hydrate)
        {
            Guard.AssertObjectIsNotDisposed(this);

            using (var reader = this.command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return hydrate(reader);
                }
            }
            return default(T);
        }

        #region ICommand Implementation

        /// <summary>
        /// Gets the underlying command interface.
        /// </summary>
        /// <value>The command.</value>
        IDbCommand ICommand.Command => this.command;

        #endregion

        #region IQueryCapableCommand Implementation

        /// <summary>
        /// Sets the Transact-SQL statement, table name or stored procedure to execute at the data source.
        /// </summary>
        /// <param name="query">The Transact-SQL statement or stored procedure to execute.</param>
        /// <returns>ICommand.</returns>
        ICommand IQueryCapableCommand.WithQuery(string query)
        {
            return this.WithQuery(query);
        }

        #endregion

        #region IParameterizableCommand Implementation

        /// <summary>
        /// Gets the parameter factory.
        /// </summary>
        /// <value>The parameter factory.</value>
        IParameterFactory IParameterizableCommand.ParameterFactory => this.parameterFactory;

        /// <summary>
        /// Adds a value to the end of the ParameterCollection.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>ICommand.</returns>
        ICommand IParameterizableCommand.WithParameter(IDataParameter parameter)
        {
            return this.WithParameter(parameter);
        }

        #endregion

        #region IExecutableCommand Implementation

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int IExecutableCommand.Execute()
        {
            return this.Execute();
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first column of the first row in the result set, a null reference if the result set is empty or exception.
        /// Returns a maximum of 2033 characters.</returns>
        /// <exception cref="ExecuteCommandException">Null could not be casted to specified type.</exception>
        T IExecutableCommand.ExecuteScalar<T>()
        {
            return this.ExecuteScalar<T>();
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.
        /// Returns a maximum of 2033 characters.</returns>
        T IExecutableCommand.ExecuteScalarOrDefault<T>()
        {
            return this.ExecuteScalarOrDefault<T>();
        }

        /// <summary>
        /// Reads the result and creates a <see cref="IEnumerable{T}" /> using <paramref name="hydrate" /> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hydrate">The build function.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> IExecutableCommand.Read<T>(Func<IDataReader, T> hydrate)
        {
            return this.Read<T>(hydrate);
        }

        /// <summary>
        /// Reads the result and creates an object of <typeparamref name="T" /> using <paramref name="hydrate" /> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hydrate">The build.</param>
        /// <returns>Returns a the object T.</returns>
        T IExecutableCommand.ReadFirstOrDefault<T>(Func<IDataReader, T> hydrate)
        {
            return this.ReadFirstOrDefault(hydrate);
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value><c>true</c> if this instance is disposed; otherwise, <c>false</c>.</value> 
        bool IDisposalState.IsDisposed => this.disposedValue;

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.command != null)
                    {
                        this.command.Dispose();
                        this.command = null;
                        this.parameterFactory = null;
                    }
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
