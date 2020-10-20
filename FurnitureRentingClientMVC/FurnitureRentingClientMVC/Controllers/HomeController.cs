using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FurnitureRentingClientMVC.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FurnitureRentingClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(HomeController));

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private static string AuthApiURL = "https://localhost:44365/";

        public IActionResult Login()
        {
            _log4net.Info("Login page displayed");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            _log4net.Info("Post Login is called");
            User usr = new User();
            user.FullName = "rentuser";
            using(var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44365/api/Token/", content))
                {
                    
                    if(!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Please Enter valid credentials";
                        return View("Login");
                    }
                    string token = await response.Content.ReadAsStringAsync();

                    
                    usr = JsonConvert.DeserializeObject<User>(token);
                    string userName = user.UserId;
                    TempData["token"] = token;
                    HttpContext.Session.SetString("token", token);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                    HttpContext.Session.SetString("owner", userName);
                }
            }

            return RedirectToAction("Index", "Furniture");
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
