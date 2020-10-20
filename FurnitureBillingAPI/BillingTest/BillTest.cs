using FurnitureBillingAPI.Model;
using FurnitureBillingAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BillingTest
{
    public class BillTest
    {
        List<ClsBill> bills = new List<ClsBill>();
        IQueryable<ClsBill> billingdata;
        Mock<DbSet<ClsBill>> mockSet;
        Mock<BillContext> billcontextmock;
        [SetUp]
        public void Setup()
        {
            bills = new List<ClsBill>()
            {
                new ClsBill{BillId = 1, BillOwner = "abc",BillAmount = 980.8, FurnitureName = "Chair"},
                 new ClsBill{BillId = 2, BillOwner = "def",BillAmount = 900.8, FurnitureName = "cupboard"}

            };
            billingdata = bills.AsQueryable();
            mockSet = new Mock<DbSet<ClsBill>>();
            mockSet.As<IQueryable<ClsBill>>().Setup(m => m.Provider).Returns(billingdata.Provider);
            mockSet.As<IQueryable<ClsBill>>().Setup(m => m.Expression).Returns(billingdata.Expression);
            mockSet.As<IQueryable<ClsBill>>().Setup(m => m.ElementType).Returns(billingdata.ElementType);
            mockSet.As<IQueryable<ClsBill>>().Setup(m => m.GetEnumerator()).Returns(billingdata.GetEnumerator());
            var p = new DbContextOptions<BillContext>();
            billcontextmock = new Mock<BillContext>(p);
            billcontextmock.Setup(x => x.Bills).Returns(mockSet.Object);

        }

        [Test]
        public void AddBillingDetailTest()
        {
            var billingrepo = new BillingRepo(billcontextmock.Object);
            var billingobj = billingrepo.AddBill(new ClsBill { BillId = 1, BillOwner = "abc", BillAmount = 980.8, FurnitureName = "Chair" });
            Assert.IsNotNull(billingobj);
        }
    }
}