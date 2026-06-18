using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _http;

    public HomeController(
        ILogger<HomeController> logger,
        IHttpClientFactory factory)
    {
        _logger = logger;
        _http = factory.CreateClient();
    }

    public async Task<IActionResult> Index()
    {
        var lowStock = await _http.GetFromJsonAsync<List<object>>(
            "http://localhost:5170/api/stock/low");

        var model = new DashboardViewModel
        {
            LowStockCount = lowStock?.Count ?? 0
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}