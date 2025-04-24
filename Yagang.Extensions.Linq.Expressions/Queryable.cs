namespace Yagang.Extensions.Linq.Expressions;

public static class Queryable
{
    public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
    {
        return queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
