﻿using Client.Models;
using Client.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Models.ViewModels.Account;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IManagerLogisticService _managerLogisticService;

        public HomeController(ILogger<HomeController> logger, IManagerLogisticService managerLogisticService)
        {
            _logger = logger;
            _managerLogisticService = managerLogisticService;
        }

        public async Task<IActionResult> Index()
        {
            List<GetAccountVM>? list = new();

            ResponseVM? response = await _managerLogisticService.GetAllManagerLogisticsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<GetAccountVM>>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }


            return View();
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
}
