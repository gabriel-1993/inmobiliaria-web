using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class InmueblesController : Controller
{
  private readonly ILogger<InmueblesController> _logger;
  private RepositorioInmueble repo;
  private RepositorioContrato repoC;
  public InmueblesController(ILogger<InmueblesController> logger)
  {
    _logger = logger;
    repo = new RepositorioInmueble();
    repoC = new RepositorioContrato();
  }

    [Authorize]
  public IActionResult Index()
  {
    if (TempData.ContainsKey("Mensaje"))
        ViewBag.Mensaje = TempData["Mensaje"];
    var lista = repo.ObtenerTodos();
    return View(lista);
  }

    [Authorize]
  public IActionResult Detalle(int id)
  {
    var inmueble = repo.Obtener(id);
    if (inmueble == null)
    {
      return RedirectToAction(nameof(Index));
    }
    ViewBag.Contratos = repoC.ObtenerPorInmueble(id);
    return View(inmueble);
  }

    [Authorize]
  public IActionResult Edicion(int id)
  {
    var repoPropietario = new RepositorioPropietario(); // Asegúrate de tener un repositorio para los propietarios
    ViewBag.Propietario = repoPropietario.ObtenerTodos(); // Asigna la lista de propietarios al ViewBag

    var repoTipoInmueble = new RepositorioTipoInmueble();
    ViewBag.TipoInmueble = repoTipoInmueble.ObtenerTodos();

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

    [Authorize]
  [HttpPost]
  public IActionResult Guardar(int id, Inmueble inmueble)
  {
    if (id == 0)
    {
      repo.Agregar(inmueble);
      TempData["Mensaje"] = "Inmueble agregado correctamente.";
    }
    else
    {
      repo.Modificar(inmueble);
      TempData["Mensaje"] = "Inmueble editado correctamente.";
    }
    return RedirectToAction(nameof(Index));
  }


    [Authorize(Policy = "Administrador")]
  public IActionResult Eliminar(int id)
  {
    repo.Desactivar(id);
    return RedirectToAction(nameof(Index));
  }

    [Authorize(Policy = "Administrador")]
  public IActionResult Activar(int id)
  {
    repo.Activar(id);
    return RedirectToAction(nameof(Index));
  }

}