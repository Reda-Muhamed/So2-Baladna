using So2Baladna.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Services
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(EmailDto emailDto);

    }
}
