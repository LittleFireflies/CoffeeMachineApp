﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeMachine.Models;
using Microsoft.AspNetCore.Http;

namespace CoffeeMachine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string baseUrl = "http://localhost:8000";
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/menus");
            HttpResponseMessage response = await _client.SendAsync(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    string responseString = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<Response<Menu[]>>(responseString);
                    List<Menu> menus = result.Data.ToList();
                    return View(menus);
                default:
                    return Error();
            }
        }

        public async Task<IActionResult> Order(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/order/" + id);
            HttpResponseMessage response = await _client.SendAsync(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    string responseString = await response.Content.ReadAsStringAsync();
                    TempData["Notification"] = responseString;
                    return RedirectToAction(nameof(Index));
                default:
                    return Error();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}