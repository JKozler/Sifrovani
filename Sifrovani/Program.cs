using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sifrovani
{
    class Program
    {
        static void Main(string[] args)
        {
            string vetaKZasifrovani = "Pokud hledáte pomocnou ruku, najdete ji na konci své paže.";
            string klic = "Pravda";
            CeaserovaSifra(vetaKZasifrovani, 7);
            BlockovaSifraXOR(klic, vetaKZasifrovani);
            Console.ReadKey();
        }

        public static void BlockovaSifraXOR(string klic, string sifra) 
        {
            StringBuilder sbKlic = new StringBuilder();
            StringBuilder sbSifra = new StringBuilder();

            foreach (char c in klic.ToCharArray())
                sbKlic.Append(Convert.ToString(c, 2).PadLeft(8, '0'));

            foreach (char c in sifra.ToCharArray())
                sbSifra.Append(Convert.ToString(c, 2).PadLeft(8, '0'));

            char[] si = sbSifra.ToString().ToCharArray();
            char[] kl = sbKlic.ToString().ToCharArray();
            string data = "";
            int helpIndex = 0;
            for (int i = 0; i < si.Length; i++)
            {
                if (helpIndex == kl.Length -1)
                    helpIndex = 0;
                data += (Convert.ToByte(si[i]) ^ Convert.ToByte(kl[helpIndex])).ToString();
                helpIndex++;
            }
            Console.WriteLine(BinaryToString(data));
        }

        public static void CeaserovaSifra(string sifra, int step)
        {
            string data = "";
            int[] asciiNumbers = new int[sifra.Length];
            int helpIndex = 0;
            foreach (char c in sifra) {
                if (Char.ToUpper(c) == c)
                {
                    int se = 0;
                    if ((step + (int)c) > 90)
                    {
                        se = step + (int)c - 90 + 64;
                        asciiNumbers[helpIndex] = se;
                    }
                    else
                        asciiNumbers[helpIndex] = (int)c + step;

                }
                else 
                {
                    int se = 0;
                    if ((step + (int)c) > 122)
                    {
                        se = step + (int)c - 122 + 97;
                        asciiNumbers[helpIndex] = se;
                    }
                    else
                        asciiNumbers[helpIndex] = (int)c + step;
                }
                helpIndex++;
            }
            for (int i = 0; i < asciiNumbers.Length; i++)
            {
                data += ((char)asciiNumbers[i]).ToString();
            }
            Console.WriteLine(data);
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();
            for (int i = 0; i < data.Length-1; i += 8)
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
    }
}
