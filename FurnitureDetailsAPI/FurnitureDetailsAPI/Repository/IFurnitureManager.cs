using FurnitureDetailsAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureDetailsAPI.Repository
{
    public interface IFurnitureManager
    {
        
        IEnumerable<Furniture> GetAllFurniture();
        Task<Furniture> PostFurniture(Furniture item);
        Furniture GetFurnitureById(int id);
        Task<Furniture> UpdateFurniture(Furniture item, int id);
        Task<Furniture> RemoveFurniture(int id);



    }
}
