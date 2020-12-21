using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankOfBitsAndBytes
{
    class Program
    {
        public static readonly char[] acceptablePasswordChars = new char[]
        { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public static int acceptablePasswordCharsLength = acceptablePasswordChars.Length;
        public static int returnedAmt = 0;
        public static BankOfBitsNBytes bbb = new BankOfBitsNBytes();
        public delegate void delg();

        public static Stack<delg> toProcess = new Stack<delg>();

        static void Main(string[] args)
        {



            startBruteForce(2);
            while (toProcess.Count > 0)
            {
                delg nextToProcess = toProcess.Pop();
                StartThread(nextToProcess);
            }


            Console.ReadLine();
        }
        public static void StartThread(delg d)
        {
            ThreadStart ts = new ThreadStart(d);
            Thread t = new Thread(ts);
            t.Start();
        }

        public static void startBruteForce(int keyLength)
        {
            var keyChars = createCharArray(keyLength, acceptablePasswordChars[0]);
            var indexOfLastChar = keyLength - 1;
            for (int j = 0; j < acceptablePasswordChars.Length; j++)
            {
                int localJ = j;
                toProcess.Push(() => createNewKey(1, createCharArray(keyLength, acceptablePasswordChars[localJ]), keyLength, indexOfLastChar));
            }

        }
        public static char[] createCharArray(int length, char defaultChar)
        {
            return (from c in new char[length] select defaultChar).ToArray();
        }
        public static void createNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < acceptablePasswordCharsLength; i++)
            {
                keyChars[currentCharPosition] = acceptablePasswordChars[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    createNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar);


                }
                else
                {
                    if (returnedAmt < 5000)
                    {
                        Console.WriteLine(keyChars);
                        int amt;
                        amt = bbb.WithdrawMoney(keyChars);
                        returnedAmt += amt;
                        Console.WriteLine(returnedAmt);
                        if (amt > 0)
                        {
                            createNewKey(0, keyChars, keyLength, indexOfLastChar);


                        }
                    }

                    else
                    {
                        return;
                    }

                }
            }
        }
    }
}
