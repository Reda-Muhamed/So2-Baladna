using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Dto
{
    public record CategoryDto (
        string Name,
        string Description
    );
    public record CategoryUpdateDto(
    int Id,
    string Name,
    string Description
);

}
