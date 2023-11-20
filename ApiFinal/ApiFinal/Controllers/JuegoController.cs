using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFinal.Models;


namespace ApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuegoController : ControllerBase
    {
        public readonly JuegosDbContext _dbcontext;

        public JuegoController(JuegosDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Juego> lista = new List<Juego>();

            try
            {
                lista = _dbcontext.Juegos.ToList();

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
            Juego oJuego = _dbcontext.Juegos.Find(id);

            if (oJuego == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oJuego = _dbcontext.Juegos.Where(p => p.Id == id).FirstOrDefault();
                //25 para incluir otra tabla
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = oJuego });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oJuego });
            }

        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Juego objeto)
        {

            try
            {
                _dbcontext.Juegos.Add(objeto);
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

        public IActionResult Editar([FromBody] Juego objeto)
        {

            Juego oJuego = _dbcontext.Juegos.Find(objeto.Id);

            if (oJuego == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oJuego.Nombre = objeto.Nombre is null ? oJuego.Nombre : objeto.Nombre;
                oJuego.Descripcion = objeto.Descripcion is null ? oJuego.Descripcion : objeto.Descripcion;
                oJuego.FechaLanzamiento = objeto.FechaLanzamiento is null ? oJuego.FechaLanzamiento : objeto.FechaLanzamiento;
                oJuego.CompañiaId = objeto.CompañiaId is null ? oJuego.CompañiaId : objeto.CompañiaId;


                _dbcontext.Juegos.Update(oJuego);
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
            Juego ojuego = _dbcontext.Juegos.Find(id);

            if (ojuego == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                _dbcontext.Juegos.Remove(ojuego);
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
