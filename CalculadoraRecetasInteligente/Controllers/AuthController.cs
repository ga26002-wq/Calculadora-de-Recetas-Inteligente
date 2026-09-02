using CalculadoraRecetasInteligente.Data;
using CalculadoraRecetasInteligente.Models;
using CalculadoraRecetasInteligente.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraRecetasInteligente.Controllers
{
    public class AuthController : Controller
    {
        private readonly RecetasDbContext _context;

        public AuthController(RecetasDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.Correo == model.Correo &&
                    u.PasswordHash == model.Contrasena &&
                    u.Activo);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty,
                    "Correo o contraseña incorrectos.");

                return View(model);
            }

            // Guardamos datos básicos de la sesión
            HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetInt32("RolId", usuario.RolId);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/Registro
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // POST: /Auth/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Verificar si el correo ya existe
            bool correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo == model.Correo);

            if (correoExiste)
            {
                ModelState.AddModelError(
                    "Correo",
                    "Este correo electrónico ya está registrado."
                );

                return View(model);
            }

            // Crear nuevo usuario
            var usuario = new Usuario
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Correo = model.Correo,
                PasswordHash = model.Contrasena,
                RolId = 2,
                Activo = true,
                FechaRegistro = DateTime.Now
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            TempData["MensajeExito"] =
                "¡Cuenta creada correctamente! Ahora puedes iniciar sesión.";

            return RedirectToAction("Login");
        }
        // CERRAR SESIÓN
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Auth");
        }
    }
}