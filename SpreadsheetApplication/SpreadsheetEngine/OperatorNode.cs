// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
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
    /// A node that stores an arithmetic operator as its value.
    /// </summary>
    public abstract class OperatorNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="value">The value stored by the node.</param>
        public OperatorNode(string value)
            : base(value)
        {
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Gets or sets the left node.
        /// </summary>
        public Node Left { get; set; }

        /// <summary>
        /// Gets or sets the right node.
        /// </summary>
        public Node Right { get; set; }

        /// <summary>
        /// Calculates and returns the result of an arithmetic operation between two doubles.
        /// </summary>
        /// <param name="operand1">The first operand.</param>
        /// <param name="operand2">The second operand.</param>
        /// <returns>The result of the arithmetic operation.</returns>
        public abstract double Evaluate(double operand1, double operand2);
    }
}
