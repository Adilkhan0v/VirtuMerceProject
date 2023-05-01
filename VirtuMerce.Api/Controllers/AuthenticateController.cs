using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using VirtuMerce.Api.ViewModels;
using VitruMerce.Bll;
using VitruMerce.Bll.Dtos;

namespace VirtuMerce.Api.Controllers;

public class AuthenticateController : ControllerBase
{
        private readonly IAuthService _authService;

        public AuthenticateController(IAuthService authService)
        {
                _authService = authService;
        }
        
        [HttpPost("api/users/signup")]
        public async Task<IActionResult> Signup([FromBody]UserSignupViewModel userSignupViewModel)
        {
            return Ok(await _authService.Signup(new UserSignupDto
            {
                Login = userSignupViewModel.Login,
                Password = userSignupViewModel.Password,
                Username = userSignupViewModel.Username
            }));
        }

        [HttpPost("api/users/signin")] 
        public async Task<IActionResult> Signin([FromBody] UserSigninViewModel userSigninViewModel)
        {
            try
            {
                return Ok(await _authService.Signin(new UserSigninDto
                {
                    Login = userSigninViewModel.Login,
                    Password = userSigninViewModel.Password
                }));
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Authorize]
        [HttpGet("api/auth")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                return Ok(await _authService.GetUserByHeaders(Request.Headers[HeaderNames.Authorization]!));
            }
            catch (ArgumentException)
            {
                return NotFound("User is not found, wrong token");
            }
        }
        
        
}