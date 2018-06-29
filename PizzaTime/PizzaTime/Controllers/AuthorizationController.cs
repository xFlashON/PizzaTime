using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PizzaTime.Auth;
using PizzaTime.ViewModels;

namespace PizzaTime.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorization")]
    public class AuthorizationController : Controller
    {

        private IDataAccess _dataAccess;
        readonly ILogger _logger;

        public AuthorizationController(IDataAccess dataAccess, ILogger<DataController> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        [HttpPost("token")]
        public async Task GetToken([FromBody] string username, string password)
        {

            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                user = identity.Name,
                email = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                deliveryAdress = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Locality).Value
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }


        [HttpPost("Register")]
        public IActionResult RegisterCustomer([FromBody] CustomerViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_dataAccess.Customers.Get(c => c.Name.ToUpper() == model.Name.ToUpper()).FirstOrDefault() != null)
            {
                ModelState.AddModelError("Name", "Such username is exist!");
                return BadRequest(ModelState);
            }

            if (_dataAccess.Customers.Get(c => c.Email.ToUpper() == model.Email.ToUpper()).FirstOrDefault() != null)
            {
                ModelState.AddModelError("Email", "Such email is exist!");
                return BadRequest(ModelState);
            }

            Customer customer = Mapper.Map<Customer>(model);
            customer.Role = "User";
            customer.PasswordHash = AuthOptions.HashPassword(model.Password);

            _dataAccess.Customers.Create(customer);

            _dataAccess.SaveChanges();

            return Ok();

        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {

            Customer user = _dataAccess.Customers.Get(u => u.Name.ToUpper() == username.ToUpper() && AuthOptions.VerifyHashedPassword(u.PasswordHash, password)).FirstOrDefault();

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                    new Claim(ClaimTypes.Locality, user.DeliveryAdress),
                    new Claim(ClaimTypes.Email, user.Email),

                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}