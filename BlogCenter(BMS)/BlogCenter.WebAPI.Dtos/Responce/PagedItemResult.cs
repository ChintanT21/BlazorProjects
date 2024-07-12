namespace BMS.Client.Dtos
{
    public class PagedItemResult<T> where T : class
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
