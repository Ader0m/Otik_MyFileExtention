using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Otik_MyFileExtention.SymbolFrequency;

namespace Otik_MyFileExtention.Haffman
{
    internal class HaffmanLogiс
    {
        private SortedDictionary<char, string> _codes;
        private Node root;
        private int countSymbol;


        public HaffmanLogiс()
        {
            ;
        }

        public HaffmanLogiс(Dictionary<char, int> freqs)
        {
            root = CreateHuffmanTree(freqs);
        }

        /// <summary>
        /// Для архивации. Бесполезен для разорхивирования.
        /// </summary>
        /// <param name="filePath"></param>
        public HaffmanLogiс(byte[] fileinfo)
        {
            FriquencyController controller = new FriquencyController(new UTF8Frequency(), fileinfo);

            controller.SortKey();
        }

        /// <summary>
        /// Для архивации
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public byte[] GetCompressData(byte[] content)
        {
            CreateHuffmanCode(CreateHuffmanTree(FriquencyController.FrequencyDict));

            return Compress(Encoding.UTF8.GetChars(content));
        }

        /// <summary>
        /// Для разорхивирования
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<char, string> GetHuffmanCode(Dictionary<char, int> frequencyDict)
        {
            return CreateHuffmanCode(CreateHuffmanTree(frequencyDict));
        }

        private byte[] Compress(char[] content)
        {
            List<byte> data = new List<byte>();
            byte sum = 0;
            byte bit = 1;


            foreach (char c in content)
            {
                foreach (char b in _codes[c])
                {
                    if (b == '1')
                    {
                        sum |= bit;
                    }
                    if (bit < 128)
                    {
                        bit <<= 1;
                    }
                    else
                    {
                        data.Add(sum);
                        sum = 0;
                        bit = 1;
                    }
                }
            }

            if (bit > 1)
                data.Add(sum);

            return data.ToArray();
        }

        public List<char> Decompress(ReadOnlySpan<byte> info, int dataLength)
        {
            int size = 0;
            Node curr = root;
            List<char> data = new List<char>();
            for (int i = 0; i < info.Length; i++)
            {
                for (int bit = 1; bit <= 128; bit <<= 1)
                {
                    bool zero = (info[i] & bit) == 0;
                    if (zero)
                        curr = curr.Bit0;
                    else
                        curr = curr.Bit1;
                    if (curr.Bit0 != null)
                        continue;
                    if (size++ < dataLength)
                        data.Add(curr.Symbol);
                    curr = root;
                }
            }

            return data;
        }

        private SortedDictionary<char, string> CreateHuffmanCode(Node root)
        {
            _codes = new SortedDictionary<char, string>();

            #region LocalFunc

            void Next(Node node, string code)
            {
                if (node.Bit0 == null)
                {
                    _codes[node.Symbol] = code;
                }
                else
                {
                    Next(node.Bit0, code + "0");
                    Next(node.Bit1, code + "1");

                }
            }

            #endregion

            Next(root, "");

            return _codes;
        }

        private Node CreateHuffmanTree(Dictionary<char, int> freqs)
        {
            PriorityQueue<Node> pq = new PriorityQueue<Node>();

            foreach (var freq in freqs)
            {
                pq.Enqueue(freq.Value, new Node(freq.Key, freq.Value));
            }

            while (pq.Count > 1)
            {
                Node bit0 = pq.Dequeue();
                Node bit1 = pq.Dequeue();
                int freq = bit0.Freq + bit1.Freq;

                Node node = new Node(freq, bit0, bit1);

                pq.Enqueue(freq, node);
            }

            return pq.Dequeue();
        }

        public byte[] GetHaffmanHeader()
        {
            if (_codes.Count == 0)
            {
                throw new Exception("Нарушена последовательность");
            }

            List<byte> header = new List<byte>();
            byte[] ans;

            foreach (var element in FriquencyController.FrequencyDict)
            {
                byte[] b = BitConverter.GetBytes(element.Key);

                header.Add(b[0]);
                header.Add(b[1]);

                b = BitConverter.GetBytes(Convert.ToInt32(element.Value));

                header.Add(b[0]);
                header.Add(b[1]);
                header.Add(b[2]);
                header.Add(b[3]);
            }

            ans = BitConverter.GetBytes(_codes.Count).Concat(header.ToArray()).ToArray();

            return ans;
        }
    }
}
