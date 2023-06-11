﻿// Adam Dernis 2023

using MIPS.Assembler.Parsers.Expressions.Abstract;
using MIPS.Assembler.Parsers.Expressions.Enums;

namespace MIPS.Assembler.Parsers.Expressions;

/// <summary>
/// A class for a <see cref="string"/> node on an expression tree.
/// </summary>
public class StringNode : ValueNode<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringNode"/> class.
    /// </summary>
    /// <param name="value"></param>
    public StringNode(string value) : base(value)
    {
    }

    /// <inheritdoc/>
    public override ExpressionType Type => ExpressionType.String;
}