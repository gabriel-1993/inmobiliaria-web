using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class TipoInmuebleController : Controller
{
    private readonly ILogger<TipoInmuebleController> _logger;
    private RepositorioTipoInmueble repo;

    public TipoInmuebleController(ILogger<TipoInmuebleController> logger)
    {
        _logger = logger;
        repo = new RepositorioTipoInmueble();
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }

    public IActionResult Detalle(int id)
    {
        var tipoInmueble = repo.Obtener(id);
        if (tipoInmueble == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(tipoInmueble);
    }

    public IActionResult Edicion(int id)
    {
        if (id == 0)
        {
            return View(new TipoInmueble());
        }
        else
        {
            var tipoInmueble = repo.Obtener(id);
            return View(tipoInmueble);
        }
    }

    [HttpPost]
    public IActionResult Agregar(TipoInmueble tipoInmueble)
    {
        repo.Agregar(tipoInmueble);
        return RedirectToAction("Edicion", "Inmuebles");
    }

    [HttpPost]
    public IActionResult Guardar(int id, TipoInmueble tipoInmueble)
    {
        if (id == 0)
        {
            repo.Agregar(tipoInmueble);
        }
        else
        {
            repo.Modificar(tipoInmueble);
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Eliminar(int id)
    {
        repo.Desactivar(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Activar(int id)
    {
        repo.Activar(id);
        return RedirectToAction(nameof(Index));
    }

}