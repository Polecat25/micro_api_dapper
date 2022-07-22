using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro_Helper
{
    public class ConexionConfiguracion
    {
        public class MySqlConfiguracion
        {
            public MySqlConfiguracion(string conexionString) => connectionString = conexionString;
            public string connectionString { get; set; }
        }

        public class SqlserverConfiguracion
        {
            public SqlserverConfiguracion(string conexionString) => connectionString = conexionString;
            public string connectionString { get; set; }
        }
    }
}
