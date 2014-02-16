using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   //     8                   8
   //   /   \               /   \
   // 7       9   =>       9     7
   // \                         /
   //  14                      14
namespace BinaryTreeProblems
{
    class Program
    {
        static void Main(string[] args)
        {
            Node n = new Node(8);
            n.Left = new Node(7);
            n.Right = new Node(9);
            n.Left.Right = new Node(14);

            Console.WriteLine("Max node value: {0}", GetMaxNodeValue(n));

            Console.WriteLine("In order traversal of mirrored version of n");
            MirrorBinaryTree(n);
            InOrder(n);

            Console.WriteLine("Are leaf nodes at the same level ?: {0}", AreLeafNodesAtSameLevel(n));
        }

        // Problem 1: Given a binary tree, find node with maximum value
        static int GetMaxNodeValue(Node n)
        {
            if (n == null)
            {
                return Int32.MinValue;
            }
            else
            {
                return Math.Max(n.Data, Math.Max(GetMaxNodeValue(n.Left), 
                                                 GetMaxNodeValue(n.Right)));
            }
        }

        // Problem 2: Mirror a binary tree
        static void MirrorBinaryTree(Node n)
        {
            if (n == null)
            {
                return;
            }
            else
            {
                MirrorBinaryTree(n.Left);
                MirrorBinaryTree(n.Right);

                Node temp = n.Left;
                n.Left = n.Right;
                n.Right = temp;
            }
        }

        // Problem 3: Inorder traversal
        static void InOrder(Node n)
        {
            if (n == null)
            {
                return;
            }

            InOrder(n.Left);
            Console.WriteLine(n.Data);
            InOrder(n.Right);
        }

        // Problem 4: Determine if all 
        // leaf nodes are at the same level
        static bool AreLeafNodesAtSameLevel(Node n)
        {
            int currentLevel = 0;
            int depth = 0;

            return AreLeafNodesAtSameLevel(n, currentLevel, ref depth);
        }

        static bool AreLeafNodesAtSameLevel(Node n, int currentLevel, ref int depth)
        {
            if (n == null)
            {
                return true;
            }
            if (n.Left == null && n.Right == null)
            {
                if (depth == 0)
                {
                    depth = currentLevel;
                    return true;
                }

                return depth == currentLevel;
            }
            else
            {
                return AreLeafNodesAtSameLevel(n.Left, currentLevel + 1, ref depth) &&
                       AreLeafNodesAtSameLevel(n.Right, currentLevel + 1, ref depth);
            }
        }
    }

    

    class Node
    {
        public Node(int data)
        {
            Data = data;
        }

        public int Data { get; private set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }
}
