using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeMachine.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeeMachine.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private string baseUrl = "http://localhost:8000";
        private readonly HttpClient _client;
        
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TakeMoney()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/takemoney");
            HttpResponseMessage response = await _client.SendAsync(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    string responseString = await response.Content.ReadAsStringAsync();
                    TempData["TakeMoneyNotification"] = responseString;
                    return RedirectToAction(nameof(Index));
                default:
                    return Error();
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}