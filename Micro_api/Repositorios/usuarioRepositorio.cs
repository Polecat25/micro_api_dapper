namespace Micro_api.Repositorios
{
    public class usuarioRepositorio : Iusuario
    {
        private ConexionConfiguracion.SqlserverConfiguracion _conexion;
        public usuarioRepositorio(ConexionConfiguracion.SqlserverConfiguracion conexion)
        {
            _conexion = conexion;         
        }   

        microHelper_data data = new microHelper_data();
        public async Task<microHelper_data> login(AuthUserPeti usuario)
        {
            SQLserverMicroHelper server = new SQLserverMicroHelper(_conexion);
            var sql = "select * from usuarios";
             
            data = await server.consultaSQL(sql);
            return data;
        }
    }
}
