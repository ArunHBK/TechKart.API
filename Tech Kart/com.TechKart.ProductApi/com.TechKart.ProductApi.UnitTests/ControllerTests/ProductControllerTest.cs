using com.TechKart.ProductApi.Controllers;
using com.TechKart.ProductApi.Models;
using com.TechKart.ProductApi.Models.DTO;
using com.TechKart.ProductApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.TechKart.ProductApi.UnitTests.ControllerTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ProductControllerTest
    {
        [Test]
        public async Task GetAllProductDetails_Test()
        {
            var mockProductRepo = new Mock<IProductRepo>();
            mockProductRepo.Setup(x => x.ViewProductDetails())
            .ReturnsAsync(new ResponseObject { Status= true });

            ProductController productController = new ProductController(mockProductRepo.Object);
            ActionResult<ResponseObject> result = await productController.GetAllProductDetails();
           // Assert.IsTrue(result.Result);
            Assert.NotNull(result);
        }
        [Test]
        public async Task GetProductById_Test()
        {
            var mockProductRepo = new Mock<IProductRepo>();
            mockProductRepo.Setup(x => x.ViewProductById(2))
            .ReturnsAsync(new ResponseObject { Status= true });

            ProductController productController = new ProductController(mockProductRepo.Object);
            ActionResult<ResponseObject> result = await productController.GetProductById(2);
            // Assert.IsTrue(result.Result);
            Assert.NotNull(result);
        }
        [Test]
        public async Task GetProductByPrice_Test()
        {
            var mockProductRepo = new Mock<IProductRepo>();
            mockProductRepo.Setup(x => x.ViewByPrice(700,800))
            .ReturnsAsync(new ResponseObject { Status= true });

            ProductController productController = new ProductController(mockProductRepo.Object);
            ActionResult<ResponseObject> result = await productController.GetByPrice(700,800);
            // Assert.IsTrue(result.Result);
            Assert.NotNull(result);
        }
        //[Test]
        //public async Task AddProduct_Test()
        //{
        //    var mockProductRepo = new Mock<IProductRepo>();
        //    mockProductRepo.Setup(x => x.ProductAdd(new ProductDetailDto
        //    {
        //        Name = "Oneplus 10 pro",
        //        Brand="Oneplus",
        //        Category="SmartPhone",
        //        Description="12GB RAM,256 GB Storage, Android 12",
        //        Price=64999
        //    }))
        //    .ReturnsAsync(new ResponseObject { Status= true });

        //    ProductController productController = new ProductController(mockProductRepo.Object);
        //    ActionResult<ResponseObject> result = await productController.AddProduct(new ProductDetailDto
        //    {
        //        Name = "Oneplus 10 pro",
        //        Brand="Oneplus",
        //        Category="SmartPhone",
        //        Description="12GB RAM,256 GB Storage, Android 12",
        //        Price=64999
        //    });
        //    // Assert.IsTrue(result.Result);
        //    Assert.NotNull(result);
        //}
        //[Test]
        //public async Task UpdateProduct_Test()
        //{
        //    var mockProductRepo = new Mock<IProductRepo>();
        //    mockProductRepo.Setup(x => x.ProductUpdate(2, new ProductDetailDto
        //    {
        //        Name = "Oneplus 10 pro",
        //        Brand="Oneplus",
        //        Category="SmartPhone",
        //        Description="12GB RAM,256 GB Storage, Android 12",
        //        Price=64999
        //    }))
        //    .ReturnsAsync(new ResponseObject { Status= true });

        //    ProductController productController = new ProductController(mockProductRepo.Object);
        //    ActionResult<ResponseObject> result = await productController.UpdateProduct(7, new ProductDetailDto
        //    {
        //        Name = "Oneplus 10 pro",
        //        Brand="Oneplus",
        //        Category="SmartPhone",
        //        Description="12GB RAM,256 GB Storage, Android 12",
        //        Price=64999
        //    });
        //    // Assert.IsTrue(result.Result);
        //    Assert.NotNull(result);
        //}
        [Test]
        public async Task DeleteProduct_Test()
        {
            var mockProductRepo = new Mock<IProductRepo>();
            mockProductRepo.Setup(x => x.ProductDelete(2))
            .ReturnsAsync(new ResponseObject { Status= true });

            ProductController productController = new ProductController(mockProductRepo.Object);
            ActionResult<ResponseObject> result = await productController.DeleteProduct(2);
            // Assert.IsTrue(result.Result);
            Assert.NotNull(result);
        }
    }
}
