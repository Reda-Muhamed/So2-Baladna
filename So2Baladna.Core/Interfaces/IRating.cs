using So2Baladna.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Interfaces
{
    public interface IRating
    {
        Task<bool> AddRatingAsync(RatingDto ratingDTO, string email);
        Task<IReadOnlyList<ReturnRatingDto>> GetAllRatingForProductAsync(int productId);
    }
}
