using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class PagosController : Controller
{
    private readonly ILogger<PagosController> _logger;
    private RepositorioPago repositorioPago;
    private RepositorioContrato repositorioContrato;

    private RepositorioAuditoria repositorioAuditoria;

  public PagosController(ILogger<PagosController> logger, RepositorioPago repositorioPago, RepositorioContrato repositorioContrato, RepositorioAuditoria repositorioAuditoria)
    {
        _logger = logger;
        this.repositorioPago = repositorioPago;
        this.repositorioContrato = repositorioContrato;
        this.repositorioAuditoria = repositorioAuditoria;
    }

    [Authorize]
    public IActionResult Index(int id)
    {
        if (TempData.ContainsKey("Mensaje"))
            ViewBag.Mensaje = TempData["Mensaje"];
        ViewBag.Contrato = repositorioContrato.Obtener(id);
        var lista = repositorioPago.ObtenerPorContrato(id);
        return View(lista);
    }

    [Authorize]
    public IActionResult Detalle(int id)
    {
        var pago = repositorioPago.Obtener(id);
        if (pago == null)
        {
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Auditorias = repositorioAuditoria.ObtenerPorPago(id);
        return View(pago);
    }

    [Authorize]
    public IActionResult Crear(int id)
    {
        //Guardar numero max de pago con estado 1 en la base, sumar 1 para nuevo pago
        ViewBag.NumeroPagoMax = repositorioPago.ObtenerNumeroPagoMax(id) + 1;
        ViewBag.Contrato = repositorioContrato.Obtener(id);
        return View("Crear", new Pago());
    }

    [Authorize]
    public IActionResult Edicion(int id)
    {
        var pago = repositorioPago.Obtener(id);
        return View(pago);
    }

    [Authorize]
    public IActionResult Guardar(int id, Pago pago)
    {

        if (!ModelState.IsValid) // Verifica si el modelo no es valido
        {
            return View("Edicion", pago); // Retorna la vista con los errores de validacion
        }

        if (id == 0)
        {
            int Id_Pago = repositorioPago.Agregar(pago);
            TempData["Mensaje"] = "Pago agregado correctamente.";

            //AGREGAR AUDITORIA POR NUEVO PAGO
            int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
            repositorioAuditoria.Agregar(Id_Usuario, null, Id_Pago, "Crear pago", DateTime.Now);
        }
        else
        {
            repositorioPago.Modificar(pago);
            TempData["Mensaje"] = "Pago editado correctamente.";
        }
        return RedirectToAction("Index", "Pagos", new { id = pago.Id_Contrato });
    }

    [Authorize]
    public IActionResult Eliminar(int id)
    {
        int? idContrato = (repositorioPago.Obtener(id))?.Id_Contrato;
        repositorioPago.Desactivar(id);

        //AGREGAR AUDITORIA POR ELIMINAR PAGO
        int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
        repositorioAuditoria.Agregar(Id_Usuario, null, id, "Pago Anulado", DateTime.Now);

        return RedirectToAction("Index", "Pagos", new { id = idContrato });
    }

    [Authorize]
    public IActionResult Habilitar(int id)
    {
        int? idContrato = (repositorioPago.Obtener(id))?.Id_Contrato;
        repositorioPago.Activar(id);
        return RedirectToAction("Index", "Pagos", new { id = idContrato });
    }

}