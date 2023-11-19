using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFinal.Models;
using Microsoft.AspNetCore.Cors;

namespace ApiFinal.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioControler : ControllerBase
    {
        public readonly JuegosDbContext _dbcontext;

        public UsuarioControler(JuegosDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista() { 
            List<Usuario> lista = new List<Usuario>();

            try
            {
                lista = _dbcontext.Usuarios.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }
        
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public IActionResult Obtner(int id)
        {
            Usuario oUsuario = _dbcontext.Usuarios.Find(id);

            if (oUsuario == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oUsuario = _dbcontext.Usuarios.Where(p => p.Id == id).FirstOrDefault();
                //25 para incluir otra tabla
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response =oUsuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oUsuario });
            }

        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Usuario objeto) {

            try
            {
                _dbcontext.Usuarios.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { ex.Message});
            }
        }

        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Usuario objeto)
        {

            Usuario oUsuario = _dbcontext.Usuarios.Find(objeto.Id);

            if (oUsuario == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oUsuario.NombreUsuario = objeto.NombreUsuario is null ? oUsuario.NombreUsuario : objeto.NombreUsuario;
                oUsuario.CorreoElectronico = objeto.CorreoElectronico is null ? oUsuario.CorreoElectronico : objeto.CorreoElectronico;
                oUsuario.Contraseña = objeto.Contraseña is null ? oUsuario.Contraseña : objeto.Contraseña;

                _dbcontext.Usuarios.Update(oUsuario);
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
            Usuario oUsuario = _dbcontext.Usuarios.Find(id);

            if (oUsuario == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                _dbcontext.Usuarios.Remove(oUsuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new {mesaje = ex.Message });
            }

        }

    }
}
