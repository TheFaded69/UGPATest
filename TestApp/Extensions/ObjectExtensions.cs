using TestApp.ViewModels.ExplorerViewModels;

namespace TestApp.Extensions;

public static class ObjectExtensions
{
    public static string GetPropertyName(this object obj)
    {
        var type = obj.GetType();
        var memInfo = type.GetMember(obj.ToString());
        if (memInfo.Length <= 0) return null;
        var attrs = memInfo[0].GetCustomAttributes(typeof(PropertyNameAttribute), false);
        return attrs.Length > 0 ? ((PropertyNameAttribute)attrs[0]).PropertyName : string.Empty;
    }
}