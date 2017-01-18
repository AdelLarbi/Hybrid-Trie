namespace src.HybridTrie.Implementations
{
    public class HybridTrieNode
    {        
        public char Character { get; set; }

        // Used for balancing the trie.
        public int Priority { get; set; }
        public int StringPriority { get; set; }        

        // Used to identify the node when printing the tree.
        public string Id { get; private set; }
        
        public HybridTrieNode LeftChild { get; set; }
        public HybridTrieNode MiddleChild { get; set; }
        public HybridTrieNode RightChild { get; set; }
        
        private static int idCounter = 0;

        public HybridTrieNode(char character) 
        {
            this.Character = character;
            this.Priority = 0;
            // Non-terminal node, stringPriority value is 0.
            this.StringPriority = 0;
            this.Id = character + (idCounter++).ToString();
            this.LeftChild = null;
            this.MiddleChild = null;
            this.RightChild = null;
        }

        public bool IsFinalNode() 
        {
            return StringPriority != 0;
        }                
        
        public void MakeFinalNode(int stringPriority)
        {
            this.StringPriority = stringPriority;
        }

        public bool HasChildren() 
        {
            return LeftChild != null || MiddleChild != null || RightChild != null;		
        }                                                
    }
}