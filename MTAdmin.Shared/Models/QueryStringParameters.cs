namespace MTAdmin.Shared.Models
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;
        public string Query { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int SortColumn { get; set; } = 1;
        public bool SortOrder { get; set; } = false; //desc
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
