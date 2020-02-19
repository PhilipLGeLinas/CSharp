// <copyright file="MultiplicationNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains the evaluation method for multiplication.
    /// </summary>
    public class MultiplicationNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        /// <param name="value">The value stored by the node.</param>
        public MultiplicationNode(string value)
            : base(value)
        {
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Calculates and returns the result of an multiplication operation between two doubles.
        /// </summary>
        /// <param name="operand1">The first operand.</param>
        /// <param name="operand2">The second operand.</param>
        /// <returns>The result of the multiplication operation.</returns>
        public override double Evaluate(double operand1, double operand2)
        {
            return operand1 * operand2;
        }
    }
}
