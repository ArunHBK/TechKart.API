using com.TechKart.ProductApi.Models;
using com.TechKart.ProductApi.Models.DTO;
using com.TechKart.ProductApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace com.TechKart.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        public ProductController(IProductRepo productRepo)
        {
             _productRepo = productRepo;
        }


        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<ResponseObject>> GetAllProductDetails()
        {
            ResponseObject result = await _productRepo.ViewProductDetails();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<ActionResult<ResponseObject>> GetProductById(int id)
        {
            ResponseObject result = await _productRepo.ViewProductById(id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("GetProductByPrice")]
        public async Task<ActionResult<ResponseObject>> GetByPrice(double priceFrom, double priceTo)
        {
            ResponseObject result = await _productRepo.ViewByPrice(priceFrom, priceTo);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        [Route("AddProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseObject>> AddProduct(ProductDetailDto product)
        {
            ResponseObject result = await _productRepo.ProductAdd(product);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseObject>> UpdateProduct(int Id, ProductDetailDto product)
        {
            ResponseObject result = await _productRepo.ProductUpdate(Id, product);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseObject>> DeleteProduct(int Id)
        {
            ResponseObject result = await _productRepo.ProductDelete(Id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
