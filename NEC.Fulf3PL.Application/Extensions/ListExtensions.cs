namespace NEC.Fulf3PL.Application.Extensions;

public static class ListExtensions
{
    /// <summary>
    ///     Add <paramref name="item"/> to <paramref name="list"/> if <paramref name="value"/> is not empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value">Value to check</param>
    /// <param name="item">Item to add</param>
    /// <returns></returns>
    public static List<T> AddItemIfValueNotEmpty<T>(this List<T> list, string value, T item)
    {
        if (!string.IsNullOrEmpty(value))
        {
            list.Add(item);
        }

        return list;
    }

    /// <summary>
    ///     Add <paramref name="item"/> to <paramref name="list"/> if <paramref name="value"/> is not null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    /// <param name="list"></param>
    /// <param name="value">Value to check</param>
    /// <param name="item">Item to add</param>
    /// <returns></returns>
    public static List<T> AddItemIfValueNotNull<T, TVal>(this List<T> list, TVal value, T item)
    {
        if (value != null)
        {
            list.Add(item);
        }

        return list;
    }

    /// <summary>
    ///     Add result of <paramref name="itemExpression"/> to <paramref name="list"/> if <paramref name="value"/> is not empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value">Value to check</param>
    /// <param name="itemExpression">ItemExpression to call when value not empty</param>
    /// <returns></returns>
    public static List<T> AddItemIfValueNotEmpty<T>(this List<T> list, string value, Func<T> itemExpression)
    {
        if (!string.IsNullOrEmpty(value))
        {
            list.Add(itemExpression());
        }

        return list;
    }

    /// <summary>
    ///     Add result of <paramref name="itemExpression"/> to <paramref name="list"/> if <paramref name="value"/> is not null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    /// <param name="list"></param>
    /// <param name="value">Value to check</param>
    /// <param name="itemExpression">ItemExpression to call when value not null</param>
    /// <returns></returns>
    public static List<T> AddItemIfValueNotNull<T, TVal>(this List<T> list, TVal value, Func<T> itemExpression)
    {
        if (value != null)
        {
            list.Add(itemExpression());
        }

        return list;
    }

}
