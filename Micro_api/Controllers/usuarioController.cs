using Micro_Helper.MicroServicies.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuarioController : ControllerBase
    {
        private readonly Iusuario _Iuser;
        private IusuarioServicio _IAuthUseri;
        public usuarioController(Iusuario iusuario, IusuarioServicio iusuarioServicio)
        {
            _Iuser = iusuario;
            _IAuthUseri = iusuarioServicio;
        } 

        [HttpPost("login_normalito")]
        public async Task<ActionResult<microHelper_data>> loginN([FromBody] AuthUserPeti user)
        {
            return  Ok(await _Iuser.login(user));
        }

        [HttpPost("login")]
        public async Task<ActionResult<microHelper_data>> login([FromBody] AuthUserPeti user)
        {
            var resul = await _IAuthUseri.Autenticacion(user);
            if (resul == null)
            {

                return BadRequest("usuario o contraseña incorrectos");
            }
            return Ok(resul);
        }
    }
}
