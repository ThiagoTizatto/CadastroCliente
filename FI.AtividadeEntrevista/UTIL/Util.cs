using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.UTIL
{
    public static class Util
    {
        public static string OnlyNumerics (this string value)
        {
            Regex regexObj = new Regex(@"[^\d]");
            string resultString = regexObj.Replace(value, "");
            return resultString;
        }

        public static string FormatCPF(this string CPF)
        {
            if (CPF.Length == 11)
            {
                CPF = CPF.Insert(9, "-");
                CPF = CPF.Insert(6, ".");
                CPF = CPF.Insert(3, ".");
            }
            return CPF;
        }
    }
}
