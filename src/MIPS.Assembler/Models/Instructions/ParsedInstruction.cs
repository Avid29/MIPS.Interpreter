﻿// Adam Dernis 2024

using MIPS.Models.Instructions;
using MIPS.Assembler.Parsers;
using System.Diagnostics.CodeAnalysis;

namespace MIPS.Assembler.Models.Instructions;

/// <summary>
/// An instruction as parsed by the <see cref="InstructionParser"/>.
/// </summary>
public class ParsedInstruction
{
    private readonly Instruction? _real;
    private readonly PseudoInstruction? _pseudo;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParsedInstruction"/> class.
    /// </summary>
    public ParsedInstruction(Instruction instruction, string? symbol = null)
    {
        _real = instruction;
        SymbolReferenced = symbol;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParsedInstruction"/> class.
    /// </summary>
    public ParsedInstruction(PseudoInstruction instruction, string? symbol = null)
    {
        _pseudo = instruction;
        SymbolReferenced = symbol;
    }

    // TODO: Replace with ref or rel table entry.
    /// <summary>
    /// Gets the symbol referenced, or null if none.
    /// </summary>
    public string? SymbolReferenced { get; }

    /// <summary>
    /// Gets whether or not the parsed instruction was a pseudo instruction.
    /// </summary>
    [MemberNotNullWhen(false, nameof(_real))]
    [MemberNotNullWhen(true, nameof(_pseudo))]
    public bool IsPseduoInstruction => _real is null;

    /// <summary>
    /// Gets the parsed instruction implemented exlcusively in real instructions.
    /// </summary>
    public Instruction[] Realize()
    {
        if (!IsPseduoInstruction)
            return [_real.Value];

        return _pseudo.Value.Expand();
    }
}
