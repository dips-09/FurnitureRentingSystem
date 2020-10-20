using FurnitureBillingAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureBillingAPI.Repository
{
    public class BillingRepo : IBillingRepo
    {
        private readonly BillContext _context;

        public BillingRepo(BillContext context)
        {
            _context = context;
        }
        public ClsBill AddBill(ClsBill item)
        {
            ClsBill bill = null;
            if (item == null)
                throw new NullReferenceException();
            else
            {
                bill = new ClsBill() { BillId = item.BillId, BillOwner = item.BillOwner, FurnitureName = item.FurnitureName, BillAmount = item.BillAmount };
                 _context.Bills.Add(bill);
                 _context.SaveChanges();
            }
            
            return item;
        }

        public ClsBill GetBillById(int id)
        {
            ClsBill bill = _context.Bills.Find(id);
            return bill;
        }
    }
}
