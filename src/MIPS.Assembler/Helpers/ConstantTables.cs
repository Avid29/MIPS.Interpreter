﻿// Adam Dernis 2024

using MIPS.Assembler.Models.Instructions;
using MIPS.Models.Instructions.Enums;
using System;
using System.Collections.Generic;

namespace MIPS.Assembler.Helpers;

/// <summary>
/// A class containing constant tables for parsing lookup.
/// </summary>
public static class ConstantTables
{
    // R type patterns
    private static readonly Argument[] NoArgPattern = [];                                                                  // <instr>
    private static readonly Argument[] StandardRPattern = [Argument.RD, Argument.RS, Argument.RT];                        // <instr>  $rd, $rs, $rt
    private static readonly Argument[] MultiplyRPattern = [Argument.RS, Argument.RT];                                     // <instr>  $rs, $rt
    private static readonly Argument[] ShiftPattern = [Argument.RD, Argument.RT, Argument.Shift];                         // <instr>  $rd, $rt, sa
    private static readonly Argument[] VariableShiftPattern = [Argument.RD, Argument.RT, Argument.RS];                    // <instr>  $rd, $rt, $rs
    private static readonly Argument[] JumpRegisterPattern = [Argument.RS];                                               // <instr>  $rs
    private static readonly Argument[] MoveFromPattern = [Argument.RD];                                                   // <instr>  $rd
    private static readonly Argument[] MoveToPattern = [Argument.RS];                                                     // <instr>  $rs

    // I type patterns
    private static readonly Argument[] StandardIPattern = [Argument.RT, Argument.RS, Argument.Immediate];                 // <instr>  $rt, $rs, imm
    private static readonly Argument[] BranchComparePattern = [Argument.RS, Argument.RT, Argument.Offset];                // <instr>  $rs, $rt, offset
    private static readonly Argument[] BranchPattern = [Argument.RS, Argument.Offset];                                    // <instr>  $rs, offset
    private static readonly Argument[] LoadImmediatePattern = [Argument.RT, Argument.Immediate];                          // <instr>  $rt, imm
    private static readonly Argument[] MemoryPattern = [Argument.RT, Argument.AddressOffset];                             // <instr>  $rt, offset($rs)

    // J type patterns
    private static readonly Argument[] JumpPattern = [Argument.Address];                                                  // <instr>  addr

    private static readonly Dictionary<string, InstructionMetadata> _instructionTable = new()
    {
        { "nop", new InstructionMetadata("nop", FunctionCode.None, NoArgPattern) },                                         // nop
        { "sll", new InstructionMetadata("sll", FunctionCode.ShiftLeftLogical, ShiftPattern) },                             // sll      $rd, $rt, sa
        { "srl", new InstructionMetadata("srl", FunctionCode.ShiftRightLogical, ShiftPattern) },                            // ssl      $rd, $rt, sa
        { "sra", new InstructionMetadata("sra", FunctionCode.ShiftRightArithmetic, ShiftPattern) },                         // sra      $rd, $rt, sa

        { "sllv", new InstructionMetadata("sllv", FunctionCode.ShiftLeftLogicalVariable, VariableShiftPattern) },           // sllv     $rd, $rt, $rs
        { "srlv", new InstructionMetadata("srlv", FunctionCode.ShiftRightLogicalVariable, VariableShiftPattern) },          // srlv     $rd, $rt, $rs
        { "srav", new InstructionMetadata("srav", FunctionCode.ShiftRightArithmeticVariable, VariableShiftPattern) },       // srav     $rd, $rt, $rs

        { "jr", new InstructionMetadata("jr", FunctionCode.JumpRegister, JumpRegisterPattern) },                            // jr       $rs
        { "jalr", new InstructionMetadata("jalr", FunctionCode.JumpAndLinkRegister, JumpRegisterPattern) },                 // jalr     $rs

        { "syscall", new InstructionMetadata("syscall", FunctionCode.SystemCall, Array.Empty<Argument>()) },                // syscall

        { "mfhi", new InstructionMetadata("mfhi", FunctionCode.MoveFromHigh, MoveFromPattern) },                            // mfhi     $rd
        { "mthi", new InstructionMetadata("mthi", FunctionCode.MoveToHigh, MoveToPattern) },                                // mthi     $rs
        { "mflo", new InstructionMetadata("mflo", FunctionCode.MoveFromLow, MoveFromPattern) },                             // mflo     $rd
        { "mtlo", new InstructionMetadata("mtlo", FunctionCode.MoveToLow, MoveToPattern) },                                 // mtlo     $rs

        { "mult", new InstructionMetadata("mult", FunctionCode.Multiply, MultiplyRPattern) },                               // mult     $rs, $rt
        { "multu", new InstructionMetadata("multu", FunctionCode.MultiplyUnsigned, MultiplyRPattern) },                     // multu    $rs, $rt
        { "div", new InstructionMetadata("div", FunctionCode.Divide, MultiplyRPattern) },                                   // div      $rs, $rt
        { "divu", new InstructionMetadata("divu", FunctionCode.DivideUnsigned, MultiplyRPattern) },                         // divu     $rs, $rt

        { "add", new InstructionMetadata("add", FunctionCode.Add, StandardRPattern) },                                      // add      $rd, $rs, $rt
        { "addu", new InstructionMetadata("addu", FunctionCode.AddUnsigned, StandardRPattern) },                            // addu     $rd, $rs, $rt
        { "sub", new InstructionMetadata("sub", FunctionCode.Subtract, StandardRPattern) },                                 // sub      $rd, $rs, $rt
        { "subu", new InstructionMetadata("subu", FunctionCode.SubtractUnsigned, StandardRPattern) },                       // subu     $rd, $rs, $rt

        { "and", new InstructionMetadata("and", FunctionCode.And, StandardRPattern) },                                      // and      $rd, $rs, $rt
        { "or", new InstructionMetadata("or", FunctionCode.Or, StandardRPattern) },                                         // or       $rd, $rs, $rt
        { "xor", new InstructionMetadata("xor", FunctionCode.ExclusiveOr, StandardRPattern) },                              // xor      $rd, $rs, $rt
        { "nor", new InstructionMetadata("nor", FunctionCode.Nor, StandardRPattern) },                                      // nor      $rd, $rs, $rt

        { "slt", new InstructionMetadata("slt", FunctionCode.SetLessThan, StandardRPattern) },                              // slt      $rd, $rs, $rt
        { "sltu", new InstructionMetadata("sltu", FunctionCode.SetLessThanUnsigned, StandardRPattern) },                    // sltu     $rd, $rs, $rt
        
        { "bltz", new InstructionMetadata("bltz", BranchCode.BranchOnLessThanZero, BranchPattern)},                         // bltz     $rs, offset
        { "bgez", new InstructionMetadata("bgez", BranchCode.BranchOnGreaterOrEqualToThanZero, BranchPattern)},             // bgez     $rs, offset
        { "bltzal", new InstructionMetadata("bltzal", BranchCode.BranchOnLessThanZeroAndLink, BranchPattern)},              // bltzal   $rs, offset
        { "bgezal", new InstructionMetadata("bgezal", BranchCode.BranchOnGreaterThanOrEqualToZeroAndLink, BranchPattern)},  // bgezal   $rs, offset

        { "j", new InstructionMetadata("j", OperationCode.Jump, JumpPattern) },                                             // j        addr
        { "jal", new InstructionMetadata("jal", OperationCode.JumpAndLink, JumpPattern) },                                  // jal      addr

        { "beq", new InstructionMetadata("beq", OperationCode.BranchOnEquals, BranchComparePattern) },                      // beq      $rs, $rt, offset
        { "bne", new InstructionMetadata("bne", OperationCode.BranchOnNotEquals, BranchComparePattern) },                   // bne      $rs, $rt, offset
        { "blez", new InstructionMetadata("blez", OperationCode.BranchOnLessThanOrEqualToZero, BranchPattern) },            // blez     $rs, offset
        { "bgtz", new InstructionMetadata("bgtz", OperationCode.BranchGreaterThanZero, BranchPattern) },                    // bgtz     $rs, offset

        { "addi", new InstructionMetadata("addi", OperationCode.AddImmediate, StandardIPattern) },                          // addi     $rt, $rs, imm
        { "addiu", new InstructionMetadata("addiu", OperationCode.AddImmediateUnsigned, StandardIPattern) },                // addiu    $rt, $rs, imm

        { "slti", new InstructionMetadata("slti", OperationCode.SetLessThanImmediate, StandardIPattern) },                  // slti     $rt, $rs, imm
        { "sltiu", new InstructionMetadata("sltiu", OperationCode.SetLessThanImmediateUnsigned, StandardIPattern) },        // sltiu    $rt, $rs, imm

        { "andi", new InstructionMetadata("andi", OperationCode.AndImmediate, StandardIPattern) },                          // andi     $rt, $rs, imm
        { "ori", new InstructionMetadata("ori", OperationCode.OrImmediate, StandardIPattern) },                             // ori      $rt, $rs, imm
        { "xori", new InstructionMetadata("xori", OperationCode.ExclusiveOrImmediate, StandardIPattern) },                  // xori     $rt, $rs, imm

        { "lui", new InstructionMetadata("lui", OperationCode.LoadUpperImmediate, LoadImmediatePattern) },                  // lui      $rt, imm

        // TODO: Coprocessors
        //{ "cop0", },
        //{ "cop1", },
        //{ "cop2", },
        //{ "cop3", },
        
        { "lb", new InstructionMetadata("lb", OperationCode.LoadByte, MemoryPattern) },                                     // lb       $rt, offset($rs)
        { "lh", new InstructionMetadata("lh", OperationCode.LoadHalfWord, MemoryPattern) },                                 // lh       $rt, offset($rs)
        { "lwl", new InstructionMetadata("lwl", OperationCode.LoadWordLeft, MemoryPattern) },                               // lwl      $rt, offset($rs)
        { "lw", new InstructionMetadata("lw", OperationCode.LoadWord, MemoryPattern) },                                     // lw       $rt, offset($rs)
        { "lbu", new InstructionMetadata("lbu", OperationCode.LoadByteUnsigned, MemoryPattern) },                           // lbu      $rt, offset($rs)
        { "lhu", new InstructionMetadata("lhu", OperationCode.LoadHalfWordUnsigned, MemoryPattern) },                       // lhu      $rt, offset($rs)
        { "lwr", new InstructionMetadata("lwr", OperationCode.LoadWordRight, MemoryPattern) },                              // lwr      $rt, offset($rs)
                                                                                                                       
        { "sb", new InstructionMetadata("sb", OperationCode.StoreByte, MemoryPattern) },                                    // sb       $rt, offset($rs)
        { "sh", new InstructionMetadata("sh", OperationCode.StoreHalfWord, MemoryPattern) },                                // sh       $rt, offset($rs)
        { "swl", new InstructionMetadata("swl", OperationCode.StoreWordLeft, MemoryPattern) },                              // swl      $rt, offset($rs)
        { "sw", new InstructionMetadata("sw", OperationCode.StoreWord, MemoryPattern) },                                    // sw       $rt, offset($rs)
        { "swr", new InstructionMetadata("swr", OperationCode.StoreWordRight, MemoryPattern) },                             // swr      $rt, offset($rs)

        // TODO: Coprocessors
        //{ "lwc0", },
        //{ "lwc1", },
        //{ "lwc2", },
        //{ "lwc3", },
        //{ "swc0", },
        //{ "swc1", },
        //{ "swc2", },
        //{ "swc3", },

        // Pseudoinstructions
        { "blt", new InstructionMetadata("blt", PseudoOp.BranchOnLessThan, [Argument.RS, Argument.RT, Argument.Offset], 2) },   // blt      $rs, $rt, offset
                                                                                                                                //    slt   $at, $rs, $rt     
                                                                                                                                //    bne   $at, $zero, offset

        { "li", new InstructionMetadata("li",  PseudoOp.LoadImmediate, [Argument.RT, Argument.FullImmediate], 2) },             // li       $rt, immediate
                                                                                                                                //    lui   $at, upper
                                                                                                                                //    ori   $rt, $at, lower

        { "abs", new InstructionMetadata("abs",  PseudoOp.AbsoluteValue, [Argument.RT, Argument.RS], 3) },                      // abs      $rt, $rs
                                                                                                                                //    addu  $rt, $rs, $zero
                                                                                                                                //    bgez  $rs, 8
                                                                                                                                //    sub   $rt, $zero, $rs

        { "move", new InstructionMetadata("move", PseudoOp.Move, [Argument.RT, Argument.RS], 1) },                              // move     $rt, $rs
                                                                                                                                //    add     $rt, $rs, $zero

        { "la", new InstructionMetadata("la", PseudoOp.LoadAddress, [Argument.RT, Argument.Address], 2) },                      // la       $rt, address
                                                                                                                                //    lui   $at, upper
                                                                                                                                //    ori   $rt, $at, lower

        { "sge", new InstructionMetadata("sge", PseudoOp.SetGreaterThanOrEqual, [Argument.RD, Argument.RS, Argument.RT], 2) },  // sge      $rd, $rs, $rt
                                                                                                                                //    addiu $rt, $rt, -1
                                                                                                                                //    slt   $rd, $rt, $rs
    };

    private static readonly Dictionary<string, Register> _registerTable = new()
    {
        { "zero", Register.Zero },

        { "at", Register.AssemblerTemporary },

        { "v0", Register.ReturnValue0 },
        { "v1", Register.ReturnValue1 },

        { "a0", Register.Argument0 },
        { "a1", Register.Argument1 },
        { "a2", Register.Argument2 },
        { "a3", Register.Argument3 },

        { "t0", Register.Temporary0 },
        { "t1", Register.Temporary1 },
        { "t2", Register.Temporary2 },
        { "t3", Register.Temporary3 },
        { "t4", Register.Temporary4 },
        { "t5", Register.Temporary5 },
        { "t6", Register.Temporary6 },
        { "t7", Register.Temporary7 },

        { "s0", Register.Saved0 },
        { "s1", Register.Saved1 },
        { "s2", Register.Saved2 },
        { "s3", Register.Saved3 },
        { "s4", Register.Saved4 },
        { "s5", Register.Saved5 },
        { "s6", Register.Saved6 },
        { "s7", Register.Saved7 },

        { "t8", Register.Temporary8 },
        { "t9", Register.Temporary9 },

        { "k0", Register.Kernel0 },
        { "k1", Register.Kernel1 },

        { "gp", Register.GlobalPointer },
        { "sp", Register.StackPointer },
        { "fp", Register.FramePointer },

        { "ra", Register.ReturnAddress },
    };

    /// <summary>
    /// Attempts to get an instruction by name.
    /// </summary>
    /// <param name="name">The name of the instruction.</param>
    /// <param name="metadata">The instruction metadata.</param>
    /// <returns>Whether or not an instruction exists by that name</returns>
    public static bool TryGetInstruction(string name, out InstructionMetadata metadata)
        => _instructionTable.TryGetValue(name, out metadata);

    /// <summary>
    /// Attempts to get an register by name.
    /// </summary>
    /// <param name="name">The name of the register.</param>
    /// <param name="register">The register enum value.</param>
    /// <returns>Whether or not an register exists by that name</returns>
    public static bool TryGetRegister(string name, out Register register)
        => _registerTable.TryGetValue(name, out register);
}
