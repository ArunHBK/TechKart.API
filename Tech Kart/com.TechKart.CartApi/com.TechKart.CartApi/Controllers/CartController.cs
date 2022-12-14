using com.TechKart.CartApi.Models;
using com.TechKart.CartApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace com.TechKart.CartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")]


    public class CartController : ControllerBase
    {
        private readonly ICartRepo _cartRepo;
        public CartController(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpGet]
        [Route("GetCartById")]
        public async Task<ActionResult<ResponseObject>> GetProductById()
        {
            ResponseObject result = await _cartRepo.ViewCartById();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("AddCart")]
        public async Task<ActionResult<ResponseObject>> AddCart()
        {
            ResponseObject result = await _cartRepo.CartAdd();
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [Route("EmptyCart")]
        public async Task<ActionResult<ResponseObject>> CartEmpty()
        {
            ResponseObject result = await _cartRepo.EmptyCart();
            return StatusCode(result.StatusCode, result);
        }
    }
}
