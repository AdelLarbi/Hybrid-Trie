using System.Collections.Generic;
using src.HybridTrie.Implementations;

namespace src.HybridTrie.Tools
{
    public class GraphPrinter
    {
        private TextFileWriter fileObject;
        private Dictionary<Color, string> colorToGraphValue;
        private Dictionary<Style, string> styleToGraphValue;

        public GraphPrinter(string fileName)
        {
            fileObject = new TextFileWriter(fileName);
            colorToGraphValue = new Dictionary<Color, string>();
            styleToGraphValue = new Dictionary<Style, string>();

            colorToGraphValue.Add(Color.BLUE, "blue");
            colorToGraphValue.Add(Color.RED, "red");
            colorToGraphValue.Add(Color.GREEN, "green");
            colorToGraphValue.Add(Color.BLACK, "black");
            colorToGraphValue.Add(Color.DEFAULT, "default");

            styleToGraphValue.Add(Style.SOLID, "solid");
            styleToGraphValue.Add(Style.DASHED, "dashed");
            styleToGraphValue.Add(Style.DOTTED, "dotted");
            styleToGraphValue.Add(Style.BOLD, "bold");
            styleToGraphValue.Add(Style.ROUNDED, "rounded");
            styleToGraphValue.Add(Style.DIAGONALS, "diagonals");
            styleToGraphValue.Add(Style.FILLED, "filled");
            styleToGraphValue.Add(Style.DEFAULT, "default");
        }

        public void Begin()
        {
            fileObject.Write("graph G {\n");
        }

        public void End()
        {
            fileObject.Write("}");
            fileObject.Close();
        }

        public void PrintNode(HybridTrieNode node)
        {
            fileObject.Write("	\"" + node.Id + "\";\n");
        }

        public void PrintNode(HybridTrieNode node, Color color, Color fontColor, Style style)
        {
            fileObject.Write("	\"" + node.Id +
                             "\" [color=" + colorToGraphValue[color] + ", " +
                             "fontcolor=" + colorToGraphValue[fontColor] + ", " +
                                 "style=" + styleToGraphValue[style] + "];\n");
        }

        public void PrintNodeLabel(HybridTrieNode node)
        {
            fileObject.Write("	\"" + node.Id + "\" [label=\"" + node.Character + "\"];\n");
        }

        public void PrintEdge(HybridTrieNode startNode, HybridTrieNode arriveNode, Color color)
        {
            fileObject.Write("	\"" + startNode.Id +
                         "\" -> \"" + arriveNode.Id +
                       "\" [color=" + colorToGraphValue[color] + "];\n");
        }                    
    }
}