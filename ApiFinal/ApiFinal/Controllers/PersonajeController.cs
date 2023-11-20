using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFinal.Models;


namespace ApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajeController : ControllerBase
    {
        public readonly JuegosDbContext _dbcontext;

        public PersonajeController(JuegosDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Personaje> lista = new List<Personaje>();

            try
            {
                lista = _dbcontext.Personajes.ToList();

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
            Personaje oPersonaje = _dbcontext.Personajes.Find(id);

            if (oPersonaje == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oPersonaje = _dbcontext.Personajes.Where(p => p.Id == id).FirstOrDefault();
                //25 para incluir otra tabla
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oPersonaje });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oPersonaje });
            }

        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Personaje objeto)
        {

            try
            {
                _dbcontext.Personajes.Add(objeto);
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

        public IActionResult Editar([FromBody] Personaje objeto)
        {

            Personaje oPersonaje = _dbcontext.Personajes.Find(objeto.Id);

            if (oPersonaje == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oPersonaje.Nombre = objeto.Nombre is null ? oPersonaje.Nombre : objeto.Nombre;
                oPersonaje.Descripcion = objeto.Descripcion is null ? oPersonaje.Descripcion : objeto.Descripcion;
                oPersonaje.JuegoId = objeto.JuegoId is null ? oPersonaje.JuegoId : objeto.JuegoId;


                _dbcontext.Personajes.Update(oPersonaje);
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
            Personaje oPersonaje = _dbcontext.Personajes.Find(id);

            if (oPersonaje == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                _dbcontext.Personajes.Remove(oPersonaje);
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
