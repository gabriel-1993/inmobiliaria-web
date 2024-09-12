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
    if(id == 0) 
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
    if(id == 0) 
    {
      repo.Agregar(inquilino);
    }
    else
    {
      repo.Modificar(inquilino);
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