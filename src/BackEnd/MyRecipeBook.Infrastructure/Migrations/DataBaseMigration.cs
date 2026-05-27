using Dapper;
using Microsoft.Data.SqlClient;

namespace MyRecipeBook.Infrastructure.Migrations
{
    public static class DataBaseMigration
    {

        public static void Migrate(string connectinString)
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
            
        }
    }
}
