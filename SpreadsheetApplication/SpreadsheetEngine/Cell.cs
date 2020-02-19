// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Programmer: Philip L. GeLinas
// Student ID: 11572868
namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Class Cell designs an abstraction for a single cell in the data table.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// The text stored by the cell.
        /// </summary>
        protected string text = string.Empty;

        /// <summary>
        /// Represents the evaluated value of the cell.
        /// </summary>
        protected string value;

        /// <summary>
        /// The index of the cell row.
        /// </summary>
        private readonly int rowIndex;

        /// <summary>
        /// The index of the cell column.
        /// </summary>
        private readonly int columnIndex;

        /// <summary>
        /// The background color of the cell.
        /// </summary>
        private uint bgcolor;

        /// <summary>
        /// A list of all cells that are dependent on this cell.
        /// </summary>
        private List<Cell> dependencies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">The index of the row in the spreadsheet.</param>
        /// <param name="columnIndex">The index of the column in the spreadsheet.</param>
        public Cell(int rowIndex, int columnIndex)
        {
            // Update member variables.
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.dependencies = new List<Cell>();
            this.bgcolor = 0xFFFFFFFF;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// A list of functions called whenever a dependent cell's text has changed.
        /// </summary>
        public event PropertyChangedEventHandler DependencyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the value of rowIndex.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets the value of columnIndex.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>
        /// Gets or sets the current evaluated value of the cell.
        /// </summary>
        public string Value
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current value of text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value == this.text)
                {
                    return;
                }
                else if (value == "RESET")
                {
                    // Pass row index, column index, and new value as args.
                    this.PropertyChanged(this, new PropertyChangedEventArgs(this.rowIndex.ToString() + " " + this.columnIndex.ToString() + " " + "RESET"));
                    foreach (Cell dependency in this.dependencies.ToList())
                    {
                        this.DependencyChanged(this, new PropertyChangedEventArgs(dependency.rowIndex.ToString() + " " + dependency.columnIndex.ToString()));
                    }
                }
                else
                {
                    string old = this.text;
                    this.text = value; // Update member text variable.

                    // Pass row index, column index, and new value as args.
                    this.PropertyChanged(this, new PropertyChangedEventArgs(this.rowIndex.ToString() + " " + this.columnIndex.ToString() + " " + old));
                    foreach (Cell dependency in this.dependencies.ToList())
                    {
                        this.DependencyChanged(this, new PropertyChangedEventArgs(dependency.rowIndex.ToString() + " " + dependency.columnIndex.ToString()));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color of the cell.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.bgcolor;
            }

            set
            {
                uint old = this.bgcolor;
                this.bgcolor = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs(this.rowIndex + " " + this.columnIndex + " " + value + " " + "bgcolor" + " " + old));
            }
        }

        /// <summary>
        /// Adds a cell to this cell's list of dependencies.
        /// </summary>
        /// <param name="cell">The cell to be added to the list of dependencies.</param>
        public void AddDependency(Cell cell)
        {
            this.dependencies.Add(cell);
        }

        /// <summary>
        /// Resets the list of dependencies for the current cell.
        /// </summary>
        public void ResetDependencies()
        {
            this.dependencies = new List<Cell>();
        }
    }
}
