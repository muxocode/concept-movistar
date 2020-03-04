using System.Text.RegularExpressions;

namespace util.common
{
    public sealed class StringChecker
    {
        string Text;
        public StringChecker(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Check if a string is a valid email
        /// </summary>
        /// <param name = "text"> </param>
        /// <returns> If it's a valid email or not </returns>
        public bool IsEmail()
        {
            var match = Regex.Match(this.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        /// Check if a string is a valid phone
        /// </summary>
        /// <param name = "text"> </param>
        /// <returns> If it's a valid phone or not </returns>
        public bool IsPhone()
        {
            var match = Regex.Match(Text, @"[0-9]{7,}", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        /// Check if a string is a valid mobile
        /// </summary>
        /// <param name = "text"> </param>
        /// <returns> If it's a valid phone or not </returns>
        public bool IsMobile()
        {
            var match = Regex.Match(Text, @"[6-7]{1}[0-9]{6,}", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        /// Format a string according to a given input mask
        /// </summary>
        /// <param name = "text"> The input text. </param>
        /// <param name = "Mask"> The input mask. eg "A ## - ## - T - ### Z" </param>
        /// <example>
        /// var s = "aaaaaaaabbbbccccddddeeeeeeeeeeee" .FormatWithMask ("Hello ######## - # A ### - #### - #### - ############ Oww ");
        /// "Hello aaaaaaaa-bAbbb-cccc-dddd-eeeeeeeeeeee Oww";
        /// var s = "abc" .FormatWithMask ("### - #");
        /// "abc-";
        /// var s = "" .FormatWithMask ("Hello ######## - # A ### - #### - #### - ############ Oww ");
        /// "";
        /// </example>
        /// <returns> The formatted text </returns>
        public string IsFormatWithMask(string Mask)
        {
            if (string.IsNullOrEmpty(Text)) return Text;
            var output = string.Empty;
            var index = 0;
            foreach (var m in Mask)
            {
                if (m == '#')
                {
                    if (index < Text.Length)
                    {
                        output += Text[index];
                        index++;
                    }
                }
                else
                    output += m;
            }
            return output;
        }
        /// <summary>
        /// Check if a string is a valid ISIN (International Securities Identification Number)
        /// <param name = "text"> the text to check </param>
        /// <returns> If it is a valid ISIN or not ... </returns>
        /// </summary>
        public bool IsIsin()
        {
            Regex Pattern = new Regex("[A-Z]{2}([A-Z0-9]){10}", RegexOptions.Compiled);

            if (string.IsNullOrEmpty(Text))
            {
                return false;
            }
            if (!Pattern.IsMatch(Text))
            {
                return false;
            }

            var digits = new int[22];
            int index = 0;
            for (int i = 0; i < 11; i++)
            {
                char c = Text[i];
                if (c >= '0' && c <= '9')
                {
                    digits[index++] = c - '0';
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    int n = c - 'A' + 10;
                    int tens = n / 10;
                    if (tens != 0)
                    {
                        digits[index++] = tens;
                    }
                    digits[index++] = n % 10;
                }
                else
                {
                    // Not a digit or upper-case letter.
                    return false;
                }
            }
            int sum = 0;
            for (int i = 0; i < index; i++)
            {
                int digit = digits[index - 1 - i];
                if (i % 2 == 0)
                {
                    digit *= 2;
                }
                sum += digit / 10;
                sum += digit % 10;
            }

            int checkDigit = Text[11] - '0';
            if (checkDigit < 0 || checkDigit > 9)
            {
                // Not a digit.
                return false;
            }
            int tensComplement = (sum % 10 == 0) ? 0 : ((sum / 10) + 1) * 10 - sum;
            return checkDigit == tensComplement;
        }

        /// <summary>
        /// Find out if the last text is a valid URL
        /// </summary>
        /// <param name = "text"> </param>
        /// <returns> true in case it is </returns>
        public bool IsUrl()
        {
            Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(Text);
        }
    }
}
