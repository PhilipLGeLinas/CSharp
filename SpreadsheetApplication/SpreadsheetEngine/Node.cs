// <copyright file="Node.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Programmer: Philip L. GeLinas
// Student ID: 11572868
namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The fundamental building block for a node in an expression tree.
    /// </summary>
    public abstract class Node
    {
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="value">The value stored by the node.</param>
        public Node(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the value of the current node.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
