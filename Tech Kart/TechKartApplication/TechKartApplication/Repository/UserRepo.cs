using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechKartApplication.Data;
using TechKartApplication.Models;
using TechKartApplication.Models.DTO;

namespace TechKartApplication.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
       private readonly IConfiguration _configuration;

        public UserRepo(DataContext context, IConfiguration configuration)
        {
            _context = context;
           _configuration=configuration;
        }
        ResponseObject response = new ResponseObject();

        
        public async Task<ResponseObject> Login(LoginDto user)
        {
            try
            {
                var userCheck = await _context.LoginDetails.Where(m => m.Username == user.Username && m.Password == user.Password).FirstOrDefaultAsync();

                if (userCheck != null)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, userCheck.Role),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("Id", userCheck.LoginId.ToString())
                };

                    var token = new JwtSecurityToken(
                        issuer: "https://localhost:44326/",
                        audience: "https://localhost:44326/",
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: credentials);

                    string returnData = new JwtSecurityTokenHandler().WriteToken(token);
                    response.Status = true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Message="Login Successfull";
                    response.Value = returnData;
                    return response;
                }
                else
                {
                    response.Status = false;
                    response.StatusCode=StatusCodes.Status400BadRequest;
                    response.Message ="Invalid username and password";
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
        public async Task<ResponseObject> UserRegistration(UserDetailDto user)
        {
            try
            {
                var userCheck = await _context.UserDetails.Where(m => m.Username == user.Username).FirstOrDefaultAsync();

                if (userCheck == null)
                {
                    LoginDetail LoginUser = new LoginDetail();
                    LoginUser.Username = user.Username;
                    LoginUser.Password = user.Password;
                    LoginUser.Role ="User";
                    UserDetail mapping = new UserDetail
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Age = user.Age,
                        Gender = user.Gender,
                        Username = user.Username,
                        Address = user.Address,
                        ContactNumber = user.ContactNumber
                    };
                    _context.UserDetails.Add(mapping);
                    _context.LoginDetails.Add(LoginUser);
                    await _context.SaveChangesAsync();

                    response.Status = true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Message="User registered Successfully";
                    return response;
                }
                else
                {
                    response.Status = false;
                    response.StatusCode=StatusCodes.Status400BadRequest;
                    response.Message ="User already exists";
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
