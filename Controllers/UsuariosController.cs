using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Data;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class UsuariosController : Controller
{
    private readonly ILogger<UsuariosController> _logger;

    private RepositorioUsuario repo;

    private readonly IConfiguration configuration;

    public UsuariosController(ILogger<UsuariosController> logger, IConfiguration configuration)
    {
        _logger = logger;
        repo = new RepositorioUsuario();
        this.configuration = configuration;

    }



    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }

    public IActionResult Edicion(int id)
    {
        if (id == 0)
        {
            return View(new Usuario());
        }
        else
        {
            var usuario = repo.Obtener(id);
            return View(usuario);
        }
    }


    public IActionResult Detalle(int id)
    {
        var usuario = repo.Obtener(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }


    public IActionResult Guardar(int id, Usuario usuario)
    {

        if (!string.IsNullOrEmpty(usuario.Clave) && id != 0 ) // Solo hashear si la clave no es nula ni vacía
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: usuario.Clave,
                salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));

            usuario.Clave = hashed; // Asignar la clave hasheada al usuario
        }

        if (id == 0)
        {

            repo.Agregar(usuario);

        }
        else
        {
            repo.Modificar(usuario);
        }


        return RedirectToAction(nameof(Index));
    }


    public IActionResult Eliminar(int id)
    {
        repo.Baja(id);
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Habilitar(int id)
    {
        repo.Habilitar(id);
        return RedirectToAction(nameof(Index));
    }



    public IActionResult Login()
    {
        // var lista = repo.ObtenerTodos();
        // return View(lista);
        return View();
    }

}