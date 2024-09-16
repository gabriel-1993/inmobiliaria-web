using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class InquilinosController : Controller
{
  private readonly ILogger<InquilinosController> _logger;
  private RepositorioInquilino repo;

  public InquilinosController(ILogger<InquilinosController> logger)
  {
    _logger = logger;
    repo = new RepositorioInquilino();
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
    var inquilino = repo.Obtener(id);
    if (inquilino == null)
    {
      return RedirectToAction(nameof(Index));
    }
    return View(inquilino);
  }

  [Authorize]

  public IActionResult Editar(int id)
  {
    if (id == 0)
    {
      return View(new Inquilino());
    }
    else
    {
      var inquilino = repo.Obtener(id);
      return View(inquilino);
    }
  }

  [Authorize]
  [HttpPost]

  public IActionResult Guardar(int id, Inquilino inquilino)
  {
    if (!ModelState.IsValid) // Verifica si el modelo no es valido
    {
      return View("Editar", inquilino); // Retorna la vista con los errores de validacion
    }

    // Verificar si el DNI ya existe
    // var existeDni = repo.ObtenerTodos().Any(i => i.Dni == inquilino.Dni && i.Id != id);
    // if (existeDni)
    // {
    //   ModelState.AddModelError("Dni", "El DNI ingresado ya está registrado.");
    //   return View("Editar", inquilino); // Retorna con el mensaje de error
    // }

    if (id == 0)
    {
      repo.Agregar(inquilino);
      TempData["Mensaje"] = "Inquilino agregado correctamente.";

    }
    else
    {
      repo.Modificar(inquilino);
      TempData["Mensaje"] = "Inquilino editado correctamente.";

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