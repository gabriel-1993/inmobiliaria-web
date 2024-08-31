using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class InmueblesController : Controller
{
  private readonly ILogger<InmueblesController> _logger;
  private RepositorioInmueble repo;

  public InmueblesController(ILogger<InmueblesController> logger)
  {
    _logger = logger;
    repo = new RepositorioInmueble();
  }

  public IActionResult Index()
  {
    var lista = repo.ObtenerTodos();
    return View(lista);
  }

  public IActionResult Detalle(int id)
  {
    var inmueble = repo.Obtener(id);
    if (inmueble == null)
    {
      return RedirectToAction(nameof(Index));
    }
    return View(inmueble);
  }

  public IActionResult Edicion(int id)
  {
    var repoPropietario = new RepositorioPropietario(); // Asegúrate de tener un repositorio para los propietarios
    ViewBag.Propietario = repoPropietario.ObtenerTodos(); // Asigna la lista de propietarios al ViewBag

    // ACA PASAR TIPO INMUEBLE A LA VISTA

    if (id == 0)
    {
      return View(new Inmueble());
    }
    else
    {
      var inmueble = repo.Obtener(id);
      return View(inmueble);
    }
  }

  [HttpPost]
  public IActionResult Guardar(int id, Inmueble inmueble)
  {
    if (id == 0)
    {
      repo.Agregar(inmueble);
    }
    else
    {
      repo.Modificar(inmueble);
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