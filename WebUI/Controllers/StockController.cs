using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using System.Data;
using ClosedXML.Excel;

namespace WebUI.Controllers
{
    public class StockController : BaseController
    {
        private readonly HttpClient _http;

        public StockController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var stock = await _http.GetFromJsonAsync<List<StockItem>>("http://localhost:5170/api/stock");
            var products = await _http.GetFromJsonAsync<List<Product>>("http://localhost:5170/api/products");
            
            if (stock != null && products != null)
            {
                foreach (var item in stock)
                {
                    var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                    item.ProductName = product?.Name ?? "—";
                }
            }
            
            return View(stock ?? new List<StockItem>());
        }



public async Task<IActionResult> ExportToExcel()
{
    var stock = await _http.GetFromJsonAsync<List<StockItem>>("http://localhost:5170/api/stock");
    var products = await _http.GetFromJsonAsync<List<Product>>("http://localhost:5170/api/products");
    
    using var workbook = new XLWorkbook();
    var worksheet = workbook.Worksheets.Add("Остатки на складе");
    
    worksheet.Cell(1, 1).Value = "ID товара";
    worksheet.Cell(1, 2).Value = "Название";
    worksheet.Cell(1, 3).Value = "Остаток";
    
    int row = 2;
    if (stock != null && products != null)
    {
        foreach (var item in stock)
        {
            var productName = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name ?? "—";
            worksheet.Cell(row, 1).Value = item.ProductId;
            worksheet.Cell(row, 2).Value = productName;
            worksheet.Cell(row, 3).Value = item.Quantity;
            row++;
        }
    }
    
    worksheet.Columns().AdjustToContents();
    
    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    var fileName = $"StockReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
    
    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
}
    }
}