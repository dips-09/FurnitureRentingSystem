using AuthenticationAPI.Controllers;
using AuthenticationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AuthTest
{
    public class TokenTesting
    {
        List<RentingUser> user = new List<RentingUser>();
        IQueryable<RentingUser> userdata;
        Mock<DbSet<RentingUser>> mockSet;
        Mock<RentingContext> usercontextmock;
        [SetUp]
        public void Setup()
        {
            user = new List<RentingUser>()
            {
                new RentingUser{UserId="user1",Password="abc123",FullName="abc"}

            };
            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<RentingUser>>();
            mockSet.As<IQueryable<RentingUser>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<RentingUser>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<RentingUser>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<RentingUser>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<RentingContext>();
            usercontextmock = new Mock<RentingContext>(p);
            usercontextmock.Setup(x => x.users).Returns(mockSet.Object);
        }

        [Test]
        public void LoginTest()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("MyCustomSecretKey");
            var controller = new TokenController( config.Object, usercontextmock.Object);
            var rentuser = new RentingUser { UserId = "user1", Password = "abc123", FullName = "abc" };
            var auth = controller.Login(rentuser) as OkObjectResult;

            Assert.AreEqual(200, auth.StatusCode);
        }

        [Test]
        public void LoginTestFail()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("MyCustomSecretKey");
            var controller = new TokenController(config.Object, usercontextmock.Object);
            var auth = controller.Login(new RentingUser { UserId = "1", Password = "c123", FullName = "abc" }) as OkObjectResult;

            Assert.IsNull(auth);

        }
    }
}