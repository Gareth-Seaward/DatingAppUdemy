namespace DatingApp.API.Helpers
{
  public class PagingParams
  {
    protected const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    protected int pageSize = 10;
    public int PageSize
    {
      get { return pageSize; }
      set { pageSize = (value > maxPageSize) ? maxPageSize : value; }
    }
  }
}