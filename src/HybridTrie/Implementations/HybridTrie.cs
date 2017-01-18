using System;
using System.Collections.Generic;
using System.Linq;
using src.HybridTrie.Interfaces;

namespace src.HybridTrie.Implementations
{
    public class HybridTrie : IHybridTrie
    {
        private HybridTrieNode Root { get; set; }
        private const int MAX_WORDS = Int16.MaxValue;

        public HybridTrie()
        {
            Root = null;
        }

        public HybridTrie(HybridTrieNode root)
        {
            this.Root = root;
        }

        public bool IsEmpty()
        {
            return Root == null;
        }

        public void Insert(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
			    throw new HybridTrieError("Inserting word: Word should not be null or empty!");
		    }

            Root = InsertRecursively(Root, word.ToCharArray(), 0);
        }

        private HybridTrieNode InsertRecursively(HybridTrieNode node, char[] key, int position)
        {
            if (node == null)
            {
                node = new HybridTrieNode(key[position]);
            }

            if (key[position] < node.Character)
            {
                // Go left, if current character smaller than character of current node.
                node.LeftChild = InsertRecursively(node.LeftChild, key, position);
            } 
            else if (key[position] > node.Character)
            {
                // Go right, if current character greater than character of current node.
                node.RightChild = InsertRecursively(node.RightChild, key, position);
            }
            else 
            {
                if (position < key.Length - 1)
                {
                    // Go middle, if current character equal to node character (If we don't exceed the key).
                    node.MiddleChild = InsertRecursively(node.MiddleChild, key, position + 1);
                } 
                else if (!node.IsFinalNode())
                {
                    node.MakeFinalNode(1);
                }
            }

            return node;
        }

        public void Insert(List<string> words)
        {
            if (words == null || !words.Any())
            {
                throw new HybridTrieError("Inserting words: Words should not be null or empty!");
            }

            foreach (string word in words)
            {
                Insert(word);
            } 
        }

        public double GetAverageDepthOfLeaves()
        {
            throw new NotImplementedException();
        }

        public int GetHeight()
        {
            throw new NotImplementedException();
        }

        public int GetNodeCount()
        {
            throw new NotImplementedException();
        }

        public int GetNullPointerCount()
        {
            throw new NotImplementedException();
        }

        public int GetPrefixCount(string word)
        {
            throw new NotImplementedException();
        }

        public List<string> GetStoredWords()
        {
            throw new NotImplementedException();
        }

        public int GetWordCount()
        {
            throw new NotImplementedException();
        }

        public void InsertBalanced(List<string> words)
        {
            throw new NotImplementedException();
        }

        public void InsertBalanced(string word)
        {
            throw new NotImplementedException();
        }
    
        public void Print(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string word)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public bool Search(string word)
        {
            throw new NotImplementedException();
        }
    }
}
