using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.API.Helper;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities;
using So2Baladna.Core.Interfaces;
using System.Security.Claims;
using static StackExchange.Redis.Role;
namespace So2Baladna.API.Controllers
{
    public class Account : BaseController
    {
        public Account(IUnitOfWork unitWork, IMapper mapper) : base(unitWork, mapper)
        {

        }
        [HttpPost("Register")]
        public async Task<IActionResult> register(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return BadRequest();
            }
            var res = await unitWork.AuthRepository.RegisterAsync(registerDTO);
            if (res != "done")
            {
                return BadRequest(new ResponseHandler<string>(400, "", res));
            }
            return Ok(new ResponseHandler<string>(200, "", res));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> login(LoginDTO loginDTO)
        {
            var res = await unitWork.AuthRepository.LoginAsync(loginDTO);
            if (res.StartsWith("please"))
            {
                return BadRequest(new ResponseHandler<string>(400, "", res));
            }

            // HttpOnly=true => This cookie is for the server only.JavaScript on the client cannot read or modify it
            Response.Cookies.Append("token", res, new CookieOptions
            {
                Secure = true, // Required for SameSite=None
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true,
                SameSite = SameSiteMode.None // ✅ REQUIRED for cross-origin cookies
            });

            return Ok(new ResponseHandler<string>(200, "", ""));
        }
        [HttpPost("active-account")]
        public async Task<IActionResult> active(ActiveAccountDTO activeAccountDTO)
        {
            var res = await unitWork.AuthRepository.ActiveAccount(activeAccountDTO);
            return res ? Ok(value: new ResponseHandler<string>(200, "", "")) : BadRequest(new ResponseHandler<string>(400, "", ""));


        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> reset(ResetPasswordDTO resetPasswordDTO )
        {
            var res = await unitWork.AuthRepository.ResetPassword(resetPasswordDTO);
            return res == "Password changed successfully" ? Ok(value: new ResponseHandler<string>(200, "", "")) : BadRequest(new ResponseHandler<string>(400, "", ""));


        }
        [HttpGet("send-email-forget-password")]
        public async Task<IActionResult> forget(string email)
        {

            var res = await unitWork.AuthRepository.SendEmailForForgetPassword(email);
            return res ? Ok(value: new ResponseHandler<string>(200, "", "")) : BadRequest(new ResponseHandler<string>(400, "", ""));

        }
        [Authorize]
        [HttpPut("update-address")]
        public async Task<IActionResult> updateAddress(ShippingAddressDto addressDTO)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var address = mapper.Map<Address>(addressDTO);
            var result = await unitWork.AuthRepository.UpdateAddress(email, address);
            return result ? Ok() : BadRequest();
        }


        [HttpGet("IsUserAuth")]
        public async Task<IActionResult> IsUserAuth()
        {

            return User.Identity.IsAuthenticated ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpGet("get-user-name")]
        public IActionResult GetUserName()
        {
            return Ok(new ResponseHandler<string>(200, User.Identity.Name,""));
        }

        [HttpGet("Logout")]
        public void logout()
        {

            Response.Cookies.Append("token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(-1)
            });
        }
        [Authorize]
        [HttpGet("get-address-for-user")]
        public async Task<IActionResult> GetAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email claim not found. Make sure it's included in the token or cookie.");
            }

            var address = await unitWork.AuthRepository.GetUserAddressAsync(email);

            if (address == null)
            {
                return NotFound("User address not found.");
            }

            var result = mapper.Map<ShippingAddressDto>(address);
            return Ok(result);
        }

    }
}
