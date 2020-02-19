// <copyright file="Log.cs" company="PlaceholderCompany">
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
    /// Defines a log of the history of a DataGridView object.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// The stack of undo commands.
        /// </summary>
        private readonly Stack<Command> undoStack;

        /// <summary>
        /// The stack of redo commands.
        /// </summary>
        private readonly Stack<Command> redoStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        public Log()
        {
            this.undoStack = new Stack<Command>(); // Initialize the undo stack.
            this.redoStack = new Stack<Command>(); // Initialize the redo stack.
        }

        /// <summary>
        /// Pushes a command to the undo stack.
        /// </summary>
        /// <param name="message">The command message.</param>
        /// <param name="column">The command column.</param>
        /// <param name="row">The command row.</param>
        /// <param name="val">The command value.</param>
        public void PushUndo(string message, int column, int row, string val)
        {
            this.undoStack.Push(new Command(message, column, row, val));
        }

        /// <summary>
        /// Pops a command from the undo stack.
        /// </summary>
        /// <returns>Returns the command at the top of the undo stack.</returns>
        public Command PopUndo()
        {
            // Check that the undo stack is non-empty.
            if (this.undoStack.Count == 0)
            {
                return null;
            }

            return this.undoStack.Pop();
        }

        /// <summary>
        /// Pushes a command to the redo stack.
        /// </summary>
        /// <param name="message">The command message.</param>
        /// <param name="column">The command column.</param>
        /// <param name="row">The command row.</param>
        /// <param name="val">The command value.</param>
        public void PushRedo(string message, int column, int row, string val)
        {
            this.redoStack.Push(new Command(message, column, row, val));
        }

        /// <summary>
        /// Pops a command from the top of the redo stack.
        /// </summary>
        /// <returns>Returns the command at the top of the redo stack.</returns>
        public Command PopRedo()
        {
            // Check that the redo stack is non-empty.
            if (this.redoStack.Count == 0)
            {
                return null;
            }

            return this.redoStack.Pop();
        }

        /// <summary>
        /// Checks the top of the undo stack.
        /// </summary>
        /// <returns>Returns the command message if the stack is not empty, null otherwise.</returns>
        public string PeekUndo()
        {
            // Check that the undo stack is non-empty.
            if (this.undoStack.Count == 0)
            {
                return null;
            }

            return this.undoStack.Peek().Message;
        }

        /// <summary>
        /// Checks the top of the redo stack.
        /// </summary>
        /// <returns>Returns the command message if the stack is not empty, null otherwise.</returns>
        public string PeekRedo()
        {
            // Check that the redo stack is non-empty.
            if (this.redoStack.Count == 0)
            {
                return null;
            }

            return this.redoStack.Peek().Message;
        }

        /// <summary>
        /// Clears the undo and redo stacks.
        /// </summary>
        public void Clear()
        {
            while (this.undoStack.Count > 0)
            {
                this.undoStack.Pop();
            }

            while (this.redoStack.Count > 0)
            {
                this.redoStack.Pop();
            }
        }
    }
}
