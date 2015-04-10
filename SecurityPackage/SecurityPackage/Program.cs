using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage
{
    class Program
    {
        static void Main(string[] args)
        {
            //SubstitutionCipher cipher = new SubstitutionCipher();
            TranspositionCipher tCipher = new TranspositionCipher();

            Console.WriteLine(tCipher.ColumnarCipher().encrypt("Which wristwatches are swiss wristwatches", "4 2 5 3 1"));
        }
    }
}
