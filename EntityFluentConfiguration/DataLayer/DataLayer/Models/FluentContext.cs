using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DataLayer.Models
{
    /// <summary>
    /// DbContext that deal with tables dynamically.
    /// </summary>
    public class FluentContext : DbContext
    {
        private static readonly object DbCompiledModelRegistrarLocker = new object();
        
        //Caches all the table names to compile the model.
        private static readonly ConcurrentDictionary<Tuple<string>, DbCompiledModel> DbModelBuilderCache
                            = new ConcurrentDictionary<Tuple<string>, DbCompiledModel>();
        static public FluentContext PrepareModelBuilder(string schema, string tableName)
        {
            string dynamicTableName = $"{schema}.{tableName}";
            var dummyDbContext = new DbContext(ConnectionString);

            return new FluentContext(dummyDbContext, GetModelBuilderAndCacheIt(dummyDbContext.Database.Connection, dynamicTableName));
        }
        static private DbCompiledModel GetModelBuilderAndCacheIt(DbConnection databaseConnection, string tableName)
        {
            var key = Tuple.Create(tableName);
            if (DbModelBuilderCache.ContainsKey(key))
                return DbModelBuilderCache[key];

            lock (DbCompiledModelRegistrarLocker)
            {
                if (DbModelBuilderCache.ContainsKey(key))
                    return DbModelBuilderCache[key];

                var modelBuilder = new DbModelBuilder();
                // This can be of any type.
                modelBuilder.Configurations.Add(new FluentConfiguration<Address>(tableName));

                //setting a maxsize for the cache so that least used dbmodels get flushed away is left as an exercise to the reader
                return DbModelBuilderCache[key] = modelBuilder.Build(databaseConnection).Compile();
            }
        }

        private DbContext _dummyDbContext;

        private FluentContext(DbContext dummyDbContext, DbCompiledModel compiledModel)
            : base(dummyDbContext.Database.Connection, compiledModel, contextOwnsConnection: true)
        {
            _dummyDbContext = dummyDbContext;
        }

        public FluentContext(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<FluentContext>(strategy: null);
            base.OnModelCreating(modelBuilder);
        }

        public static string ConnectionString = "";

        public DbSet<Address> Addresss { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dummyDbContext?.Dispose();
                _dummyDbContext = null;
            }

            base.Dispose(disposing);
        }
    }
}
