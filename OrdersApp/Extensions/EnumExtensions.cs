using OrdersApp.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OrdersApp.Extensions;

public static class EnumExtensions
{

    public static TEnum? GetEnumFromDescription<TEnum>(this string enumAsString)
        where TEnum : struct, Enum
    {
        var comparison = StringComparison.OrdinalIgnoreCase;
        foreach (var field in typeof(TEnum).GetFields())
        {
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute != null)
            {
                if (string.Compare(attribute.Description, enumAsString, comparison) == 0)
                    return (TEnum)field.GetValue(null);
            }
            if (string.Compare(field.Name, enumAsString, comparison) == 0)
                return (TEnum)field.GetValue(null);
        }
        return null;
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

    public static string Name(this Enum value)
    {
        if (value == null)
        {
            return null;
        }
        FieldInfo fi = value.GetType().GetField(value.ToString());

        if (fi == null)
        {
            return value.ToString();
        }

        DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

        if ((attributes != null) && (attributes.Length > 0))
        {
            return attributes[0].GetName();
        }
        else
        {
            return value.ToString();
        }
    }

    public static TEnum? GetEnumFromString<TEnum>(this string enumAsString)
        where TEnum : struct, Enum
    {
        var comparison = StringComparison.OrdinalIgnoreCase;
        foreach (var field in typeof(TEnum).GetFields())
        {
            var attribute = Attribute.GetCustomAttribute(field, typeof(StringValueAttribute)) as StringValueAttribute;
            if (attribute != null)
            {
                if (string.Compare(attribute.Value, enumAsString, comparison) == 0)
                    return (TEnum)field.GetValue(null);
            }
            if (string.Compare(field.Name, enumAsString, comparison) == 0)
                return (TEnum)field.GetValue(null);
        }
        return null;
    }

    public static string StringValue(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        if (fi == null)
        {
            return value.ToString();
        }

        StringValueAttribute[] attributes = (StringValueAttribute[])fi.GetCustomAttributes(
            typeof(StringValueAttribute), false);

        if ((attributes != null) && (attributes.Length > 0))
        {
            return attributes[0].Value;
        }
        else
        {
            return value.ToString();
        }
    }

    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? Arr[0] : Arr[j];
    }

}
