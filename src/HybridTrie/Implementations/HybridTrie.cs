using System;
using System.Collections.Generic;
using System.Linq;
using src.HybridTrie.Interfaces;
using src.HybridTrie.Tools;

namespace src.HybridTrie.Implementations
{
    public class HybridTrie : IHybridTrie
    {
        private HybridTrieNode Root { get; set; }

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
                throw new ArgumentException("Inserting word", "Word can't be null or empty!");
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
                throw new ArgumentException("Inserting words", "Words can't be null or empty!");
            }

            foreach (string word in words)
            {
                Insert(word);
            }
        }

        public void RemoveAll()
        {
            Root = null;
        }

        public int GetNodeCount()
        {
            if (IsEmpty())
            {
                return 0;
            }

            return CountNodesRecursively(Root, 0);
        }

        private int CountNodesRecursively(HybridTrieNode node, int nodeCount)
        {
            if (node != null)
            {
                nodeCount++;

                nodeCount = CountNodesRecursively(node.LeftChild, nodeCount);
                nodeCount = CountNodesRecursively(node.RightChild, nodeCount);
                nodeCount = CountNodesRecursively(node.MiddleChild, nodeCount);
            }

            return nodeCount;
        }

        public bool Search(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Searching word", "Word can't be null or empty!");
            }

            return SearchRecursively(Root, word.ToCharArray(), 0);
        }

        private bool SearchRecursively(HybridTrieNode node, char[] key, int position)
        {
            if (node == null)
            {
                return false;
            }

            if (key[position] < node.Character)
            {
                return SearchRecursively(node.LeftChild, key, position);
            }
            else if (key[position] > node.Character)
            {
                return SearchRecursively(node.RightChild, key, position);
            }
            else
            {
                if (position < key.Length - 1)
                {
                    return SearchRecursively(node.MiddleChild, key, position + 1);
                }
                else
                {
                    return node.IsFinalNode();
                }
            }
        }

        public int GetWordCount()
        {
            if (IsEmpty())
            {
                return 0;
            }

            return CountWordsRecursively(Root, 0);
        }

        private int CountWordsRecursively(HybridTrieNode node, int wordsCounter)
        {
            if (node != null)
            {
                if (node.IsFinalNode())
                {
                    wordsCounter++;
                }

                wordsCounter = CountWordsRecursively(node.LeftChild, wordsCounter);
                wordsCounter = CountWordsRecursively(node.RightChild, wordsCounter);
                wordsCounter = CountWordsRecursively(node.MiddleChild, wordsCounter);
            }

            return wordsCounter;
        }

        public List<string> GetStoredWords()
        {
            if (IsEmpty())
            {
                return null;
            }

            return ListWordsRecursively(Root, string.Empty, new List<string>());
        }

        private List<string> ListWordsRecursively(HybridTrieNode node, String word, List<String> listWords)
        {
            if (node != null)
            {
                ListWordsRecursively(node.LeftChild, word, listWords);
                word += node.Character;

                if (node.IsFinalNode())
                {
                    listWords.Add(word);
                }

                ListWordsRecursively(node.MiddleChild, word, listWords);
                word = RemoveLastCaracter(word);
                ListWordsRecursively(node.RightChild, word, listWords);
            }

            return listWords;
        }

        // Helper method for {@link #ListWordsRecursively()}.     
        // Returning the word without its last character.
        private string RemoveLastCaracter(string word)
        {
            return word.Substring(0, word.Length - 1);
        }

        public int GetNullPointerCount()
        {
            if (IsEmpty())
            {
                return 0;
            }

            return CountNullRecursively(Root, 0);
        }

        private int CountNullRecursively(HybridTrieNode node, int nullCounter)
        {
            if (node != null)
            {
                nullCounter = CountNullRecursively(node.LeftChild, nullCounter);
                nullCounter = CountNullRecursively(node.RightChild, nullCounter);
                nullCounter = CountNullRecursively(node.MiddleChild, nullCounter);
            }
            else
            {
                nullCounter++;
            }

            return nullCounter;
        }

        public int GetHeight()
        {
            if (IsEmpty())
            {
                return 0;
            }

            return CalculateHeightRecursively(Root, -1, 0);
        }

        private int CalculateHeightRecursively(HybridTrieNode node, int heightCounter, int heightResult)
        {
            if (node != null)
            {
                heightCounter++;
                heightResult = CalculateHeightRecursively(node.LeftChild, heightCounter, heightResult);

                if (heightCounter > heightResult && node.IsFinalNode())
                {
                    heightResult = heightCounter;
                }

                heightResult = CalculateHeightRecursively(node.RightChild, heightCounter, heightResult);
                heightResult = CalculateHeightRecursively(node.MiddleChild, heightCounter, heightResult);
            }

            return heightResult;
        }

        public double GetAverageDepthOfLeaves()
        {
            if (IsEmpty())
            {
                return 0.0;
            }

            return (double)CountTotalLeafDepthRecursively(Root, -1, 0) / (double)CountTotalLeafRecursively(Root, 0);
        }

        // Helper method for {@link #GetAverageDepthOfLeaves()}.
        private int CountTotalLeafDepthRecursively(HybridTrieNode node, int depthCounter, int totalDepth)
        {
            if (node != null)
            {
                depthCounter++;

                if (!node.HasChildren())
                {
                    totalDepth += depthCounter;
                }

                totalDepth = CountTotalLeafDepthRecursively(node.LeftChild, depthCounter, totalDepth);
                totalDepth = CountTotalLeafDepthRecursively(node.RightChild, depthCounter, totalDepth);
                totalDepth = CountTotalLeafDepthRecursively(node.MiddleChild, depthCounter, totalDepth);
            }

            return totalDepth;
        }

        // Helper method for {@link #GetAverageDepthOfLeaves()}.
        // Returns the node count for a given node by regarding the node itself and all of
        // its successors.
        private int CountTotalLeafRecursively(HybridTrieNode node, int leavesCounter)
        {
            if (node != null)
            {
                if (!node.HasChildren())
                {
                    leavesCounter++;
                }

                leavesCounter = CountTotalLeafRecursively(node.LeftChild, leavesCounter);
                leavesCounter = CountTotalLeafRecursively(node.RightChild, leavesCounter);
                leavesCounter = CountTotalLeafRecursively(node.MiddleChild, leavesCounter);
            }

            return leavesCounter;
        }

        public int GetPrefixCount(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Getting prefix count for a word", "Word can't be null or empty!");
            }

            return CountPrefixRecursively(Root, word.ToCharArray(), 0);
        }

        private int CountPrefixRecursively(HybridTrieNode node, char[] key, int position)
        {
            if (node == null)
            {
                return 0;
            }

            if (key[position] < node.Character)
            {
                return CountPrefixRecursively(node.LeftChild, key, position);
            }
            else if (key[position] > node.Character)
            {
                return CountPrefixRecursively(node.RightChild, key, position);
            }
            else
            {
                if (position < key.Length - 1)
                {
                    return CountPrefixRecursively(node.MiddleChild, key, position + 1);
                }
                else
                {
                    if (node.MiddleChild == null)
                    {
                        return 1;
                    }
                    else
                    {
                        return CountWords(node.MiddleChild) + (node.IsFinalNode() ? 1 : 0);
                    }
                }
            }
        }

        // Helper method for {@link #CountPrefixRecursively()}.
        // Counting all the stored words for a given node.
        private int CountWords(HybridTrieNode node)
        {
            if (IsEmpty())
            {
                return 0;
            }

            return CountWordsRecursively(node, 0);
        }

        public bool Remove(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Removing a word", "Word can't be null or empty!");
            }

            return RemoveRecursively(null, Root, word.ToCharArray(), 0);
        }

        private bool RemoveRecursively(HybridTrieNode parent, HybridTrieNode node, char[] key, int position)
        {
            if (node == null)
            {
                return false;
            }

            var log = true;

            if (key[position] < node.Character)
            {
                log = RemoveRecursively(node, node.LeftChild, key, position);
            }
            else if (key[position] > node.Character)
            {
                log = RemoveRecursively(node, node.RightChild, key, position);
            }
            else
            {
                // If we are on our last character.
                if (position == key.Length - 1)
                {
                    if (node.IsFinalNode())
                    {
                        // The node is an end node, remove the end value.
                        node.MakeFinalNode(0);
                    }
                    else
                    {
                        // If there is no end key as it does not exist within the tree.
                        return false;
                    }
                }
                else if (position < key.Length + 1)
                {
                    log = RemoveRecursively(node, node.MiddleChild, key, position + 1);
                }
            }

            // Only remove if the node's middle child is null and if the node is not an end key.
            // If log is false, the value has never been found. Thus the key does not exist within this tree.
            if (log && node.MiddleChild == null && !node.IsFinalNode())
            {
                // Case 1: No children, safe to delete.
                if (!node.HasChildren())
                {
                    Transplant(parent, node, null);
                }
                // Case 2: Right is null, transplant it to left.
                else if (node.RightChild == null)
                {
                    Transplant(parent, node, node.LeftChild);
                }
                // Case 3: Left is null, transplant it to right.
                else if (node.LeftChild == null)
                {
                    Transplant(parent, node, node.RightChild);
                }
                // Case 4: Both left and right children exists, find successor and transplant.
                else
                {
                    HybridTrieNode successor = FindSuccessor(node.RightChild);
                    HybridTrieNode successorParent = FindSuccessorParent(successor);
                    // If successor node has left child(ren).
                    if (successorParent != node)
                    {
                        Transplant(parent, node, node.RightChild);
                    }
                    else
                    {
                        Transplant(parent, node, successor);
                    }
                    // Make the node's left child as left child for successor node.
                    successor.LeftChild = node.LeftChild;
                }

                node = null;
            }

            return log;
        }

        // Helper method for {@link #RemoveRecursively()}.
        // Make a link between a parent node and its child's successor node.
        private void Transplant(HybridTrieNode parent, HybridTrieNode node, HybridTrieNode successor)
        {
            if (parent == null)
            {
                Root = successor;
            }
            else if (node == parent.LeftChild)
            {
                parent.LeftChild = successor;
            }
            else if (node == parent.RightChild)
            {
                parent.RightChild = successor;
            }
            else
            {
                parent.MiddleChild = successor;
            }
        }

        // Helper method for {@link #RemoveRecursively()}.
        // Find the successor (in in-order traversal) in the right child.
        private HybridTrieNode FindSuccessor(HybridTrieNode node)
        {
            if (node.LeftChild == null)
            {
                return node;
            }
            else
            {
                return FindSuccessor(node.LeftChild);
            }
        }

        // Helper method for {@link #removeRecursively()}.
        // Find the successor's parent
        private HybridTrieNode FindSuccessorParent(HybridTrieNode successor)
        {
            HybridTrieNode parent = null;
            HybridTrieNode node = Root;

            while (node != null)
            {
                if (successor.Character < node.Character)
                {
                    parent = node;
                    node = node.LeftChild;
                }
                else if (successor.Character > node.Character)
                {
                    parent = node;
                    node = node.RightChild;
                }
                else
                {
                    if (node.Id.Equals(successor.Id))
                    {
                        break;
                    }

                    parent = node;
                    node = node.MiddleChild;
                }
            }

            return parent;
        }

        public void Print(string fileName)
        {
            GraphPrinter graphPrinter = new GraphPrinter(fileName);
            graphPrinter.Begin();

            if (!Root.IsFinalNode())
            {
                graphPrinter.PrintNode(Root);
            }
            else
            {
                graphPrinter.PrintNode(Root, Color.BLACK, Color.BLACK, Style.DASHED);
            }

            graphPrinter.PrintNodeLabel(Root);
            PrintRecursively(null, Root, Color.BLUE, graphPrinter);

            graphPrinter.End();
        }

        private void PrintRecursively(HybridTrieNode previousNode, HybridTrieNode nextNode, Color color, GraphPrinter graphPrinter)
        {
            if (nextNode != null)
            {
                if (previousNode != null)
                {
                    if (color.Equals(Color.BLUE))
                    {
                        graphPrinter.PrintEdge(previousNode, nextNode, Color.BLUE);
                    }
                    else if (color.Equals(Color.RED))
                    {
                        graphPrinter.PrintEdge(previousNode, nextNode, Color.RED);
                    }
                    else if (color.Equals(Color.GREEN))
                    {
                        graphPrinter.PrintEdge(previousNode, nextNode, Color.GREEN);
                    }

                    if (nextNode.IsFinalNode())
                    {
                        if (color.Equals(Color.BLUE))
                        {
                            graphPrinter.PrintNode(nextNode, Color.BLUE, Color.BLUE, Style.DASHED);
                        }
                        else if (color.Equals(Color.RED))
                        {
                            graphPrinter.PrintNode(nextNode, Color.RED, Color.RED, Style.DASHED);
                        }
                        else if (color.Equals(Color.GREEN))
                        {
                            graphPrinter.PrintNode(nextNode, Color.GREEN, Color.GREEN, Style.DASHED);
                        }
                    }

                    graphPrinter.PrintNodeLabel(nextNode);
                }

                PrintRecursively(nextNode, nextNode.LeftChild, Color.BLUE, graphPrinter);
                PrintRecursively(nextNode, nextNode.MiddleChild, Color.RED, graphPrinter);
                PrintRecursively(nextNode, nextNode.RightChild, Color.GREEN, graphPrinter);
            }
        }

        public void InsertBalanced(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                throw new ArgumentException("Inserting word balanced", "Word can't be null or empty!");
            }

            Root = InsertBalancedRecursively(Root, word.ToCharArray(), 0);
        }

        private HybridTrieNode InsertBalancedRecursively(HybridTrieNode node, char[] key, int position)
        {
            if (node == null)
            {
                node = new HybridTrieNode(key[position]);
            }

            if (key[position] < node.Character)
            {
                node.LeftChild = InsertBalancedRecursively(node.LeftChild, key, position);
                if (node.LeftChild.Priority > node.Priority)
                {
                    node = RotateWithLeft(node);
                }
            }
            else if (key[position] > node.Character)
            {
                node.RightChild = InsertBalancedRecursively(node.RightChild, key, position);
                if (node.RightChild.Priority > node.Priority)
                {
                    node = RotateWithRight(node);
                }
            }
            else
            {
                if (position < key.Length - 1)
                {
                    node.MiddleChild = InsertBalancedRecursively(node.MiddleChild, key, position + 1);
                }
                else if (!node.IsFinalNode())
                {
                    node.MakeFinalNode(RandomUnique.Number);
                }
                if (node.MiddleChild == null)
                {
                    node.Priority = node.StringPriority;
                }
                else
                {
                    node.Priority = Max(node.StringPriority, node.MiddleChild.Priority);
                }
            }

            return node;
        }

        private HybridTrieNode RotateWithLeft(HybridTrieNode nodeX)
        {
            HybridTrieNode nodeY = nodeX.LeftChild;
            nodeX.LeftChild = nodeY.RightChild;
            nodeY.RightChild = nodeX;

            return nodeY;
        }

        private HybridTrieNode RotateWithRight(HybridTrieNode nodeX)
        {
            HybridTrieNode nodeY = nodeX.RightChild;
            nodeX.RightChild = nodeY.LeftChild;
            nodeY.LeftChild = nodeX;

            return nodeY;
        }

        private int Max(int n1, int n2)
        {
            return (n1 > n2) ? n1 : n2;
        }

        public void InsertBalanced(List<string> words)
        {
            if (words == null || !words.Any())
            {
                throw new ArgumentException("Inserting words balanced", "Words can't be null or empty!");
            }

            foreach (string word in words)
            {
                InsertBalanced(word);
            }
        }
    }
}

static class RandomUnique
{
    private const int MAX_WORDS = Int16.MaxValue;
    private static List<int> generatedNumbers = new List<int>();

    public static int Number
    {
        get
        {
            var number = 0;
            while (true)
            {
                number = new Random().Next(1, MAX_WORDS);
                if (!generatedNumbers.Contains(number))
                {
                    generatedNumbers.Add(number);
                    break;
                }
            }

            return number;
        }
        private set { }
    }
}