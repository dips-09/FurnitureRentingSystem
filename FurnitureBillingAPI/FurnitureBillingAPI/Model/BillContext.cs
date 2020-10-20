using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureBillingAPI.Model
{
    public class BillContext : DbContext
    {
        public BillContext(DbContextOptions<BillContext> option) : base(option)
        {

        }

        public virtual DbSet<ClsBill> Bills { get; set; }
    }
}
