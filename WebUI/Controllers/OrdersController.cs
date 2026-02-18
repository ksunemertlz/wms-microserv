using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly HttpClient _http;

        public OrdersController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var json = await _http.GetStringAsync(
                "http://localhost:5170/api/orders"
            );

            ViewBag.Data = json;
            return View();
        }
    }
}
