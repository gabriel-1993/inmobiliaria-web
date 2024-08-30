using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class ContratosController : Controller
{
    private readonly ILogger<ContratosController> _logger;

    private RepositorioContrato repoContrato;

    //  REPOSITORIOS PARA MOSTRAR DATOS ESPECIFICOS DEL CONTRATO: DUEÑO,INQUILINO,PROPIEDAD(sino solo tenemos el id)
    //Se recuperan datos en Views--Contratos--Index--linea 3



    public ContratosController(ILogger<ContratosController> logger)
    {
        _logger = logger;
        repoContrato = new RepositorioContrato();

    }

    public IActionResult Index()
    {
        var contratos = repoContrato.ObtenerTodos(); // Lista de Contratos
        return View(contratos);
    }


    public IActionResult Edicion(int? id)

    {
        if (id == null || id == 0)
        {
            // Crear un nuevo contrato
            return View(new Contrato());
        }
        else
        {
            // Obtener el contrato con el id proporcionado
            var contrato = repoContrato.Obtener(id.Value); // Usa id.Value para obtener el valor int
            return View(contrato);
        }
    }

    [HttpPost]

    public IActionResult Guardar(int id, Contrato contrato)
    {
        if (id == 0)
        {
            repoContrato.Alta(contrato);
        }
        else
        {
            repoContrato.Modificar(contrato);
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Eliminar(int id)
    {
        repoContrato.Baja(id);
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Habilitar(int id)
    {
        repoContrato.Habilitar(id);
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Detalle(int id)
    {
        var contrato = repoContrato.Obtener(id);
        if (contrato == null)
        {
            return NotFound();
        }
        return View(contrato);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
