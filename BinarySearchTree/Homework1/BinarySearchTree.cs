// <copyright file="BinarySearchTree.cs" company="Philip L. GeLinas">
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
    /// The infrastructure and methods necessary to
    /// implement a binary search tree.
    /// </summary>
    public class BinarySearchTree
    {
        private BinarySearchTreeNode head;
        private int nodeCount;
        private int levelCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree"/> class.
        /// </summary>
        /// <param name="head">The head node of the binary search tree.</param>
        public BinarySearchTree(BinarySearchTreeNode head)
        {
            this.head = head;
        }

        /// <summary>
        /// Gets or sets the topmost node of a binary search tree.
        /// </summary>
        /// <value>
        /// The topmost node of a binary search tree.
        /// </value>
        public BinarySearchTreeNode Head
        {
            get { return this.head;  }

            set { this.head = value;  }
        }

        /// <summary>
        /// Method used to add a new node to a binary search tree.
        /// </summary>
        /// <param name="node">The new node to add to the binary search tree.</param>
        public void AddNode(BinarySearchTreeNode node)
        {
            AddNodeHelper(head, node);
        }

        /// <summary>
        /// Method used to perform an in-order traversal of a binary search tree.
        /// </summary>
        /// <param name="currentNode">The current node being examined during tree traversal.</param>
        public void TraverseTree(BinarySearchTreeNode currentNode)
        {
            if (currentNode.Left != null)
            {
                TraverseTree(currentNode.Left);
            }

            Console.Write(currentNode.Value + " ");

            if (currentNode.Right != null)
            {
                TraverseTree(currentNode.Right);
            }
        }

        /// <summary>
        /// Method used to count all nodes of a binary search tree.
        /// </summary>
        /// <param name="node">The head (starting) node for node count.</param>
        /// <returns>Returns the total number of nodes in the binary search tree.</returns>
        public int NodeCount(BinarySearchTreeNode node)
        {
            nodeCount = 0;

            NodeCountHelper(node);

            return nodeCount;
        }

        /// <summary>
        /// Method used to count all levels of a binary search tree.
        /// </summary>
        /// <param name="node">The head (starting) node used for level count.</param>
        /// <returns>Returns the total number of levels in the binary search tree.</returns>
        public int LevelCount(BinarySearchTreeNode node)
        {
            levelCount = 1;

            LevelCountHelper(node, levelCount);

            return levelCount;
        }

        /// <summary>
        /// Algorithm used to determine minimum number of levels that a tree can have given a node count.
        /// </summary>
        /// <param name="nodeCount">The total number of nodes in the binary search tree.</param>
        /// <returns>Returns the minimum number of levels that a given binary search tree can have.</returns>
        public int MinimumLevelCount(int nodeCount)
        {
            return Convert.ToInt32(Math.Ceiling(Math.Log(Convert.ToDouble(nodeCount + 1), 2.0)));
        }

        /// <summary>
        /// Helper method used to determine the number of nodes in a binary search tree.
        /// </summary>
        /// <param name="currentNode">The node currently being counted during node count.</param>
        private void NodeCountHelper(BinarySearchTreeNode currentNode)
        {
            if (currentNode.Left != null)
            {
                NodeCountHelper(currentNode.Left);
            }

            nodeCount++;

            if (currentNode.Right != null)
            {
                NodeCountHelper(currentNode.Right);
            }
        }

        /// <summary>
        /// Helper method used to determine the number of levels in a binary search tree.
        /// </summary>
        /// <param name="currentNode">The node currently being examined for level count.</param>
        /// <param name="levels">The current number of levels counted in the binary search tree.</param>
        private void LevelCountHelper(BinarySearchTreeNode currentNode, int levels)
        {
            if (levels > levelCount)
            {
                levelCount = levels;
            }

            if (currentNode.Left != null)
            {
                LevelCountHelper(currentNode.Left, levels + 1);
            }

            if (currentNode.Right != null)
            {
                LevelCountHelper(currentNode.Right, levels + 1);
            }
        }

        /// <summary>
        /// Helper function used to add a new node to a binary search tree.
        /// </summary>
        /// <param name="currentNode">The node currently being compared to the new node.</param>
        /// <param name="node">The new node to add to the binary search tree.</param>
        private void AddNodeHelper(BinarySearchTreeNode currentNode, BinarySearchTreeNode node)
        {
            if (node.Value > currentNode.Value)
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = node;
                }
                else
                {
                    AddNodeHelper(currentNode.Right, node);
                }
            }
            else
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = node;
                }
                else
                {
                    AddNodeHelper(currentNode.Left, node);
                }
            }
        }
    }
}
