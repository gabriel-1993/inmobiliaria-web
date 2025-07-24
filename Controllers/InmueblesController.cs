using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class InmueblesController : Controller
{
  private readonly ILogger<InmueblesController> _logger;
  private RepositorioInmueble repositorioInmueble;
  private RepositorioContrato repositorioContrato;
  private RepositorioPropietario repositorioPropietario;
  private RepositorioTipoInmueble repositorioTipoInmueble;


  public InmueblesController(ILogger<InmueblesController> logger, RepositorioInmueble repositorioInmueble, RepositorioContrato repositorioContrato, RepositorioPropietario repositorioPropietario, RepositorioTipoInmueble repositorioTipoInmueble)
  {
    _logger = logger;
    this.repositorioInmueble = repositorioInmueble;
    this.repositorioContrato = repositorioContrato;
    this.repositorioPropietario = repositorioPropietario;
    this.repositorioTipoInmueble = repositorioTipoInmueble;
  }

  [Authorize]
  public IActionResult Index()
  {
    if (TempData.ContainsKey("Mensaje"))
      ViewBag.Mensaje = TempData["Mensaje"];
    var lista = repositorioInmueble.ObtenerTodos();
    return View(lista);
  }


  [Authorize]
  public IActionResult FiltrarBusqueda()
  {
    if (TempData.ContainsKey("Mensaje"))
      ViewBag.Mensaje = TempData["Mensaje"];
    var lista = repositorioInmueble.ObtenerTodos();
    return View(lista);
  }

  [Authorize]
  public IActionResult Detalle(int id)
  {
    var inmueble = repositorioInmueble.Obtener(id);
    if (inmueble == null)
    {
      return RedirectToAction(nameof(Index));
    }
    ViewBag.Contratos = repositorioContrato.ObtenerPorInmueble(id);
    return View(inmueble);
  }

  [Authorize]
  public IActionResult Edicion(int id)
  {
    ViewBag.Propietarios = repositorioPropietario.ObtenerTodos(); // Asigna la lista de propietarios al ViewBag

    ViewBag.TipoInmueble = repositorioTipoInmueble.ObtenerTodos();

    if (id == 0)
    {
      return View(new Inmueble());
    }
    else
    {
      var inmueble = repositorioInmueble.Obtener(id);
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
      ViewBag.Propietarios = repositorioPropietario.ObtenerTodos();
      ViewBag.TipoInmueble = repositorioTipoInmueble.ObtenerTodos();
      return View("Edicion", inmueble); // Retorna la vista con los errores de validacion
    }

    if (id == 0)
    {
      repositorioInmueble.Agregar(inmueble);
      TempData["Mensaje"] = "Inmueble agregado correctamente.";
    }
    else
    {
      repositorioInmueble.Modificar(inmueble);
      TempData["Mensaje"] = "Inmueble editado correctamente.";
    }
    return RedirectToAction(nameof(Index));
  }


  [Authorize(Policy = "Administrador")]
  public IActionResult Eliminar(int id)
  {
    repositorioInmueble.Desactivar(id);
    return RedirectToAction(nameof(Index));
  }

  [Authorize(Policy = "Administrador")]
  public IActionResult Activar(int id)
  {
    repositorioInmueble.Activar(id);
    return RedirectToAction(nameof(Index));
  }

  [Authorize]
  public IActionResult FiltrarContratosFechas(FiltrarFechaView filtrarFechaView)
  {

    var lista = repositorioInmueble.inmueblesDisponiblesPorFechas(filtrarFechaView.fechaDesde, filtrarFechaView.fechaHasta);
    return View("Index", lista);

  }

}