using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1New
{
    class Program
    {
       static char[] Alphabet = 
            new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        static void Main(string[] args)
        {
            StreamReader textFileReader = new StreamReader("Myfile.txt");

            string msg = textFileReader.ReadLine();
            Console.WriteLine("msg = " + msg);
            char[] key = MyShuffle();


            string answerCoded = Code(msg, key);
            string answerDecoded = Uncode(answerCoded, key);

            Console.Write("Alphabet = ");
            for (int i = 0; i < key.Length; i++)
            {
                Console.Write(Alphabet[i] + " ");
            }

            Console.WriteLine();


            Console.Write("Key = ");
            for (int i = 0; i < key.Length; i++)
            {
                Console.Write(key[i] + " ");
            }

            Console.WriteLine();
            Console.WriteLine("coded = " + answerCoded);
            Console.WriteLine("Uncoded = " + answerDecoded);

            Console.ReadKey();

        }

     public static  char[] MyShuffle()
        {
            List<char> currentAlphabet = new List<char>(Alphabet);

            char[] Key = new char[26];
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i= 0; i < 26; i++)
            {
                
                int index = random.Next(0,currentAlphabet.Count);
                Key[i] = currentAlphabet[index];
                currentAlphabet.RemoveAt(index);

            }
            return Key;
        }

      static  string Code(string msg,char[] key)
        {
            string answer = "";

            for (int i = 0; i < msg.Length; i++)
            {
                int index = GetAlphabetNumber(msg[i],Alphabet);
                if (msg[i] == ' ' || index > -1)
                {
                    answer += key[index];
                }
            }

            return answer;
        }

      static  string Uncode(string msg,char[] key)
        {
            string answer = "";

            for (int i = 0; i < msg.Length; i++)
            {
                int index = GetAlphabetNumber(msg[i], key);
                if (msg[i] == ' ' || index > -1)
                {
                    answer +=Alphabet[index];
                }
            }

            return answer;
        }

      static  int GetAlphabetNumber(char symb,char[] alphabet)
        {
            for (int j = 0; j < alphabet.Length; j++)
            {
                if (alphabet[j] == symb)
                {
                    return j;
                }
                
            }

            return -1;
        }



    }

}
