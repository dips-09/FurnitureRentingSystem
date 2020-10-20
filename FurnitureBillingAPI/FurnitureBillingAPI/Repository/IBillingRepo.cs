using FurnitureBillingAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureBillingAPI.Repository
{
    public interface IBillingRepo
    {
        ClsBill AddBill(ClsBill item);
        ClsBill GetBillById(int id);
    }
}
