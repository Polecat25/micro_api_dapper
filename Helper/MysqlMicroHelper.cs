
using Dapper;
using Micro_Helper.MicroServicies;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace Micro_Helper
{
   

    public class MysqlMicroHelper
    {
        

        private ConexionConfiguracion.MySqlConfiguracion _conexion;
        
        // public MysqlMicroHelper() { }

        public MysqlMicroHelper(ConexionConfiguracion.MySqlConfiguracion conexion)
        {
            _conexion = conexion;
            
        }
 
        protected MySqlConnection dbConexion()
        {
            return new MySqlConnection(_conexion.connectionString);
        }

        //fin de conexion 
        List<errorItems> er = new List<errorItems>();
        microHelper_data data = new microHelper_data();
        DynamicParameters Parameters = new DynamicParameters();

        DapperParametros dp = new DapperParametros();



        







        public async Task<microHelper_data> ejecutarSP(string procedimiento, List<parametros> parametrosSP)
        {
            //parametros 
            
            var paramss = dp.MakeParametros(parametrosSP);
            
            var db = dbConexion();
            try
            {
                using (db)
                {
                    IEnumerable<dynamic> consulta;
                    switch (parametrosSP)
                    {
                        case null:
                            consulta = await db.QueryAsync(procedimiento, commandType: CommandType.StoredProcedure);
                            break;
                        case not null:
                            consulta = await db.QueryAsync(procedimiento, paramss, commandType: CommandType.StoredProcedure);
                            break;
                    }

                    if (consulta.Count() > 0)
                    {
                        data.datos = consulta;
                    }
                    else
                    {
                        er.Add(new errorItems("404", "NO se encontro registro uwu", 1));
                        data.Errors = er;
                        data.HasError = true;
                    }
                    
                        if (paramss != null && parametrosSP != null) 
                        { 
                            foreach (var mensajeOut in parametrosSP.Where(b=> b.DireccionParametro == ParameterDirection.Output))
                            {
                                var valor = paramss.Get<string>(mensajeOut.nombre);
                                if (valor != null)
                                {
                                    er.Add(new errorItems("--", valor, 2)); ///deberia ser -> er.Add(new errorItems(VariableCOdigo, valor, VariableCriticidad)); variables que deberian ser ouputs
                                    data.Errors = er;
                                    data.HasError = true;
                                }
                                    
                            }                        

                        }                   
                }
            }
            catch (Exception e)
            {
                er.Add(new errorItems(e.Message.ToString()));
                data.Errors = er;
                data.HasError = true;
            }

            return data;
        }
        public async Task<microHelper_data> ConsultaNormal(string consulta, [Optional] object parametros)
        {
            var db = dbConexion();

            microHelper_data data = new microHelper_data();
            if (consulta == "" || consulta == null)
            {
                er.Add(new errorItems("Consulta vacia"));
                data.Errors = er;
                return data;
            }
            try
            {
                using (db)
                {
                    var Rowcount = db.Query(consulta, parametros).Count();                   
                    if (Rowcount==0)
                    {
                       
                        er.Add(new errorItems("404", "No se encontró datos relacionados", 0));
                        data.Errors = er;
                        
                    }
                    else
                    {
                        data.datos = await db.QueryAsync(consulta, parametros);
                    }
                        
                }
            }
            catch (Exception e)
            {
                er.Add(new errorItems(e.Message));
                data.Errors = er;
           
            }
            if (data.Errors.Count !=0)
            {
                data.HasError = true;
            }
            return data;
        }
        
    }
}
