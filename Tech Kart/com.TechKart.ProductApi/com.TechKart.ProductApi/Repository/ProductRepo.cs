using com.TechKart.ProductApi.Models.DTO;
using com.TechKart.ProductApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using com.TechKart.ProductApi.Data;
using System.Linq;
using System.Collections.Generic;

namespace com.TechKart.ProductApi.Repository 
{
    public class ProductRepo : IProductRepo
    {
        private readonly DataContext _context;
        public ProductRepo(DataContext context)
        {
            _context = context;
        }
        ResponseObject response = new ResponseObject();
        public async Task<ResponseObject> ViewProductDetails()
        {
            try
            {
                List<ProductDetail> value = await _context.ProductDetails.ToListAsync();
                if (value.Count != 0)
                {
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Value = value;
                    response.Message = "Product List";
                    return response;
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message="No Products to show";
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
        public async Task<ResponseObject> ViewProductById(int id)
        {
            try
            {
                var value = await _context.ProductDetails.FindAsync(id);
                if (value != null)
                {
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Value = value;
                    response.Message = "Product";
                    return response;
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message="No Products to show";
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
       
        public async Task<ResponseObject> ViewByPrice(double priceFrom, double priceTo)
        {
            try
            {
                var value = await _context.ProductDetails.Where(r => r.Price > priceFrom && r.Price < priceTo).ToListAsync();
                if (value.Count != 0)
                {
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Value = value;
                    response.Message = "Product List";
                    return response;
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message="No Products to show";
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
        public async Task<ResponseObject> ProductAdd(ProductDetailDto product)
        {
            try
            {
                var Check = await _context.ProductDetails.Where(m => m.Name == product.Name).FirstOrDefaultAsync();

                if (Check == null)
                {
                    ProductDetail mapping = new ProductDetail
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Brand = product.Brand,
                        Category = product.Category,
                        Description = product.Description,
                        ImgUrl=product.ImgUrl
                    };
                    _context.ProductDetails.Add(mapping);
                    await _context.SaveChangesAsync();
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Message = "Product added successfully";
                    return response;
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message ="Product already exists";
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

        public async Task<ResponseObject> ProductUpdate(int Id, ProductDetailDto product)
        {
            try
            {
                var result = await _context.ProductDetails.FindAsync(Id);
                if (result != null)
                {
                    result.Name = product.Name;
                    result.Price = product.Price;
                    result.Brand = product.Brand;
                    result.Category = product.Category;
                    result.Description = product.Description;
                    result.ImgUrl=product.ImgUrl;
                    await _context.SaveChangesAsync();
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Message = "Product details Updated successfully";
                    return response;
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message ="Product not found";
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

        public async Task<ResponseObject> ProductDelete(int Id)
        {
            try
            {
                var value = await _context.ProductDetails.FindAsync(Id);

                if (value != null)
                {
                    _context.ProductDetails.Remove(value);
                    await _context.SaveChangesAsync();
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Message = "Product has been deleted successfully";
                    return response;
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message ="Product not found";
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