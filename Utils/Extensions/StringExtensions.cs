using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HeliumBot.Utils.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// in: {111,222,333,444} param=1 out:{222,333,444}
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string[] CopyStringArray(this string[] ex, int startIndex)
        {
            var arr = new string[ex.Length - startIndex];
            
            Array.Copy(ex, startIndex, arr, 0, ex.Length - startIndex);

            return arr;
        }
        
        public static bool IsEmptyOrNull(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNumber(this string source)
        {
            try
            {
                Convert.ToInt32(source);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ToElement(this string source)
        {
            switch (source)
            {
                case "Anemo":
                    return "风";

                case "Pyro":
                    return "火";

                case "Electro":
                    return "雷";

                case "Cryo":
                    return "冰";

                case "Geo":
                    return "岩";

                case "Hydro":
                    return "水";

                default:
                    return source;
            }
        }

        public static string GetMd5(this string str)
        {
            var md5 = MD5.Create();
            var buffer = Encoding.Default.GetBytes(str);
            var bufferMd5 = md5.ComputeHash(buffer);
            var sb = new StringBuilder();

            foreach (var t in bufferMd5)
                sb.Append(t.ToString("x2"));

            return sb.ToString();
        }

        public static long ToLong(this string ex)
        {
            return Convert.ToInt64(ex);
        }

        public static DateTime TimestampToDateTime(this string ex)
        {
            return DateTimeOffset.FromUnixTimeSeconds(ex.ToLong()).DateTime;
        }
    }
}