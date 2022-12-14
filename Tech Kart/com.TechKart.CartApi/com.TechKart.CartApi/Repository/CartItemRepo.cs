using com.TechKart.CartApi.Data;
using com.TechKart.CartApi.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using com.TechKart.CartApi.Models.DTO;

namespace com.TechKart.CartApi.Repository
{
    public class CartItemRepo:ICartItemRepo
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepo _cartRepo;

        public CartItemRepo(DataContext context, IHttpContextAccessor httpContextAccessor, ICartRepo cartRepo)
        {
            _context = context;
            _httpContextAccessor=httpContextAccessor;
            _cartRepo=cartRepo;
        }
        ResponseObject response = new ResponseObject();

        public async Task<ResponseObject> CartItemAdd(int productId)
        {
            try
            {
                int LoginId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("Id")); ;
                var UserCart = await _context.Carts.Where(m => m.LoginId == LoginId).FirstOrDefaultAsync();
                if(CartExists(LoginId))
                {
                    if (!CartItemExists(productId,UserCart.CartId))
                    {
                        var productToAdd = await _context.ProductDetails.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
                        if (productToAdd!=null)
                        {
                            CartItems cartItems = new CartItems()
                            {
                                cart=UserCart,
                                Item_Total=productToAdd.Price,
                                Quantity=1,
                                Created_at=DateTime.Now,
                                Modified_at=DateTime.Now,
                                product=productToAdd
                            };
                            UserCart.GrandTotal=productToAdd.Price;
                            UserCart.Modified_at=DateTime.Now;

                            _context.CartItems.Add(cartItems);
                            _context.Carts.Update(UserCart);
                            await _context.SaveChangesAsync();

                            response.Status=true;
                            response.StatusCode= StatusCodes.Status200OK;
                            response.Message ="Item added to cart";
                            return response;
                        }
                        else
                        {
                            response.Status=false;
                            response.StatusCode= StatusCodes.Status400BadRequest;
                            response.Message ="product not found";
                            return response;
                        }
                    }
                    else
                    {
                        response.Status=false;
                        response.StatusCode= StatusCodes.Status400BadRequest;
                        response.Message ="Item already exists in cart";
                        return response;
                    }
                }
                else
                {
                   await _cartRepo.CartAdd();
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Message="Cart has been Created";
                    return response;
                }
                    
            }
            catch (Exception ex)
            {
                response.Status=false;
                response.StatusCode= StatusCodes.Status500InternalServerError;
                response.Message=ex.Message;
                return response;
            }
        }
        
        private bool CartExists(int loginId)
        {
            return _context.Carts.Any(c => c.LoginId == loginId);
        }
        private bool CartItemExists(int productId, int cartId)
        {
            return _context.CartItems.Any(c => c.ProductId == productId && c.CartId ==cartId );
        }

        public async Task<ResponseObject> CartItemsDelete(int Id)
        {
            try
            {
                var value = await _context.CartItems.FindAsync(Id);

                if (value != null)
                {
                    _context.CartItems.Remove(value);
                    await _context.SaveChangesAsync();
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Message = "Cart Item has been deleted successfully";
                    return response;
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message ="Cart Item not found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Status=false;
                response.StatusCode= StatusCodes.Status500InternalServerError;
                response.Message=ex.Message;
                return response;
            }
        }
    }
}