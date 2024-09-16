using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class InmueblesController : Controller
{
  private readonly ILogger<InmueblesController> _logger;
  private RepositorioInmueble repo;
  private RepositorioContrato repoC;
  private RepositorioPropietario repoPropietario;
  private RepositorioTipoInmueble repoTipoInmueble;


  public InmueblesController(ILogger<InmueblesController> logger)
  {
    _logger = logger;
    repo = new RepositorioInmueble();
    repoC = new RepositorioContrato();
    repoPropietario = new RepositorioPropietario();
    repoTipoInmueble = new RepositorioTipoInmueble();
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
    ViewBag.Propietarios = repoPropietario.ObtenerTodos(); // Asigna la lista de propietarios al ViewBag

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
    //VALIDAR CAMPOS
    if (!ModelState.IsValid) // Verifica si el modelo no es valido
    {
      ViewBag.Propietarios = repoPropietario.ObtenerTodos();
      ViewBag.TipoInmueble = repoTipoInmueble.ObtenerTodos();
      return View("Edicion", inmueble); // Retorna la vista con los errores de validacion
    }

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