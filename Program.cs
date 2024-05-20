using static System.Console;
using System.Drawing.Imaging;

namespace Replication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfCopies;
            string? filePath;
            string copyFilePath;

            Console.WriteLine("Hello, World!");
            Write("Please Provide the path to your file: ");
            filePath = ReadLine();
            if (File.Exists(filePath))
            {
                Write("How many copies do you want to make? ");
                string response = ReadLine();
                
                if (int.TryParse(response, out numberOfCopies))
                {
                    Write("Please provide the path and name for the copy/copies: ");
                    copyFilePath = ReadLine();

                    byte[] Content = SelectFileContent(filePath);
                    if (makeCopies(copyFilePath, Content, numberOfCopies))
                    {
                        WriteLine($"{numberOfCopies} {(numberOfCopies > 1 ? "Copies" : "Copy")}");
                    }
                }
                else
                {
                    WriteLine("Please Enter a number");
                }
                
            }
            else
            {
                WriteLine("Please check your filepath. ");
            }


        }

        public static bool makeCopies(string filePathAndName, byte[] content, int numberOfCopies)
        {
            bool succeeded;
            string newName = filePathAndName.Split('.')[0];
            string fileExtension = filePathAndName.Split('.')[1];
            try
            {
                for(int i = 0; i < numberOfCopies; i++)
                {
                    Stream file = File.Create($"{newName}_{i}.{fileExtension}");
                    file.Write(content);
                }
                succeeded = true;
            } catch(Exception ex)
            {
                succeeded = false;
                WriteLine(ex.Message);
            }
            return succeeded;
        }

        public static byte[] SelectFileContent(string filePath)
        {
            byte[] binary = new byte[128];

            try
            {
                binary = File.ReadAllBytes(filePath);
            } catch(Exception ex)
            {
                WriteLine(ex.Message);
            }

            return binary;
        }
    }
}