using Pawn_Shop.Utilities.IUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Utilities
{
    class MMMoneyConverter : IMMMoneyConverter
    {
        private readonly Dictionary<char, string> numbersInMM = new Dictionary<char, string>
            {
                { '0', "သုည" },
                { '1', "တစ်" },
                { '2', "နှစ်" },
                { '3', "သုံး" },
                { '4', "လေး" },
                { '5', "ငါး" },
                { '6', "ခြောက်" },
                { '7', "ခုနစ်" },
                { '8', "ရှစ်" },
                { '9', "ကိုး" }
            };

        private readonly Dictionary<int, string> units = new Dictionary<int, string>
            {
                { 1, "" },
                { 2, "ဆယ်" },
                { 3, "ရာ" },
                { 4, "ထောင်" },
                { 5, "သောင်း" },
                { 6, "သိန်း" },
                { 7, "သန်း" }
            };

        /**
         * Money converter from En to Mm (supports up to 9999999)
         */
        public string Convert(string moneyInEn)
        {
            var moneyInEnCharArray = moneyInEn.ToCharArray();
            int index = moneyInEnCharArray.Length;

            string result;
            if (index > 6)
            {
                char[] bb = moneyInEn.Substring(2).ToCharArray();
                char[] cc = moneyInEn.Substring(0, 2).ToCharArray();
                result = calculate(cc, cc.Length) + "သိန်း" + calculate(bb, bb.Length);
            }
            else
            {
                result = calculate(moneyInEnCharArray, index);
            }

            return result;
        }

        private string calculate(char[] moneyInEn, int index)
        {
            string result = "";
            foreach (char c in moneyInEn)
            {
                if (!'0'.Equals(c))
                {
                    if (index == 1) result += numbersInMM[c];
                    else result += numbersInMM[c] + units[index];
                }
                index--;
            }

            return result;
        }
    }
}
