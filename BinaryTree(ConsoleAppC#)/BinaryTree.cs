using System;

public class BinaryTree
{
    public TreeNode Root;

    public BinaryTree(int rootValue) =>
        Root = new TreeNode(rootValue);

    public void PreOrderTraversal(TreeNode node)
    {
        if (node == null) return;

        Console.Write(node.Value + " ");
        PreOrderTraversal(node.Left);
        PreOrderTraversal(node.Right);
    }

    public void InOrderTraversal(TreeNode node)
    {
        if (node == null) return;

        InOrderTraversal(node.Left);
        Console.Write(node.Value + " ");
        InOrderTraversal(node.Right);
    }

    public void PostOrderTraversal(TreeNode node)
    {
        if (node == null) return;

        PostOrderTraversal(node.Left);
        PostOrderTraversal(node.Right);
        Console.Write(node.Value + " ");
    }
}
