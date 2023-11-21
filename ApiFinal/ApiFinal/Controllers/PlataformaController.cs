using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using ApiFinal.Models;
using System.Data.SqlClient;
using System;

namespace ApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        private IConfiguration _Config;

        public PlataformaController(IConfiguration config)
        {
            _Config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Plataforma>>> GetPlataforma()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var oPlatafroma = conexion.Query<Plataforma>("VerPlataformaSinId", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oPlatafroma);
        }

        [HttpGet]
        [Route("obtener/{PlataformaId:int}")]
        public async Task<ActionResult<List<Plataforma>>> GetPlataformaId(int PlataformaId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", PlataformaId);
            var oPlatafroma = conexion.Query<Plataforma>("VerPlataforma", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oPlatafroma);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<List<Plataforma>>> InsertPlataforma(Plataforma plat)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@nombre", plat.Nombre);
            var oPlataforma = conexion.Query<Plataforma>("AgregarPlataforma", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oPlataforma);
        }

        [HttpPut]
        [Route("actualizar")]
        public async Task<ActionResult<List<Plataforma>>> ActPlataforma(Plataforma plat)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", plat.Id);
            param.Add("@nombre", plat.Nombre);
            var oPlataforma = conexion.Query<Plataforma>("ActualizarPlataforma", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oPlataforma);
        }

        [HttpDelete]
        [Route("eliminar/{PlataformaId:int}")]
        public async Task<ActionResult<List<Plataforma>>> DelPlataforma(int PlataformaId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", PlataformaId);
            var oPlataforma = conexion.Query<Plataforma>("BorraPlataforma", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oPlataforma);
        }
    }
}
