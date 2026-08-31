using CalculadoraRecetasInteligente.Data;
using CalculadoraRecetasInteligente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CalculadoraRecetasInteligente.Controllers
{
    public class HomeController : Controller
    {
        private readonly RecetasDbContext _context;

        public HomeController(RecetasDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MisRecetas()
        {
            var recetas = await _context.Recetas
                .OrderByDescending(r => r.FechaCreacion)
                .ToListAsync();

            return View(recetas);
        }

        public IActionResult CrearReceta()
        {
            return View();
        }

        public IActionResult Privacy()
        {
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