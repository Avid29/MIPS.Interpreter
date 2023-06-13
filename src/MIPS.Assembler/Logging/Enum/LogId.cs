﻿// Adam Dernis 2023

namespace MIPS.Assembler.Logging.Enum;

/// <summary>
/// An id for types of logs.
/// </summary>
public enum LogId
{
    #pragma warning disable CS1591

    // Instruction parser errors
    InvalidInstructionName,
    InvalidInstructionArgCount,
    InvalidRegisterArgument,
    InvalidAddressOffsetArgument,

    // Instruction parser warnings
    IntegerTruncated,

    // Expression parser errors
    UnparsableExpression,

    // Marker parser errors
    InvalidMarkerDataArg,
    InvalidMarkerArg,
    InvalidMarkerArgCount,

    #pragma warning restore CS1591
}
