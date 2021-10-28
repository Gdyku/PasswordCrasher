using System;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace PasswordCrasher
{
    class Program
    {
        #region PrivateVars
        private static string password = "abcdef";
        private static char[] charsForPassword =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u',
            'w', 'x', 'y', 'z', 'q', 'v', '0', '1', '2', '3',
            '4', '5', '6', '7', '8', '9'
        };
        private static string result;

        private static int charsToTestLength = charsForPassword.Length;
        private static int computedKeys = 0;

        private static bool isMatched = false;

        #endregion
        static void Main(string[] args)
        {
            var timeStarted = DateTime.Now;
            Console.WriteLine($"Start BruteForce - {timeStarted}");

            var estimatedPasswordLength = 0;

            while(!isMatched)
            {
                estimatedPasswordLength++;
                startBruteForce(estimatedPasswordLength);
            }

            using (var archive = SevenZipArchive.Open(@"C:\Users\patryk.gdyczynski\Desktop\Akademia\Ćwiczenia\PasswordCrasher\PasswordCrasher\PatrykG.7z",
                                    new ReaderOptions() { Password = password, LookForHeader = false }))
            {

            }

                Console.WriteLine($"Password matched. {DateTime.Now}");
            Console.WriteLine($"Time passed: {DateTime.Now.Subtract(timeStarted).TotalSeconds}s");
            Console.WriteLine($"Resolve password: {result}");
            Console.WriteLine($"Computed Keys: {computedKeys}");

            Console.ReadLine();
    }

    #region PrivateMethods

    private static void startBruteForce(int keyLength)
        {
            var keychars = createCharArray(keyLength, charsForPassword[0]);
            var indexOfLastChar = keyLength - 1;
            createNewKey(0, keychars, keyLength, indexOfLastChar);
        }

        private static void createNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < charsToTestLength; i++)
            {
                keyChars[currentCharPosition] = charsForPassword[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    createNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar);
                }
                else
                {
                    if ((new String(keyChars)) == password)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }

        private static char[] createCharArray(int length, char defaultChar)
        {
            return (from c in new char[length] select defaultChar).ToArray();
        }

        #endregion
    }
    /*
     
    PasswordGenerator generator = new PasswordGenerator();
            //charsToTestLength = chars.Length;

            for (var i = 1; ; i++)
            {
                string password = ReturnPassword(i);

                while (!isMatched)
                {
                    estimatedPasswordLength++;
                }
                try
                {
                    using (var archive = SevenZipArchive.Open(@"C:\Users\patryk.gdyczynski\Desktop\Akademia\Ćwiczenia\PasswordCrasher\PasswordCrasher\PatrykG.7z",
                                    new ReaderOptions() { Password = password, LookForHeader = false }))
                    {
                        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                        {
                            entry.WriteToDirectory("", new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                    return;
                }
                catch (InvalidFormatException ex)
                {
                    Console.WriteLine(ex);
                }
            }

     */
}
