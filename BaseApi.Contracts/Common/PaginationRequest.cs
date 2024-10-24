namespace BaseApi.Contracts.Common;

public class PaginationRequest
{
    private const int MinPage = 1;
    private const int DefaultPage = 1;

    private const int MinPageSize = 1;
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;

    private int _page = DefaultPage;
    private int _pageSize = DefaultPageSize;
    
    public int Page
    {
        get => _page;
        set => _page = (value < MinPage) ? DefaultPage : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) 
                ? MaxPageSize 
                : (value < MinPageSize ? DefaultPageSize : value);
    }
}