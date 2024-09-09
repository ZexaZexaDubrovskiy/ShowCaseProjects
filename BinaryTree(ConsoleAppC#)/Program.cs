using System;

public class Program
{
    public static void Main()
    {
        BinaryTree tree = new BinaryTree(1);
        tree.Root.Left = new TreeNode(2);
        tree.Root.Right = new TreeNode(3);
        tree.Root.Left.Left = new TreeNode(4);
        tree.Root.Left.Right = new TreeNode(5);
        tree.Root.Right.Left = new TreeNode(6);
        tree.Root.Right.Right = new TreeNode(7);

        Console.WriteLine("Pre-order:");
        tree.PreOrderTraversal(tree.Root);
        Console.WriteLine();

        Console.WriteLine("In-order:");
        tree.InOrderTraversal(tree.Root);
        Console.WriteLine();

        Console.WriteLine("Post-order:");
        tree.PostOrderTraversal(tree.Root);
        Console.WriteLine();
    }
}
