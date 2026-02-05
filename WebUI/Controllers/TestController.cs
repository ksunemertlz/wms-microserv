using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class TestController : Controller
    {
        private readonly HttpClient _httpClient;

        public TestController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetStringAsync("http://localhost:5170/api/test");
            ViewBag.Message = result;
            return View();
        }
    }
}
