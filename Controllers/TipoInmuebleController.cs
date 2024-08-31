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
        return RedirectToAction("Edicion", "Inmuebles");
    }
}