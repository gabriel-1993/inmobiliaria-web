using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class ContratosController : Controller
{
    private readonly ILogger<ContratosController> _logger;
    private RepositorioContrato repositorioContrato;
    private RepositorioInquilino repositorioInquilino;
    private RepositorioInmueble repositorioInmueble;
    private RepositorioAuditoria repositorioAuditoria;
    private RepositorioPago repositorioPago;

  //  REPOSITORIOS PARA MOSTRAR DATOS ESPECIFICOS DEL CONTRATO: DUEÑO,INQUILINO,PROPIEDAD(sino solo tenemos el id)
  //Se recuperan datos en Views--Contratos--Index--linea 3

    public ContratosController(ILogger<ContratosController> logger, RepositorioContrato repositorioContrato, RepositorioInquilino repositorioInquilino, RepositorioInmueble repositorioInmueble, RepositorioAuditoria repositorioAuditoria, RepositorioPago repositorioPago)
    {
        _logger = logger;
        this.repositorioContrato = repositorioContrato;
        this.repositorioInquilino = repositorioInquilino;
        // Dentro de cada <Inmueble> tenemos <Propietario> 
        this.repositorioInmueble = repositorioInmueble;
        this.repositorioAuditoria = repositorioAuditoria;
        this.repositorioPago = repositorioPago;
    }

    [Authorize]
    public IActionResult Index()
    {
        if (TempData.ContainsKey("Mensaje"))
            ViewBag.Mensaje = TempData["Mensaje"];
        var contratos = repositorioContrato.ObtenerTodos(); // Lista de Contratos
        return View(contratos);
    }

    [Authorize]
    public IActionResult Edicion(int? id)

    {
        ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();

        if (id == null || id == 0)
        {
            // Crear un nuevo contrato
            return View(new Contrato());
        }
        else
        {
            // Obtener el contrato con el id proporcionado
            var contrato = repositorioContrato.Obtener(id.Value); // Usa id.Value para obtener el valor int
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
        if (!ModelState.IsValid) // Verifica si el modelo no es valido
        {
            ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
            ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
            return View("Edicion", contrato); // Retorna la vista con los errores de validacion
        }
        if (id == 0)
        {
            int Id_Contrato = repositorioContrato.Alta(contrato);
            repositorioInmueble.NoDisponible(contrato.Id_Inmueble);
            TempData["Mensaje"] = "Contrato agregado correctamente.";

            //AGREGAR AUDITORIA POR CREAR CONTRATO
            int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
            repositorioAuditoria.Agregar(Id_Usuario, Id_Contrato, null, "Crear Contrato", DateTime.Now);
        }
        else
        {
            if (contrato.FechaTerminacion != null)
            {
                if (contrato.Multa > 0)
                {

                    int num = repositorioPago.ObtenerNumeroPagoMax(contrato.Id) + 1;
                    repositorioPago.Agregar(new Pago
                    {
                        Id = 0,
                        Id_Contrato = contrato.Id,
                        NumeroPago = num,
                        FechaPago = DateTime.Now,
                        Detalle = "Multa Pagada",
                        Importe = contrato.Multa,
                        Estado = true
                    });
                }
                repositorioInmueble.SiDisponible(contrato.Id_Inmueble);

                //AGREGAR AUDITORIA POR FINALIZAR CONTRATO
                int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
                repositorioAuditoria.Agregar(Id_Usuario, id, null, "Contrato Finalizado", DateTime.Now);
            }

            repositorioContrato.Modificar(contrato);
            TempData["Mensaje"] = "Contrato editado correctamente.";
        }
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        repositorioContrato.Baja(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "Administrador")]
    public IActionResult Habilitar(int id)
    {
        repositorioContrato.Habilitar(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public IActionResult Detalle(int id)
    {
        var contrato = repositorioContrato.Obtener(id);
        if (contrato == null)
        {
            return NotFound();
        }

        ViewBag.Auditorias = repositorioAuditoria.ObtenerPorContrato(id);
        return View(contrato);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public IActionResult Renovar(int id)
    {
        ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
        var contrato = repositorioContrato.Obtener(id);
        if (contrato == null)
        {
            return NotFound();
        }

        return View("Renovar", contrato);
    }

    [Authorize]
    public IActionResult CrearRenovacion(Contrato contrato)
    {
        int Id_Contrato = repositorioContrato.Alta(contrato);
        repositorioInmueble.NoDisponible(contrato.Id_Inmueble);
        TempData["Mensaje"] = "Contrato Renovado correctamente.";

        //AGREGAR AUDITORIA POR CREAR CONTRATO
        int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
        repositorioAuditoria.Agregar(Id_Usuario, Id_Contrato, null, "Renovar Contrato", DateTime.Now);
        return RedirectToAction(nameof(Index));
    }
    
}
