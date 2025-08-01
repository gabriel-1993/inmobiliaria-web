using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaVargasHuancaTorrez.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaVargasHuancaTorrez.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly RepositorioPropietario repositorioPropietario;
  private readonly RepositorioInmueble repositorioInmueble;
  private readonly RepositorioInquilino repositorioInquilino;
  private readonly RepositorioContrato repositorioContrato;

  public HomeController(ILogger<HomeController> logger, RepositorioPropietario repositorioPropietario, RepositorioInmueble repositorioInmueble, RepositorioInquilino repositorioInquilino, RepositorioContrato repositorioContrato)
  {
    _logger = logger;
    this.repositorioPropietario = repositorioPropietario;
    this.repositorioInmueble = repositorioInmueble;
    this.repositorioInquilino = repositorioInquilino;
    this.repositorioContrato = repositorioContrato;
  }

  [Authorize]
  public IActionResult Index()
  {
    ViewBag.Propietarios = repositorioPropietario.Cantidad();
    ViewBag.Inmuebles = repositorioInmueble.Cantidad();
    ViewBag.Inquilinos = repositorioInquilino.Cantidad();
    ViewBag.Contratos = repositorioContrato.Cantidad();
    return View();
  }

  public IActionResult Restringido()
  {
    return View();
  }


  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
