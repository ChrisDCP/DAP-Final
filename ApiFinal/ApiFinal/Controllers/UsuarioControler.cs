using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using ApiFinal.Models;
using System.Data.SqlClient;

namespace ApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IConfiguration _Config;

        public UsuarioController(IConfiguration config)
        {
            _Config = config;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<Usuario>>> GetUsuario()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var oUsuario = conexion.Query<Usuario>("VerUsuariosSinID", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oUsuario);
        }

        [HttpGet]
        [Route("obtener/{UsuarioId:int}")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarioId(int UsuarioId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", UsuarioId);
            var oUsuario = conexion.Query<Usuario>("VerUsuarios", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oUsuario);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<List<Usuario>>> InsertUsuario(Usuario user)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@nombreUsuario", user.NombreUsuario);
            param.Add("@correoElectronico", user.CorreoElectronico);
            param.Add("@contraseña", user.Contraseña);
            var oUsuario = conexion.Query<Usuario>("AgregarUsuario", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oUsuario);
        }

        [HttpPut]
        [Route("actualizar")]
        public async Task<ActionResult<List<Usuario>>> ActUsuario(Usuario user)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", user.Id);
            param.Add("@nombre", user.NombreUsuario);
            var oUsuario = conexion.Query<Usuario>("ActualizarUsuario", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oUsuario);
        }

        [HttpDelete]
        [Route("eliminar/{UsuarioId:int}")]
        public async Task<ActionResult<List<Usuario>>> DelUsuario(int UsuarioId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", UsuarioId);
            var oUsuario = conexion.Query<Usuario>("BorrarUsuario", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oUsuario);
        }
    }
}
