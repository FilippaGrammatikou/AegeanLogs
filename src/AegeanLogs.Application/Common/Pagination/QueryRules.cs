namespace AegeanLogs.Application.Common.Pagination;

public static class QueryRules
{
    public static int NormalizePage(int page)
    {
        return page < 1 ? 1 : page;
    }

    public static int NormalizePageSize(int pageSize)
    {
        const int defaultPageSize = 10;
        const int maxPageSize = 50;
        if (pageSize < 1)
        {
            return defaultPageSize;
        }
        return pageSize > maxPageSize ? maxPageSize : pageSize;
    }
}
