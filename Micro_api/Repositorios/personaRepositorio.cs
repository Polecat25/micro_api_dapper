using Micro_Helper;
using System.Data;


namespace Micro_api.Repositorios
{
    public class personaRepositorio : Ipersonas
    {
        private ConexionConfiguracion.MySqlConfiguracion _conexion;
        private ConexionConfiguracion.SqlserverConfiguracion _sqlConexion;

        public microHelper_data table = new microHelper_data();
        
        public  DynamicParameters parametrosSP = new DynamicParameters();
        List<string> nombresOuputs = new List<string>();
        List<parametros> ParamsList = new List<parametros>();
        public personaRepositorio(ConexionConfiguracion.MySqlConfiguracion conexion, ConexionConfiguracion.SqlserverConfiguracion sqlConexion)
        {
            _conexion = conexion;
            _sqlConexion = sqlConexion;
        }




        //***************pruebas SQL SERVER*************************//
        public async Task<microHelper_data> getAllPErsonas2(long cuenta) //con procedimiento
        {
            //sql server                     
            SQLserverMicroHelper microHelper = new SQLserverMicroHelper(_sqlConexion);
           
            var sql = "selectAllPersonasn_noParams";


            ParamsList.AddRange(new parametros[]
           {
               
              //  new pd.parametros ( "_id", null),
                new parametros ( "MENSAJE2", DbType.String, ParameterDirection.Output, 50),
           });

            // parametrosSP.Add("MENSAJE", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parametrosSP.Add("MENSAJE2", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            nombresOuputs.AddRange(new string[] { "MENSAJE2" });
            nombresOuputs.AddRange(new string[] { "MENSAJE", "MENSAJE2" });
            table = await microHelper.EjecutarSp(sql);
            return table;
        }




        //***************pruebas fin *************************//

        public async Task<microHelper_data> getAllPErsonas()
        {
         
            MysqlMicroHelper microHelper = new MysqlMicroHelper(_conexion);
                     
           // parametrosSP.Add("_tipo", 2);
           // parametrosSP.Add("_id", null);
            //parametrosSP.Add("_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            //nombresOuputs.AddRange(new string[] { "_mensaje"});

            

            //se agregan todos los parametros a utilizar 
            ParamsList.AddRange(new parametros[]
            {
                new parametros ( "_tipo",  2),
                new parametros ( "_id", null),
                new parametros ( "_mensaje", DbType.String, ParameterDirection.Output, 50),             
            });

            // var lista =  

           // parametrosSP= microHelper.MakeParametros(ParamsList);

            table = await microHelper.ejecutarSP("sp_allPersonas", ParamsList);

            return table;
        }





        public async Task<microHelper_data> getPersona(int id)
        {
            
            MysqlMicroHelper microHelper = new MysqlMicroHelper(_conexion);
  
            parametrosSP.Add("_tipo", 1);
            parametrosSP.Add("_id", id);
            parametrosSP.Add("_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 150);
            nombresOuputs.AddRange(new string[] { "_mensaje" });

            List<parametros> ParamsList = new List<parametros>();

            //se agregan todos los parametros a utilizar 
            ParamsList.AddRange(new parametros[]
            {
                new parametros ( "_tipo",  1),
                new parametros ( "_id", id),
                new parametros ( "_mensaje", DbType.String, ParameterDirection.Output, 50),
            });
            // table = await microHelper.ejecutarSP("sp_allPersonas", ParamsList);

            return table;
        }

        public async Task<microHelper_data> addPersona(personas persona)
        {

            MysqlMicroHelper microHelper = new MysqlMicroHelper(_conexion);


            parametrosSP.AddDynamicParams(new
            {
                _nombre = persona.Nombre,
                _email = persona.email,
                _phone = persona.phone,
                _cuenta = persona.cuenta,
                _cantidadMatriculadas = persona.cantidadMatriculadas,
                _periodo = persona.periodo,

            });

            parametrosSP.Add("_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 150);
            nombresOuputs.AddRange(new string[] { "_mensaje" });
            //table = await microHelper.ejecutarSP("sp_insertPErsonas", parametrosSP, nombresOuputs);
            return table;
        }

        public async Task<microHelper_data> UpdatePersonas(personas persona)
        {
        
            MysqlMicroHelper microHelper = new MysqlMicroHelper(_conexion);
      
                    
            parametrosSP.AddDynamicParams(new {
                _nombre= persona.Nombre,
                _email= persona.email,
                _telefono= persona.phone,
                _cuenta= persona.cuenta,
                _matriculdas=persona.cantidadMatriculadas,
                _periodo= persona.periodo,
                _id= persona.id,
            });
            parametrosSP.Add("_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 150);
            nombresOuputs.AddRange(new string[] { "_mensaje" });
            //table = await microHelper.ejecutarSP("sp_updatePersona", parametrosSP, nombresOuputs);

            return table;
        }

        public async Task<microHelper_data> membresiaPersona(long cuenta)
        {
            string sql = @"SELECT * FROM view_membresias WHERE cuenta = @cuenta_";
            //string sql = @"SELECT * FROM view_membresias";
            parametrosSP.AddDynamicParams(new { cuenta_=cuenta });

            MysqlMicroHelper mysqlMicroHelper = new MysqlMicroHelper(_conexion);
            table = await mysqlMicroHelper.ConsultaNormal(sql, parametrosSP);
            return table;
        }
    }
}
