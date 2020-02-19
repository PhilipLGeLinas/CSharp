// <copyright file="Command.cs" company="PlaceholderCompany">
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
    using System.Windows.Forms;

    /// <summary>
    /// Represents an undo or redo command.
    /// </summary>
    public class Command
    {
        private readonly string message;
        private readonly string val;
        private readonly int column;
        private readonly int row;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="message">The prior button message.</param>
        /// <param name="column">The prior column edited.</param>
        /// <param name="row">The prior row edited.</param>
        /// <param name="val">The prior value.</param>
        public Command(string message, int column, int row, string val)
        {
            this.message = message;
            this.val = val;
            this.column = column;
            this.row = row;
        }

        /// <summary>
        /// Gets the command message.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }
        }

        /// <summary>
        /// Gets the previous value.
        /// </summary>
        public string Val
        {
            get
            {
                return this.val;
            }
        }

        /// <summary>
        /// Gets the previous column.
        /// </summary>
        public int Column
        {
            get
            {
                return this.column;
            }
        }

        /// <summary>
        /// Gets the previous row.
        /// </summary>
        public int Row
        {
            get
            {
                return this.row;
            }
        }
    }
}
