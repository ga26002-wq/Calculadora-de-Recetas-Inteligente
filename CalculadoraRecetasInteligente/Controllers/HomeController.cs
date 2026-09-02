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

        public async Task<IActionResult> Index()
        {
            var totalRecetas = await _context.Recetas.CountAsync();
            var totalIngredientes = await _context.Ingredientes.CountAsync();

            ViewBag.TotalRecetas = totalRecetas;
            ViewBag.TotalIngredientes = totalIngredientes;

            ViewBag.AjustesRealizados =
    HttpContext.Session.GetInt32("AjustesRealizados") ?? 0;

            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre") ?? "Chef";

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

        // MOSTRAR AJUSTE INTELIGENTE
        [HttpGet]
        public async Task<IActionResult> AjusteInteligente()
        {
            var recetas = await _context.Recetas
                .OrderBy(r => r.Nombre)
                .ToListAsync();

            ViewBag.Recetas = recetas;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjustarReceta(
    int recetaId,
    int nuevasPorciones)
        {
            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.RecetaId == recetaId);

            if (receta == null)
            {
                return NotFound();
            }

            if (nuevasPorciones <= 0)
            {
                TempData["MensajeError"] =
                    "Las porciones deben ser mayores que cero.";

                return RedirectToAction(nameof(AjusteInteligente));
            }

            var ingredientes = await _context.Ingredientes
                .Where(i => i.RecetaId == recetaId)
                .OrderBy(i => i.Nombre)
                .ToListAsync();

            decimal factor = (decimal)nuevasPorciones / receta.Porciones;

            var resultado = new ResultadoAjusteViewModel
            {
                NombreReceta = receta.Nombre,
                PorcionesOriginales = receta.Porciones,
                NuevasPorciones = nuevasPorciones
            };

            foreach (var ingrediente in ingredientes)
            {
                resultado.Ingredientes.Add(new IngredienteAjustadoViewModel
                {
                    Nombre = ingrediente.Nombre,
                    CantidadOriginal = ingrediente.Cantidad,
                    NuevaCantidad = ingrediente.Cantidad * factor,
                    UnidadMedida = ingrediente.UnidadMedida
                });
            }

            var ajustesRealizados = HttpContext.Session.GetInt32("AjustesRealizados") ?? 0;

            HttpContext.Session.SetInt32(
                "AjustesRealizados",
                ajustesRealizados + 1
            );

            return View("ResultadoAjuste", resultado);
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

        // MOSTRAR FORMULARIO PARA AGREGAR INGREDIENTE A UNA RECETA
        [HttpGet]
        public async Task<IActionResult> AgregarIngredienteReceta(int recetaId)
        {
            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.RecetaId == recetaId);

            if (receta == null)
            {
                return NotFound();
            }

            ViewBag.NombreReceta = receta.Nombre;

            var model = new CrearIngredienteRecetaViewModel
            {
                RecetaId = recetaId
            };

            return View(model);
        }


        // GUARDAR INGREDIENTE EN LA RECETA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarIngredienteReceta(
    CrearIngredienteRecetaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var recetaError = await _context.Recetas
                    .FirstOrDefaultAsync(r => r.RecetaId == model.RecetaId);

                ViewBag.NombreReceta = recetaError?.Nombre;

                return View(model);
            }

            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.RecetaId == model.RecetaId);

            if (receta == null)
            {
                return NotFound();
            }

            var ingrediente = new Ingrediente
            {
                Nombre = model.Nombre,
                Cantidad = model.Cantidad,
                UnidadMedida = model.UnidadMedida,
                RecetaId = model.RecetaId
            };

            _context.Ingredientes.Add(ingrediente);

            await _context.SaveChangesAsync();

            TempData["MensajeExito"] =
                "¡Ingrediente agregado correctamente!";

            return RedirectToAction(
                nameof(VerReceta),
                new { id = model.RecetaId });
        }
        public async Task<IActionResult> VerReceta(int id)
        {
            var receta = await _context.Recetas
                .Include(r => r.Ingredientes)
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

        // ================================
        // MI PERFIL
        // ================================

        [HttpGet]
        public async Task<IActionResult> MiPerfil()
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuarioId == 1);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // MOSTRAR FORMULARIO PARA EDITAR PERFIL
        [HttpGet]
        public async Task<IActionResult> EditarPerfil()
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuarioId == 1);

            if (usuario == null)
            {
                return NotFound();
            }

            var model = new EditarPerfilViewModel
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPerfil(EditarPerfilViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuarioId == model.UsuarioId);

            if (usuario == null)
            {
                return NotFound();
            }

            // VERIFICAR SI EL CORREO YA PERTENECE A OTRO USUARIO
            var correoExiste = await _context.Usuarios
                .AnyAsync(u =>
                    u.Correo == model.Correo &&
                    u.UsuarioId != model.UsuarioId);

            if (correoExiste)
            {
                ModelState.AddModelError(
                    "Correo",
                    "Este correo ya está registrado por otro usuario.");

                return View(model);
            }

            // ACTUALIZAR DATOS
            usuario.Nombre = model.Nombre;
            usuario.Apellido = model.Apellido;
            usuario.Correo = model.Correo;

            await _context.SaveChangesAsync();

            TempData["MensajeExito"] =
                "¡Perfil actualizado correctamente!";

            return RedirectToAction(nameof(MiPerfil));
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