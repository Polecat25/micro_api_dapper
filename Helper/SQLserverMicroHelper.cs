using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Micro_Helper.MicroServicies;

namespace Micro_Helper
{
    public class SQLserverMicroHelper
    {
        public ConexionConfiguracion.SqlserverConfiguracion _conexion;
       
        public SQLserverMicroHelper(ConexionConfiguracion.SqlserverConfiguracion conexion)
        {
             _conexion= conexion;
        }

        
        protected SqlConnection dbConexion()
        {
            return new SqlConnection(_conexion.connectionString);    
        }
        
        microHelper_data data = new microHelper_data();
        List<errorItems> er = new List<errorItems>();
        DapperParametros dp = new DapperParametros();

        public async Task<microHelper_data> EjecutarSp(string procedimiento, [Optional] List<parametros> parametrosSP)
        {
            var paramss = new DynamicParameters();
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
                            paramss = dp.MakeParametros(parametrosSP);
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
                        foreach (var mensajeOut in parametrosSP.Where(b => b.DireccionParametro == ParameterDirection.Output))
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

                    //arreglar el usos de errores
        public async Task<microHelper_data> consultaSQL(string sql, [Optional] object parametros)
        {
            var db = dbConexion();
            using (db)
            {
                switch (parametros)
                {
                    case null:
                        data.datos = await db.QueryAsync(sql);
                        break;
                    case not null:
                        data.datos = await db.QueryAsync(sql, parametros);
                        break;
                    
                }
                
            }
            return data;
        }

        
    }
}
