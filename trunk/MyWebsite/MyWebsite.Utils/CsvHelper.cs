using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyWebsite.Utils
{
    public static class CsvHelper
    {
        private const string QUOTE = "\"";
        private const string ESCAPED_QUOTE = "\"\"";
        private static readonly char[] EscapableCharacters = { '"', ',', '\r', '\n' };

        public static void ToCsv<T>(IEnumerable<T> collection, Stream stream, params string[] onlyFields)
            where T : class
        {
            Dictionary<PropertyInfo, string> fieldNames = null;
            var sw = new StreamWriter(stream, Encoding.UTF8);

            foreach (T item in collection)
            {
                // Only on the first iteration we get the list of properties from the object type
                // We use a dictionary of <PropertyInfo, string> instead of just a list of PropertyInfo, 
                // because we extract the display name of the property (if exists) to use it as the "column" header
                if (fieldNames == null)
                {
                    fieldNames = GetProperties(typeof(T), onlyFields);
                    // Write the column headers
                    WriteRow(sw, fieldNames.Select(v => v.Value));
                }

                T current = item;
                IEnumerable<string> valueList = fieldNames.Keys.Select(prop => prop.GetValue(current, null))
                                                          .Select(Convert.ToString);

                WriteRow(sw, valueList);
            }

            // Reset the stream position to the beginning
            stream.Seek(0, SeekOrigin.Begin);
        }

        private static Dictionary<PropertyInfo, string> GetProperties(Type type, string[] onlyFields)
        {
            IOrderedEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                                             BindingFlags.FlattenHierarchy)
                                                              .Where(prop => IsSimpleOrNullableType(prop.PropertyType))
                                                              .OrderBy(prop =>
                                                              {
                                                                  var displayAttr =
                                                                      prop.GetCustomAttributes(
                                                                          typeof(DisplayAttribute), true)
                                                                          .FirstOrDefault() as DisplayAttribute;
                                                                  return displayAttr != null
                                                                             ? displayAttr.GetOrder()
                                                                             : int.MaxValue;
                                                              });

            // If the entity has MetadataTypeAttribute's, use them
            var metadata = (MetadataTypeAttribute[])type.GetCustomAttributes(typeof(MetadataTypeAttribute), true);

            var names = new Dictionary<PropertyInfo, string>();
            foreach (PropertyInfo property in properties)
            {
                if (onlyFields.Length == 0 ||
                    onlyFields.Contains(property.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    string text = GetDisplayName(property, metadata);

                    names.Add(property, text);
                }
            }

            return names;
        }

        private static string GetDisplayName(PropertyInfo property, IEnumerable<MetadataTypeAttribute> metadata)
        {
            // Extract the display name from the DisplayAttribute on the object or on any of the MetadataTypeAttribute's 
            // it may contain
            string displayText = metadata.Select(m => m.MetadataClassType.GetProperty(property.Name))
                                         .Where(p => p != null)
                                         .SelectMany(
                                             p =>
                                             (DisplayAttribute[])p.GetCustomAttributes(typeof(DisplayAttribute), true))
                                         .Concat(
                                             (DisplayAttribute[])
                                             property.GetCustomAttributes(typeof(DisplayAttribute), true))
                                         .Select(m => m.GetName())
                                         .FirstOrDefault(n => !String.IsNullOrEmpty(n));

            // Return the display text if found, otherwise return the property name
            return displayText ?? property.Name;
        }

        private static bool IsSimpleOrNullableType(Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                t = Nullable.GetUnderlyingType(t);
            }

            return IsSimpleType(t);
        }

        private static bool IsSimpleType(Type t)
        {
            return t.IsPrimitive || t.IsEnum || t == typeof(string) || t == typeof(Decimal) || t == typeof(DateTime) ||
                   t == typeof(Guid);
        }

        private static void WriteRow(TextWriter sw, IEnumerable<string> values)
        {
            int index = 0;
            foreach (string value in values)
            {
                if (index > 0)
                {
                    sw.Write(",");
                }

                WriteValue(sw, value);
                index++;
            }

            sw.Write(Environment.NewLine);
            sw.Flush();
        }

        private static void WriteValue(TextWriter sw, string value)
        {
            bool needsEscaping = value.IndexOfAny(EscapableCharacters) >= 0;

            if (needsEscaping)
            {
                sw.Write(QUOTE);
                sw.Write(value.Replace(QUOTE, ESCAPED_QUOTE));
                sw.Write(QUOTE);
            }
            else
            {
                sw.Write(value);
            }
        }
    }
}
