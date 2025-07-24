using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class PropietariosController : Controller
{
    private readonly ILogger<PropietariosController> _logger;
    private RepositorioPropietario repositorioPropietario;
    private RepositorioInmueble repositorioInmueble;

    public PropietariosController(ILogger<PropietariosController> logger, RepositorioPropietario repositorioPropietario, RepositorioInmueble repositorioInmueble)
    {
        _logger = logger;
        this.repositorioPropietario = repositorioPropietario;
        this.repositorioInmueble = repositorioInmueble;
    }

    [Authorize]
    public IActionResult Index()
    {
        if (TempData.ContainsKey("Mensaje"))
            ViewBag.Mensaje = TempData["Mensaje"];
        var lista = repositorioPropietario.ObtenerTodos();
        return View(lista);
    }

    [Authorize]
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
            var propietario = repositorioPropietario.Obtener(id.Value); // Usa id.Value para obtener el valor int
            return View(propietario);
        }
    }

    [Authorize]
    [HttpPost]
    public IActionResult Guardar(int id, Propietario propietario)
    {
        if (!ModelState.IsValid) // Verifica si el modelo no es valido
        {
            return View("Edicion", propietario); // Retorna la vista con los errores de validacion
        }
        if (id == 0)
        {
            repositorioPropietario.Alta(propietario);
            TempData["Mensaje"] = "Propietario agregado correctamente.";
        }
        else
        {
            repositorioPropietario.Modificar(propietario);
            TempData["Mensaje"] = "Propietario editado correctamente.";
        }
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        repositorioPropietario.Baja(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "Administrador")]

    public IActionResult Habilitar(int id)
    {
        repositorioPropietario.Habilitar(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public IActionResult Detalle(int id)
    {
        var propietario = repositorioPropietario.Obtener(id);
        if (propietario == null)
        {
            return NotFound();
        }

        ViewBag.Inmuebles = repositorioInmueble.ObtenerPorPropietario(id);
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
