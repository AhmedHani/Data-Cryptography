using SecurityPackage.Ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage.SubstitutionCiphers
{
    public class Monoalphabetic : Strategy 
    {
        private char[] alphabet = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z'};

        public Monoalphabetic() { }

        public override string encrypt(string text, string key)
        {
            string encrypted = "";
            char[] newAlphabet = key.ToCharArray(); // 26

            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < newAlphabet.Length; j++)
                {
                    if (!Char.IsLetter(text[i]))
                    {
                        encrypted += text[i];
                        break;
                    }

                    if (Char.IsUpper(text[i]))
                    {
                        char toSmall = Char.ToLower(text[i]);

                        if (toSmall == alphabet[j])
                        {
                            encrypted += Char.ToUpper(newAlphabet[j]);
                            break;
                        }
                    }
                    else
                    {
                        if (text[i] == alphabet[j])
                        {
                            encrypted += newAlphabet[j];
                            break;
                        }
                    }
                }
            }

            return encrypted.ToUpper();
        }

    }
}
