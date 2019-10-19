namespace Interpreter.NET.Interpreters {
    using System;

    public sealed class UnknownVariableNameException : Exception {
        public UnknownVariableNameException(string name) : base(string.Format("A variable with name \"{0}\" couldn't be found.", name)) {}
    }
}