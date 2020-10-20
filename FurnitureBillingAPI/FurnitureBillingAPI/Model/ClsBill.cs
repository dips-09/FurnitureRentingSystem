using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureBillingAPI.Model
{
    public class ClsBill
    {
        [Key]
        public int BillId { get; set; }
        public string BillOwner { get; set; }
        public string FurnitureName { get; set; }
        public double BillAmount { get; set; }
    }
}
