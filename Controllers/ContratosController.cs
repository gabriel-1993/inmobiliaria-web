using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class ContratosController : Controller
{
    private readonly ILogger<ContratosController> _logger;

    private RepositorioContrato repoContrato;

    private RepositorioInquilino repoInquilino;

    private RepositorioInmueble repoInmueble;

    private RepositorioAuditoria repoAuditoria;

    //  REPOSITORIOS PARA MOSTRAR DATOS ESPECIFICOS DEL CONTRATO: DUEÑO,INQUILINO,PROPIEDAD(sino solo tenemos el id)
    //Se recuperan datos en Views--Contratos--Index--linea 3



    public ContratosController(ILogger<ContratosController> logger)
    {
        _logger = logger;
        repoContrato = new RepositorioContrato();
        repoInquilino = new RepositorioInquilino();
        // Dentro de cada <Inmueble> tenemos <Propietario> 
        repoInmueble = new RepositorioInmueble();
        repoAuditoria = new RepositorioAuditoria();
    }

    [Authorize]
    public IActionResult Index()
    {
        if (TempData.ContainsKey("Mensaje"))
            ViewBag.Mensaje = TempData["Mensaje"];
        var contratos = repoContrato.ObtenerTodos(); // Lista de Contratos
        return View(contratos);
    }

    [Authorize]
    public IActionResult Edicion(int? id)

    {
        ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repoInmueble.ObtenerTodos();

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

    [Authorize]
    public IActionResult CalcularMulta(DateTime fechaTerminacion, DateTime fechaInicio, DateTime fechaFin, double montoAlquiler)
    {
        double multa = 0;

        // Calcula la duración del contrato en días
        double duracionTotalDias = (fechaFin - fechaInicio).TotalDays;
        double diasHastaTerminacion = (fechaTerminacion - fechaInicio).TotalDays;

        if (fechaFin == fechaTerminacion)
        {
            return Json(new { multa = 0 });
        }

        // Verifica si se cumplió menos de la mitad del tiempo original de alquiler
        if (diasHastaTerminacion < duracionTotalDias / 2)
        {
            multa = montoAlquiler * 2;
        }
        else
        {
            multa = montoAlquiler;
        }

        // Aquí también podrías verificar si hay meses de alquiler adeudados

        return Json(new { multa = multa });
    }


    [HttpPost]
    [Authorize]
    public IActionResult Guardar(int id, Contrato contrato)
    {
        
        if (id == 0)
        {
            int Id_Contrato = repoContrato.Alta(contrato);
            repoInmueble.NoDisponible(contrato.Id_Inmueble);
            TempData["Mensaje"] = "Contrato agregado correctamente.";

            //AGREGAR AUDITORIA POR CREAR CONTRATO
            int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
            repoAuditoria.Agregar(Id_Usuario, Id_Contrato, null, "Crear Contrato", DateTime.Now);
        }
        else
        {
            if (contrato.FechaTerminacion != null)
            {
                repoInmueble.SiDisponible(contrato.Id_Inmueble);

                //AGREGAR AUDITORIA POR FINALIZAR CONTRATO
                int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
                repoAuditoria.Agregar(Id_Usuario, id, null, "Contrato Finalizado", DateTime.Now);
            }

            repoContrato.Modificar(contrato);
            TempData["Mensaje"] = "Contrato editado correctamente.";
        }
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        repoContrato.Baja(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "Administrador")]
    public IActionResult Habilitar(int id)
    {
        repoContrato.Habilitar(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public IActionResult Detalle(int id)
    {
        var contrato = repoContrato.Obtener(id);
        if (contrato == null)
        {
            return NotFound();
        }

        ViewBag.Auditorias =  repoAuditoria.ObtenerPorContrato(id);
        return View(contrato);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
