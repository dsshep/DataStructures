namespace DataStructures;

public class Treap
{
    private Node? root;

    private void RightRotate(Node x)
    {
        if (x is null || x.IsRoot())
        {
            throw new InvalidOperationException("x cannot be null or root.");
        }

        var y = x.Parent!;

        if (y.Left != x)
        {
            throw new InvalidOperationException("x must be to the left of it's parent.");
        }

        var p = y.Parent;

        if (p is not null)
        {
            if (p.Left == y)
            {
                p.SetLeft(x);
            }
            else
            {
                p.SetRight(x);
            }
        }
        else
        {
            root = x;
            x.SetParent(null);
        }

        y.SetLeft(x.Right!);
        x.SetRight(y);
    }

    private void LeftRotate(Node x)
    {   
        if (x is null || x.IsRoot())
        {
            throw new InvalidOperationException("x cannot be null or root.");
        }

        var y = x.Parent!;
        
        if (y.Right != x)
        {
            throw new InvalidOperationException("x must be to the right of it's parent.");
        }

        var p = y.Parent;
        
        if (p is not null)
        {
            if (p.Left == y)
            {
                p.SetLeft(x);
            }
            else
            {
                p.SetRight(x);
            }
        }
        else
        {
            root = x;
            x.SetParent(null);
        }

        y.SetRight(x.Left!);
        x.SetLeft(y);
    }

    public bool Search(string key) => root?.Search(key) is not null;

    public void Insert(string key, double priority)
    {
        var node = root;
        Node? parent = null;
        var newNode = Node.Create(key, priority);

        while (node is not null)
        {
            parent = node;

            node = string.Compare(key, node.Key, StringComparison.Ordinal) <= 0
                ? node.Left
                : node.Right;
        }

        if (parent is null)
        {
            root = newNode;
            return;
        }

        if (string.Compare(key, parent.Key, StringComparison.Ordinal) <= 0)
        {
            parent.SetLeft(newNode);
        }
        else
        {
            parent.SetRight(newNode);
        }

        newNode.SetParent(parent);

        while (newNode.Parent is not null && newNode.Priority < newNode.Parent.Priority)
        {
            if (newNode == newNode.Parent.Left)
            {
                RightRotate(newNode);
            }
            else
            {
                LeftRotate(newNode);
            }
        }

        if (newNode.Parent is null)
        {
            root = newNode;
        }
    }

    private class Node(string key, double priority, Node? left, Node? right, Node? parent)
    {
        public string Key { get; } = key;
        public double Priority { get; } = priority;
        public Node? Left { get; private set; } = left;
        public Node? Right { get; private set; } = right;
        public Node? Parent { get; private set; } = parent;

        public static Node Create(string key, double priority)
        {
            return new Node(key, priority, null, null ,null);
        }

        public override string ToString() => $"{Key} - {Priority}";

        public void SetLeft(Node? node)
        {
            Left = node;
            if (node is not null)
            {
                node.Parent = this;
            }
        }

        public void SetRight(Node? node)
        {
            Right = node;
            if (node is not null)
            {
                node.Parent = this;
            }
        }

        public void SetParent(Node? node)
        {
            Parent = node;
        }

        public bool IsRoot() => Parent is null;

        public Node? Search(string key)
        {
            if (key == Key)
            {
                return this;
            }
        
            return string.Compare(key, Key, StringComparison.Ordinal) <= 0 
                ? Left?.Search(key) 
                : Right?.Search(key);
        }
    }
}
