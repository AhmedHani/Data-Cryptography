using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage.Ciphers
{
    public class GeneralCeaser : Strategy
    {
        public GeneralCeaser() {}

        public override string encrypt(string text, int key)
        {
            string encrypted = "";
            key = key % 26 + 26;

            for (int i = 0; i < text.Length; i++)
            {
                if (!Char.IsLetter(text[i]))
                {
                    encrypted += text[i];
                    continue;
                }

                if (Char.IsUpper(text[i]))
                {
                    encrypted += ((char)('A' + (text[i] - 'A' + key) % 26));
                }
                else
                {
                    encrypted += ((char)('a' + (text[i] - 'a' + key) % 26));
                }
            }

            return encrypted.ToUpper();
        }

        public override string decrypt(string text, int key)
        {
            key = 26 - key;

            return this.encrypt(text, key);
        }
    }
}
