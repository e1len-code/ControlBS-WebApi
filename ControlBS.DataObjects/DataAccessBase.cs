using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ControlBS.DataObjects
{

    public class DataAccessBase
    {
        public Database Db;

        public DataAccessBase()
        {
            //-- Capturar la conexion
            ProcesarConexion("Conexion");

            //-- Conexion a la base de datos
            Db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(stringConexion);
        }

        private static IConfiguration? _configuration;
        static string Conexion = "Conexion";

        public static string stringConexion
        {
            get
            {
                return _configuration?.GetConnectionString(Conexion) ?? "";
            }
        }
        public static void ProcesarConexion(string nameConexion)
        {
            Conexion = nameConexion;

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            _configuration = configurationBuilder.Build();
        }
    }
}