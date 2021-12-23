using System;
using System.ComponentModel;
using System.Reflection;

namespace ReadImages.DTO.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this IConvertible value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            try
            {
                DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }
        public static T ToEnum<T>(this string description)
        {
            Type type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (FieldInfo field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description.ToUpper() == description.ToUpper())
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.ToUpper() == description.ToUpper())
                        return (T)field.GetValue(null);
                }
            }

            return default;
        }
    }
}
