﻿// Adam Dernis 2023

namespace MIPS.Models.Instructions.Enums;

/// <summary>
/// An enum for r-type instruction function codes.
/// </summary>
public enum FunctionCode : byte
{
#pragma warning disable CS1591

    None = 0,

    ShiftLeftLogical = 0x00,
    ShiftRightLogical = 0x02,
    ShiftRightArithmetic = 0x03,

    ShiftLeftLogicalVariable = 0x04,
    ShiftRightLogicalVariable = 0x06,
    ShiftRightArithmeticVariable = 0x07,

    JumpRegister = 0x08,
    JumpAndLinkRegister = 0x09,

    SystemCall = 0x0c,
    Break = 0x0d,

    MoveFromHigh = 0x10,
    MoveToHigh = 0x11,
    MoveFromLow = 0x12,
    MoveToLow = 0x13,

    Multiply = 0x18,
    MultiplyUnsigned = 0x19,
    Divide = 0x1a,
    DivideUnsigned = 0x1b,

    Add = 0x20,
    AddUnsigned = 0x21,
    Subtract = 0x22,
    SubtractUnsigned = 0x23,

    And = 0x24,
    Or = 0x25,
    ExclusiveOr = 0x26,
    Nor = 0x27,

    SetLessThan = 0x2a,
    SetLessThanUnsigned = 0x2b,

#pragma warning restore CS1591
}
