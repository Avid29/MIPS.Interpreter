﻿// Adam Dernis 2024

using MIPS.Helpers.Instructions;
using MIPS.Models.Instructions.Enums;

namespace MIPS.Assembler.Models.Instructions;

/// <summary>
/// A struct containing metadata on an instruction.
/// </summary>
public readonly struct InstructionMetadata
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstructionMetadata"/> struct.
    /// </summary>
    public InstructionMetadata(string name, OperationCode opCode, Argument[] argumentPattern, Version version = Version.All)
    {
        Name = name;
        OpCode = opCode;
        FuncCode = FunctionCode.None;
        BranchCode = RegImmCode.None;
        ArgumentPattern = argumentPattern;
        RealizedInstructionCount = 1;
        MIPSVersion = version;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InstructionMetadata"/> struct.
    /// </summary>
    public InstructionMetadata(string name, FunctionCode funcCode, Argument[] argumentPattern, Version version = Version.All)
    {
        Name = name;
        OpCode = OperationCode.RType;
        FuncCode = funcCode;
        BranchCode = RegImmCode.None;
        ArgumentPattern = argumentPattern;
        RealizedInstructionCount = 1;
        MIPSVersion = version;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InstructionMetadata"/> struct.
    /// </summary>
    public InstructionMetadata(string name, RegImmCode branchCode, Argument[] argumentPattern, Version version = Version.All)
    {
        Name = name;
        OpCode = OperationCode.RegisterImmediate;
        FuncCode = FunctionCode.None;
        BranchCode = branchCode;
        ArgumentPattern = argumentPattern;
        RealizedInstructionCount = 1;
        MIPSVersion = version;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InstructionMetadata"/> struct.
    /// </summary>
    /// <remarks>
    /// This is constructor is only for pseudo-instructions.
    /// </remarks>
    public InstructionMetadata(string name, PseudoOp pseudoOp, Argument[] argumentPattern, int realizedCount, Version version = Version.All)
    {
        Name = name;
        PseudoOp = pseudoOp;
        OpCode = OperationCode.PseudoInstruction;
        FuncCode = FunctionCode.None;
        BranchCode = RegImmCode.None;
        ArgumentPattern = argumentPattern;
        RealizedInstructionCount = realizedCount;
        MIPSVersion = version;
    }

    /// <summary>
    /// Gets the name of the instruction.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the instruction operation code.
    /// </summary>
    public OperationCode OpCode { get; }

    /// <summary>
    /// Gets the instruction function code.
    /// </summary>
    public FunctionCode FuncCode { get; }

    /// <summary>
    /// Gets the instruction branch code.
    /// </summary>
    public RegImmCode BranchCode { get; }

    /// <summary>
    /// Gets the pseudo op for a pseudo-instruction.
    /// </summary>
    public PseudoOp PseudoOp { get; }

    /// <summary>
    /// Gets the function type.
    /// </summary>
    public readonly InstructionType Type => InstructionTypeHelper.GetInstructionType(OpCode);

    /// <summary>
    /// Gets the instruction parse type
    /// </summary>
    public Argument[] ArgumentPattern { get; }

    /// <summary>
    /// Gets the number of real instructions required to execute the instruction.
    /// </summary>
    /// <remarks>
    /// This exists for pseudo instructions.
    /// </remarks>
    public int RealizedInstructionCount { get; }

    /// <summary>
    /// Gets whether or not the instruction is a pseudo instruction.
    /// </summary>
    public bool IsPseudoInstruction => OpCode is OperationCode.PseudoInstruction;

    /// <summary>
    /// Gets the version of MIPS required for the instruction.
    /// </summary>
    public Version MIPSVersion { get; }
}
