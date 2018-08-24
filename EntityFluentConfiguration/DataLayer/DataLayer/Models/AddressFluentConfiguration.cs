using System.Data.Entity.ModelConfiguration;

namespace DataLayer.Models
{
    /// <summary>
    /// Fluent configuration that defines model attributes, such as Key, required, Ignore.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FluentConfiguration<T> : EntityTypeConfiguration<T> where T : Base
    {
        /// <summary>
        /// Table name with schema
        /// </summary>
        /// <param name="tableName"></param>
        public FluentConfiguration(string tableName)
        {
            HasKey(t => t.PK);
            ToTable(tableName: tableName);
        }
        /// <summary>
        /// Constructor that accepts schema name
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="tableName"></param>
        public FluentConfiguration(string schema,string tableName) 
            : this($"{schema}.{tableName}") { }
    }
}
