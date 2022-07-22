using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Micro_Helper
{
    public class microHelper_data
    {
       
        public bool HasError { get; set; } 
        public List<errorItems> Errors { get; set; } //es un objeto de clase error
        public Object datos { get; set; }   //pa guardar la info provieniente 
        //iria el token pero no

    }
    
    public class errorItems
    {

        //constructores consobrecargas 
        //public errorItems() : this("") { }
        public errorItems() { }
        public errorItems(string descripcion) : this(descripcion, 0) { }
        public errorItems(string error, string descripcion) : this(error, descripcion, 0) { }
        public errorItems(string descripcion, int criticidad) : this("", descripcion, criticidad) { }
        public errorItems(string codigo, string descripcion, string criticidad) : this(codigo, descripcion, Convert.ToInt32(criticidad)) { }
        public errorItems(string codigo, string descripcion, int criticidad)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Criticidad = criticidad;
        }
        public string? Descripcion { get; set; }
        public int Criticidad { get; set; }
        public string? Codigo { get; set; }

    }

    public class parametros
    {
        public string nombre { get; set; }
        public object valor { get; set; }
        public DbType tipo { get; set; }
        public ParameterDirection DireccionParametro { get; set; }
        public int size { get; set; }

        public parametros() { }
        //para parametros normales
        public parametros(string nombre, object valor)
        {
            this.nombre = nombre;
            this.valor = valor;
        }

        //para parametros output
        public parametros(string nombre, DbType tipo, ParameterDirection direccionParametro, [Optional] int size) : this(nombre)
        {
            this.tipo = tipo;
            DireccionParametro = direccionParametro;
            this.size = size;
        }
        //para prametros especificos
        public parametros(string nombreP, [Optional] object valorP, [Optional] DbType tipoP, [Optional] ParameterDirection direccionP, [Optional] int sizeP)
        {
            nombre = nombreP;
            valor = valorP;
            tipo = tipoP;
            DireccionParametro = direccionP;
            size = sizeP;
        }
    }



}
