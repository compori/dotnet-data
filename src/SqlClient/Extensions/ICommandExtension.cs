namespace Compori.Data.SqlClient.Extensions
{
    /// <summary>
    /// Class ICommandExtension.
    /// </summary>
    public static class ICommandExtension
    {
        /// <summary>
        /// Select the first field value in a table.
        /// 
        /// Notice: The table and field name as well the where condition will not be escaped.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <param name="table">The table.</param>
        /// <param name="field">The field.</param>
        /// <param name="where">The where.</param>
        /// <returns>T.</returns>
        public static T SelectOne<T>(this ICommand command, string table, string field, string where)
        {
            return command
                .WithQuery($"SELECT TOP 1 {field} FROM {table} WHERE {where}")
                .ExecuteScalar<T>();
        }

        /// <summary>
        /// Gets the table value.
        /// 
        /// Notice: The table and field name as well the where condition will not be escaped.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <param name="table">The table.</param>
        /// <param name="field">The field.</param>
        /// <param name="where">The where.</param>
        /// <returns>T.</returns>
        public static T SelectOneOrDefault<T>(this ICommand command, string table, string field, string where)
        {
            return command
                .WithQuery($"SELECT TOP 1 {field} FROM {table} WHERE {where}")
                .ExecuteScalarOrDefault<T>();
        }
    }
}
