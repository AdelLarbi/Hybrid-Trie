using System.Collections.Generic;
using NUnit.Framework;
using src.HybridTrie.Interfaces;

namespace test.HybridTrieTests
{    
    public class HybridTrieTestsHelper
    {
        void TestSearch(IHybridTrie trie, string word, bool expectedTrieSerchResult) {						
            bool calculatedTrieSerch = trie.Search(word);
            if (expectedTrieSerchResult) {                
                Assert.That(calculatedTrieSerch, Is.True);
            } else {			                
                Assert.That(calculatedTrieSerch, Is.False);
            }
        }
        
        void TestCountWords(IHybridTrie trie, int expectedTrieCountWords) {						
            int calculatedHybridTrieCountWords = trie.GetWordCount();            
            Assert.AreEqual(calculatedHybridTrieCountWords, expectedTrieCountWords);
        }
        
        void TestListWords(IHybridTrie trie, List<string> expectedTrieListWords) {						
            List<string> calculatedHybridTrieList = trie.GetStoredWords();
            Assert.AreEqual(calculatedHybridTrieList, expectedTrieListWords);
        }

        void TestCountNull(IHybridTrie trie, int expectedTrieCountNull) {
            int calculatedTrieCountNull = trie.GetNullPointerCount();
            Assert.AreEqual(calculatedTrieCountNull, expectedTrieCountNull);
        }
        
        void TestHeight(IHybridTrie trie, int expectedTrieHeight) {		
            int calculatedTrieHeight = trie.GetHeight();
            Assert.AreEqual(calculatedTrieHeight, expectedTrieHeight);
        }
        
        void TestAverageDepth(IHybridTrie trie, double expectedTrieAverageDepth) {
            const double precision = 0.00001;
            double calculatedTrieAverageDepth = trie.GetAverageDepthOfLeaves();
            Assert.AreEqual(calculatedTrieAverageDepth, expectedTrieAverageDepth, precision);
        }
        
        void TestPrefix(IHybridTrie trie, string word, int expectedPrefixCount) {
            int calculatedPrefixCount = trie.GetPrefixCount(word);
            Assert.AreEqual(calculatedPrefixCount, expectedPrefixCount);
        }
        
        void TestRemove(IHybridTrie trie, string word, int expectedCountNullResult, int expectedHeightResult) {		
            if (!trie.IsEmpty()) {
                bool previewsSearchResult = trie.Search(word);
                int previewsCountWordsResult = trie.GetWordCount();
                List<string> previewsListWordsResult = trie.GetStoredWords();
                int previewsPrefixResult = trie.GetPrefixCount(word);
                
                trie.Remove(word);			
                        
                bool newSearchResult;
                int newCountWordsResult;
                List<string> newListWordsResult;
                int newCountNullResult; 
                int newHeightResult;
                int newPrefixResult;		
                                                            
                if (!trie.IsEmpty()) {
                    trie.Print(word); // display a tree for visual comparison
                }

                newSearchResult = trie.Search(word);                
                Assert.That(previewsSearchResult == true && newSearchResult == false, Is.True);

                newCountWordsResult = trie.GetWordCount();
                Assert.AreEqual(newCountWordsResult, previewsCountWordsResult - 1);                

                newListWordsResult = trie.GetStoredWords();
                if (previewsListWordsResult != null && newListWordsResult != null) {
                    Assert.That(previewsListWordsResult.Contains(word) && !newListWordsResult.Contains(word), Is.True);
                }

                newCountNullResult = trie.GetNullPointerCount();
                Assert.AreEqual(newCountNullResult, expectedCountNullResult);                
            
                newHeightResult = trie.GetHeight();
                Assert.AreEqual(newHeightResult, expectedHeightResult);

                newPrefixResult = trie.GetPrefixCount(word);                                                                                            
                Assert.AreEqual(newPrefixResult, previewsPrefixResult - 1);
            } else {			                
                Assert.That(trie.IsEmpty(), Is.True);
            }		
        }
    }
}