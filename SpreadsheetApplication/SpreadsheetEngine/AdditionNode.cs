// <copyright file="AdditionNode.cs" company="PlaceholderCompany">
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
    /// Contains the evaluation method for addition.
    /// </summary>
    public class AdditionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        /// <param name="value">The value stored by the node.</param>
        public AdditionNode(string value)
            : base(value)
        {
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Calculates and returns the result of an addition operation between two doubles.
        /// </summary>
        /// <param name="operand1">The first operand.</param>
        /// <param name="operand2">The second operand.</param>
        /// <returns>The result of the addition operation.</returns>
        public override double Evaluate(double operand1, double operand2)
        {
            return operand1 + operand2;
        }
    }
}
