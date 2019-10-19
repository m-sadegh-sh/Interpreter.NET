namespace Interpreter.NET.Interpreters {
    using System;

    public sealed class DuplicateVariableNameException : Exception {
        public DuplicateVariableNameException(string name) : base(string.Format("A variable with name \"{0}\" is already declared.", name)) {}
    }
}