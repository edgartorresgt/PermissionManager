using System.ComponentModel;

namespace PermissionManager.Core.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());

        var attributes = (DescriptionAttribute[])fi!.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length switch
        {
            > 0 => attributes[0].Description,
            _ => value.ToString()
        };
    }
}