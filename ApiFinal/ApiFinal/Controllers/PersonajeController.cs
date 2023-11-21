using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ApiFinal.Models;
using Dapper;


namespace ApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajeController : ControllerBase
    {
        private IConfiguration _Config;

        public PersonajeController(IConfiguration config)
        {
            _Config = config;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<Personaje>>> GetPersonaje()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var oPersonaje = conexion.Query<Personaje>("VerPersonajeSinId", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oPersonaje);
        }

        [HttpGet]
        [Route("obtener/{PersonajeId:int}")]
        public async Task<ActionResult<List<Personaje>>> GetPersonajeId(int PersonajeId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", PersonajeId);
            var oPersonaje = conexion.Query<Personaje>("VerPersonaje", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oPersonaje);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<List<Juego>>> InsertPersonaje(Personaje per)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@nombre", per.Nombre);
            param.Add("@descripcion", per.Descripcion);
            param.Add("@juegoId", per.JuegoId);
            var oPersonaje = conexion.Query<Juego>("AgregarPersonaje", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oPersonaje);
        }

        [HttpPut]
        [Route("actualizar")]
        public async Task<ActionResult<List<Juego>>> ActPersonaje(Personaje per)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", per.Id);
            param.Add("@nombre", per.Nombre);
            param.Add("@descripcion", per.Descripcion);
            param.Add("@juegoId", per.JuegoId);
            var oPersonaje = conexion.Query<Personaje>("ActualizarPersonaje", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oPersonaje);
        }

        [HttpDelete]
        [Route("borrar/{PersonajeId:int}")]
        public async Task<ActionResult<List<Personaje>>> DelPersonaje(int PersonajeId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", PersonajeId);
            var oPersonaje = conexion.Query<Juego>("BorrarPersonaje", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oPersonaje);
        }

    }
}
