using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FurnitureRentingClientMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FurnitureRentingClientMVC.Controllers
{
    public class BillingController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BillingController));
        // GET: BillingController
        public async Task<ActionResult> AddBillAsync(Bill bill)
        {
            _log4net.Info("Bill Addition ");
            Bill bil = new Bill();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bill), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44321/api/billing/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bil = JsonConvert.DeserializeObject<Bill>(apiResponse);
                }
            }
            return RedirectToAction("Details",bil);
        }

        // GET: BillingController/Details/5
        public ActionResult Details(Bill bill)
        {
            _log4net.Info("Bill Displayed");
            return View(bill);
        }

        // GET: BillingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillingController/Create
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

        // GET: BillingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BillingController/Edit/5
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

        // GET: BillingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BillingController/Delete/5
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
