using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp1
{
    public static class PhonewordTranslator
    {
        public static string ToNumber(string txt)
        {
            string retVal = string.Empty;

            if (string.IsNullOrWhiteSpace(txt)) return null;

            string letters = txt.ToUpper();

            foreach(var letter in letters)
            {
                if ("0123456789-".Contains(letter))
                {
                    retVal += letter;
                }
                else
                {
                    var check = numPairs.Find(x => x.Contains(letter));
                    if(check != null)
                    {
                        retVal += Convert.ToString(numPairs.IndexOf(check) + 2);
                    }
                }
            }

            return retVal;
        }

        static List<string> numPairs = new List<string>{ "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ" };
    }
}
