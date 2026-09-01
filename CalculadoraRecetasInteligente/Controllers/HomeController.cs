using CalculadoraRecetasInteligente.Data;
using CalculadoraRecetasInteligente.Models;
using CalculadoraRecetasInteligente.ViewModels;
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

        public async Task<IActionResult> Ingredientes()
        {
            var ingredientes = await _context.Ingredientes
                .OrderBy(i => i.Nombre)
                .ToListAsync();

            return View(ingredientes);
        }



        // MOSTRAR FORMULARIO PARA CREAR INGREDIENTE
        [HttpGet]
        public IActionResult CrearIngrediente()
        {
            return View();
        }


        // GUARDAR INGREDIENTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearIngrediente(CrearIngredienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ingrediente = new Ingrediente
            {
                Nombre = model.Nombre,
                Cantidad = model.Cantidad,
                UnidadMedida = model.UnidadMedida
            };

            _context.Ingredientes.Add(ingrediente);

            await _context.SaveChangesAsync();

            TempData["MensajeExito"] =
                "¡Ingrediente agregado correctamente!";

            return RedirectToAction(nameof(Ingredientes));
        }

        
        // MOSTRAR FORMULARIO PARA EDITAR INGREDIENTE

        [HttpGet]
        public async Task<IActionResult> EditarIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes
                .FirstOrDefaultAsync(i => i.IngredienteId == id);

            if (ingrediente == null)
            {
                return NotFound();
            }

            var model = new EditarIngredienteViewModel
            {
                IngredienteId = ingrediente.IngredienteId,
                Nombre = ingrediente.Nombre,
                Cantidad = ingrediente.Cantidad,
                UnidadMedida = ingrediente.UnidadMedida
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarIngrediente(
            EditarIngredienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ingrediente = await _context.Ingredientes
                .FirstOrDefaultAsync(i => i.IngredienteId == model.IngredienteId);

            if (ingrediente == null)
            {
                return NotFound();
            }

            ingrediente.Nombre = model.Nombre;
            ingrediente.Cantidad = model.Cantidad;
            ingrediente.UnidadMedida = model.UnidadMedida;

            await _context.SaveChangesAsync();

            TempData["MensajeExito"] =
                "¡Ingrediente actualizado correctamente!";

            return RedirectToAction(nameof(Ingredientes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes
                .FirstOrDefaultAsync(i => i.IngredienteId == id);

            if (ingrediente == null)
            {
                return NotFound();
            }

            _context.Ingredientes.Remove(ingrediente);

            await _context.SaveChangesAsync();

            TempData["MensajeExito"] =
                "¡Ingrediente eliminado correctamente!";

            return RedirectToAction(nameof(Ingredientes));
        }

        public async Task<IActionResult> VerReceta(int id)
        {
            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.RecetaId == id);

            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        [HttpGet]
        public async Task<IActionResult> EditarReceta(int id)
        {
            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.RecetaId == id);

            if (receta == null)
            {
                return NotFound();
            }

            var model = new EditarRecetaViewModel
            {
                RecetaId = receta.RecetaId,
                Nombre = receta.Nombre,
                Descripcion = receta.Descripcion,
                Porciones = receta.Porciones,
                TiempoPreparacionMin = receta.TiempoPreparacionMin,
                TiempoCoccionMin = receta.TiempoCoccionMin
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarReceta(int id)
        {
            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.RecetaId == id);

            if (receta == null)
            {
                return NotFound();
            }

            _context.Recetas.Remove(receta);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MisRecetas));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarReceta(EditarRecetaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.RecetaId == model.RecetaId);

            if (receta == null)
            {
                return NotFound();
            }

            receta.Nombre = model.Nombre;
            receta.Descripcion = model.Descripcion;
            receta.Porciones = model.Porciones;
            receta.TiempoPreparacionMin = model.TiempoPreparacionMin;
            receta.TiempoCoccionMin = model.TiempoCoccionMin;
            receta.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["MensajeExito"] =
                "¡Receta actualizada correctamente!";

            return RedirectToAction("VerReceta", new { id = receta.RecetaId });
        }

        // MOSTRAR FORMULARIO
        [HttpGet]
        public IActionResult CrearReceta()
        {
            return View();
        }

        // GUARDAR RECETA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearReceta(CrearRecetaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var receta = new Receta
            {
                UsuarioId = 1,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Porciones = model.Porciones,
                TiempoPreparacionMin = model.TiempoPreparacionMin,
                TiempoCoccionMin = model.TiempoCoccionMin,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            _context.Recetas.Add(receta);

            await _context.SaveChangesAsync();

            TempData["MensajeExito"] =
                "¡Receta creada correctamente!";

            return RedirectToAction(nameof(MisRecetas));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}