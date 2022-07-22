using Micro_api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Micro_Helper.MicroServicies.Auth
{
    public class UsuarioServicio : IusuarioServicio
    {

        private ConexionConfiguracion.SqlserverConfiguracion _conexion;
        private readonly appSettings _appSettings; //para obtener el secreto 
        public UsuarioServicio(ConexionConfiguracion.SqlserverConfiguracion conexion, IOptions<appSettings> appSettings)
        {
            _conexion = conexion;
            _appSettings = appSettings.Value;
        }
        protected SqlConnection dbConexion()
        {
            return new SqlConnection(_conexion.connectionString);
        }

        public async Task<microHelper_data> Autenticacion(AuthUserPeti user)
        {
            microHelper_data data = new microHelper_data();
            List<errorItems> er = new List<errorItems>(); 
            SQLserverMicroHelper server = new SQLserverMicroHelper(_conexion);
            AuthUserResp respuesta = new AuthUserResp();
            var password = Encrypt.GetSHA256(user.pass);
            var sql = "select * from usuarios where email = @email and pass = @pass";
            try
            {
                using (dbConexion())
                {
                    var usr = await dbConexion().QueryAsync<AuthUserPeti>(sql, new { email = user.email, pass = password });
                    if (usr.FirstOrDefault() != null)
                    {
                        respuesta.email = user.email;
                        respuesta.token = getToken(user);
                        data.datos = respuesta;
                    }
                    else
                    {
                        data.HasError = true;
                        er.Add(new errorItems("No se encontro usr", 0));
                        data.Errors = er;
                    }

                    
                }
            }
            catch (Exception e)
            {

                data.HasError = true;
                er.Add(new errorItems(e.Message, e.HResult));
                data.Errors = er;
            }

            return data;
            //var res = await server.consultaSQL(sql, new { email = user.email, pass = password });
           
            
        }

        private string getToken( AuthUserPeti user)
        {
            var tokenHAndler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.keySecret);
            //Claims que se guardan en el token 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        // new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                        new Claim(ClaimTypes.Email, user.email)

                    }
                ),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
                
                
            };
            var token = tokenHAndler.CreateToken(tokenDescriptor);
            return tokenHAndler.WriteToken(token);
        }
    }
}
