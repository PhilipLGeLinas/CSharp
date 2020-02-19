// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Programmer: Philip L. GeLinas
// Student ID: 11572868
namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using SpreadsheetEngine;

    /// <summary>
    /// An expression tree that can build arithmetic expressions and parse them.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// The head of the expression tree.
        /// </summary>
        private readonly OperatorNode head;

        /// <summary>
        /// Represents a tree containing a single double.
        /// </summary>
        private readonly double singleNodeDouble;

        /// <summary>
        /// Represents a tree containing a single variable.
        /// </summary>
        private readonly string singleNodeString;

        /// <summary>
        /// Dictionary of user-defined variables.
        /// </summary>
        private readonly Dictionary<string, double> variables = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">The arithmetic expression to be generated.</param>
        public ExpressionTree(string expression)
        {
            bool isSingleNodeTree = true;
            foreach (char op in this.Operators)
            {
                if (expression.Contains(op))
                {
                    isSingleNodeTree = false;
                    break;
                }
            }

            if (isSingleNodeTree)
            {
                try
                {
                    this.singleNodeDouble = Convert.ToDouble(expression);
                }
                catch (Exception)
                {
                    this.singleNodeString = expression;
                }

                return;
            }

            // Generate the postfix notation for the expression.
            string[] postfixArray = this.ConvertToPostfix(expression);

            // Declare local variables.
            Node left, right;
            OperatorNode center;
            Stack<Node> nodeStack = new Stack<Node>();
            ExpressionTreeFactory etf = new ExpressionTreeFactory();

            // Iterate through each string in the postfix expression.
            for (int i = 0; i < postfixArray.Length; i++)
            {
                string s = postfixArray[i];

                // Check for an empty string.
                if (s == string.Empty)
                {
                    continue;
                }

                // Operator found!
                if (s.Length == 1 && this.Operators.Contains(s[0]))
                {
                    // Create a new node with the top two nodes of the stack
                    // as children and push it onto the top of the stack.
                    center = (OperatorNode)etf.CreateNode(s);
                    right = nodeStack.Pop();
                    left = nodeStack.Pop();
                    center.Left = left;
                    center.Right = right;
                    nodeStack.Push(center);
                }
                else
                {
                    nodeStack.Push(etf.CreateNode(s)); // Push a non-operator node onto the stack.
                }
            }

            // Set the new head of the expression tree.
            this.head = (OperatorNode)nodeStack.Peek();
            nodeStack.Pop();
        }

        /// <summary>
        /// Gets the current set of operators.
        /// </summary>
        public char[] Operators { get; } = new char[] { '+', '-', '*', '/', '(', ')' };

        /// <summary>
        /// Sets a variable within the expression tree.
        /// </summary>
        /// <param name="variableName">The given variable name.</param>
        /// <param name="variableValue">The given variable value.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables.Add(variableName, variableValue);
        }

        /// <summary>
        /// Prints the content of the expression tree to the console.
        /// </summary>
        /// <returns>Returns the infix notation of the expression tree.</returns>
        public string PrintTree()
        {
            string result = string.Empty;
            this.PrintTreeHelper(this.head, result);
            Console.WriteLine();
            return result;
        }

        /// <summary>
        /// Evaluates the expression tree's arithmetic.
        /// </summary>
        /// <returns>Returns the arithmetic calculation of the expression tree.</returns>
        public double Evaluate()
        {
            if (this.head == null)
            {
                if (this.singleNodeString != null)
                {
                    return this.variables[this.singleNodeString];
                }
                else
                {
                    return this.singleNodeDouble;
                }
            }

            OperatorNode current = this.head; // A reference to the head of this expression tree.
            return this.EvaluateHelper(current); // Return the result of the helper method.
        }

        /// <summary>
        /// Evaluates the arithmetic logic of an expression tree.
        /// </summary>
        /// <param name="current">The head of the tree to be evaluated.</param>
        /// <returns>Returns the evaluation of the expression tree.</returns>
        private double EvaluateHelper(OperatorNode current)
        {
            // Both children are leaves.
            if (!this.IsOperatorNode(current.Left) && !this.IsOperatorNode(current.Right))
            {
                double leftVal, rightVal;
                if (this.IsVariableNode(current.Left))
                {
                    leftVal = this.variables[current.Left.Value]; // Get left value from dictionary.
                }
                else
                {
                    leftVal = Convert.ToDouble(current.Left.Value); // Get left value from left node.
                }

                if (this.IsVariableNode(current.Right))
                {
                    rightVal = this.variables[current.Right.Value]; // Get right value from dictionary.
                }
                else
                {
                    rightVal = Convert.ToDouble(current.Right.Value); // Get right value from right node.
                }

                return current.Evaluate(leftVal, rightVal);
            }
            else if (!this.IsOperatorNode(current.Left))
            {
                // Only the right node is an operator node.
                double leftVal;
                if (this.IsVariableNode(current.Left))
                {
                    leftVal = this.variables[current.Left.Value]; // Get left value from dictionary.
                }
                else
                {
                    leftVal = Convert.ToDouble(current.Left.Value); // Get left value from left node.
                }

                return current.Evaluate(leftVal, this.EvaluateHelper((OperatorNode)current.Right));
            }
            else if (!this.IsOperatorNode(current.Right))
            {
                // Only the left node is an operator node.
                double rightVal;
                if (this.IsVariableNode(current.Right))
                {
                    rightVal = this.variables[current.Right.Value]; // Get right value from dictionary.
                }
                else
                {
                    rightVal = Convert.ToDouble(current.Right.Value); // Get right value from right node.
                }

                return current.Evaluate(this.EvaluateHelper((OperatorNode)current.Left), rightVal);
            }
            else
            {
                // Both children are operator nodes.
                OperatorNode leftNode = (OperatorNode)current.Left, rightNode = (OperatorNode)current.Right;
                return current.Evaluate(this.EvaluateHelper(leftNode), this.EvaluateHelper(rightNode));
            }
        }

        /// <summary>
        /// Prints the content of the expression tree to the console.
        /// </summary>
        /// <param name="node">The head of the expression tree.</param>
        /// <param name="result">The resultant string.</param>
        private void PrintTreeHelper(OperatorNode node, string result)
        {
            // In-order traversal algorithm.
            if (node.Left != null && node.Right != null)
            {
                // Check for OperatorNode or other node on left.
                if (!this.IsOperatorNode(node.Left))
                {
                    Console.Write(node.Left.Value);
                    result += node.Left.Value;
                }
                else
                {
                    this.PrintTreeHelper((OperatorNode)node.Left, result);
                }

                // Print current node value.
                Console.Write(node.Value);
                result += node.Value;

                // Check for OperatorNode or other node on right.
                if (!this.IsOperatorNode(node.Right))
                {
                    Console.Write(node.Right.Value);
                }
                else
                {
                    this.PrintTreeHelper((OperatorNode)node.Right, result);
                }
            }
            else
            {
                // Print the leaf's value.
                Console.WriteLine(node.Value);
            }
        }

        /// <summary>
        /// Converts an infix arithmetic expression to its respective postfix notation.
        /// </summary>
        /// <param name="expression">The given infix expression.</param>
        /// <returns>The postfix notation for the given infix expression.</returns>
        private string[] ConvertToPostfix(string expression)
        {
            // Declare local variables
            List<string> postfixNotation = new List<string>();
            Stack<char> operatorStack = new Stack<char>();
            string subexpression = string.Empty;

            // Iterate through each character in the given infix expression.
            for (int i = 0; i < expression.Length; i++)
            {
                char character = expression[i];

                // Check for spaces.
                if (character == ' ')
                {
                    continue;
                }
                else if (!this.Operators.Contains(character))
                {
                    // If the character is a non-operator character, add it to the subexpression.
                    subexpression += character;
                }
                else if (character == '(')
                {
                    // If the character is an opening parenthesis, add it to the stack.
                    if (subexpression != string.Empty)
                    {
                        postfixNotation.Add(subexpression);
                        subexpression = string.Empty;
                    }

                    operatorStack.Push(character);
                }
                else if (character == ')')
                {
                    // If the character is a closing parenthesis...
                    if (subexpression != string.Empty)
                    {
                        postfixNotation.Add(subexpression);
                        subexpression = string.Empty;
                    }

                    // Add operators to the postfix expression until an opening parenthesis is encountered.
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        postfixNotation.Add(operatorStack.Pop().ToString());
                    }

                    // Check for an invalid expression.
                    if (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        Console.WriteLine("Invalid expression!");
                        return null;
                    }
                    else
                    {
                        operatorStack.Pop();
                    }
                }
                else
                {
                    // Character is an arithmetic operator.
                    if (subexpression != string.Empty)
                    {
                        postfixNotation.Add(subexpression);
                        subexpression = string.Empty;
                    }

                    // Add arithmetic operators to the postfix expression until an operator of lower precedence is encountered.
                    while (operatorStack.Count > 0 && this.OperatorPrecedence(character) <= this.OperatorPrecedence(operatorStack.Peek()))
                    {
                        if (subexpression != string.Empty)
                        {
                            postfixNotation.Add(subexpression);
                            subexpression = string.Empty;
                        }

                        postfixNotation.Add(operatorStack.Pop().ToString());
                    }

                    operatorStack.Push(character);
                }
            }

            if (subexpression != string.Empty)
            {
                postfixNotation.Add(subexpression);
            }

            // Add the remaining operators to the postfix expression.
            while (operatorStack.Count > 0)
            {
                postfixNotation.Add(operatorStack.Pop().ToString());
            }

            string[] result = new string[postfixNotation.Count]; // Resultant array.

            // Add all values from list to array.
            for (int i = 0; i < postfixNotation.Count; i++)
            {
                result[i] = postfixNotation[i];
            }

            return result; // Return the resultant array.
        }

        /// <summary>
        /// Determines the precedence of a given operator.
        /// </summary>
        /// <param name="op">The given operator.</param>
        /// <returns>Returns 1 for high-precedence operators, 0 for low-precedence operators, and -1 otherwise.</returns>
        private int OperatorPrecedence(char op)
        {
            if (op == '+' || op == '-')
            {
                // Low-precedence operator!
                return 0;
            }
            else if (op == '*' || op == '/')
            {
                // High-precedence operator!
                return 1;
            }

            return -1;
        }

        /// <summary>
        /// Determines whether or not a given node is a variable node.
        /// </summary>
        /// <param name="node">The given node.</param>
        /// <returns>Returns true if the node is a variable node, and false otherwise.</returns>
        private bool IsVariableNode(Node node)
        {
            if (node.GetType().ToString().Equals("SpreadsheetEngine.VariableNode"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether or not a given node is an operator node.
        /// </summary>
        /// <param name="node">The given node.</param>
        /// <returns>Returns true if the node is an operator node, and false otherwise.</returns>
        private bool IsOperatorNode(Node node)
        {
            if (node.GetType().ToString().Equals("SpreadsheetEngine.OperatorNode") || node.GetType().ToString().Equals("SpreadsheetEngine.AdditionNode") || node.GetType().ToString().Equals("SpreadsheetEngine.SubtractionNode") || node.GetType().ToString().Equals("SpreadsheetEngine.MultiplicationNode") || node.GetType().ToString().Equals("SpreadsheetEngine.DivisionNode"))
            {
                return true;
            }

            return false;
        }
    }
}
