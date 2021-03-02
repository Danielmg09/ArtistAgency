using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation.Utilities
{
    public static class GeneralUtilities
    {
        public static int? askForInt(string text)
        {
            Console.WriteLine(text);
            int? result = null;
            if (int.TryParse(Console.ReadLine(), out int intResult))
            {
                result = intResult;
            }
            return result;
        }
        public static decimal? askForDecimal(string text)
        {
            Console.WriteLine(text);
            decimal? result = null;
            if (int.TryParse(Console.ReadLine(), out int decResult))
            {
                result = decResult;
            }
            return result;
        }

        public static string askForString(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();

        }


    }
}
