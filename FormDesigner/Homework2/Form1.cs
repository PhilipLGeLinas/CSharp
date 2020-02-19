// <copyright file="Form1.cs" company="Philip L. GeLinas">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Homework2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Class Form1 generates a list of 10,000 integer values between 0 and 20,000, inclusive, and counts
    /// the distinct integers using 3 separate algorithms, printing the results to the windows form.
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
        /// Generates a list of 10,000 pseudo-random
        /// integers between 0 and 20,000, inclusive.
        /// </summary>
        /// <param name="random">A random integer generator object.</param>
        /// <returns>A list of 10,000 pseudo-random integers between 0 and 20,000, inclusive.</returns>
        public List<int> GenerateList(Random random)
        {
            List<int> list = new List<int>(10000);

            // Generate a list of 10,000 pseudo-random integers.
            for (int i = 0; i < 10000; i++)
            {
                list.Add(random.Next(20001));
            }

            return list;
        }

        /// <summary>
        /// Determines the number of distinct integers in the given list using a HashSet.
        /// </summary>
        /// <param name="sb">A StringBuilder object used to build the final string text.</param>
        /// <param name="list">A list of integers to be examined for distinct values.</param>
        /// <returns>The number of unique integers contained in the given list.</returns>
        public int Method1(StringBuilder sb, List<int> list)
        {
            HashSet<int> hashSet = new HashSet<int>();
            foreach (int i in list)
            {
                hashSet.Add(i);
            }

            sb.AppendLine("1. HashSet method: " + hashSet.Count + " unique numbers");
            sb.AppendLine("The time complexity of this method is O(n) to add each of the integers to the list and then" +
                "O(n) to add each of the integers to the HashSet. Therefore, although we will observe an average time" +
                "complexity of O(nlog(n)), the worst case time complexity will be O(n^2).\n");

            return hashSet.Count();
        }

        /// <summary>
        /// Determines the number of distinct integers in the given list without altering the list.
        /// </summary>
        /// <param name="sb">A StringBuilder object used to build the final string text.</param>
        /// <param name="list">A list of integers to be examined for distinct values.</param>
        /// <returns>The number of unique integers contained in the given list.</returns>
        public int Method2(StringBuilder sb, List<int> list)
        {
            int count = 0;
            bool unique;

            for (int i = 0; i < 10000; i++)
            {
                unique = true;
                for (int j = i + 1; j < 10000; j++)
                {
                    if (list[j] == list[i])
                    {
                        unique = false;
                        break;
                    }
                }

                if (unique)
                {
                    count++;
                }
            }

            sb.AppendLine("2. O(1) storage method: " + count.ToString() + " unique numbers");
            return count;
        }

        /// <summary>
        /// Determines the number of distinct integers in the given list by sorting the list.
        /// </summary>
        /// <param name="sb">A StringBuilder object used to build the final string text.</param>
        /// <param name="list">A list of integers to be examined for distinct values.</param>
        /// <returns>The number of unique integers contained in the given list.</returns>
        public int Method3(StringBuilder sb, List<int> list)
        {
            int count = 1;
            list.Sort();
            for (int i = 1; i < 10000; i++)
            {
                int j = list[i];
                if (j != list[i - 1])
                {
                    count++;
                }
            }

            sb.AppendLine("3. Sorted method: " + count.ToString() + " unique numbers");
            return count;
        }

        /// <summary>
        /// Displays to the windows form the result of all strings added to the StringBuilder object.
        /// </summary>
        /// <param name="sb">A StringBuilder object that contains the final text to be displayed.</param>
        /// <returns>Returns true if the results are successfully displayed.</returns>
        public bool DisplayResults(StringBuilder sb)
        {
            this.Text = "Philip GeLinas - 11572868";
            this.textBox1.Text = sb.ToString();
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(); // Used to build the final string text.
            Random random = new Random(); // Used to generate pseudo-random integers for a list.

            // Generate a list of 10,000 pseudo-random
            // integers between 0 and 20,000, inclusive.
            List<int> list = this.GenerateList(random);

            // Returns the count of integers in the
            // passed list using the HashSet method.
            this.Method1(sb, list);

            // Returns the count of integers in the
            // passed list using the unaltered method.
            this.Method2(sb, list);

            // Returns the count of integers in the
            // passed list using the sort method.
            this.Method3(sb, list);

            // Displays the results to the windows form.
            this.DisplayResults(sb);
        }
    }
}
