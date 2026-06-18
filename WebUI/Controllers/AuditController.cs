using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AuditController : BaseController
    {
        private readonly HttpClient _http;

        public AuditController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _http.GetFromJsonAsync<List<AuditLog>>(
                "http://localhost:5170/api/products/audit"
            );

            return View(logs);
        }
    }
}