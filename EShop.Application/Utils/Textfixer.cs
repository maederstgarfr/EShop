using System;
using System.Text.RegularExpressions;

namespace EShop.Application.Utils
{
    public static class Textfixer
    {
        public static string FixText(this string text) => text?.Trim().Replace(" ", " ");
        public static string FixEmail(this string email) => email?.Trim().Replace(" ", " ");
        public static string RemoveHtmlTagsExceptionBreak(string text) => Regex.Replace(text, @"<(?!br[\x20/>])[^<>]+>");
        public static string ReplaceNewLineTextArea(string text) => text?.Replace(Environment.NewLine, "<br />");
        public static string ReplaceBrToNewLine(string text) => text?.Replace( "<br />",Environment.NewLine);

        public static string FixTextForUrl(this string text)
        {
            return text.Replace(" ", "-");
        }

        public static string ConvertBrToNewLine(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace("<br />", Environment.NewLine);
        }

        public static string ConvertNewLineToBr(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace( Environment.NewLine,"<br />");
        }
        
        public static string FixdEmail(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace( Environment.NewLine,"<br />");
        }
        public static string[] SplitTags(this string tags)
        {
            return tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string FixTitleForUrl(this string url)
        {
            return url.Replace(" ", "-").Replace("+", "").Replace("#", "");
        }
        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
        public static string LongString150(this string text, int lengh = 150)
        {
            if (text.Length>= lengh)
            {
                return text.Substring(0, lengh) + "...";
            }

            return text;
        }
        public static string LongString50(this string text, int lengh = 50)
        {
            if (text.Length>= lengh)
            {
                return text.Substring(0, lengh) + "...";
            }

            return text;
        }
        public static string LongString40(this string text, int lengh = 40)
        {
            if (text.Length>= lengh)
            {
                return text.Substring(0, lengh) + "...";
            }

            return text;
        }
        public static string LongString30(this string text, int lengh = 30)
        {
            if (text.Length >= lengh)
            {
                return text.Substring(0, lengh) + "...";
            }

            return text;
        }
        public static string LongString20(this string text, int lengh = 20)
        {
            if (text.Length >= lengh)
            {
                return text.Substring(0, lengh) + "...";
            }

            return text;
        }
        public static string ToRial(this int Price)
        {
            return Price.ToString("#, 0 ");

    }
}
