// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Philip L. GeLinas - 11572868

namespace Homework3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Class Form1 creates a GUI allowing a user to load and save files.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the value of myTextBox.
        /// </summary>
        public TextBox MyTextBox
        {
            get; set;
        }

        /// <summary>
        /// Adds the ability to load text from another file into the form's textbox.
        /// </summary>
        /// <param name="tr">A text reader that allows one to copy text from an external file.</param>
        public void LoadText(TextReader tr)
        {
            this.textBox1.Text = tr.ReadToEnd(); // Replace text box text with text reader content.
        }

        /// <summary>
        /// Presents a dialog that allows a user to open an external file's text in the text box.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The function pointer events.</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    // Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string fileContent = reader.ReadToEnd(); // Retrieve file content.
                        this.textBox1.Text = fileContent; // Replace text box text with file content.
                    }
                }
            }
        }

        /// <summary>
        /// Generates the first 50 numbers of the Fibonacci series and places them in the textbox.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The function pointer delegates.</param>
        private void Button2_Click(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(50); // Generate a Fibonacci series of 50 numbers.
            this.textBox1.Text = ftr.ReadToEnd(); // Replace text box text with the result of the Fibonacci series.
        }

        /// <summary>
        /// Generates the first 100 numbers of the Fibonacci series and places them in the textbox.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The function pointer delegates.</param>
        private void Button3_Click(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(100); // Generate a Fibonacci series of 100 numbers.
            this.textBox1.Text = ftr.ReadToEnd(); // Replace text box text with the result of the Fibonacci series.
        }

        /// <summary>
        /// Adds the functionality for when the "Save to file..." button is pressed.
        /// </summary>
        /// <param name="sender">The object being sent.</param>
        /// <param name="e">The list of function pointer delegates.</param>
        private void Button4_Click(object sender, EventArgs e)
        {
            Stream stream; // A stream used for writing the content of the text box to an external file.
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            // Check that our save file dialog appears successfully.
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Create a stream using an external txt file.
                if ((stream = saveFileDialog.OpenFile()) != null)
                {
                    char[] bytes = this.textBox1.Text.ToCharArray(); // Load the text box content into a char array.
                    foreach (byte b in bytes)
                    {
                        stream.WriteByte(b); // Write the next byte of data to the external file.
                    }

                    stream.Close();
                }
            }

            saveFileDialog.Dispose();
        }
    }
}
