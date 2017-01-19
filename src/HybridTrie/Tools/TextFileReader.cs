using System.IO;
using System.Collections.Generic;

namespace src.HybridTrie.Tools
{
    class TextFileReader
    {
        private string[] lines;

        public TextFileReader(string filePath)
        {
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            lines = File.ReadAllLines(filePath);            
        }

        public List<string> WordsListe
        {
            get
            {                
                return PutWordsInList();
            }
            private set { }
        }

        private List<string> PutWordsInList()
        {
            var wordsListe = new List<string>();

            foreach (string line in lines)
            {                    
                if (line.Contains(" "))
                {
                    foreach (string word in line.Split(' '))
                    {
                        wordsListe.Add(word);
                    }
                }
                else
                {
                    wordsListe.Add(line);
                }
            }

            return wordsListe;
        }        
    }
}