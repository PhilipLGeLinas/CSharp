// <copyright file="NumericalNode.cs" company="PlaceholderCompany">
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
    /// A node that contains a numerical value.
    /// </summary>
    public class NumericalNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericalNode"/> class.
        /// </summary>
        /// <param name="value">The value stored by the node.</param>
        public NumericalNode(string value)
            : base(value)
        {
        }
    }
}
