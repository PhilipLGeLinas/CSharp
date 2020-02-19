// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Programmer: Philip L. GeLinas
// Student ID: 11572868
namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using CptS321;

    /// <summary>
    /// Serves as a container for a 2D array of cells.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// A 2D array of cells that represents a spreadsheet.
        /// </summary>
        public readonly SpreadsheetCell[,] Cells;

        /// <summary>
        /// The number of rows in the spreadsheet.
        /// </summary>
        private readonly int rowCount;

        /// <summary>
        /// The number of columns in the spreadsheet.
        /// </summary>
        private readonly int columnCount;

        private Cell cell;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">The number of rows of cells to add to the spreadsheet.</param>
        /// <param name="columns">The number of columns of cells to add to the spreadsheet.</param>
        public Spreadsheet(int rows, int columns)
        {
            SpreadsheetCell[,] cells = new SpreadsheetCell[rows, columns];

            // Initialize each cell within the array.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Create new cell, subscribe to property changed event, add to 2D array.
                    SpreadsheetCell cell = new SpreadsheetCell(i, j);
                    cell.PropertyChanged += this.Sheet_PropertyChanged;
                    cells[i, j] = cell;
                }
            }

            // Update member variables.
            this.Cells = cells;
            this.rowCount = rows;
            this.columnCount = columns;
        }

        /// <summary>
        /// Handles the event in which a cell's property is changed.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the number of rows in the spreadsheet.
        /// </summary>
        public int RowCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of columns in the spreadsheet.
        /// </summary>
        public int ColumnCount
        {
            get;
        }

        /// <summary>
        /// Gets the cell located at the given row and column.
        /// </summary>
        /// <param name="row">The row location of the cell.</param>
        /// <param name="column">The column location of the cell.</param>
        /// <returns>Returns the cell at the given row and column if it exists and null otherwise.</returns>
        public Cell GetCell(int row, int column)
        {
            // Check for valid row and column.
            if (row < this.rowCount && column < this.columnCount && row >= 0 && column >= 0)
            {
                return this.Cells[row, column];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the cell located at the given row and column.
        /// </summary>
        /// <param name="location">The row and column location of the cell.</param>
        /// <returns>Returns the cell at the given row and column if it exists and null otherwise.</returns>
        public Cell GetCell(string location)
        {
            // Check for invalid string length.
            if (location.Length < 2)
            {
                return null;
            }

            // Check for non-alpha characters.
            if (location[0] < 65 || (location[0] > 90 && location[0] < 97) || location[0] > 122)
            {
                return null;
            }

            // Ensure remaining values are integers.
            for (int i = 1; i < location.Length; i++)
            {
                char c = location[i];
                if (c < 48 || c > 57)
                {
                    return null;
                }
            }

            int column = location[0].ToString().ToLower().ToCharArray()[0] - 97; // Allow capital or lowercase characters.
            int row = Convert.ToInt32(location.Substring(1)) - 1;

            // Check for valid row and column.
            if (column < this.columnCount && row < this.rowCount)
            {
                return this.Cells[row, column];
            }

            return null;
        }

        /// <summary>
        /// Loads a spreadsheet to an XML document.
        /// </summary>
        public void LoadSpreadsheet()
        {
            string fileContent = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file.
                    string filePath = openFileDialog.FileName;

                    // Read the contents of the file into a stream.
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            foreach (Cell c in this.Cells)
            {
                c.Text = string.Empty;
                c.BGColor = 0xFFFFFFFF;
            }

            XDocument srcTree = XDocument.Parse(fileContent);
            foreach (XElement element in srcTree.Descendants().ToList())
            {
                switch (element.Name.ToString())
                {
                    case "cell":
                        this.cell = this.GetCell(element.Attribute("name").Value);
                        break;
                    case "bgcolor":
                        this.cell.BGColor = Convert.ToUInt32(element.Value);
                        break;
                    case "text":
                        this.cell.Text = element.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Saves a spreadsheet to an XML document.
        /// </summary>
        public void SaveSpreadsheet()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    XDocument srcTree = new XDocument(
                        new XElement("spreadsheet"));

                    foreach (Cell cell in this.Cells)
                    {
                        if (cell.BGColor != 0xFFFFFFFF || cell.Text != string.Empty)
                        {
                            srcTree.Root.Add(
                                new XElement(
                                    "cell",
                                    new XAttribute("name", ((char)(cell.ColumnIndex + 65)).ToString() + (cell.RowIndex + 1)),
                                    new XElement("bgcolor", cell.BGColor),
                                    new XElement("text", cell.Text)));
                        }
                    }

                    srcTree.Save(myStream);
                }
            }

            saveFileDialog1.Dispose();
        }

        /// <summary>
        /// Updates the spreadsheet.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">A property changed event arguments.</param>
        private void Sheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (!e.PropertyName.Contains("bgcolor"))
                {
                    int row = Convert.ToInt32(e.PropertyName.Split(' ')[0]);
                    int column = Convert.ToInt32(e.PropertyName.Split(' ')[1]);
                    Cell cell = this.Cells[row, column];

                    // Check to see if cell evaluation is necessary.
                    if (cell.Text.StartsWith("="))
                    {
                        // Check for a self-reference.
                        if (cell.Text.Contains(((char)(column + 65)).ToString() + (row + 1)) || cell.Text.Contains(((char)(column + 97)).ToString() + (row + 1)))
                        {
                            this.CellPropertyChanged.Invoke(this, new PropertyChangedEventArgs(e.PropertyName + " SELF_REFERENCE")); // Call CellPropertyChanged methods in Form1.
                            return;
                        }

                        string expression = cell.Text.Substring(1);
                        ExpressionTree et = new ExpressionTree(expression);
                        et = new ExpressionTree(expression);
                        string[] expArr = expression.Split(et.Operators, StringSplitOptions.None);

                        // Check the length of the expression array.
                        if (expArr.Length == 1)
                        {
                            if (this.CircularReferenceCheck(this.GetCell(row, column), this.GetCell(expArr[0])))
                            {
                                this.CellPropertyChanged.Invoke(this, new PropertyChangedEventArgs(e.PropertyName + " CIRCULAR_REFERENCE"));
                                return;
                            }

                            this.GetCell(expArr[0]).DependencyChanged += this.Dependency_PropertyChanged;
                            this.GetCell(expArr[0]).AddDependency(cell);
                            cell.Value = this.GetCell(expArr[0]).Value;
                        }
                        else
                        {
                            foreach (string s in expArr)
                            {
                                foreach (char c in s)
                                {
                                    if (c < 48 || c > 57)
                                    {
                                        if (this.CircularReferenceCheck(this.GetCell(row, column), this.GetCell(s)))
                                        {
                                            this.CellPropertyChanged.Invoke(this, new PropertyChangedEventArgs(e.PropertyName + " CIRCULAR_REFERENCE"));
                                            return;
                                        }

                                        et.SetVariable(s, Convert.ToDouble(this.GetCell(s).Value));
                                        if (!e.PropertyName.Contains("RESET"))
                                        {
                                            this.GetCell(s).DependencyChanged += this.Dependency_PropertyChanged;
                                            this.GetCell(s).AddDependency(cell);
                                        }

                                        break;
                                    }
                                }
                            }

                            cell.Value = et.Evaluate().ToString();
                        }
                    }
                    else
                    {
                        cell.Value = cell.Text;
                    }
                }

                this.CellPropertyChanged.Invoke(this, e); // Call CellPropertyChanged methods in Form1.
            }
            catch (Exception)
            {
                // Bad reference detected!
                this.CellPropertyChanged.Invoke(this, new PropertyChangedEventArgs(e.PropertyName + " BAD_REFERENCE")); // Call CellPropertyChanged methods in Form1.
            }
        }

        /// <summary>
        /// Checks for a circular reference.
        /// </summary>
        /// <returns>Returns true if a circular reference is detected, false otherwise.</returns>
        private bool CircularReferenceCheck(Cell current, Cell other)
        {
            // Circular reference found!
            if (current.Equals(other))
            {
                return true;
            }

            // Check that the expression includes an equals sign.
            string otherText = other.Text;
            ExpressionTree et = new ExpressionTree(otherText);
            if (otherText.StartsWith("="))
            {
                otherText = otherText.Substring(1);
            }

            // Split the string on operators and check for variables.
            bool result = false;
            List<Cell> cellList = new List<Cell>();
            foreach (string s in otherText.Split(et.Operators, StringSplitOptions.None))
            {
                if (!s.Equals(string.Empty))
                {
                    foreach (char c in s)
                    {
                        if (c < 48 || c > 57)
                        {
                            cellList.Add(this.GetCell(s));
                        }
                    }
                }
            }

            // Check that all variables within the list do not contain circular references, recursively.
            foreach (Cell c in cellList)
            {
                result = result || this.CircularReferenceCheck(current, c);
            }

            return result;
        }

        /// <summary>
        /// Called whenever a dependent cell's text is changed.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">A property changed event arguments.</param>
        private void Dependency_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            int row = Convert.ToInt32(e.PropertyName.Split(' ')[0]);
            int column = Convert.ToInt32(e.PropertyName.Split(' ')[1]);
            Cell cell = this.Cells[row, column];
            cell.Text = "RESET";
        }

        /// <summary>
        /// Allows the Spreadsheet class to instantiate SpreadsheetCells that inherit from the Cell class.
        /// </summary>
        public class SpreadsheetCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
            /// </summary>
            /// <param name="rowIndex">The row of the given cell in the spreadsheet.</param>
            /// <param name="columnIndex">The column of the given cell in the spreadsheet.</param>
            public SpreadsheetCell(int rowIndex, int columnIndex)
                : base(rowIndex, columnIndex)
            {
            }
        }
    }
}
