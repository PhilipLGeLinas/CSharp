// <copyright file="FibonacciTextReader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// Philip L. GeLinas - 11572868

namespace Homework3
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Passed to the loading function in order to load
    /// a given number of values from the Fibonacci sequence.
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        /// <summary>
        /// The maximum number of lines to be printed of the Fibonacci series.
        /// </summary>
        private readonly int maxLines;

        /// <summary>
        /// The current count of lines written of the Fibonacci series.
        /// </summary>
        private int count;

        /// <summary>
        /// Represents one F_(n-1) of the Fibonacci series.
        /// </summary>
        private BigInteger minusOne;

        /// <summary>
        /// Represents one F_(n-2) of the Fibonacci series.
        /// </summary>
        private BigInteger minusTwo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// <param name="maxLines">The maximum number of numbers to be generated in the Fibonacci sequence.</param>
        public FibonacciTextReader(int maxLines)
        {
            this.maxLines = maxLines;
            this.count = 0;
            this.minusOne = new BigInteger(1);
            this.minusTwo = new BigInteger(1);
        }

        /// <inheritdoc/>
        public override string ReadLine()
        {
            string result = string.Empty; // The next line of the Fibonacci series to be returned.

            // Check for edge cases.
            if (this.maxLines < 0 || this.count == this.maxLines)
            {
                this.count = 0;
                return null;
            }

            // Check for base cases.
            else if (this.count == 0 || this.count == 1)
            {
                result += (this.count + 1).ToString() + ". " + "1";
            }

            // Otherwise, return F_(n-1) + F_(n-2).
            else
            {
                BigInteger sum = BigInteger.Add(this.minusOne, this.minusTwo);
                this.minusTwo = this.minusOne;
                this.minusOne = sum;
                result += (this.count + 1).ToString() + ". " + sum.ToString();
            }

            this.count++; // Increment the number of lines that have been returned.
            return result;
        }

        /// <inheritdoc/>
        public override string ReadToEnd()
        {
            StringBuilder sb = new StringBuilder(); // Used to concatenate all lines of the Fibonacci series.
            for (int i = 0; i < this.maxLines; i++)
            {
                sb.Append(this.ReadLine() + Environment.NewLine); // Append each line with a "\n\r".
            }

            return sb.ToString(); // Return the concatenated result.
        }
    }
}
