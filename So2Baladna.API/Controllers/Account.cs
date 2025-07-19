using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.API.Helper;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Interfaces;

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
                Secure = true,
                HttpOnly = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true,
                SameSite = SameSiteMode.Strict
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



    }
}
