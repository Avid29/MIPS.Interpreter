﻿// Adam Dernis 2023

using CommunityToolkit.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIPS.Assembler.Models.Directives;
using MIPS.Assembler.Parsers;

namespace MIPS.Assembler.Tests;

[TestClass]
public class DirectiveParserTests
{
    private const string Global = ".globl main"; 

    private const string Byte = ".byte 10"; 
    private const string Word = ".word 10"; 
    private const string Bytes = ".byte 10, 10";

    [TestMethod(Global)]
    public void GlobalTest() => RunGlobalTest(Global, "main");
    
    [TestMethod(Byte)]
    public void ByteTest() => RunDataTest(Byte, 10);

    [TestMethod(Word)]
    public void WordTest() => RunDataTest(Word, 0, 0, 0, 10);

    [TestMethod(Bytes)]
    public void BytesTest() => RunDataTest(Bytes, 10, 10);

    public static void RunGlobalTest(string input, string expected)
    {
        var parser = new DirectiveParser();

        TokenizeDirective(input, out var name, out var args);
        parser.TryParseDirective(name, args, out var directive);

        if (directive is not GlobalDirective)
            Assert.Fail();

        var actual = ((GlobalDirective)directive).Symbol;
        Guard.IsNotNull(actual);

        Assert.AreEqual(expected, actual);
    }

    public static void RunDataTest(string input, params byte[] expected)
    {
        var parser = new DirectiveParser();

        TokenizeDirective(input, out var name, out var args);
        parser.TryParseDirective(name, args, out var directive);

        if (directive is not DataDirective)
            Assert.Fail();

        var actual = ((DataDirective)directive).Data;
        Guard.IsNotNull(actual);

        Assert.AreEqual(expected.Length, actual.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], actual[i]);
        }
    }

    private static void TokenizeDirective(string line, out string name, out string[] args)
    {
        var nameEnd = line.IndexOf(' ');
        name = line[1..nameEnd];
        args = line[(nameEnd + 1)..].Split(',');
    }
}
