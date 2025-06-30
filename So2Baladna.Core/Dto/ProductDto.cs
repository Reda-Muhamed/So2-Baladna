using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Dto
{
    public record ProductDto(
   
        string Name,
        string Description,
        decimal Price,
        string CategoryName,
        string PhotoUrl
    );

}
