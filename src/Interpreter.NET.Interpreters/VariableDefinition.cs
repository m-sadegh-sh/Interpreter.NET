namespace Interpreter.NET.Interpreters {
    using System;

    public class VariableDefinition {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}