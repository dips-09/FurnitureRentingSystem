using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureDetailsAPI.Model
{
    public class FurnitureContext : DbContext
    {
        public FurnitureContext(DbContextOptions<FurnitureContext> option) : base(option)
        {

        }

        public virtual DbSet<Furniture> Furnitures { set; get; }
    }
}
