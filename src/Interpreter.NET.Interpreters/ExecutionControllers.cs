namespace Interpreter.NET.Interpreters {
    using System.Collections.Generic;

    public class ExecutionControllers : Dictionary<string, string> {
        public ExecutionControllers() {
            Add(";", "EndLine");
            Add("{", "StartBlock");
            Add("}", "EndBlock");
        }
    }
}