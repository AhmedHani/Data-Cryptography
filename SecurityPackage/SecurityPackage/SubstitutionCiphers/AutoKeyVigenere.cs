using SecurityPackage.Ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage.SubstitutionCiphers
{
    public class AutoKeyVigenere : Strategy
    {
        private char[,] tabulaRecta;

        public AutoKeyVigenere()
        {
            this.tabulaRecta = new char[26, 26];

            this.initializeTable();
        }

        public override string encrypt(string text, string key)
        {
            List<string> nonAlpha = new List<string>();
            string pureText = StringOperations.GetPureText(text, ref nonAlpha);

            int textActualLength = pureText.Length;
            int nonAlphaLength = nonAlpha.Count;
            int keyActualLength = key.Length;

            if (keyActualLength < textActualLength)
            {
                for (int i = 0; i < textActualLength - keyActualLength; i++)
                {
                    if (Char.IsLetter(pureText[i]))
                    {
                        key += pureText[i];
                    }
                }
            } /* ... Append the text characters to the key
               * ex: plain text = MEETATTHEFOUNTAIN
               *     key = KILT
               *     new key = KILTMEETATTHEFOUN 
               */

            // Holds decrypted chars
            string encryptedPureText = "";

            for (int i = 0; i < textActualLength; i++)
            {
                int textCharIndex = Math.Abs(pureText[i] - 'A');
                int keyCharIndex = Math.Abs(key[i] - 'A');

                encryptedPureText += this.tabulaRecta[textCharIndex, keyCharIndex];
            } // ... Get the decrypted chars from the table

            // Holds the decrypted chars + non alpha chars
            char[] encryptedText = new char[text.Length];

            for (int i = 0; i < nonAlphaLength; i++)
            {
                int currentNonAlphaIndex = int.Parse(nonAlpha[i].Substring(1, nonAlpha[i].Length - 1));

                encryptedText[currentNonAlphaIndex] = nonAlpha[i][0];
            } // ... Set non alpha chars in the array in their original indices

            return StringOperations.GetFullText(encryptedPureText, nonAlpha).ToUpper();
        }

        public override string decrypt(string text, string key)
        {
            // Holds the text alphapet characters [a-zA-Z]
            List<string> nonAlpha = new List<string>();
            string pureText = StringOperations.GetPureText(text, ref nonAlpha);

            int textActualLength = pureText.Length;
            int nonAlphaLength = nonAlpha.Count;
            int keyActualLength = key.Length;

            if (textActualLength != key.Length)
            {
                Console.WriteLine("Text and Key should be the same length!");

                return string.Empty;
            }

            string decryptedPureText = "";

            for (int i = 0; i < textActualLength; i++)
            {
                char alpha = 'A';

                for (int j = 0; j < 26; j++)
                {
                    int keyCharacterIndex = Math.Abs(key[i] - 'A');

                    if (pureText[i] == this.tabulaRecta[j, keyCharacterIndex])
                    {
                        break;
                    }

                    alpha++;
                } /* ... We have the key [Column]
                   * Iterate through the decrypted, then find the row, if the char matched an index in the table
                   * get the char
                  */

                decryptedPureText += alpha;
            }

            return StringOperations.GetFullText(decryptedPureText, nonAlpha).ToUpper();
            
        }

        #region Initialize table's values
        private void initializeTable()
        {
            char alpha = 'A';

            for (int i = 0; i < 26; i++)
            {
                char init = alpha;

                for (int j = 0; j < 26; j++)
                {
                    this.tabulaRecta[j, i] = init++;

                    if (init > 'Z')
                    {
                        init = 'A';
                    }
                }

                alpha++;
            }
        }

        #endregion
    }
}
