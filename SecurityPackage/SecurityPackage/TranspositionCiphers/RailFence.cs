using SecurityPackage.Ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage.TranspositionCiphers
{
    public class RailFence : Strategy
    {
        public RailFence() {}

        public override string encrypt(string text, int key)
        {
            List<string> nonAlpha = new List<string>();
            string pureText = StringOperations.GetPureText(text, ref nonAlpha);

            int rows = key;
            int columns = (int)Math.Ceiling((double)pureText.Length / (double)key);
            char[,] textMatrix = new char[rows, columns];

            int charsIndex = 0;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    textMatrix[j, i] = pureText[charsIndex++];
                    if (charsIndex == pureText.Length) break; 
                }
                if (charsIndex == pureText.Length) break; 
            }
            
            string encryptedText = "";

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (textMatrix[i, j] != '\0')
                    {
                        encryptedText += textMatrix[i, j];
                    }
                }
            }

            return StringOperations.GetFullText(encryptedText, nonAlpha).ToUpper();
        }

        public override string decrypt(string text, int key)
        {
            List<string> nonAlpha = new List<string>();
            string pureText = StringOperations.GetPureText(text, ref nonAlpha);

            int rows = key;
            int columns = (int)Math.Ceiling((double)pureText.Length / (double)key);
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

            string decryptedText = "";

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (textMatrix[j, i] != '\0')
                    {
                        decryptedText += textMatrix[j, i];
                    }
                }
            }

            return StringOperations.GetFullText(decryptedText, nonAlpha).ToUpper();
        }
    }
}
