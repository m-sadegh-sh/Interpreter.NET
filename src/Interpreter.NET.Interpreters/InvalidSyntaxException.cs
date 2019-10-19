namespace Interpreter.NET.Interpreters {
    using System;

    public sealed class InvalidSyntaxException : Exception {
        public InvalidSyntaxException(string message) : base(message) {}
    }
}