using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebApplicationProject
{
    public class Settings
    {
        public static string ConnectionString = new SqlConnectionStringBuilder
        {
            DataSource = "localhost\\MSSQLSERVER01",
            InitialCatalog = "MyProjectDb",
            IntegratedSecurity = true,
            TrustServerCertificate = true

           // Encrypt = SqlConnectionEncryptOption.Optional
        }.ConnectionString;

        public static JsonSerializerOptions SerializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
    }
}
