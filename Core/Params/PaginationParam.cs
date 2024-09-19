namespace Core.Params
{
    public class PaginationParam
    {
        private const int MAX_PAGE_SIZE = 50;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
        }
        public int PageIndex { get; set; } = 1;
    }
}
