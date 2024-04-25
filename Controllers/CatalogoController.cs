using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly IWebHostEnvironment _entornoWeb;

        public CatalogoController(IWebHostEnvironment entornoWeb)
        {
            _entornoWeb = entornoWeb;
        }


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

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> agregarLibro()
        {
            string titulo = Request.Form["titulo"];
            string autor = Request.Form["autor"];
            string isbn = Request.Form["isbn"];
            string anio_publicacion = Request.Form["anio_publicacion"];
            string ejemDisp = Request.Form["ejemplares_disponibles"];
            string categoria = Request.Form["categoria"];
            var archivo = HttpContext.Request.Form.Files;
            if (archivo != null && archivo.Count > 0)
            {
                string ruta = _entornoWeb.WebRootPath + "\\img\\Libros\\" + isbn + Path.GetExtension(archivo[0].FileName);
                using (var flujoArchivo = new FileStream(ruta, FileMode.Create))
                {
                    await archivo[0].CopyToAsync(flujoArchivo);
                }
            }
            string[] datos = { "titulo:" + titulo, "autor:" + autor, "isbn:" + isbn, "anio_publicacion:" + anio_publicacion, "ejemplares_disponibles:" + ejemDisp, "id_categoria:" + categoria, "logo:" + isbn + Path.GetExtension(archivo[0].FileName) };
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

        public IActionResult ObtenerLibro(string tabla, string id)
        {

            Modelo query = new Modelo();
            var libro = query.ObtenerLibro(tabla, id);

            if (libro != null && libro.Read())
            {
                var lib = new
                {
                    id_libro = libro["id_libro"].ToString(),
                    titulo = libro["titulo"].ToString(),
                    autor = libro["autor"].ToString(),
                    isbn = libro["isbn"].ToString(),
                    anio_publicacion = libro["anio_publicacion"].ToString(),
                    ejemplares_disponibles = libro["ejemplares_disponibles"].ToString(),
                    categoria = libro["id_categoria"].ToString(),
                    portada = libro["logo"].ToString()
                };

                return Json(lib);

            }
            else
            {
                return Json(null);
            }
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