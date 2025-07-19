using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO login);
        Task<bool> SendEmailForForgetPassword(string email);
        Task<string> ResetPassword(ResetPasswordDTO restPassword);
        Task<bool> ActiveAccount(ActiveAccountDTO accountDTO);
        Task<bool> UpdateAddress(string email, Address address);
        Task<Address> getUserAddress(string email);
    }
}
