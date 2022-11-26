using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using WebAPI.Utils.ActionFilters;

namespace WebAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;

        public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<User> userManager,
            IAuthenticationManager authManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
			var refreshToken = _authManager.GenerateRefreshToken();
			var jwtToken = await _authManager.CreateToken();
			SetJwtToken(jwtToken);
			await SetRefreshToken(refreshToken);
			return StatusCode(201, new { Token = jwtToken });
        }



        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
                return Unauthorized();
            }
            var refreshToken = _authManager.GenerateRefreshToken();
            var jwtToken = await _authManager.CreateToken();
            SetJwtToken(jwtToken);
			await SetRefreshToken(refreshToken);
            return Ok(new { Token = jwtToken });
        }



        [HttpGet("refresh-token/{token}")]
        public async Task<ActionResult<string>> RefreshToken(string token)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = await _authManager.GetUserFromJwtAsync(token);
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Refresh Token expired.");
            }

            var newRefreshToken = _authManager.GenerateRefreshToken();
            await SetRefreshToken(newRefreshToken);
			var jwtToken = await _authManager.CreateToken();
			SetJwtToken(jwtToken);
			return Ok(new { Token = jwtToken });
        }


        private async Task SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            await _authManager.UpdateUser(newRefreshToken);
        }

		private void SetJwtToken(string token)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = DateTime.Now.AddMinutes(5)
			};

            Response.Cookies.Append("jwtToken", token, cookieOptions);
		}

	}
}
