﻿// Adam Dernis 2024

namespace MIPS.Assembler.Tokenization.Enums;

/// <summary>
/// An enum for what type of declaration occurs on a line.
/// </summary>
public enum LineType
{
    #pragma warning disable CS1591
    
    None,
    Macro,
    Instruction,
    Directive,

    #pragma warning disable CS1591
}
