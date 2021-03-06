﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   //     8 
   //   /   \
   // 7       9
   // \      /
   //  4    1
namespace BinaryTreeProblems
{
    class Program
    {
        static void Main(string[] args)
        {
            Node n = new Node(8);
            n.Left = new Node(7);
            n.Right = new Node(9);
            n.Left.Right = new Node(4);
            n.Right.Left = new Node(1);

            Console.WriteLine("Max node value: {0}", GetMaxNodeValue(n));

            Console.WriteLine("Are leaf nodes at the same level ?: {0}", AreLeafNodesAtSameLevel(n));

            Console.WriteLine("Sum of paths from root: {0}", SumOfPaths(n));

            Console.WriteLine("Max sum represented by binary tree: {0}", MaxSum(n));

            Console.WriteLine("List representation of binary tree");

            LinkedList<int> result = ConvertTreeToDoubleLinkedList(n);

            LinkedListNode<int> node = result.First;
            while (node != null)
            {
                Console.Write(node.Value + " ");
                node = node.Next;
            }
            Console.WriteLine();

            Node deepestLeftNode = GetDeepestLeftNode(n);
            Console.WriteLine("Deepest left node is {0}", deepestLeftNode.Data);

            Node deepestNodeAtOddLevel = GetNodeAtDeepestOddLevel(n);
            Console.WriteLine("Node at deepest odd level is {0}", deepestNodeAtOddLevel.Data);

            // Mirror changes the reference of n so keep this as the last test
            Console.WriteLine("In order traversal of mirrored version of n");
            MirrorBinaryTree(n);
            InOrder(n);

            Node head = new Node(3);
            head.Left = new Node(1);
            head.Right = new Node(4);

            Console.WriteLine("Updated version of head");
            UpdateNode(head);
            InOrder(head);

            int sum = 21;
            List<int> output = new List<int>();

               //   7
               // /    \
               //3      4
               // \      \
               //   6      1
               //  / \
               // 2   5

            Node test = new Node(7);
            test.Left = new Node(3);
            test.Right = new Node(4);
            test.Right.Right = new Node(1);
            test.Left.Right = new Node(6);
            test.Left.Right.Left = new Node(2);
            test.Left.Right.Right = new Node(5);

            bool result2 = DoesPathAddUp(test, sum, output);
            Console.WriteLine("Does path add up for sum {0} : {1}", sum, result2);

            Console.WriteLine("Path is");
            if (result2)
            {
                for (int i = output.Count - 1; i >= 0; i--)
                {
                    Console.Write(output[i] + " ");
                }
            }

            // TODO - Add test case for IsSubTree
            // END TODO

            Console.WriteLine();
            int sum2 = 11;
            Console.WriteLine("All paths with sum {0}", sum2);
            FindSum(test, sum2);

            // Test case for nodes at distance k from leaf ndoes
            Console.WriteLine("Nodes at distance {0} from leaf", 1);
            PrintNodesAtDistanceKFromLeaf(test, 1);

            // Test case for problem 17
               /*    1
                   /   \
                  2     3
                 /  \ 
                4    5                     
               / \                         
              6   7                        */


            Node root = new Node(1);
            root.Left = new Node(2);
            root.Right = new Node(3);
            root.Left.Left = new Node(4);
            root.Left.Right = new Node(5);
            root.Left.Right.Left = new Node(6);
            root.Left.Right.Right = new Node(7);

            int a = 3;
            int b = 7;
            int distance = FindDistance(root, a, b);

            Console.WriteLine("Distance between {0} and {1} is {2}", a, b, distance);

            int diameter = Diameter(root);
            Console.WriteLine("Diameter of given tree is {0}", diameter);
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

        // Problem 5: Given a binary tree with digits (0 - 9), 
        // return sum of numbers represented by each path
        // For example, in the above example
        // answer is 874 + 891
        static int SumOfPaths(Node n)
        {
            int number = 0;

            return Sum(n, number);
        }

        static int Sum(Node n, int number)
        {
           if (n == null)
           {
              return 0;
           }

           // NOTE: This can be modified to support nodes
           // having values with more than 1 digit
           number = number * 10 + n.Data;

           if (n.Left == null && n.Right == null)
           {
              return number;
           }
   
           return Sum(n.Left, number) + Sum(n.Right, number);
        }

        // Problem 6: Return max sum represented by any path
        static int MaxSum(Node n)
        {
            if (n == null)
            {
                return Int32.MinValue;
            }

            return Math.Max(n.Data, n.Data + Math.Max(MaxSum(n.Left), MaxSum(n.Right)));
        }

        // Problem 7: Convert binary tree to double linked list 
        // For given tree, we could do inorder traversal and create a double linked list
        // 7 - 4 - 8 - 1 - 9
        static LinkedList<int> ConvertTreeToDoubleLinkedList(Node n)
        {
            LinkedList<int> list = new LinkedList<int>();

            ConvertTreeToDoubleLinkedList(n, list);

            return list;
        }

        static void ConvertTreeToDoubleLinkedList(Node n, LinkedList<int> list)
        {
            if (n == null)
            {
                return;
            }

            ConvertTreeToDoubleLinkedList(n.Left, list);

            list.AddLast(n.Data);

            ConvertTreeToDoubleLinkedList(n.Right, list);
        }

        // Problem 8: Get deepest left node
        static Node GetDeepestLeftNode(Node n)
        {
            int currentLevel = 1;
            int maxDepth = 1;

            return GetDeepestLeftNode(n, currentLevel, ref maxDepth, false);
        }

        static Node GetDeepestLeftNode(Node n, int currentLevel, ref int maxDepth, bool isLeft)
        {
           if (n == null)
           {
              return null;
           }
           else if (n.Left == null && n.Right == null)
           {
               if ((currentLevel > maxDepth) && isLeft)
               {
                   maxDepth = currentLevel;
               }

               if (isLeft && currentLevel  == maxDepth)
               {
                   return n;
               }

               return null;
           }
           else
           {
               Node leftResult = GetDeepestLeftNode(n.Left, currentLevel + 1, ref maxDepth, true);
               Node rightResult = GetDeepestLeftNode(n.Right, currentLevel + 1, ref maxDepth, false);

               if (leftResult != null)
               {
                   return leftResult;
               }
               else if (rightResult != null)
               {
                   return rightResult;
               }
               else
               {
                   return null;
               }
           }
        }


        // Problem 9: Get node at deepest odd level
        // If there is more than 1 node at the same level
        // which happens to be the deeepest odd level then
        // return the first one that is encountered
        static Node GetNodeAtDeepestOddLevel(Node n)
        {
            int currentLevel = 1;
            int maxOddDepth = 1;

            return GetNodeAtDeepestOddLevel(n, currentLevel, ref maxOddDepth);
        }

        static Node GetNodeAtDeepestOddLevel(Node n, int currentLevel, ref int maxOddDepth)
        {
            if (n == null)
            {
                return null;
            }
            else if (n.Left == null && n.Right == null)
            {
               if ((currentLevel % 2 != 0) && (currentLevel > maxOddDepth))
               {
                  maxOddDepth = currentLevel;
                  return n;
               }

               return null;
            }
            else
            {
                Node leftResult = GetNodeAtDeepestOddLevel(n.Left, currentLevel + 1, ref maxOddDepth);
                Node rightResult = GetNodeAtDeepestOddLevel(n.Right, currentLevel + 1, ref maxOddDepth);

                return leftResult != null ? leftResult : rightResult;
            }
        }

        // Problem 11: Given a binary search tree, update each node to contain the sum
        // of its key and all keys greater. Example below

         //     8                         13
         //   /   \           =>         /   \
         //  2     5                  15       5
        static void UpdateNode(Node n)
        {
            int value = 0;
            UpdateNode(n, ref value);
        }

        static void UpdateNode(Node n, ref int value)
        {
            if (n == null)
            {
                return;
            }

            UpdateNode(n.Right, ref value);

            n.Data += value;
            value = n.Data;

            UpdateNode(n.Left, ref value);
        }

         // Problem 12 : Given a value, determine if any path of the given binary tree
         // adds up to the value and if so print the path
        static bool DoesPathAddUp(Node n, int sum, List<int> output)
        {
            if (n == null)
            {
                return sum == 0;
            }

            bool leftResult = DoesPathAddUp(n.Left, sum - n.Data, output);
            bool rightResult = DoesPathAddUp(n.Right, sum - n.Data, output);

            if (leftResult || rightResult)
            {
                output.Add(n.Data);
            }

            return leftResult || rightResult;
        }

        // Problem 13: You have two very large binary trees. T1 with millions of nodes
        // and T2 with hundreds of nodes. Create an algorithm to decide if T2 is a subtree of 
        // T1

        bool containsTree(Node t1, Node t2)
        {
            if (t2 == null)
            {
                return true;
            }

            return IsSubTree(t1, t2);
        }

        bool IsSubTree(Node n1, Node n2)
        {
            if (n1 == null)
            {
                return false;
            }

            if (n1.Data == n2.Data)
            {
                return Match(n1, n2);
            }

            return IsSubTree(n1.Left, n2) || IsSubTree(n1.Right, n2);
        }

        bool Match(Node n1, Node n2)
        {
            if (n1 == null & n2 == null)
            {
                return true;
            }

            if (n1 == null || n2 == null)
            {
                return false;
            }

            if (n1.Data != n2.Data)
            {
                return false;
            }

            return Match(n1.Left, n2.Left) && Match(n1.Right, n2.Right);
        }

        // Problem 14: Given a binary tree and a sum value, print 
        // all possible sets of nodes in a path
        // that add up to the value. Note - all nodes in a given
        // path do not have to add up to the sum, even a subset will 
        // do

        static void FindSum(Node n, int sum)
        {
            int depth = GetDepth(n);

            int[] path = new int[depth];

            FindSum(n, sum, path, 0);
        }

        static int GetDepth(Node n)
        {
            if (n == null)
            {
                return 0;
            }

            return (1 + Math.Max(GetDepth(n.Left), GetDepth(n.Right)));
        }

        static void FindSum(Node n, int sum, int[] path, int level)
        {
            if (n == null)
            {
                return;
            }

            path[level] = n.Data;

            int tempSum = 0;

            for (int i = level; i >= 0; i--)
            {
                tempSum += path[i];

                if (tempSum == sum)
                {
                    Print(path, i, level);
                }
            }

            FindSum(n.Left, sum, path, level + 1);
            FindSum(n.Right, sum, path, level + 1);

            path[level] = Int32.MinValue; // not to set to 0 since this could have been a negative value
        }

        static void Print(int[] path, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.Write(path[i] + " ");
            }
            Console.WriteLine();
        }

        // Problem 15: Given a Binary Tree and a positive integer k, 
        // print all nodes that are distance k from a leaf node

        static void PrintNodesAtDistanceKFromLeaf(Node n, int k)
        {
            int depth = GetDepth(n);

            bool[] visited = new bool[depth];

            int[] arr = new int[depth];

            PrintNodesAtDistanceKFromLeaf(n, k, arr, visited, 0);
        }

        // Problem 16: Pass in an array of size depth, which will be the max length of any path
        // also pass in a visited array of same size so same values are not repeated
        // At each node, add value to array and also mark that as visited.
        // When the leaf node is encountered, check if (level - k) was not visited,
        // in that case print it and mark as visited.
        // remember to reset visited state when value is overwritten
        static void PrintNodesAtDistanceKFromLeaf(Node n, int k, int[] arr, bool[] visited, int level)
        {
            if (n == null)
            {
                return;
            }

            arr[level] = n.Data;
            // NOTE: reset the boolean flag when overwriting data
            visited[level] = false;

            if ((n.Left == null && n.Right == null) &&
                (visited[level - k] == false) &&
                ((level - k) >= 0))
            {
                Console.WriteLine("{0}", arr[level - k]);
                visited[level - k] = true;
            }

            PrintNodesAtDistanceKFromLeaf(n.Left, k, arr, visited, level + 1);
            PrintNodesAtDistanceKFromLeaf(n.Right, k, arr, visited, level + 1);
        }

        // Problem 17: Find distance between two given keys of a bianry tree
        // Distance - minimum number of edges that need to be 
        // traversed to get from node a to node b
        // Dist(4,5) = 2
        // Disr(4, 3) = 3
        /*                1
                        /   \
                      2       3
                    /  \ 
                   4    5                                      
                       / \                                 
                      6   7                        */

        // Guarantee - a and b should be present in tree
        // rooted at n
        static int FindDistance(Node n, int a, int b)
        {
            if (n == null)
            {
                return -1;
            }

            Node lcaNode = FindLCA(n, a, b);

            return FindDistanceUtil(lcaNode, a, 0) +
                   FindDistanceUtil(lcaNode, b, 0);
        }


        // Find distance between n and node represented by value
        static int FindDistanceUtil(Node n, int value, int level)
        {
            if (n == null)
            {
                return -1;
            }

            if (n.Data == value)
            {
                return level;
            }

            int leftDistance = FindDistanceUtil(n.Left, value, level + 1);

            if (leftDistance == -1)
            {
                return FindDistanceUtil(n.Right, value, level + 1);
            }

            return leftDistance;
        }

        static Node FindLCA(Node root, int a, int b)
        {
            if (root == null)
            {
                return null;
            }

            if (root.Data == a || root.Data == b)
            {
                return root;
            }

            Node leftResult = FindLCA(root.Left, a, b);
            Node rightResult = FindLCA(root.Right, a, b);

            if (leftResult != null && rightResult != null)
            {
                return root;
            }
            else if (leftResult != null)
            {
                return leftResult;
            }
            else if (rightResult != null)
            {
                return rightResult;
            }
            else
            {
                return null;
            }
        }

        // Problem 18: Find the diameter of a binary tree
        // The diameter of a tree (sometimes called the width) is the number of nodes on the longest path between two leaves in the tree
        // Diameter of a tree is the largest of 3 things
        // Diameter of T's left subtree
        // Diameter of T's right subtree
        // longest path through root = height(left sub tree) + 1 + height(right sub tree)

        static int Diameter(Node n)
        {
            if (n == null)
            {
                return 0;
            }

            int leftHeight = Height(n.Left);
            int rightHeight = Height(n.Right);

            int leftDiameter = Diameter(n.Left);
            int rightDiameter = Diameter(n.Right);

            return Math.Max(leftHeight + rightHeight + 1,
                            Math.Max(leftDiameter, rightDiameter));

        }

        static int Height(Node n)
        {
            if (n == null)
            {
                return 0;
            }

            return 1 + Math.Max(Height(n.Left), Height(n.Right));
        }

    }

    

    class Node
    {
        public Node(int data)
        {
            Data = data;
        }

        public int Data { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }
}
