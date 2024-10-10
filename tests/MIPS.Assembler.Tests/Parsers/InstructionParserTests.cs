// Adam Dernis 2024

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIPS.Assembler.Logging;
using MIPS.Assembler.Logging.Enum;
using MIPS.Assembler.Models.Instructions;
using MIPS.Assembler.Parsers;
using MIPS.Assembler.Tokenization;
using MIPS.Assembler.Tokenization.Enums;
using MIPS.Models.Instructions;
using MIPS.Models.Instructions.Enums;
using System;

namespace MIPS.Assembler.Tests.Parsers;

[TestClass]
public class InstructionParserTests
{
    private const string NOP = "nop";
    private const string Add = "add $t0, $s0, $s1";
    private const string Addi = "addi $t0, $s0, 100";
    private const string Sll = "sll $t0, $s0, 3";
    private const string LoadWord = "lw $t0, 100($s0)";
    private const string StoreByte = "sb $t0, -100($s0)";
    private const string Jump = "j 1000";
    private const string JumpExpression = "j 10*10";

    private const string LoadImmediate = "li $t0, 0x10001";

    private const string SllWarnTruncate = "sll $t0, $s0, 33";
    private const string SllWarnSigned = "sll $t0, $s0, -1";

    private const string XkcdFail = "xkcd $t0, $s0, $s1";
    private const string TooFewArgs = "add $t0, $s0";
    private const string TooManyArgs = "add $t0, $s0, $s1, $s1";

    [TestMethod(NOP)]
    public void NopTest() => RunTest(NOP, new ParsedInstruction(Instruction.NOP));

    [TestMethod(Add)]
    public void AddTest()
    {
        Instruction expected = Instruction.Create(FunctionCode.Add, Register.Saved0, Register.Saved1, Register.Temporary0);
        RunTest(Add, new ParsedInstruction(expected));
    }

    [TestMethod(Addi)]
    public void AddiTest()
    {
        Instruction expected = Instruction.Create(OperationCode.AddImmediate, Register.Saved0, Register.Temporary0, 100);
        RunTest(Addi, new ParsedInstruction(expected));
    }

    [TestMethod(Sll)]
    public void SllTest()
    {
        Instruction expected = Instruction.Create(FunctionCode.ShiftLeftLogical, Register.Zero, Register.Saved0, Register.Temporary0, 3);
        RunTest(Sll, new ParsedInstruction(expected));
    }

    [TestMethod(LoadWord)]
    public void LoadWordTest()
    {
        Instruction expected = Instruction.Create(OperationCode.LoadWord, Register.Saved0, Register.Temporary0, 100);
        RunTest(LoadWord, new ParsedInstruction(expected));
    }

    [TestMethod(StoreByte)]
    public void StoreByteTest()
    {
        Instruction expected = Instruction.Create(OperationCode.StoreByte, Register.Saved0, Register.Temporary0, -100);
        RunTest(StoreByte, new ParsedInstruction(expected));
    }

    [TestMethod(Jump)]
    public void JumpTest()
    {
        Instruction expected = Instruction.Create(OperationCode.Jump, 1000);
        RunTest(Jump, new ParsedInstruction(expected));
    }

    [TestMethod(JumpExpression)]
    public void JumpExpressionTest()
    {
        Instruction expected = Instruction.Create(OperationCode.Jump, 100);
        RunTest(JumpExpression, new ParsedInstruction(expected));
    }
    
    [TestMethod(LoadImmediate)]
    public void LoadImmediateTest()
    {
        PseudoInstruction expected = new PseudoInstruction(PseudoOp.LoadImmediate){ RT = Register.Temporary0, Immediate = 0x10001 };
        RunTest(LoadImmediate, new ParsedInstruction(expected));
    }

    [TestMethod(SllWarnTruncate)]
    public void SllWarnTruncateTest()
    {
        Instruction expected = Instruction.Create(FunctionCode.ShiftLeftLogical, Register.Zero, Register.Saved0, Register.Temporary0, 1);
        RunTest(SllWarnTruncate, new ParsedInstruction(expected), logId:LogId.IntegerTruncated);
    }

    [TestMethod(SllWarnSigned)]
    public void SllWarnSignedTest()
    {
        Instruction expected = Instruction.Create(FunctionCode.ShiftLeftLogical, Register.Zero, Register.Saved0, Register.Temporary0, 31);
        RunTest(SllWarnSigned, new ParsedInstruction(expected), logId:LogId.IntegerTruncated);
    }

    [TestMethod(XkcdFail)]
    public void XkdFailTest()
    {
        RunTest(XkcdFail, logId: LogId.InvalidInstructionName);
    }

    [TestMethod(TooFewArgs)]
    public void TooFewArgsTest()
    {
        RunTest(TooFewArgs, logId: LogId.InvalidInstructionArgCount);
    }

    [TestMethod(TooManyArgs)]
    public void TooManyArgsTest()
    {
        RunTest(TooManyArgs, logId: LogId.InvalidInstructionArgCount);
    }

    private static void RunTest(string input, ParsedInstruction? expected = null, LogId? logId = null, string? expectedSymbol = null)
    {
        bool succeeds = expected is not null;

        var logger = new AssemblerLogger();
        var parser = new InstructionParser(null, logger);

        var tokens = Tokenizer.TokenizeLine(input, nameof(RunTest));
        var args = tokens.TrimType(TokenType.Instruction, out var name);
        var succeeded = parser.TryParse(name!, args, out var actual, out var symbol);

        Assert.AreEqual(succeeds, succeeded);
        if (succeeds)
        {
            var expectedReal = expected!.Realize();
            var actualReal = actual!.Realize();

            for (int i = 0 ; i < expectedReal.Length; i++)
            {
                Assert.AreEqual(expectedReal[i], actualReal[i]);
            }
        }

        if (logId.HasValue)
        {
            Assert.IsTrue(logger.Logs[0].Id == logId.Value);
        }
    }
}
