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

  public IActionResult Edicion(int id)
  {
    if(id == 0) 
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
  [HttpPost]
public IActionResult Guardar(int id_inmueble, Inmueble inmueble)
{
    if (id_inmueble == 0)
    {
        repo.Agregar(inmueble);
    }
    else
    {
        inmueble.Id_inmueble = id_inmueble; // Asegurarse de asignar el Id existente
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