using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ApiFinal.Models;
using Dapper;


namespace ApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuegoController : ControllerBase
    {
        private IConfiguration _Config;

        public JuegoController(IConfiguration config)
        {
            _Config = config;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<Juego>>> GetJuego()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var oJuego = conexion.Query<Juego>("VerJuegoSinId", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oJuego);
        }

        [HttpGet]
        [Route("obtener/{JuegoId:int}")]
        public async Task<ActionResult<List<Juego>>> GetJuegoId(int JuegoId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", JuegoId);
            var oJuego = conexion.Query<Juego>("VerJuego", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oJuego);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<List<Juego>>> InsertJuego(Juego gam)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@nombre", gam.Nombre);
            param.Add("@descripcion", gam.Descripcion);
            param.Add("@fechaLanzamiento", gam.FechaLanzamiento);
            param.Add("@compañiaId", gam.CompañiaId);
            var oJuego = conexion.Query<Juego>("AgregarJuego", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oJuego);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<ActionResult<List<Juego>>> ActJuego(Juego gam)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", gam.Id);
            param.Add("@nombre", gam.Nombre);
            param.Add("@descripcion", gam.Descripcion);
            param.Add("@fechaLanzamiento", gam.FechaLanzamiento);
            param.Add("@compañiaId", gam.CompañiaId);
            var oJuego = conexion.Query<Juego>("ActualizarJuego", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oJuego);
        }

        [HttpDelete]
        [Route("borrar/{JuegoId:int}")]
        public async Task<ActionResult<List<Juego>>> DelJuego(int JuegoId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", JuegoId);
            var oJuego = conexion.Query<Juego>("BorrarJuego", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oJuego);
        }

    }
}
