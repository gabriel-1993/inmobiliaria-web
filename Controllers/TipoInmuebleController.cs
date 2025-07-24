using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class TipoInmuebleController : Controller
{
    private readonly ILogger<TipoInmuebleController> _logger;
    private RepositorioTipoInmueble repositorioTipoInmueble;

    public TipoInmuebleController(ILogger<TipoInmuebleController> logger, RepositorioTipoInmueble repositorioTipoInmueble)
    {
        _logger = logger;
        this.repositorioTipoInmueble = repositorioTipoInmueble;
    }

    [Authorize]

    public IActionResult Index()
    {
        if (TempData.ContainsKey("Mensaje"))
            ViewBag.Mensaje = TempData["Mensaje"];
        var lista = repositorioTipoInmueble.ObtenerTodos();
        return View(lista);
    }

    [Authorize]
    public IActionResult Detalle(int id)
    {
        var tipoInmueble = repositorioTipoInmueble.Obtener(id);
        if (tipoInmueble == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(tipoInmueble);
    }

    [Authorize]

    public IActionResult Edicion(int id)
    {
        if (id == 0)
        {
            return View(new TipoInmueble());
        }
        else
        {
            var tipoInmueble = repositorioTipoInmueble.Obtener(id);
            return View(tipoInmueble);
        }
    }

    [Authorize]
    [HttpPost]
    public IActionResult Agregar(TipoInmueble tipoInmueble)
    {
        repositorioTipoInmueble.Agregar(tipoInmueble);
        return RedirectToAction("Edicion", "Inmuebles");
    }

    [Authorize]
    [HttpPost]
    public IActionResult Guardar(int id, TipoInmueble tipoInmueble)
    {

        if (!ModelState.IsValid) // Verifica si el modelo no es valido
        {
            return View("Edicion", tipoInmueble); // Retorna la vista con los errores de validacion
        }
        if (id == 0)
        {
            repositorioTipoInmueble.Agregar(tipoInmueble);
            TempData["Mensaje"] = "Tipo de inmueble agregado correctamente.";
        }
        else
        {
            repositorioTipoInmueble.Modificar(tipoInmueble);
            TempData["Mensaje"] = "Tipo de inmueble editado correctamente.";
        }
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        repositorioTipoInmueble.Desactivar(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "Administrador")]

    public IActionResult Activar(int id)
    {
        repositorioTipoInmueble.Activar(id);
        return RedirectToAction(nameof(Index));
    }

}