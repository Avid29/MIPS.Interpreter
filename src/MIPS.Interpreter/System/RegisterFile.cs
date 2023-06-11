﻿// Adam Dernis 2023

using MIPS.Models.Instructions.Enums;

namespace MIPS.Emulator.System;

/// <summary>
/// A class representing a register file.
/// </summary>
public class RegisterFile
{
    private readonly uint[] _registers;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterFile"/> class.
    /// </summary>
    public RegisterFile()
    {
        _registers = new uint[32 + 2];
    }

    /// <summary>
    /// Gets or sets the value in a register.
    /// </summary>
    public uint this[Register register]
    {
        get => _registers[(int)register];
        set
        {
            // Cannot set zero register. Do nothing.
            if (register is Register.Zero)
                return;

            int index = (int)register;

            // Register is out of the indexable bounds. Do nothing.
            if (index >= _registers.Length)
                return;

            _registers[index] = value;
        }
    }

    /// <summary>
    /// Gets or sets the value in the high register.
    /// </summary>
    public uint High
    {
        get => _registers[(int)Register.High];
        set => _registers[(int)Register.High] = value;
    }
    
    /// <summary>
    /// Gets or sets the value in the low register.
    /// </summary>
    public uint Low
    {
        get => _registers[(int)Register.Low];
        set => _registers[(int)Register.Low] = value;
    }
}
