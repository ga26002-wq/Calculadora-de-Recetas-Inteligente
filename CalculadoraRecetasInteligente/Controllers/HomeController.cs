using CalculadoraRecetasInteligente.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CalculadoraRecetasInteligente.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Verificamos si el usuario inició sesión
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Enviamos el nombre del usuario a la vista
            ViewBag.UsuarioNombre =
                HttpContext.Session.GetString("UsuarioNombre");

            return View();
        }
        
        public IActionResult Privacy()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}