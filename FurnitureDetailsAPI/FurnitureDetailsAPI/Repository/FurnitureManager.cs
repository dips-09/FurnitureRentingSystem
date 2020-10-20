using FurnitureDetailsAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureDetailsAPI.Repository
{
    public class FurnitureManager : IFurnitureManager
    {
        private readonly FurnitureContext _context;

        public FurnitureManager()
        {

        }
        public FurnitureManager(FurnitureContext context)
        {
            _context = context;
        }

        public IEnumerable<Furniture> GetAllFurniture()
        {
            return _context.Furnitures.ToList();
        }

        public Furniture GetFurnitureById(int id)
        {
            Furniture item = _context.Furnitures.Find(id);

            return item;
        }

        public async Task<Furniture> PostFurniture(Furniture item)
        {
            Furniture fr = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                fr = new Furniture() { FurnitureId = item.FurnitureId, FurnitureName = item.FurnitureName, Price = item.Price };
                await _context.Furnitures.AddAsync(fr);
                await _context.SaveChangesAsync();
            }
            return fr;
        }

        public async Task<Furniture> RemoveFurniture(int id)
        {
            Furniture fr = await _context.Furnitures.FindAsync(id);
            if (fr == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _context.Furnitures.Remove(fr);
                await _context.SaveChangesAsync();
            }
            return fr;
        }

        public async Task<Furniture> UpdateFurniture(Furniture item, int id)
        {
            Furniture fr = await _context.Furnitures.FindAsync(id);
            fr.FurnitureName = item.FurnitureName;
            fr.Price = item.Price;
            await _context.SaveChangesAsync();

            return fr;
        }
    }
}
