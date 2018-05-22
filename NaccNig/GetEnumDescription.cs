using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NaccNig
{
    public static class Helper
    {
           public static string GetEnumDescription(this Enum value)
            {
            //var field = value.GetType().GetField(value.ToString());
            //  var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            //return attr.Length == 0 ? value.ToString() : (attr[0] as DescriptionAttribute).Description;

            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }



           }
            public static object EnumValueOf(string value, Type enumType)
            {
                string[] names = Enum.GetNames(enumType);
                foreach (string name in names)
                {
                    if (GetEnumDescription((Enum)Enum.Parse(enumType, name)).Equals(value))
                    {
                        return Enum.Parse(enumType, name);
                    }
                }

                throw new ArgumentException("The string is not a description or value of the specified enum.");
            }

    }

}