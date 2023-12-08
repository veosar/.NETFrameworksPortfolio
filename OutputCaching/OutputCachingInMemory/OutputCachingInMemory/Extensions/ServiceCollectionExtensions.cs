using System.Data;
using Dapper;
using Npgsql;
using OutputCachingInMemory.Repositories;
using OutputCachingInMemory.SqlTypeHandlers;

namespace OutputCachingInMemory.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection, ConfigurationManager config)
    {
        collection.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(config.GetConnectionString("PostgresSql")));
        collection.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}