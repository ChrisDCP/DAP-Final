using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ApiFinal.Models;
using Dapper;


namespace ApiFinal.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompañiaController : ControllerBase
    {
        private IConfiguration _Config;

        public CompañiaController(IConfiguration config)
        {
            _Config = config;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<Compañia>>> GetCompañia()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var oCompañia = conexion.Query<Compañia>("VerCompañiaSinID", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCompañia);
        }

        [HttpGet]
        [Route("obtener/{compañiaId:int}")]
        public async Task<ActionResult<List<Compañia>>> GetCompañiaId(int CompañiaId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", CompañiaId);
            var oCompañia = conexion.Query<Compañia>("VerCompañia", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oCompañia);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<List<Compañia>>> InsertCompañia(Compañia com)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@nombre", com.Nombre);
            var oCompañia = conexion.Query<Compañia>("AgegarCompañia", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oCompañia);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult<List<Compañia>>> ActCompañia(Compañia com)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", com.Id);
            param.Add("@nombre", com.Nombre);
            var oCompañia = conexion.Query<Compañia>("ActualizarCompañia", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oCompañia);
        }

        [HttpDelete]
        [Route("borrar/{CompañiaId:int}")]
        public async Task<ActionResult<List<Compañia>>> DelCompañia(int CompañiaId)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("MyDB"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@id", CompañiaId);
            var oCompañia = conexion.Query<Compañia>("EliminarCompañia", param, commandType: System.Data.CommandType.StoredProcedure)
            .SingleOrDefault();
            return Ok(oCompañia);
        }

    }
}
