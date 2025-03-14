using Microsoft.OpenApi.Attributes;
using Microsoft.OpenApi.Extensions;
using OrdersApp.Enums;
using System.ComponentModel;
using System.Reflection;

namespace OrdersApp.Extensions;

public static class CommandsExtensions
{

    public static Commands GetByDescriptionName(string desriptionName)
    {
        var enumType = typeof(Commands);
        Commands comm = Enum.GetValues(typeof(Commands))
                        .Cast<Commands>()
                        .FirstOrDefault(v => v.GetDescription() == desriptionName);

        return comm;
    }

    public static string? GetDescription(this Enum value)
    {
        Type type = value.GetType();
        string? name = Enum.GetName(type, value);
        if (name != null)
        {
            FieldInfo? field = type.GetField(name);
            if (field != null)
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                {
                    return attr.Description;
                }
            }
        }
        return null;
    }

}
