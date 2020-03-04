using System;
using System.Linq;
using System.Text;

namespace util.common
{
    /// <summary>
    /// Extension methos to string
    /// </summary>
    public static class StringUtil
    {
        public static StringChecker Check(this string text)
        {
            return new StringChecker(text);
        }
        /// <summary>
        /// Get last positions from a string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="number"></param>
        /// <returns>El string resultado</returns>
        public static string Last(this string text, int number)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var value = text.Trim();
            return number >= value.Length ? value : value.Substring(value.Length - number);
        }


        /// <summary>
        /// Repeat the string as many times as the parameters indicate
        /// </summary>
        /// <param name = "text"> </param>
        /// <param name = "number"> </param>
        /// <returns> </returns>
        public static string Repeat(this string text, int number)
        {
            if (text == null)
            {
                return null;
            }

            var sb = new StringBuilder();

            for (var repeat = 0; repeat < number; repeat++)
            {
                sb.Append(text);
            }

            return sb.ToString();
        }

        static Random random;
        static void initRandom()
        {
            random = random ?? new Random();
        }
        /// <summary>
        /// Gets a randomize string with size
        /// </summary>
        /// <returns></returns>
        public static string Random(int size = 7, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            initRandom();
            return new string(Enumerable.Repeat(chars, size).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Randomize a text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Random(this string text)
        {
            return Random(text.Length, text);
        }

    }
}
