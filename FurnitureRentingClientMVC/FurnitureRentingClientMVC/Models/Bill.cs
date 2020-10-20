using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureRentingClientMVC.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public string BillOwner { get; set; }
        public string FurnitureName { get; set; }
        public double BillAmount { get; set; }

    }
}
