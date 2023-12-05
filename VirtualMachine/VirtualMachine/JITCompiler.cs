namespace SVM.VirtualMachine;

#region Using directives
using System.Reflection;
#endregion

/// <summary>
/// Utility class which generates compiles a textual representation
/// of an SML instruction into an executable instruction instance
/// </summary>
internal static class JITCompiler
{
    #region Constants
    #endregion

    #region Fields
    #endregion

    #region Constructors
    #endregion

    #region Properties
    #endregion

    #region Public methods
    #endregion

    #region Non-public methods
    internal static IInstruction CompileInstruction(string opcode)
    {
        IInstruction instruction = null;

        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        opcode = opcode.ToLower();  // Convert opcode to lowercase for case-insensitive comparison

        // Iterate over all loaded assemblies
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // Iterate over all types in the assembly
            foreach (var type in assembly.GetTypes())
            {
                // Check if the type implements IInstruction and matches the opcode
                if (typeof(IInstruction).IsAssignableFrom(type) && type.Name.ToLower() == opcode)
                {
                    // Create an instance of the instruction
                    instruction = (IInstruction)Activator.CreateInstance(type);
                    return instruction;  // Return the instruction immediately after finding a match
                }
            }
        }

        if (instruction == null)
        {
            // If no matching type is found, throw an exception
            throw new SvmCompilationException($"No instruction found for opcode: {opcode}");
        }
        #endregion

        return instruction;
    }

    internal static IInstruction CompileInstruction(string opcode, params string[] operands)
    {
        IInstructionWithOperand instruction = null;

        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        opcode = opcode.ToLower();  // Convert opcode to lowercase for case-insensitive comparison

        // Iterate over all loaded assemblies
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // Iterate over all types in the assembly
            foreach (var type in assembly.GetTypes())
            {
                // Check if the type implements IInstructionWithOperand and matches the opcode
                if (typeof(IInstructionWithOperand).IsAssignableFrom(type) && type.Name.ToLower() == opcode)
                {
                    // Create an instance of the instruction
                    instruction = (IInstructionWithOperand)Activator.CreateInstance(type);

                    // Assign operands if any
                    if (operands != null && operands.Length > 0)
                    {
                        instruction.Operands = operands;
                    }

                    return instruction;  // Return the instruction immediately after finding a match
                }
            }
        }

        if (instruction == null)
        {
            // If no matching type is found, throw an exception
            throw new SvmCompilationException($"No instruction with operands found for opcode: {opcode}");
        }
        #endregion

        return instruction;
    }
    #endregion
}

