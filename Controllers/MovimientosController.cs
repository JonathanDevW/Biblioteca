using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class MovimientosController : Controller
    {
        public IActionResult Prestamos()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Prestamos");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarNuevoPrestamo()
        {
            string titulo = Request.Form["titulo"];
            string nickname = Request.Form["nickname"];
            string fecha_prestamo = Request.Form["fecha_prestamo"];
            string fecha_devolucion = Request.Form["fecha_devolucion"];
            string categoria = Request.Form["nombre"];
            string estado = Request.Form["estado"];

            string[] datos = { "id_titulo:" + titulo, "id_usuario:" + nickname, /*"fecha_prestamo:" + fecha_prestamo,*/ "fecha_devolucion:" + fecha_devolucion, "id_categoria:" + categoria, "id_estado:" + estado };
            Modelo com = new Modelo();
            try
            {
                com.Agregar("prestamo", datos);
                ViewBag.msg = "El registro ha sido agregado con éxito";
            }
            catch
            {
                ViewBag.msg = "Error al procesar los datos";
            }

            ViewBag.query = com.BuscarTodo("Vista_Prestamos");
            return View("Prestamos");

        }
    }


}


