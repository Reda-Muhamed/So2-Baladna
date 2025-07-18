using So2Baladna.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Services
{
    public interface IGenerateToken
    {
       string GetAndCreateToken(AppUser appUser);
    }
}
