using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipeBook.Infrastructure.Migrations;

public static class DataBaseMigration
{

    public static void Migrate(string connectinString, IServiceProvider serviceProvider)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectinString);

        var dataBaseName = connectionStringBuilder.InitialCatalog;

        connectionStringBuilder.Remove("DataBase");

        using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters();
        parameters.Add("name",dataBaseName);

        var record = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

        if(!record.Any())           
            dbConnection.Execute($"CREATE DATABASE {dataBaseName}");

        MigrateDataBase(serviceProvider);


    }

    private static void MigrateDataBase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();

        runner.MigrateUp();

    }
}
