using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Models;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace AspNetCoreTodo.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }


    public async Task<Object> GetWeather()
    {
        HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:8080"),
        };

        var response = await httpClient.GetFromJsonAsync<List<WeatherForecast>>("WeatherForecast");
        return response;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

