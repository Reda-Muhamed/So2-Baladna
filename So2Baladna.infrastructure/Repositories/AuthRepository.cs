
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities;
using So2Baladna.Core.Interfaces;
using So2Baladna.Core.Services;
using So2Baladna.Core.Sharing;
using So2Baladna.infrastructure.Data;
namespace Ecom.infrastructure.Repositries
{
    public class AuthRepositry : IAuthRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateToken generateToken;
        private readonly ApplicationDbContext context;
        public AuthRepositry(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, ApplicationDbContext context , IGenerateToken generateToken)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.generateToken = generateToken;
            this.context = context;
        }

        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return null;
            }
            if (await userManager.FindByNameAsync(registerDTO.UserName) is not null)
            {
                return "this UserName is already registerd";
            }
            if (await userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return "this email is already registerd";
            }

            AppUser user = new()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                DisplayName = registerDTO.DisplayName
            };

            IdentityResult result = await userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded is not true)
            {
                return result.Errors.ToList()[0].Description;
            }
            // Send Active Email
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmail(user.Email, token, "active", "Activation Email", "Please active your email, click on button to active");
        
            return "done";
        }



        public async Task SendEmail(string email, string code, string component, string subject, string message)
        {
            var result = new EmailDto(email,
                "reda2542006@gmail.com",
                subject
                , EmailBody.Send(email, code, component, message));
            await emailService.SendEmailAsync(result);
        }




        public async Task<string> LoginAsync(LoginDTO login)
        {
            if (login == null)
            {
                return null;
            }
            var finduser = await userManager.FindByEmailAsync(login.Email);

            if (!finduser.EmailConfirmed)
            {
                string token = await userManager.GenerateEmailConfirmationTokenAsync(finduser);

                await SendEmail(finduser.Email, token, "active", "Activation Email", "Please active your email, click on button to active");

                return "Please confirem your email first, we have send activat to your E-mail";
            }

            var result = await signInManager.CheckPasswordSignInAsync(finduser, login.Password, true);

            if (result.Succeeded)
            {
                return generateToken.GetAndCreateToken(finduser);
            }

            return "please check your email and password, something went wrong";
        }





        public async Task<bool> SendEmailForForgetPassword(string email)
        {
            var findUser = await userManager.FindByEmailAsync(email);
            if (findUser is null)
            {
                return false;
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(findUser);
            await SendEmail(findUser.Email, token, "Reset-Password", "Rest pssword", "click on button to Reset your password");

            return true;

        }

        public async Task<string> ResetPassword(ResetPasswordDTO restPassword)
        {
            var findUser = await userManager.FindByEmailAsync(restPassword.Email);
            if (findUser is null)
            {
                return null;
            }

            var result = await userManager.ResetPasswordAsync(findUser, restPassword.Token, restPassword.Password);

            if (result.Succeeded)
            {
                return "Password changed successfully";
            }
            return result.Errors.ToList()[0].Description;
        }
        // check if the resend email has the same token or not
        public async Task<bool> ActiveAccount(ActiveAccountDTO accountDTO)
        {
            var findUser = await userManager.FindByEmailAsync(accountDTO.Email);
            if (findUser is null)
            {
                return false;
            }

            var reslt = await userManager.ConfirmEmailAsync(findUser, accountDTO.Token);
            if (reslt.Succeeded)
                return true;

            var token = await userManager.GenerateEmailConfirmationTokenAsync(findUser);
            await SendEmail(findUser.Email, token, "active", "ActiveEmail", "Please active your email, click on button to active");

            return false;
        }

        public async Task<bool> UpdateAddress(string email, Address address)
        {
            var findUser = await userManager.FindByEmailAsync(email);
            if (findUser is null)
            {
                return false;
            }
            var Myaddress = await context.Addresss.AsNoTracking()
                .FirstOrDefaultAsync(m => m.AppUserId == findUser.Id);

            if (Myaddress is null)
            {
                address.AppUserId = findUser.Id;
                await context.Addresss.AddAsync(address);
            }
            else
            {
                context.Entry(Myaddress).State = EntityState.Detached;
                address.Id = Myaddress.Id;
                address.AppUserId = Myaddress.AppUserId;
                context.Addresss.Update(address);

            }
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Address?> GetUserAddressAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return null;

            return await context.Addresss.FirstOrDefaultAsync(a => a.AppUserId == user.Id);
        }

    }
}