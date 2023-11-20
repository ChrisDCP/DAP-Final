using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFinal.Models;


namespace ApiFinal.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompañiaController : ControllerBase
    {
        public readonly JuegosDbContext _dbcontext;

        public CompañiaController(JuegosDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Compañia> lista = new List<Compañia>();

            try
            {
                lista = _dbcontext.Compañias.ToList();

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
            Compañia oCompañia = _dbcontext.Compañias.Find(id);

            if (oCompañia == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oCompañia = _dbcontext.Compañias.Where(p => p.Id == id).FirstOrDefault();
                //25 para incluir otra tabla
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oCompañia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oCompañia});
            }

        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Compañia objeto)
        {

            try
            {
                _dbcontext.Compañias.Add(objeto);
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

        public IActionResult Editar([FromBody] Compañia objeto)
        {

            Compañia oCompañia = _dbcontext.Compañias.Find(objeto.Id);

            if (oCompañia == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oCompañia.Nombre = objeto.Nombre is null ? oCompañia.Nombre : objeto.Nombre;
  
                _dbcontext.Compañias.Update(oCompañia);
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
            Compañia oCompañia = _dbcontext.Compañias.Find(id);

            if (oCompañia == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                _dbcontext.Compañias.Remove(oCompañia);
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
