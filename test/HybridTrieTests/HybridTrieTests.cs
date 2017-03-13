using System.Collections.Generic;
using NUnit.Framework;
using src.HybridTrie.Tools;
using src.HybridTrie.Implementations;
using src.HybridTrie.Interfaces;
using test.HybridTrieTests;
using System;

namespace test.HybridTrieTests
{
    [TestFixture]
    public class HybridTrieTests
    {
        private const string EXAMPLE_PATH = "files/exerciseExample.txt";
        private List<string> wordsList;
        private HybridTrie hybridTrie;

        [SetUp]
        protected void SetUp()
        {
            wordsList = new TextFileReader(EXAMPLE_PATH).WordsListe;
            hybridTrie = new HybridTrie();

            hybridTrie.Insert(wordsList);
        }

		#region Basic Primitives Tests
        [Test]
    	public void testHybridTrieConstructor()
        {            
			Assert.That(new HybridTrie(), Is.Not.Null);
        }

        [Test]
    	public void testIsEmpty_beforeAddingWords()
        {            
			Assert.That(new HybridTrie().IsEmpty());
        }

        [Test]
    	public void testIsEmpty_afterAddingWords()
        {            
			Assert.That(hybridTrie.IsEmpty(), Is.False);
        }

        [Test]
    	public void testRemoveAll()
        {
            HybridTrie hybridTrie = new HybridTrie();
            hybridTrie.Insert("word1");
            hybridTrie.Insert("word2");
            hybridTrie.Insert("word3");
            hybridTrie.RemoveAll();
            
            Assert.That(hybridTrie.IsEmpty(), Is.True);
        }
        
        [Test]
        public void runNominalTestNodCount_beforeAddingWords()
        {
            Assert.AreEqual(new HybridTrie().GetNodeCount(), 0);
        }

        [Test]
        public void runNominalTestNodeCount_afterAddingWords()
        {            
            Assert.AreEqual(hybridTrie.GetNodeCount(), 34);
        }

        [Test]
        public void testInsertWordsOneByOne()
        {
            HybridTrie hybridTrie = new HybridTrie();
            hybridTrie.Insert("lou");
            hybridTrie.Insert("leve");
            hybridTrie.Insert("les");
            hybridTrie.Insert("loups");
            hybridTrie.Insert("dans");
            hybridTrie.Insert("le");
            hybridTrie.Insert("lourds");

            const int expectedHybridTrieSize = 7;
            int calculatedHybridTrieSize = hybridTrie.GetWordCount();
    
            Assert.AreEqual(calculatedHybridTrieSize, expectedHybridTrieSize);
        }

        [Test]
        public void testInsertWordsFromList()
        {
            HybridTrie hybridTrie = new HybridTrie();
            hybridTrie.Insert(wordsList);

            const int expectedHybridTrieSize = 12;
            int calculatedHybridTrieSize = hybridTrie.GetWordCount();
            
            Assert.AreEqual(calculatedHybridTrieSize, expectedHybridTrieSize);
        }

        [Test]
        public void testInsertWordExist()
        {
            int oldHybridTrieSize = hybridTrie.GetWordCount();
            hybridTrie.Insert("dans");
            int newHybridTrieSize = hybridTrie.GetWordCount();
            
            Assert.AreEqual(oldHybridTrieSize, newHybridTrieSize);
        }

        [Test]        
	    public void testInsertNullWord()
        {
            const string word = null;            
            Assert.Throws<ArgumentException>(() => hybridTrie.Insert(word));
        }


        [Test]        
	    public void testInsertEmptyWord()
        {
            const string word = "";
            Assert.Throws<ArgumentException>(() => hybridTrie.Insert(word));
        }


        [Test]        
	    public void testInsertNullListOfWords()
        {
            const List<string> words = null;
            Assert.Throws<ArgumentException>(() => hybridTrie.Insert(words));
        }

        [Test]        
	    public void testInsertEmptyListOfWords()
        {
            List<string> words = new List<string>();
            Assert.Throws<ArgumentException>(() => hybridTrie.Insert(words));
        }
        #endregion

        #region Advanced Functions Tests
        [Test]
        public void runNominalTestSearch_beforeAddingWords()
        {
            TestSearch(new HybridTrie(), "lourds", false);
            TestSearch(new HybridTrie(), "dans", false);
        }

        [Test]
        public void runNominalTestSearch_afterAddingWords()
        {
            TestSearch(hybridTrie, "lourds", true);
            TestSearch(hybridTrie, "dans", true);
            TestSearch(hybridTrie, "le", true);
            TestSearch(hybridTrie, "lourd", false);
            TestSearch(hybridTrie, "lour", false);
            TestSearch(hybridTrie, "d", false);
        }


        [Test]
	    public void runExceptionalTestSearch_CaseForWordIsNull()
        {
            TestSearch(hybridTrie, null, false);
            Assert.Throws<ArgumentException>(() => ???);
        }


        [Test]
    	public void runExceptionalTestSearch_CaseForWordIsEmpty()
        {
            TestSearch(hybridTrie, "", false);
            Assert.Throws<ArgumentException>(() => ???);
        }

        [Test]
        public void runNominalTestCountWords_beforeAddingWords()
        {
            TestCountWords(new HybridTrie(), 0);
        }

        [Test]
        public void runNominalTestCountWords_afterAddingWords()
        {
            TestCountWords(hybridTrie, 12);
        }

        [Test]
        public void runNominalTestListWords_beforeAddingWords()
        {
            TestListWords(new HybridTrie(), null);
        }

        [Test]
        public void runNominalTestListWords_afterAddingWords()
        {
            List<string> wordsListClone = new List<string>(wordsList);
            wordsListClone.Sort();
            TestListWords(hybridTrie, wordsListClone);
        }

        [Test]
        public void runNominalTestCountNull_beforeAddingWords()
        {
            TestCountNull(new HybridTrie(), 0);
        }

        [Test]
        public void runNominalTestCountNull_afterAddingWords()
        {
            TestCountNull(hybridTrie, 69);
        }

        [Test]
        public void runNominalTestHeight_beforeAddingWords()
        {
            TestHeight(new HybridTrie(), 0);
        }

        [Test]
        public void runNominalTestHeight_afterAddingWords()
        {
            TestHeight(hybridTrie, 6);
        }

        [Test]
        public void runNominalTestAverageDepth_beforeAddingWords()
        {
            TestAverageDepth(new HybridTrie(), 0);
        }

        [Test]
        public void runNominalTestAverageDepth_afterAddingWords()
        {
            const double totalDepthForLeaves = 45.0;
            const double leavesCount = 10.0;
            TestAverageDepth(hybridTrie, totalDepthForLeaves / leavesCount);
        }

        [Test]
        public void runNominalTestPrefix_beforeAddingWords()
        {
            TestPrefix(new HybridTrie(), "lou", 0);
            TestPrefix(new HybridTrie(), "ta", 0);
            TestPrefix(new HybridTrie(), "l", 0);
        }

        [Test]
        public void runNominalTestPrefix_afterAddingWords()
        {
            TestPrefix(hybridTrie, "lou", 3);
            TestPrefix(hybridTrie, "ta", 1);
            TestPrefix(hybridTrie, "lox", 0);
            TestPrefix(hybridTrie, "lourds", 1);
            TestPrefix(hybridTrie, "lourd.", 0);
            TestPrefix(hybridTrie, "le", 3);
            TestPrefix(hybridTrie, "t", 1);
            TestPrefix(hybridTrie, "l", 7);
        }


        [Test]
        public void runExceptionalTestPrefix_caseForWordIsNull()
        {
            TestPrefix(hybridTrie, null, -1);
            Assert.Throws<ArgumentException>(() => ???);
        }


        [Test]
	    public void runExceptionalTestPrefix_caseForWordIsEmpty()
        {
            TestPrefix(hybridTrie, "", -1);
            Assert.Throws<ArgumentException>(() => ???);
        }

        [Test]
        public void runNominalTestRemove_beforeAddingWords()
        {
            Assert.That(new HybridTrie().Remove("hello"), Is.False);
        }

        [Test]
        public void runNominalTestRemove_afterAddingWords()
        {
            // Set up before starting removing words
            IHybridTrie hybridTrie = new HybridTrie();
            hybridTrie.Insert(wordsList);
            hybridTrie.Insert("l");
            hybridTrie.Print("original");

            // Run tests after adding words
            Assert.That(hybridTrie.Remove("lourd"), Is.False);
            Assert.That(hybridTrie.Remove("lourdss"), Is.False);
            TestRemove(hybridTrie, "luxe", hybridTrie.GetNullPointerCount() - 6, hybridTrie.GetHeight() - 0);
            TestRemove(hybridTrie, "leve", hybridTrie.GetNullPointerCount() - 4, hybridTrie.GetHeight() - 0);
            TestRemove(hybridTrie, "les", hybridTrie.GetNullPointerCount() - 2, hybridTrie.GetHeight() - 0);
            TestRemove(hybridTrie, "lou", hybridTrie.GetNullPointerCount() - 0, hybridTrie.GetHeight() - 0);
            TestRemove(hybridTrie, "lourds", hybridTrie.GetNullPointerCount() - 6, hybridTrie.GetHeight() - 0);
            TestRemove(hybridTrie, "loups", hybridTrie.GetNullPointerCount() - 8, hybridTrie.GetHeight() - 0);
            TestRemove(hybridTrie, "le", hybridTrie.GetNullPointerCount() - 2, hybridTrie.GetHeight() - 0);
            TestRemove(hybridTrie, "l", hybridTrie.GetNullPointerCount() - 2, hybridTrie.GetHeight() - 1);
            TestRemove(hybridTrie, "olive", hybridTrie.GetNullPointerCount() - 10, hybridTrie.GetHeight() - 1);
            TestRemove(hybridTrie, "tapis", hybridTrie.GetNullPointerCount() - 10, hybridTrie.GetHeight() - 0);
            TestRemove(hybridTrie, "vert", hybridTrie.GetNullPointerCount() - 8, hybridTrie.GetHeight() - 1);
            TestRemove(hybridTrie, "dans", hybridTrie.GetNullPointerCount() - 6, hybridTrie.GetHeight() - 2);
            TestRemove(hybridTrie, "de", hybridTrie.GetNullPointerCount() - 5, hybridTrie.GetHeight() - 1);
        }

        [Test]                     
	    public void runExceptionalTestRemove_caseForWordIsNull()
        {            
           Assert.Throws<ArgumentException>(() => new HybridTrie().Remove(null));
        }

        [Test]    
	    public void runExceptionalTestRemove_caseForWordIsEmpty()
        {            
            Assert.Throws<ArgumentException>(() => new HybridTrie().Remove(""));
        }
        #endregion

        #region Create File Representing The Trie Tests
        [Test]
        public void testPrint()
        {
            hybridTrie.Print("HT_printTest");
            const string expectedPrintResult = "\n"
                     + "In order to know if the print test goes well, you should make a visual check.\n"
                     + "First, go to '/drawables' and open the file HT_printTest.txt with a DOT Viewer.\n"
                     + "Then, make sure that this properties are correct :\n"
                     + "- The number of nodes is 34,\n"
                     + "- The number of final nodes is 12,\n"
                     + "- The number of leaves is 10,\n"
                     + "- The number of red arcs is 24,\n"
                     + "- The number of blue arcs is 4,\n"
                     + "- The number of green arcs is 5,\n"
                     + "- The number of nil pointers is 69,\n"
                     + "- The average depth is 4.5,\n"
                     + "- The height is 6.\n";            
            Console.WriteLine(expectedPrintResult);
        }
        #endregion

        #region Complex Functions Tests
        [Test]
        public void testInsertBalanced()
        {
            /* The purpose of this manipulation is to make an unbalanced tries by inserting a few words in order. */
            TextFileReader textFileReader = new TextFileReader("triesalgav/files/Shakespeare/john.txt");
            List<string> wordsListFromFile = new List<string>();
            List<string> sortedList = new List<string>();
            List<string> ordinaryList = new List<string>();
            wordsListFromFile = textFileReader.WordsListe;

            // Split the original list to two other lists. 
            for (int wordIndex = 0; wordIndex < wordsListFromFile.Count; wordIndex++)
            {
                if (wordIndex % 500 == 0)
                {
                    sortedList.Add(wordsListFromFile[wordIndex]);
                }
                else
                {
                    ordinaryList.Add(wordsListFromFile[wordIndex]);
                }
            }

            // Sort the first list in an ascending order and keep the second one as it is.
            sortedList.Sort();

            // Insert both lists in each trie
            HybridTrie hybridTrie = new HybridTrie();
            hybridTrie.Insert(sortedList);
            hybridTrie.Insert(ordinaryList);

            HybridTrie balancedHybridTrie = new HybridTrie();
            balancedHybridTrie.InsertBalanced(sortedList);
            balancedHybridTrie.InsertBalanced(ordinaryList);

            // insertBalanced() method should guarantee a better average depth i.e. a balanced trie.
            Assert.That(balancedHybridTrie.GetAverageDepthOfLeaves() < hybridTrie.GetAverageDepthOfLeaves(), Is.True);
        }

        [Test]                
    	public void testInsertBalanced_caseForWordIsNull()
        {
            const string word = null;            
            Assert.Throws<ArgumentException>(() => hybridTrie.InsertBalanced(word));
        }

        [Test]        
	    public void testInsertBalanced_caseForWordIsEmpty()
        {            
            Assert.Throws<ArgumentException>(() => hybridTrie.InsertBalanced(""));
        }

        [Test]
	    public void testInsertBalanced_caseForWordsListIsNull()
        {
            const List<string> words = null;
            Assert.Throws<ArgumentException>(() => hybridTrie.InsertBalanced(words));
        }

        [Test]
	    public void testInsertBalanced_caseForWordsListIsEmpty()
        {
            List<string> words = new List<string>();
            Assert.Throws<ArgumentException>(() => hybridTrie.InsertBalanced(words));
        }
        #endregion
    }
}