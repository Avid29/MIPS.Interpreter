﻿// Adam Dernis 2024

using MIPS.Assembler.Parsers.Expressions.Enums;
using MIPS.Assembler.Parsers.Expressions.Evaluator;
using MIPS.Models.Addressing;

namespace MIPS.Assembler.Parsers.Expressions.Abstract;

/// <summary>
/// A class for nodes in an expression tree.
/// </summary>
public abstract class ExpNode
{
    /// <summary>
    /// Gets the node's parent in the expression tree.
    /// </summary>
    public OperNode? Parent { get; set; }

    /// <summary>
    /// Gets the type of the expression node.
    /// </summary>
    public abstract ExpressionType Type { get; }

    /// <summary>
    /// Evaluate the expression tree as a type.
    /// </summary>
    /// <param name="evaluator">The <see cref="IEvaluator{T}"/> to use in evaluating the tree.</param>
    /// <param name="result">The evaluated expression tree.</param>
    /// <returns><see langword="true"/> when tree was successfully evaluated. <see langword="false"/> otherwise.</returns>
    public abstract bool TryEvaluate(IEvaluator<Address> evaluator, out Address result);
}
