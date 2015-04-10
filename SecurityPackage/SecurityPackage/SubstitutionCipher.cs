using SecurityPackage.Ciphers;
using SecurityPackage.SubstitutionCiphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage
{
    public class SubstitutionCipher
    {
        private static GeneralCeaser generalCeaserInstance = new GeneralCeaser();
        private static Monoalphabetic monoalphabeticInstance = new Monoalphabetic();
        private static AutoKeyVigenere autoKeyVigenereInstance = new AutoKeyVigenere();
        private static RepeatingKeyVigenere repeatingKeyVigenereInstance = new RepeatingKeyVigenere();
        private static PlayFair playFairInstance = new PlayFair();
        private static HillCipher hillCipherInstance = new HillCipher();

        public SubstitutionCipher()
        {

        }

        public GeneralCeaser GeneralCeaserCipher()
        {
            return generalCeaserInstance;
        }

        public Monoalphabetic MonoalphabeticCipher()
        {
            return monoalphabeticInstance;
        }


        public AutoKeyVigenere AutoKeyVigenereCipher()
        {
            return autoKeyVigenereInstance;
        }

        public RepeatingKeyVigenere RepeatingKeyVigenereCipher()
        {
            return repeatingKeyVigenereInstance;
        }

        public PlayFair PlayFairCipher()
        {
            return playFairInstance;
        }

        public HillCipher HillCipher()
        {
            return hillCipherInstance;
        }
    }
}
