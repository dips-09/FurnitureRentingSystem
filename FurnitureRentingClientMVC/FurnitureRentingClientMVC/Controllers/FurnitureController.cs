using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FurnitureRentingClientMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FurnitureRentingClientMVC.Controllers
{
    public class FurnitureController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(FurnitureController));

        public static string FurnitureApiURL = "https://localhost:44337/api/furniture";
        // GET: FurnitureController
        public async Task<IActionResult> Index()
        {
            _log4net.Info("List of furnitures displayed");
            if (HttpContext.Session.GetString("token") == null)
            {
                ViewBag.Message = "Please Login First";
                return View("Home", "Login");
            }
            List<Furniture> FurnitureList = new List<Furniture>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44337/api/furniture"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    FurnitureList = JsonConvert.DeserializeObject<List<Furniture>>(apiResponse);
                }
            }
            return View(FurnitureList);
        }

        // GET: FurnitureController/Details/5
        public async Task<ActionResult> Book(int id)
        {
            _log4net.Info("Booking method called");
            Furniture fur = new Furniture();
            using(var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync("https://localhost:44337/api/furniture/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fur = JsonConvert.DeserializeObject<Furniture>(apiResponse);
                }

            }


            Bill bill = new Bill() { BillOwner = HttpContext.Session.GetString("owner"),FurnitureName = fur.FurnitureName, BillAmount = fur.Price };

            return RedirectToAction("AddBill","Billing",bill);
        }

        // GET: FurnitureController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FurnitureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FurnitureController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FurnitureController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FurnitureController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FurnitureController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
