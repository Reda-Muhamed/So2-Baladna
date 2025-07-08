using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Sharing
{
    public class ProductParams
    {
        //string sort, int? categoryId,int pageSize, int pageNumber
        public string ?Sort { get; set; }
        public int ?CategoryId { get; set; }
        public string Search { get; set; } = string.Empty; // Default to empty string for no search
        public int MaxPageSize { get; set; } = 10; // Default value
        public int PageNumber { get; set; } = 1; // Default value
        private int pageSize = 5;
        public int TotalCount { get; set; }
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value > MaxPageSize)
                    pageSize = MaxPageSize;
                else if (value < 1)
                    pageSize = 1;
                else
                    pageSize = value;
            }
        }


    }
}
