using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("dashboard")]
    public IActionResult Dashboard(User user)
    {
        if (ModelState.IsValid)
        {
            // Se crea la Session
            HttpContext.Session.SetString("UserSession", user.Name);

            // Se crea un numero para inicializar el contador
            HttpContext.Session.SetInt32("UserNumber", 22);

            return RedirectToAction("Dashboard");
        }

        return View("Index");
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }

    // Ruta post para manipular los valores enviados desde Dashboard formulario de inputs 
    [HttpPost("update-number")]
    public IActionResult Update(OperationFunction operation)
    {
        // Obtengo el número pasado a la session
        int? currentNumber = HttpContext.Session.GetInt32("UserNumber");

        // Evalúa si el valor del botón pasado al modelo y si el numero actual de la session es distinto de null
        if (operation.Add != null && currentNumber != null)
        {
            // Al número de la session le adiciona 1
            HttpContext.Session.SetInt32("UserNumber", (int)currentNumber + 1);
        }
        else if (operation.Substract != null && currentNumber != null)
        {
            // Al número de la session le resta 1
            HttpContext.Session.SetInt32("UserNumber", (int)currentNumber - 1);
        }
        else if (operation.Multiply != null && currentNumber != null)
        {
            // Al número de la session le multiplica 2
            HttpContext.Session.SetInt32("UserNumber", (int)currentNumber * 2);
        }
        else if (operation.Random != null && currentNumber != null)
        {
            // Instancia la funcion Random y genera número aleatorio entre 1 y 10 inclusive
            Random rand = new Random();
            int randomNumber = rand.Next(1, 11);

            // Le asigno un número aleatorio al valor de la session
            HttpContext.Session.SetInt32("UserNumber", (int)currentNumber + randomNumber);
        }

        return RedirectToAction("Dashboard");
    }

    // Logout, elimina la session
    [HttpPost("logout")]
    public IActionResult Logout(User user)
    {
        if (user.Logout != null)
        {
            HttpContext.Session.Clear();
        }
        return RedirectToAction("Index");
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
