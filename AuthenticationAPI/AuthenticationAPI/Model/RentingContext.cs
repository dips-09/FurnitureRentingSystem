using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAPI.Model
{
    public class RentingContext : DbContext
    {
        public RentingContext(DbContextOptions<RentingContext> options) : base(options)
        {

        }


        public virtual DbSet<RentingUser> users { set; get; }
    }
}
