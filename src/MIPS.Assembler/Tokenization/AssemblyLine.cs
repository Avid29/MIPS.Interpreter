﻿// Adam Dernis 2024

using MIPS.Assembler.Tokenization.Enums;
using System;

namespace MIPS.Assembler.Tokenization;

/// <summary>
/// A line of tokenized line assembly of assembly.
/// </summary>
public class AssemblyLine
{
    private readonly Token[] _tokens;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyLine"/> struct.
    /// </summary>
    /// <param name="tokens"></param>
    public AssemblyLine(Token[] tokens)
    {
        _tokens = tokens;
        SubTokenize();
    }

    /// <summary>
    /// Gets a token from the assembly line.
    /// </summary>
    /// <param name="index">The index of the token to retrieve.</param>
    /// <returns>The token at <paramref name="index"/> in the line.</returns>
    public Token this[int index] => _tokens[index];

    /// <summary>
    /// Gets the number of tokens in the line.
    /// </summary>
    public int Count => _tokens.Length;

    /// <summary>
    /// Gets what type of declaration occurs on the line, 
    /// </summary>
    public LineType Type {get; private set; }
    
    /// <summary>
    /// Gets the tokens on the line of assembly.
    /// </summary>
    public ReadOnlySpan<Token> Tokens => _tokens;

    /// <summary>
    /// Gets the label declared on the line, if any.
    /// </summary>
    public Token? Label { get; private set; }

    /// <summary>
    /// Gets the instruction token on the line, if any.
    /// </summary>
    public Token? Instruction { get; private set; }

    /// <summary>
    /// Gets the directive token on the line, if any.
    /// </summary>
    public Token? Directive { get; private set; }

    /// <summary>
    /// Gets the macro declared on the line, if any.
    /// </summary>
    public Token? Macro { get; private set; }

    /// <summary>
    /// Gets the args declared on the line.
    /// </summary>
    public AssemblyLineArgs Args { get; private set; }

    private void SubTokenize()
    {
        Type = LineType.None;

        // If line is empty, do nothing
        if (_tokens.Length is 0)
            return;
        
        // Convert the line to a segment
        ArraySegment<Token> segment = _tokens;

        // Grab the label
        if (segment[0].Type is TokenType.LabelDeclaration)
        {
            Label = segment[0];
            segment = segment[1..];
        }

        // The line only contains a label
        if (segment.Count == 0)
            return;

        // Handle line type
        var next = segment[0];
        switch(next.Type)
        {   
            case TokenType.MacroDefinition:
                Macro = next;
                Type = LineType.Macro;
                return;
            case TokenType.Instruction:
                Instruction = next;
                Type = LineType.Instruction;
                break;
            case TokenType.Directive:
                Directive = next;
                Type = LineType.Directive;
                break;
        }
        
        // NOTE: The assignment token is left as part of the arg.
        // The assembler will need to verify it is present, and log if it is not.
        // However, unless proceeded by an assignment token this never should have
        // been tokenized as a macro.
        Args = new AssemblyLineArgs(segment[1..]);
    }
}
