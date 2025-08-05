using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Dto
{
    public class RatingDto
    {
        public int stars { get; set; }
        public string content { get; set; }

        public int productId { get; set; }
    }
    public class ReturnRatingDto
    {
        public int stars { get; set; }
        public string content { get; set; }

        public string userName { get; set; }
        public DateTime ReviewTime { get; set; }
    }
}
