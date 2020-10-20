using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurnitureBillingAPI.Model;
using FurnitureBillingAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureBillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BillingController));
        private readonly IBillingRepo _context;

        public BillingController(IBillingRepo context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostBill([FromBody]ClsBill bill)
        {
            _log4net.Info("Post method is called!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tempbill = _context.AddBill(bill);

            return Ok(tempbill);
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            _log4net.Info("Get by id is called!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tempbill = _context.GetBillById(id);
            _log4net.Info("Data of the id returned!");

            if (tempbill == null)
            {
                return NotFound();
            }

            return Ok(tempbill);
        }
    }
}
