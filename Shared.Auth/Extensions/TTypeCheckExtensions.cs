using Shared.Auth.Exceptions;

namespace Shared.Auth.Extensions;
public static class TTypeCheckExtensions {

    public static T ThrowIfNull<T>(this T? source , string propertyName) {
        if(source is null)
            throw new ArgumentNullException(propertyName);
        return (T) source;
    }

    public static T? ThrowIfFound<T>(this T? source , string propertyName) {
        if(source is not null)
            throw new NotNullException("Founded" , $"The <{propertyName}> must be not exist");
        return source;
    }


    public static Dictionary<TKey , TValue> ThrowIfEmpty<TKey, TValue>(
        this Dictionary<TKey , TValue> dic , string propertyName) where TKey:notnull {
        if(dic is null)
            throw new ArgumentNullException(propertyName);
        if(dic.Count <= 0) {
            throw new EmptyListException($"the <{propertyName} can not be empty.");
        }
        return dic;
    }

}
