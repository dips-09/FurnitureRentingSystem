using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurnitureDetailsAPI.Model;
using FurnitureDetailsAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        //public FurnitureContext _context;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(FurnitureController));
        private readonly IFurnitureManager _context;

        public FurnitureController(IFurnitureManager context)
        {
            _context = context;
        }

        [HttpGet]
        //[Route("api/[controller]")]
        public IEnumerable<Furniture> Get()
        {
            _log4net.Info("All Items Displayed!");
            return _context.GetAllFurniture();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _log4net.Info("Get by id is called!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var furniture = _context.GetFurnitureById(id);
            _log4net.Info("Data of the id returned!");

            if (furniture == null)
            {
                return NotFound();
            }

            return Ok(furniture);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Furniture fr)
        {
            _log4net.Info("Post method is called!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tempbill = await _context.PostFurniture(fr);

            return Ok(tempbill);
        }
    }
}
