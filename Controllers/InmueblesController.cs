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

  public IActionResult Index(string estado)
{
    var lista = repo.ObtenerTodos();
    var repoPropietario = new RepositorioPropietario();

    // Crear un diccionario para mapear Id_propietario con los nombres
    var propietariosDict = repoPropietario.ObtenerTodos()
                                           .ToDictionary(p => p.Id, p => p.Nombre + " " + p.Apellido);

    // Asignar el nombre del propietario a cada inmueble
    foreach (var inmueble in lista)
    {
        if (propietariosDict.TryGetValue(inmueble.Id_propietario, out var nombrePropietario))
        {
            inmueble.Propietario = new Propietario { Nombre = nombrePropietario };
        }
    }

    // Filtrar los inmuebles según el estado
    if (estado == "activo")
    {
        lista = lista.Where(i => i.Estado).ToList();
    }
    else if (estado == "desactivado")
    {
        lista = lista.Where(i => !i.Estado).ToList();
    }

    return View(lista);
}

  public IActionResult Edicion(int id)
  {
    var repoPropietario = new RepositorioPropietario(); // Asegúrate de tener un repositorio para los propietarios
    ViewBag.Propietario = repoPropietario.ObtenerTodos(); // Asigna la lista de propietarios al ViewBag

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