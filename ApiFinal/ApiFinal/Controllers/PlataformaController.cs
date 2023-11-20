using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFinal.Models;


namespace ApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        public readonly JuegosDbContext _dbcontext;

        public PlataformaController(JuegosDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Plataforma> lista = new List<Plataforma>();

            try
            {
                lista = _dbcontext.Plataformas.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }

        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public IActionResult Obtner(int id)
        {
            Plataforma oPlataforma = _dbcontext.Plataformas.Find(id);

            if (oPlataforma == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oPlataforma = _dbcontext.Plataformas.Where(p => p.Id == id).FirstOrDefault();
                //25 para incluir otra tabla
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oPlataforma });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oPlataforma });
            }

        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Plataforma objeto)
        {

            try
            {
                _dbcontext.Plataformas.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Plataforma objeto)
        {

            Plataforma oPlataforma = _dbcontext.Plataformas.Find(objeto.Id);

            if (oPlataforma == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oPlataforma.Nombre = objeto.Nombre is null ? oPlataforma.Nombre : objeto.Nombre;

                _dbcontext.Plataformas.Update(oPlataforma);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            Plataforma oPlataforma = _dbcontext.Plataformas.Find(id);

            if (oPlataforma == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                _dbcontext.Plataformas.Remove(oPlataforma);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mesaje = ex.Message });
            }

        }

    }
}
