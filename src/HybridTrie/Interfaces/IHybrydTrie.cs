using System.Collections.Generic;

namespace src.HybridTrie.Interfaces
{
    public interface IHybridTrie
    {
        // Basic primitives.
        bool IsEmpty();
        void Insert(string word);
        void Insert(List<string> words);
        void RemoveAll();
        int GetNodeCount();
        
        // Advanced functions.
        bool Search(string word);
        int GetWordCount();
        List<string> GetStoredWords();
        int GetNullPointerCount();
        int GetHeight();
        double GetAverageDepthOfLeaves();
        int GetPrefixCount(string word);
        bool Remove(string word);

        // Complex functions.	
        void InsertBalanced(string word);
        void InsertBalanced(List<string> words);        
        
        // Create file representing the trie.
        void Print(string fileName);
    }
}