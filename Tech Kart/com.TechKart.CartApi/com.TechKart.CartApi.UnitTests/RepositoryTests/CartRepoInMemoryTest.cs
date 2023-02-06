using com.TechKart.CartApi.Data;
using com.TechKart.CartApi.Models;
using com.TechKart.CartApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace com.TechKart.CartApi.UnitTests.RepositoryTests
{

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CartRepoInMemoryTest
    {
        Mock<IHttpContextAccessor> mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: "TechKartDb")
          .Options;

        DataContext context;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();

            CartRepo cartRepo = new CartRepo(context,mockHttpContextAccessor.Object);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var carts = new List<Cart>()
            {
                new Cart()
                {
                    GrandTotal=34736,LoginId=1
                },
                new Cart()
                {
                    GrandTotal=58736,LoginId=2
                },
                new Cart()
                {
                    GrandTotal=47648,LoginId=3
                },
            };
            context.Carts.AddRange(carts);
            var cartItems = new List<CartItems>()
            {
                new CartItems()
                {
                    Quantity=1, Item_Total=609, CartId=2, ProductId=2
                 },
                new CartItems()
                {
                    Quantity=1, Item_Total=699, CartId=3, ProductId=6},
                new CartItems()
                {
                    Quantity=1, Item_Total=7509, CartId=3, ProductId=7
                },
            };
            context.Carts.AddRange(carts);
            context.SaveChanges();
        }

        [Test]
        public async Task View_cartBy_Id_Test()
        {
            this.mockHttpContextAccessor.Setup(req => req.HttpContext.User.Identity.Name).Returns("1");

            CartRepo cartRepo = new CartRepo(context, mockHttpContextAccessor.Object);

            ResponseObject result = await cartRepo.ViewCartById();

            Assert.AreEqual("Cart Items", result.Message);
        }
    }
}
