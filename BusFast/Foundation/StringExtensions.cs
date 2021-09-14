using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Foundation
{
    public static class StringExtensions
    {

        public static string[] SearchTokens(this string s)
        {
            var wds = string.Concat(s.ToLowerInvariant().Select(c =>
               {
                   if (char.IsLetterOrDigit(c))
                       return c.ToString();

                   if (char.IsWhiteSpace(c))
                       return " ";

                   return "";
               }).ToArray());

            return wds.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
