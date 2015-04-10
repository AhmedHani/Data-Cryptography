using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage.Ciphers
{
    public abstract class Strategy
    {
        public virtual string encrypt(string text, int key) { return string.Empty; }
        public virtual string decrypt(string text, int key) { return string.Empty; }

        public virtual string encrypt(string text, string key) { return string.Empty; }
        public virtual string decrypt(string text, string key) { return string.Empty; }

        public virtual string encrypt(int[,] numbers, int[,] key) { return string.Empty; }
    }
}
