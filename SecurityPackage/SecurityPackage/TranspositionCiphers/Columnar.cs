using SecurityPackage.Ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage.TranspositionCiphers
{
    public class Columnar : Strategy
    {
        public Columnar() { }

        public override string encrypt(string text, string key)
        {
            key = this.prepareKey(key);
            List<string> nonAlpha = new List<string>();
            string pureText = StringOperations.GetPureText(text, ref nonAlpha);

            int keyLength = key.Length;
            int columns = key.Length;
            int rows = (int)Math.Ceiling((double)pureText.Length / (double)columns);

            char[,] textMatrix = new char[rows, columns];

            int charsIndex = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    textMatrix[i, j] = pureText[charsIndex++];
                    if (charsIndex == pureText.Length) break;
                }
                if (charsIndex == pureText.Length) break;
            }

            string encryptedText = "";

            for (int columnIndex = 1; columnIndex <= columns; columnIndex++)
            {
                int targetIndex = 0;

                for (int i = 0; i < keyLength; i++)
                {
                    if (key[i] == columnIndex.ToString()[0])
                    {
                        targetIndex = i;
                        break;
                    }
                }

                for (int row = 0; row < rows; row++)
                {
                    encryptedText += textMatrix[row, targetIndex];
                }
            }

            return StringOperations.GetFullText(encryptedText, nonAlpha).ToUpper();
        }

        public override string decrypt(string text, string key)
        {
            key = this.prepareKey(key);
            List<string> nonAlpha = new List<string>();
            string pureText = StringOperations.GetPureText(text, ref nonAlpha);
            int keyLength = key.Length;
            int columns = key.Length;
            int rows = (int)Math.Ceiling((double)pureText.Length / (double)columns);

            char[,] textMatrix = new char[rows, columns];
            string decryptedText = "";
            int charsIndex = 0;

            for (int columnIndex = 1; columnIndex <= columns; columnIndex++)
            {
                int targetIndex = 0;

                for (int i = 0; i < keyLength; i++)
                {
                    if (key[i] == columnIndex.ToString()[0])
                    {
                        targetIndex = i;
                        break;
                    }
                }

                for (int row = 0; row < rows; row++)
                {
                    textMatrix[row, targetIndex] = pureText[charsIndex++];

                    if (charsIndex == pureText.Length) break;

                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    decryptedText += textMatrix[i, j];
                }
            }

            return StringOperations.GetFullText(decryptedText, nonAlpha).ToUpper();
        }

        private string prepareKey(string key)
        {
            string pureKey = "";

            for (int i = 0; i < key.Length; i++)
            {
                if (Char.IsDigit(key[i]))
                {
                    pureKey += key[i];
                }
            }

            return pureKey;
        }
    }
}
