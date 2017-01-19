using System;
using System.IO;

namespace src.HybridTrie.Tools
{
    class TextFileWriter
    {        
        private const string DRAWABLES_DIRECTORY = "drawables/";
        private string fileName;
        private string filePath;
        private FileStream fileStream;

        public TextFileWriter(string fileName)
        {
            this.fileName = fileName;
            filePath = DRAWABLES_DIRECTORY + fileName;
            fileStream = null;
            Console.WriteLine("Creating a new file \"" + fileName);
        }

        public void Write(String text)
        {
            fileStream = new FileStream(filePath, FileMode.Append);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(text);
                Console.Write(".");
            }
        }

        public void Close()
        {
            var message = "\nClosing the file." + "\nGenerated and saved : " + filePath
            + "\nUse the following command to open it: xdot " + fileName
            + "\nIf xdot is not found, you can install it with this command: sudo apt install xdot";

            if (fileStream != null)
            {
                fileStream.Dispose();
                Console.WriteLine(message);
            }
        }
    }
}