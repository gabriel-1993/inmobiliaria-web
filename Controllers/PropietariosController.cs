using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class PropietariosController : Controller
{
    private readonly ILogger<PropietariosController> _logger;
    private RepositorioPropietario repo;

    public PropietariosController(ILogger<PropietariosController> logger)
    {
        _logger = logger;
        repo = new RepositorioPropietario();
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }


    public IActionResult Edicion(int? id)
    {
        if (id == null || id == 0)
        {
            // Crear un nuevo propietario
            return View(new Propietario());
        }
        else
        {
            // Obtener el propietario con el id proporcionado
            var propietario = repo.Obtener(id.Value); // Usa id.Value para obtener el valor int
            return View(propietario);
        }
    }

    [HttpPost]

    public IActionResult Guardar(int id, Propietario propietario)
    {
        if (id == 0)
        {
            repo.Alta(propietario);
        }
        else
        {
            repo.Modificar(propietario);
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


    public IActionResult Detalle(int id)
    {
        var propietario = repo.Obtener(id);
        if (propietario == null)
        {
            return NotFound();
        }
        return View(propietario);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
