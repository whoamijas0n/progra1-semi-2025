using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fiexpress.Data;
using fiexpress.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(MyDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Buscar usuario por username
                var usuario = await _context.Usuarios
                    .Include(u => u.Empleado)
                        .ThenInclude(e => e.Rol)
                    .Include(u => u.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Include(u => u.Empleado.Supervisor)
                    .FirstOrDefaultAsync(u => u.username == request.Username && u.activo);

                if (usuario == null)
                {
                    return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
                }

                // TODO: Implementar hash de contraseña (bcrypt, etc.)
                if (usuario.password != request.Password)
                {
                    return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
                }

                // Verificar que el empleado esté activo
                if (!usuario.Empleado.activo)
                {
                    return Unauthorized(new { mensaje = "Empleado inactivo" });
                }

                // Actualizar último login
                usuario.ultimo_login = DateTime.Now;
                await _context.SaveChangesAsync();

                // Crear claims para la sesión
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.idUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.username),
                    new Claim(ClaimTypes.Email, usuario.Empleado.email),
                    new Claim("EmpleadoId", usuario.Empleado.idEmpleado.ToString()),
                    new Claim("NombreCompleto", usuario.Empleado.nombre),
                    new Claim(ClaimTypes.Role, usuario.Empleado.Rol.nombre)
                };

                // Verificar si es supervisor
                if (usuario.Empleado.Supervisor != null)
                {
                    claims.Add(new Claim("EsSupervisor", "true"));
                    claims.Add(new Claim("SupervisorId", usuario.Empleado.Supervisor.idSupervisor.ToString()));
                }

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in
                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = request.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });

                // Registrar auditoría
                var auditoria = new Auditoria
                {
                    idAuditoriaUsuario = usuario.idUsuario,
                    accion = "LOGIN",
                    entidad_afectada = "Usuario",
                    fecha_accion = DateTime.Now,
                    ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                    descripcion = $"Login exitoso de {usuario.username}"
                };
                _context.Auditorias.Add(auditoria);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = "Login exitoso",
                    usuario = new
                    {
                        id = usuario.idUsuario,
                        username = usuario.username,
                        nombre = usuario.Empleado.nombre,
                        email = usuario.Empleado.email,
                        rol = usuario.Empleado.Rol.nombre,
                        esSupervisor = usuario.Empleado.Supervisor != null,
                        departamento = usuario.Empleado.Departamento.nombre
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en login");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    // Registrar auditoría
                    var auditoria = new Auditoria
                    {
                        idAuditoriaUsuario = int.Parse(userId),
                        accion = "LOGOUT",
                        entidad_afectada = "Usuario",
                        fecha_accion = DateTime.Now,
                        ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                        descripcion = "Logout del sistema"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                await HttpContext.SignOutAsync("CookieAuth");
                return Ok(new { mensaje = "Logout exitoso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en logout");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        // GET: api/auth/verificar
        [HttpGet("verificar")]
        public IActionResult Verificar()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Ok(new { autenticado = true });
            }
            return Unauthorized(new { autenticado = false });
        }

        // GET: api/auth/usuario-actual
        [HttpGet("usuario-actual")]
        [Authorize]
        public async Task<IActionResult> UsuarioActual()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var usuario = await _context.Usuarios
                    .Include(u => u.Empleado)
                        .ThenInclude(e => e.Rol)
                    .Include(u => u.Empleado.Departamento)
                    .Include(u => u.Empleado.Supervisor)
                    .FirstOrDefaultAsync(u => u.idUsuario == int.Parse(userId));

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                return Ok(new
                {
                    id = usuario.idUsuario,
                    username = usuario.username,
                    nombre = usuario.Empleado.nombre,
                    email = usuario.Empleado.email,
                    telefono = usuario.Empleado.telefono,
                    rol = usuario.Empleado.Rol.nombre,
                    departamento = usuario.Empleado.Departamento.nombre,
                    esSupervisor = usuario.Empleado.Supervisor != null,
                    foto_url = usuario.Empleado.foto_url
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario actual");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }
    }

    // DTOs
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}