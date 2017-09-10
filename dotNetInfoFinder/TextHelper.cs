using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dotNetVersionFinder
{
    public class TextHelper
    {

        public static string Clean(string text)
        {
            return Regex.Replace(text, "[^ -~]", string.Empty);
        }

        public static string GetFramework(string text)
        {
            try
            {
                var temp = Regex.Replace(text, "[^ -~]", "@");
                var index = temp.IndexOf('@');

                text = temp.Remove(index);

                if (text.Contains("4.6.1"))
                {
                    return "4.6.1";
                }
                else if (text.Contains("4.5.2"))
                {
                    return "4.5.2";
                }
                else if (text.Contains("4.5.1"))
                {
                    return "4.5.1";
                }
                else if (text.Contains("4"))
                {
                    return "4";
                }
                else if (text.Contains("3.5"))
                {
                    return "3.5";
                }
            }
            catch (Exception ex)
            {

            }

            return text;

            //foreach (var x in temp)
        }
    }
}
