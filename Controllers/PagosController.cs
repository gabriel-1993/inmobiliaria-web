using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class PagosController : Controller
{
    private readonly ILogger<PagosController> _logger;
    private RepositorioPago repo;
    private RepositorioContrato repoContrato;

    private RepositorioAuditoria repoAuditoria;

    public PagosController(ILogger<PagosController> logger)
    {
        _logger = logger;
        repo = new RepositorioPago();
        repoContrato = new RepositorioContrato();
        repoAuditoria = new RepositorioAuditoria();
    }

    [Authorize]
    public IActionResult Index(int id)
    {
        if (TempData.ContainsKey("Mensaje"))
            ViewBag.Mensaje = TempData["Mensaje"];
        ViewBag.Contrato = repoContrato.Obtener(id);
        var lista = repo.ObtenerPorContrato(id);
        return View(lista);
    }

    [Authorize]
    public IActionResult Detalle(int id)
    {
        var pago = repo.Obtener(id);
        if (pago == null)
        {
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Auditorias = repoAuditoria.ObtenerPorPago(id);
        return View(pago);
    }

    [Authorize]
    public IActionResult Crear(int id)
    {
        //Guardar numero max de pago con estado 1 en la base, sumar 1 para nuevo pago
        ViewBag.NumeroPagoMax = repo.ObtenerNumeroPagoMax(id) + 1;
        ViewBag.Contrato = repoContrato.Obtener(id);
        return View("Crear", new Pago());
    }

    [Authorize]
    public IActionResult Edicion(int id)
    {
        var pago = repo.Obtener(id);
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
            int Id_Pago = repo.Agregar(pago);
            TempData["Mensaje"] = "Pago agregado correctamente.";

            //AGREGAR AUDITORIA POR NUEVO PAGO
            int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
            repoAuditoria.Agregar(Id_Usuario, null, Id_Pago, "Crear pago", DateTime.Now);
        }
        else
        {
            repo.Modificar(pago);
            TempData["Mensaje"] = "Pago editado correctamente.";
        }
        return RedirectToAction("Index", "Pagos", new { id = pago.Id_Contrato });
    }

    [Authorize]
    public IActionResult Eliminar(int id)
    {
        int? idContrato = (repo.Obtener(id))?.Id_Contrato;
        repo.Desactivar(id);

        //AGREGAR AUDITORIA POR ELIMINAR PAGO
        int Id_Usuario = int.Parse(User.Claims.First(x => x.Type == "IdUsuario").Value);
        repoAuditoria.Agregar(Id_Usuario, null, id, "Pago Anulado", DateTime.Now);

        return RedirectToAction("Index", "Pagos", new { id = idContrato });
    }

    [Authorize]
    public IActionResult Habilitar(int id)
    {
        int? idContrato = (repo.Obtener(id))?.Id_Contrato;
        repo.Activar(id);
        return RedirectToAction("Index", "Pagos", new { id = idContrato });
    }

}