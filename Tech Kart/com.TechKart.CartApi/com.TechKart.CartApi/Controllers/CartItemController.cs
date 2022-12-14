using com.TechKart.CartApi.Models;
using com.TechKart.CartApi.Models.DTO;
using com.TechKart.CartApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace com.TechKart.CartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")]

    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepo _cartItemRepo;
        public CartItemController(ICartItemRepo cartItemRepo)
        {
            _cartItemRepo = cartItemRepo;
        }

        [HttpGet]
        [Route("AddCartItem")]
        public async Task<ActionResult<ResponseObject>> AddCartItems(int productId)
        {
            ResponseObject result = await _cartItemRepo.CartItemAdd(productId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [Route("DeleteCartItem")]
        public async Task<ActionResult<ResponseObject>> CartItemDelete(int Id)
        {
            ResponseObject result = await _cartItemRepo.CartItemsDelete(Id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
