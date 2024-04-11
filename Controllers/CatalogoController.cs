using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Libros()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Libros");
            return View();
        }

        public IActionResult Categorias()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Categoria");
            return View();
        }

        public IActionResult AgregarLibro()
        {
            string titulo = Request.Form["titulo"];
            string autor = Request.Form["autor"];
            string isbn = Request.Form["isbn"];
            string anio_publi = Request.Form["anio_publicacion"];
            string ejem = Request.Form["ejemplares_disponibles"];
            string categoria = Request.Form["nombre"];

            string[] datos = { "titulo:" + titulo, "autor:" + autor, "isbn:" + isbn, "anio_publicacion:" + anio_publi, "ejemplares_disponibles:" + ejem, "id_categoria:" + categoria };
            Modelo com = new Modelo();
            try
            {
                com.Agregar("libro", datos);
                ViewBag.msg = "El registro ha sido agregado con éxito";
            }
            catch
            {
                ViewBag.msg = "Error al procesar los datos";
            }

            ViewBag.query = com.BuscarTodo("Vista_Libros");
            return View("Libros");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarCategoria()
        {
            string rol = Request.Form["nombre"];
            string descripcion = Request.Form["descripcion"];


            string[] datos = { "nombre:" + rol, "descripcion:" + descripcion };
            Modelo com = new Modelo();
            try
            {
                com.Agregar("categoria", datos);
                ViewBag.msg = "El registro ha sido agregado con éxito";
            }
            catch
            {
                ViewBag.msg = "Error al procesar los datos";
            }

            ViewBag.query = com.BuscarTodo("Vista_Categoria");
            return View("Categorias");

        }
    }
}
