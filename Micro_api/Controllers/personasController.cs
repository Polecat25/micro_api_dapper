using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Micro_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class personasController : ControllerBase
    {
        private readonly Ipersonas _Ipersonas; 
        public personasController(Ipersonas ipersonas)
        {
            _Ipersonas = ipersonas;
        }

        // GET: api/<personasController>
        [HttpGet]
        public async Task<ActionResult<microHelper_data>> Get()
        {
            var resultado = await _Ipersonas.getAllPErsonas();
            if (resultado.HasError)
            {
                return NotFound(resultado) ;
            }
            return Ok(resultado);
        }

        //***************pruebas *************************//



        [HttpGet("/pruebas")]
        public async Task<ActionResult<microHelper_data>> Get2222(long cuenta)
        {
            var resultado = await _Ipersonas.getAllPErsonas2(cuenta);


            if (resultado.HasError)
            {
                //que esto solo devuelve el arreglo de resultados no el resultado enteor
                return NotFound(resultado);
            }
            return Ok(resultado);
            //return new string[] { "value1", "value2" };
        }


        //***************pruebas *************************//



        // GET api/<personasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<microHelper_data>> Get(int id)
        {
            return Ok( await _Ipersonas.getPersona(id));
        }

        [HttpGet("/membresias/{cuenta}")]
        public async Task<ActionResult<microHelper_data>> Getmembresia(long cuenta)
        {
            return Ok(await _Ipersonas.membresiaPersona(cuenta));
        }

        // POST api/<personasController>
        [HttpPost]
        public async Task<ActionResult<microHelper_data>> Post([FromBody] personas persona)
        {
            return Ok(await _Ipersonas.addPersona(persona));
        }

        // PUT api/<personasController>/5
        [HttpPut]
        public async Task<ActionResult<microHelper_data>> Put( [FromBody] personas persona)
        {
            return Ok(await _Ipersonas.UpdatePersonas(persona));
        }

        
       
    }
}
