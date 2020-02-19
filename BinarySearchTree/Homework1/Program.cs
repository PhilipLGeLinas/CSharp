// <copyright file="Program.cs" company="Philip L. GeLinas">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Homework1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This program is designed to retrieve a list of integers from the user,
    /// generate a binary search tree, and print statistics regarding the tree.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This function performs all calls to outside functions in
        /// order to accomplish the design of the program.
        /// </summary>
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter a collection of numbers in the range [0, 100], separated by spaces:");
                string input = Console.ReadLine(); // User-entered values
                HashSet<int> inputSet = new HashSet<int>(); // HashSet used to remove duplicates

                if (input != string.Empty)
                {
                    // Add all user-entered values to a HashSet to remove duplicates.
                    foreach (string s in input.Split())
                    {
                        inputSet.Add(Convert.ToInt32(s));
                    }

                    int[] inputArray = inputSet.ToArray(); // Array of user-entered values used for indexing

                    // Construct a binary search tree with the first user-entered value.
                    BinarySearchTree bst = new BinarySearchTree(new BinarySearchTreeNode(inputArray[0]));

                    // Create and add all nodes to the binary search tree.
                    for (int i = 1; i < inputArray.Length; i++)
                    {
                        bst.AddNode(new BinarySearchTreeNode(inputArray[i]));
                    }

                    // Print tree content and statistics to the console.
                    Console.Write("Tree contents: ");
                    bst.TraverseTree(bst.Head);
                    Console.WriteLine();
                    Console.WriteLine("Tree statistics:");
                    int nodeCount = bst.NodeCount(bst.Head);
                    Console.WriteLine("  Number of nodes: " + nodeCount);
                    int levelCount = bst.LevelCount(bst.Head);
                    Console.WriteLine("  Number of levels: " + levelCount);
                    int minimumLevelCount = bst.MinimumLevelCount(nodeCount);
                    Console.WriteLine("  Minimum number of levels that a tree with " + nodeCount + " nodes could have = " + minimumLevelCount);
                    Console.WriteLine("Done");
                }
                else
                {
                    // Print empty tree statistics to the console.
                    Console.WriteLine("Tree contents: ");
                    Console.WriteLine("Tree statistics:");
                    Console.WriteLine("  Number of nodes: 0");
                    Console.WriteLine("  Number of levels: 0");
                    Console.WriteLine("  Minimum number of levels that a tree with 0 nodes could have = 0");
                    Console.WriteLine("Done");
                }

                // Prepare application for next loop.
                Console.WriteLine();
                Console.WriteLine("Press \"enter\" to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
