// <copyright file="ExpressionTreeFactory.cs" company="PlaceholderCompany">
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
    /// Returns a numerical, operator, or variable node, depending on the given input.
    /// </summary>
    public class ExpressionTreeFactory
    {
        /// <summary>
        /// Determines if a given string should construct a numerical or variable node.
        /// </summary>
        /// <param name="s">The string to be parsed.</param>
        /// <returns>Returns true if the string contains only numerical values, false otherwise.</returns>
        public Node CreateNode(string s)
        {
            // Check that each character is numerical or a decimal.
            foreach (char c in s)
            {
                if (c != '.' && (c < 48 || c > 57))
                {
                    if (c == '+')
                    {
                        return new AdditionNode(s);
                    }
                    else if (c == '-')
                    {
                        return new SubtractionNode(s);
                    }
                    else if (c == '*')
                    {
                        return new MultiplicationNode(s);
                    }
                    else if (c == '/')
                    {
                        return new DivisionNode(s);
                    }
                    else
                    {
                        return new VariableNode(s);
                    }
                }
            }

            return new NumericalNode(s);
        }
    }
}
