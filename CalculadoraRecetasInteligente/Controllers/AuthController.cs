using CalculadoraRecetasInteligente.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraRecetasInteligente.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }
    }
}