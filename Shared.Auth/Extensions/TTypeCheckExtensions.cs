namespace Shared.Auth.Extensions;
public static class TTypeCheckExtensions {

    public static T ThrowIfNull<T>(this T? source , string propertyName) {
        if(source is null) 
            throw new ArgumentNullException(propertyName);
        return (T)source;
    }

}
