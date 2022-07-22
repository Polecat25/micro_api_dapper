using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro_Helper.MicroServicies
{
    public class DapperParametros
    {
        //funcion para convertir una lista de valores a DynamicParameters 
        public DynamicParameters MakeParametros(List<parametros> valores)
        {
            DynamicParameters param = new DynamicParameters();

            foreach (var item in valores)
            {
                if (item.DireccionParametro == ParameterDirection.Output)
                {
                    param.Add(item.nombre, dbType: item.tipo, direction: item.DireccionParametro, size: item.size);
                }
                else
                {
                    param.Add(item.nombre, item.valor);
                }
               
            }
            return param;
        }
    }
}
