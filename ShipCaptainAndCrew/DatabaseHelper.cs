using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ShipCaptainAndCrew
{
    public class DatabaseHelper
    {
        public static DapperUserRepository Connect()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            IDbConnection connection = new SqlConnection(connectionString);
            var userRepo = new DapperUserRepository(connection);
            return userRepo;
        }
    }
}
