using SecurityPackage.TranspositionCiphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityPackage
{
    public class TranspositionCipher
    {
        private static Columnar columnarCipherInstance = new Columnar();
        private static RailFence railFenceInstance = new RailFence();

        public TranspositionCipher()
        {

        }

        public Columnar ColumnarCipher()
        {
            return columnarCipherInstance;
        }

        public RailFence RailFenceCipher()
        {
            return railFenceInstance;
        }
    }
}
