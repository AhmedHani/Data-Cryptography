using SecurityPackage.Ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage.SubstitutionCiphers
{
    public class PlayFair : Strategy
    {
        private const char DUMMY = 'X';
        private char[,] keyMatrix;

        public PlayFair()
        {
            this.keyMatrix = new char[5, 5];
        }

        public override string encrypt(string text, string key)
        {
            this.prepareKey(key);
            List<string> nonAlpha = new List<string>();
            string pureText = StringOperations.GetPureText(text, ref nonAlpha).ToUpper();
            int index = 0;

            int pureTextLength = pureText.Length;
            List<string> alphaPairs = new List<string>();

            for (int i = 0; i < pureTextLength; i++)
            {
                string temp = pureText[i].ToString();

                if (i + 1 != pureTextLength && pureText[i] == pureText[i + 1])
                {
                    temp += DUMMY;
                }

                else if (i + 1 != pureTextLength)
                {
                    temp += pureText[i + 1].ToString();
                    i++;
                }

                alphaPairs.Add(temp);
                index++;
            }

            if (alphaPairs[alphaPairs.Count - 1].Length == 1)
            {
                alphaPairs[alphaPairs.Count - 1] += DUMMY;
            }

            List<string> alphaEncryptedPairs = new List<string>();
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;

            for (int i = 0; i < alphaPairs.Count; i++) 
            {
                string currentPair = alphaPairs[i];

                this.getCharsPosition(currentPair[0], currentPair[1], ref x1, ref y1, ref x2, ref y2);
                alphaEncryptedPairs.Add(this.getNewChars(x1, y1, x2, y2, true));
            }

            string finalEncryption = string.Concat(alphaEncryptedPairs.ToArray());

            return StringOperations.GetFullText(finalEncryption, nonAlpha).ToUpper();
        }

        public override string decrypt(string text, string key)
        {
            this.prepareKey(key);
            List<string> nonAlpha = new List<string>();
            string pureText = StringOperations.GetPureText(text, ref nonAlpha).ToUpper();
            int index = 0;

            int pureTextLength = pureText.Length;
            List<string> alphaPairs = new List<string>();

            for (int i = 0; i < pureTextLength; i++)
            {
                string temp = pureText[i].ToString();

                if (i + 1 != pureTextLength && pureText[i] == pureText[i + 1])
                {
                    temp += DUMMY;
                }

                else if (i + 1 != pureTextLength)
                {
                    temp += pureText[i + 1].ToString();
                    i++;
                }

                alphaPairs.Add(temp);
                index++;
            }

            if (alphaPairs[alphaPairs.Count - 1].Length == 1)
            {
                alphaPairs[alphaPairs.Count - 1] += DUMMY;
            }

            List<string> alphaDecryptedPairs = new List<string>();
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;

            for (int i = 0; i < alphaPairs.Count; i++)
            {
                string currentPair = alphaPairs[i];

                this.getCharsPosition(currentPair[0], currentPair[1], ref x1, ref y1, ref x2, ref y2);
                alphaDecryptedPairs.Add(this.getNewChars(x1, y1, x2, y2, false));
            }

            string finalDecryption = string.Concat(alphaDecryptedPairs.ToArray());

            return StringOperations.GetFullText(finalDecryption, nonAlpha).ToUpper();

        }

        private void prepareKey(string key)
        {
            key = key.ToLower();
            int keyLength = key.Length;

            bool[] visited = new bool[26];
            bool endOfString = false;
            int endOfKeyRow = 0;
            int endOfKeyColumn = 0;

            int charIndex = 0;

            for (int row = 0; row < 5; row++)
            {
                if (endOfString)
                {
                    endOfKeyRow = row - 1;
                    break;
                }

                for (int column = 0; column < 5; )
                {
                    if (!visited[key[charIndex] - 'a'])
                    {
                        this.keyMatrix[row, column] = Char.ToUpper(key[charIndex]);
                        visited[key[charIndex] - 'a'] = true;

                        if (key[charIndex] == 'i')
                        {
                            visited['j' - 'a'] = true;
                        }

                        if (key[charIndex] == 'j')
                        {
                            visited['i' - 'a'] = true;
                        }

                        column++;
                    }

                    charIndex++;

                    if (charIndex == key.Length)
                    {
                        endOfString = true;
                        endOfKeyColumn = column;
                        break;
                    }
                }
            }

            List<char> remainingAlpha = new List<char>();

            for (char alpha = 'a'; alpha <= 'z'; alpha++)
            {
                if (!visited[alpha - 'a'])
                {
                    remainingAlpha.Add(Char.ToUpper(alpha));
                }
            }

            int remainingAlphaIndex = 0;

            for (int row = endOfKeyRow; row < 5; row++)
            {
                if (remainingAlphaIndex == remainingAlpha.Count) break;

                for (int column = endOfKeyColumn; column < 5; column++)
                {
                    if (this.keyMatrix[row, column] == '\0')
                    {
                        this.keyMatrix[row, column] = remainingAlpha[remainingAlphaIndex++];
                    }

                    if (remainingAlphaIndex == remainingAlpha.Count)
                    {
                        break;
                    }
                }
            }
        }

        private void getCharsPosition(char first, char second, ref int x1, ref int y1, ref int x2, ref int y2)
        {
            for (int i = 0; i < this.keyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.keyMatrix.GetLength(1); j++)
                {
                    if (this.keyMatrix[i, j] == first)
                    {
                        x1 = i;
                        y1 = j;
                    }

                    if (this.keyMatrix[i, j] == second)
                    {
                        x2 = i;
                        y2 = j;
                    }
                }
            }
        }

        private string getNewChars(int x1, int y1, int x2, int y2, bool encryption)
        {
            CASE charsCase = this.getCase(x1, y1, x2, y2);
            string encryptedChars = "";

            if (encryption)
            {
                switch (charsCase)
                {
                    case CASE.ROW:
                        encryptedChars += this.keyMatrix[x1, y1 % 4 == 0 ? y1 == 0 ? y1 += 1 : y1 = 0 : y1 = (y1 % 4) + 1]; //y1 = (y1 + 1) % 5 :D 
                        encryptedChars += this.keyMatrix[x2, y2 % 4 == 0 ? y2 == 0 ? y2 += 1 : y2 = 0 : y2 = (y2 % 4) + 1]; // y2 = (y2 + 1) % 5 :D
                        break;

                    case CASE.COLUMN:
                        encryptedChars += this.keyMatrix[x1 % 4 == 0 ? x1 == 0 ? x1 += 1 : x1 = 0 : x1 = (x1 % 4) + 1, y1]; // 
                        encryptedChars += this.keyMatrix[x2 % 4 == 0 ? x2 == 0 ? x2 += 1 : x2 = 0 : x2 = (x2 % 4) + 1, y2];
                        break;

                    case CASE.RECTANGLE:
                        encryptedChars += this.keyMatrix[x1, y2];
                        encryptedChars += this.keyMatrix[x2, y1];
                        break;
                }
            }

            else
            {
                switch (charsCase)
                {
                    case CASE.ROW:
                        encryptedChars += this.keyMatrix[x1, y1 % 4 == 0 ? y1 == 0 ? y1 = 4 : y1 = 0 : y1 = (y1 % 4) - 1]; //y1 = (y1 + 1) % 5 :D 
                        encryptedChars += this.keyMatrix[x2, y2 % 4 == 0 ? y2 == 0 ? y2 = 4 : y2 = 0 : y2 = (y2 % 4) - 1]; // y2 = (y2 + 1) % 5 :D
                        break;

                    case CASE.COLUMN:
                        encryptedChars += this.keyMatrix[x1 % 4 == 0 ? x1 == 0 ? x1 = 4 : x1 = 0 : x1 = (x1 % 4) - 1, y1]; // 
                        encryptedChars += this.keyMatrix[x2 % 4 == 0 ? x2 == 0 ? x2 = 4 : x2 = 0 : x2 = (x2 % 4) - 1, y2];
                        break;

                    case CASE.RECTANGLE:
                        encryptedChars += this.keyMatrix[x1, y2];
                        encryptedChars += this.keyMatrix[x2, y1];
                        break;
                }
            }

            return encryptedChars;
        }

        private CASE getCase(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2)
            {
                return CASE.ROW;
            }
            else if (y1 == y2)
            {
                return CASE.COLUMN;
            }
            else
            {
                return CASE.RECTANGLE;
            }
        }

        private enum CASE
        {
            RECTANGLE,
            COLUMN,
            ROW
        }
    }
}
