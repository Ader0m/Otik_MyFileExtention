using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.Haffman
{
    internal class Node
    {
        public readonly char Symbol;
        public readonly int Freq;
        public readonly Node? Bit0;
        public readonly Node? Bit1;

        public Node(char symbol, int freq)
        {
            this.Symbol = symbol;
            this.Freq = freq;
        }

        public Node(int freq, Node? bit0, Node? bit1)
        {
            this.Bit0 = bit0;
            this.Bit1 = bit1;
            this.Freq = freq;
        }
    }
}
