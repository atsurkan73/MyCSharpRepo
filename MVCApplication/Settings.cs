using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MVCApplication
{
    public class Settings
    {
        public static string ConnectionString = new SqlConnectionStringBuilder
        {
            DataSource = "localhost\\MSSQLSERVER01",
            InitialCatalog = "MyDb",
            IntegratedSecurity = true,
            Encrypt = SqlConnectionEncryptOption.Optional,
        }.ConnectionString;

        public static JsonSerializerOptions SerializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
    }
}
