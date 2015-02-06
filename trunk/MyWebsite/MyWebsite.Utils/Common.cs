using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;

namespace MyWebsite.Utils
{
    public sealed class Common
    {
        public static String UCS2Convert(String sContent)
        {
            sContent = sContent.Trim();
            String sUTF8Lower = "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

            String sUTF8Upper = "A|Á|À|Ả|Ã|Ạ|Ă|Ắ|Ằ|Ẳ|Ẵ|Ặ|Â|Ấ|Ầ|Ẩ|Ẫ|Ậ|Đ|E|É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ|I|Í|Ì|Ỉ|Ĩ|Ị|O|Ó|Ò|Ỏ|Õ|Ọ|Ô|Ố|Ồ|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ|U|Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự|Y|Ý|Ỳ|Ỷ|Ỹ|Ỵ";

            String sUCS2Lower = "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

            String sUCS2Upper = "A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|D|E|E|E|E|E|E|E|E|E|E|E|E|I|I|I|I|I|I|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|U|U|U|U|U|U|U|U|U|U|U|U|Y|Y|Y|Y|Y|Y";

            String[] aUTF8Lower = sUTF8Lower.Split(new Char[] { '|' });

            String[] aUTF8Upper = sUTF8Upper.Split(new Char[] { '|' });

            String[] aUCS2Lower = sUCS2Lower.Split(new Char[] { '|' });

            String[] aUCS2Upper = sUCS2Upper.Split(new Char[] { '|' });

            Int32 nLimitChar;

            nLimitChar = aUTF8Lower.GetUpperBound(0);

            for (int i = 1; i <= nLimitChar; i++)
            {

                sContent = sContent.Replace(aUTF8Lower[i], aUCS2Lower[i]);

                sContent = sContent.Replace(aUTF8Upper[i], aUCS2Upper[i]);

            }
            string sUCS2regex = @"[A-Za-z0-9- ]";
            string sEscaped = new Regex(sUCS2regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).Replace(sContent, String.Empty);
            if (String.IsNullOrEmpty(sEscaped))
                return sContent;
            sEscaped = sEscaped.Replace("[", "\\[");
            sEscaped = sEscaped.Replace("]", "\\]");
            sEscaped = sEscaped.Replace("^", "\\^");
            string sEscapedregex = @"[" + sEscaped + "]";
            return new Regex(sEscapedregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).Replace(sContent, String.Empty);
        }

        public static string ConvertToUrlString(string str)
        {
            str = (new Regex(@"[-]{2}")).Replace((new Regex(@"[^\w]")).Replace(UCS2Convert(str.ToLower()), "-"), "-");

            return str;
        }

        public static string ConvertToUrlTitle(string str)
        {
            str = (new Regex(@"\s+")).Replace((new Regex(@"[^\w]")).Replace(UCS2Convert(str.ToLower()), " "), " ");

            return str;
        }

        public static string SubString(string input, int length)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            if (input.Length < length)
            {
                return input;
            }
            int endPoint = input.Substring(0, length).LastIndexOf(" ");
            //endPoint = endPoint < 0 ? length : endPoint; // khong cat duoc
            endPoint = (length / 2 + 1) > endPoint ? length : endPoint; // khong cat duoc

            string ret = input.Substring(0, endPoint);

            if (ret.Length > length)
            {
                ret = ret.Substring(0, length);
            }

            return ret + " ...";
        }

        public static string SubStringComment(string input)
        {
            if (input.Contains("\n"))
            {
                return input.Substring(0, input.IndexOf('\n', 0));
            }
            return input;
        }

        public static string SubString2(string input, int length)
        {
            return SubString(input, length).Replace(" ...", "");
        }

        public static string BoolToString(bool value)
        {
            if (value)
            {
                return "Yes";
            }
            return "No";
        }

        public static bool IsPredefinedType(object obj)
        {
            if (obj.GetType() != typeof (bool)&&
                obj.GetType() != typeof (byte)&&
                obj.GetType() != typeof (sbyte)&&
                obj.GetType() != typeof (char)&&
                obj.GetType() != typeof (decimal)&&
                obj.GetType() != typeof (double)&&
                obj.GetType() != typeof (float)&&
                obj.GetType() != typeof (int)&&
                obj.GetType() != typeof (long)&&
                obj.GetType() != typeof (ulong)&&
                obj.GetType() != typeof (short)&&
                obj.GetType() != typeof (ushort)&&
                obj.GetType() != typeof (string)&&
                obj.GetType() != typeof(DateTime))
            {
                return false;
            }
            return true;
        }

        public static bool IsPredefinedType(Type type)
        {
            if (type != typeof(bool) &&
                type != typeof(byte) &&
                type != typeof(sbyte) &&
                type != typeof(char) &&
                type != typeof(decimal) &&
                type != typeof(double) &&
                type != typeof(float) &&
                type != typeof(int) &&
                type != typeof(long) &&
                type != typeof(ulong) &&
                type != typeof(short) &&
                type != typeof(ushort) &&
                type != typeof(string) &&
                type != typeof(DateTime) &&
                !type.IsEnum)
            {
                return false;
            }
            return true;
        }

        public static bool IsObjectEqual<T>(object obj1, object obj2) where T : class, new()
        {
            var properties = obj1.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var value1 = property.GetValue(obj1);
                var value2 = property.GetValue(obj2);
                if (value1 != null && value2 != null)
                {
                    if (IsPredefinedType(value1))
                    {
                        if (value1.ToString().Trim() != value2.ToString().Trim())
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!IsObjectEqual<T>(value1, value2))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        public static T SubStringObject<T>(object fromSource) where T : class, new()
        {
            var ret = new T();
            if (fromSource == null)
            {
                return ret;
            }
            var sourceProps = fromSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var desProp in destinationProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(m => m.Name == desProp.Name);

                if (sourceProp == null || desProp.SetMethod == null) continue;
                var sourceValue = sourceProp.GetValue(fromSource, null);
                if (sourceValue != null)
                {
                    desProp.SetValue(ret,
                        sourceProp.PropertyType == typeof (string)
                            ? SubString(sourceValue.ToString(), 30)
                            : sourceValue, null);
                }
            }

            return ret;
        }

        public static string GetAppStr(string name)
        {
            return ConfigurationManager.AppSettings[name] ?? String.Empty;
        }

        public static string ChangeUrl(string currentUrl, string queryName, string queryValue)
        {
            string result = "";
            if (currentUrl.Contains("?"))
            {
                if (currentUrl.Contains("&" + queryName))
                {
                    string backupUrl = currentUrl.Substring(currentUrl.IndexOf("&" + queryName) + 1);
                    if (backupUrl.Contains("&"))
                    {
                        result = currentUrl.Replace("&" + backupUrl.Substring(0, backupUrl.IndexOf("&")),
                            "&" + queryName + "=" + queryValue);
                    }
                    else
                    {
                        result = currentUrl.Replace("&" + backupUrl, "&" + queryName + "=" + queryValue);
                    }
                }
                else if (currentUrl.Contains(queryName))
                {
                    string backupUrl = currentUrl.Substring(currentUrl.IndexOf(queryName));
                    if (backupUrl.Contains("&"))
                    {
                        result = currentUrl.Replace(backupUrl.Substring(0, backupUrl.IndexOf("&")),
                            queryName + "=" + queryValue);
                    }
                    else
                    {
                        result = currentUrl.Replace(backupUrl, queryName + "=" + queryValue);
                    }
                }
                else
                {
                    result = currentUrl + "&" + queryName + "=" + queryValue;
                }
            }
            else
            {
                if (currentUrl.Contains("/"))
                {
                    result = currentUrl + "?" + queryName + "=" + queryValue;
                }
                else
                {
                    result = currentUrl + "/?" + queryName + "=" + queryValue;
                }
            }
            return result;
        }

        public static string ChangeLanguage(string currentUrl, string lang)
        {
            string result = ChangeUrl(currentUrl, Constants.LanguageQueryString, lang);
            return result;
        }
        #region Sorter

        public static void Sort<T>(ref IQueryable<T> list, string expression, string sortDirection) where T : class
        {
            if (expression == null || expression.Trim().Length < 1)
            {
                return;
            }

            string command = sortDirection.Equals("asc") ? "OrderBy" : "OrderByDescending";

            var type = typeof(T);

            var property = type.GetProperty(expression);

            var parameter = Expression.Parameter(type, "p");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },

                                   list.Expression, Expression.Quote(orderByExpression));

            list = list.Provider.CreateQuery<T>(resultExpression);

        }

        #endregion
    }
}
