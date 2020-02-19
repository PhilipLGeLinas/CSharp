// <copyright file="BinarySearchTreeNode.cs" company="Philip L. GeLinas">
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
    /// The infrastructure for a single node that stores
    /// the information for a binary search tree.
    /// </summary>
    public class BinarySearchTreeNode
    {
        private BinarySearchTreeNode left;
        private BinarySearchTreeNode right;
        private int value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTreeNode"/> class.
        /// </summary>
        /// <param name="value">The value stored by the node.</param>
        public BinarySearchTreeNode(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the left node.
        /// </summary>
        /// <value>
        /// The left node.
        /// </value>
        public BinarySearchTreeNode Left
        {
            get { return this.left; }
            set { this.left = value; }
        }

        /// <summary>
        /// Gets or sets the right node.
        /// </summary>
        /// <value>
        /// The right node.
        /// </value>
        public BinarySearchTreeNode Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        /// <summary>
        /// Gets or sets the node's value.
        /// </summary>
        /// <value>
        /// The node's stored value.
        /// </value>
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
