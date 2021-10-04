using AspNetTestTask.Api.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AspNetIdentity.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;
        public AuthController(UserManager<IdentityUser> userManager, IMapper mapper, IConfiguration conf)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = conf;
        }

        [HttpPost("login")]
        public ActionResult LoginUser(LoginDTO model)
        {
            var item = _mapper.Map<IdentityUser>(model);
            var user = _userManager.FindByEmailAsync(item.Email);

            if (user == null)
            {
                return BadRequest("There is no user with that email");
            }

            var result = _userManager.CheckPasswordAsync(user.Result, model.Password);
            if(!result.Result)
            {
                return BadRequest("Wrong password");
            }

            //generate access token
            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Result.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new UserResponseDTO { 
                Message = tokenAsString, 
                IsSuccess = true, 
                ExpireDate = token.ValidTo});
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterUserDTO model)
        {
            if (model == null)
                return BadRequest();

            var user = _mapper.Map<IdentityUser>(model);
            var result = _userManager.CreateAsync(user, model.Password);
            if (!result.Result.Succeeded)
            {
                var errors = result.Result.Errors.Select(e => e.Description);
                return BadRequest(new UserResponseDTO { Errors = errors });
            }
            return Ok();
        }
    }
}
