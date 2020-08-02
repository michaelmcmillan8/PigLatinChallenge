using System;

namespace PigLatinChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            string readPath;
            string writePath;

            Translator translator = new Translator();

            Console.WriteLine("This program converts a text file to pig latin.");
            Console.WriteLine('\n' + @"Type a path to the text file you want to be translated (ex: C:\Users\Public\InputFile.txt):");
            readPath = Console.ReadLine();

            Console.WriteLine('\n' + @"Now type a path to where the output file will be generated (ex: C:\Users\Public\OutputFile.txt)");
            writePath = Console.ReadLine();

            Console.WriteLine("Reading File");
            string[] text = System.IO.File.ReadAllLines(readPath);
            Console.WriteLine("Finished Reading File");

            Console.WriteLine("Translating File");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(writePath))
            {
                foreach (string item in text)
                {
                    file.WriteLine(translator.convertPhrase(item));
                }
            }

            Console.WriteLine("Finished Translating File");

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }

 
}
