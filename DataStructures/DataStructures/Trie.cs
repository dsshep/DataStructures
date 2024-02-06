namespace DataStructures;

public class Trie
{
    private class Node
    {
        /// <summary>
        /// True if this node, from the root, stores a valid key 
        /// </summary>
        public bool StoresKey { get; set; }

        public Dictionary<char, Node> Children = new();

        public Node(bool storesKey)
        {
            StoresKey = storesKey;
        }
    }

    private Node root;

    public Trie()
    {
        root = new Node(false);
    }

    public void Insert(string s)
    {
        if (s == "")
        {
            root.StoresKey = true;
            return;
        }

        var index = 0;

        var currentNode = root;

        while (index < s.Length)
        {
            if (!currentNode.Children.TryGetValue(s[index], out var nextNode))
            {
                nextNode = new Node(index == s.Length - 1);
                currentNode.Children[s[index]] = nextNode;
            }

            currentNode = nextNode;

            index++;
        }
    }

    public bool Contains(string s)
    {
        if (s == "" && root.StoresKey)
        {
            return true;
        }

        var node = InnerSearch(s);
        
        return node?.StoresKey ?? false;
    }

    public void Remove(string s, bool prune = true)
    {
        if (s == "" && root.StoresKey)
        {
            root.StoresKey = false;
            return;
        }

        if (!prune)
        {
            var node = InnerSearch(s);

            if (node is null)
            {
                return;
            }
            
            node.StoresKey = false;
        }
        else
        {
            RemoveAndPrune(s);
        }
    }

    private void RemoveAndPrune(ReadOnlySpan<char> s)
    {
        var nodes = new Stack<Node>(s.Length);
        var index = 0;
        var currentNode = root;
        var foundNode = false;

        while (currentNode.Children.TryGetValue(s[index], out currentNode))
        {
            nodes.Push(currentNode);
            index++;
            
            if (index == s.Length)
            {
                foundNode = currentNode.StoresKey;
                break;
            }
        }

        if (!foundNode)
        {
            return;
        }
        
        var lastNode = nodes.Pop();
        var indexFromEnd = 0;
        
        while (nodes.TryPop(out currentNode) && !currentNode.StoresKey)
        {
            lastNode = currentNode;
            indexFromEnd++;
        }

        var removeKey = s[^indexFromEnd];

        lastNode.Children.Remove(removeKey);
    }

    private Node? InnerSearch(ReadOnlySpan<char> s)
    {
        var index = 0;
        var currentNode = root;

        while (currentNode.Children.TryGetValue(s[index], out currentNode))
        {
            index++;
            if (index == s.Length)
            {
                return currentNode;
            }
        }

        return null;
    }
}