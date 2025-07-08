namespace So2Baladna.API.Helper
{
    public class Pagination<T> where T: class
    {
        public Pagination(int pageIndex, int pageSize, int totalCount,IReadOnlyList<T>data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data; 
        }

        public int PageIndex { get; set; } = 1; // Default to the first page
        public int PageSize { get; set; }  // Default to 10 items per page
        public int TotalCount { get; set; } // Total number of items
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); // Calculate total pages
        public bool HasPreviousPage => PageIndex > 1; // Check if there is a previous page
        public bool HasNextPage => PageIndex < TotalPages; // Check if there is a next page
        public IReadOnlyList<T> Data { get; set; }
    }
}
