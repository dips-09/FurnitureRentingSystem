using FurnitureDetailsAPI.Controllers;
using FurnitureDetailsAPI.Model;
using FurnitureDetailsAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureDetailsTesting
{
    public class FurnitureTest
    {
        List<Furniture> furnitures = new List<Furniture>();
        IQueryable<Furniture> furdata;
        Mock<DbSet<Furniture>> mockSet;
        Mock<FurnitureContext> furniturecontextmock;
        

        [SetUp]
        public void Setup()
        {
            furnitures = new List<Furniture>()
            {
                new Furniture{FurnitureId = 1,FurnitureName = "abc", Price = 987.8},
                 new Furniture{FurnitureId = 2,FurnitureName = "def", Price = 907.8}

            };
            furdata = furnitures.AsQueryable();
            mockSet = new Mock<DbSet<Furniture>>();
            mockSet.As<IQueryable<Furniture>>().Setup(m => m.Provider).Returns(furdata.Provider);
            mockSet.As<IQueryable<Furniture>>().Setup(m => m.Expression).Returns(furdata.Expression);
            mockSet.As<IQueryable<Furniture>>().Setup(m => m.ElementType).Returns(furdata.ElementType);
            mockSet.As<IQueryable<Furniture>>().Setup(m => m.GetEnumerator()).Returns(furdata.GetEnumerator());
            var p = new DbContextOptions<FurnitureContext>();
            furniturecontextmock = new Mock<FurnitureContext>(p);
            furniturecontextmock.Setup(x => x.Furnitures).Returns(mockSet.Object);
        }

        [Test]
        public void Get_AllFurnitureList()
        {
            var furrepo = new FurnitureManager(furniturecontextmock.Object);
            var furlist = furrepo.GetAllFurniture();
            Assert.AreEqual(2, furlist.Count());
        }


    }
}