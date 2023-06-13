﻿// Adam Dernis 2023

using MIPS.Assembler;

namespace Mipser;

/// <summary>
/// The class containing the program entry point.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point.
    /// </summary>
    public static async Task<int> Main(params string[] args)
    {
        if (args.Length == 0)
        {
            await RequestArgs();
            return 0;
        }

        if (args.Length != 1)
        {
            Console.WriteLine("Invalid number of arguments.");
            return -1;
        }

        await Run(args[0]);
        return 0;
    }

    private static async Task RequestArgs()
    {
        Console.WriteLine($"Current directory: {Path.GetFullPath(".")}");

        string? path = null;
        while (path is null)
        {
            Console.Write("Please enter asm file path: ");
            path = Console.ReadLine();
        }
        
        await Main(path);
    }

    private static async Task Run(string filePath)
    {
        var stream = File.Open(filePath, FileMode.Open);
        var assembler = await Assembler.AssembleAsync(stream);

        // TODO: Failure message
        Console.WriteLine($"Assembled with {assembler.Logs.Count} messages.");
        if (assembler.Logs.Count > 0)
        {
            foreach (var log in assembler.Logs)
            {
                Console.WriteLine($"{log.Severity} on line {log.LineNumber}: {log.Message}");
            }
        }
    }
}