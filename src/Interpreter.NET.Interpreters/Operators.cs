namespace Interpreter.NET.Interpreters {
    using System.Collections.Generic;

    public class Operators : Dictionary<string, string> {
        public Operators() {
            Add("<", "LessThan");
            Add("<=", "LessThanOrEqual");
            Add("==", "Equal");
            Add("!=", "NotEqual");
            Add(">", "GreaterThan");
            Add(">=", "GreaterThanOrEqual");
        }
    }
}