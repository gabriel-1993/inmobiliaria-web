using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using System.Runtime.Intrinsics.X86;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class UsuariosController : Controller
{
    private readonly ILogger<UsuariosController> _logger;
    private RepositorioUsuario repo;

    public UsuariosController(ILogger<UsuariosController> logger)
    {
        _logger = logger;
        repo = new RepositorioUsuario();
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

    public IActionResult Guardar(int id, Usuario usuario)
    {
        if (id == 0)
        {
            // string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //                     password: u.Clave,
            //                     salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
            //                     prf: KeyDerivationPrf.HMACSHA1,
            //                     iterationCount: 1000,
            //                     numBytesRequested: 256 / 8));
            // u.Clave = hashed;
            repo.Agregar(usuario);
        }
        else
        {
            repo.Modificar(usuario);
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Login()
    {
        // var lista = repo.ObtenerTodos();
        // return View(lista);
        return View();
    }
}