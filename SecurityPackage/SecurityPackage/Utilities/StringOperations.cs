using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage
{
    public static class StringOperations
    {
        public static string GetPureText(string text, ref List<string> nonAlpha)
        {
            string pureText = "";

            int textActualLength = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]))
                {
                    pureText += text[i];
                    textActualLength++;
                } // ... Build the pure string if the current char is alpha

                else
                {
                    nonAlpha.Add(text[i].ToString() + i.ToString());
                } // ... Build the nonAlpha array if the current char isn't alpha
            }

            return pureText;
        }

        public static string GetFullText(string pureText, List<string> nonAlpha)
        {
            int nonAlphaLength = nonAlpha.Count;
            char[] fullText = new char[pureText.Length + nonAlphaLength];

            for (int i = 0; i < nonAlphaLength; i++)
            {
                int currentNonAlphaIndex = int.Parse(nonAlpha[i].Substring(1, nonAlpha[i].Length - 1));

                fullText[currentNonAlphaIndex] = nonAlpha[i][0];
            } // ... Set non alpha chars in the array in their original indices

            int charIndex = 0;

            for (int i = 0; i < fullText.Length; i++)
            {
                if (fullText[i] == '\0')
                {
                    fullText[i] = pureText[charIndex++];

                    if (charIndex == pureText.Length)
                    {
                        break;
                    } // ... There is no more chars
                } // ... To not overwrite the non alpha index
            }

            return new string(fullText);
        }

    }
}
