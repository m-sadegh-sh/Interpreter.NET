namespace Interpreter.NET.Interpreters {
    using System.Collections.Generic;

    public class Keywords : Dictionary<string, string> {
        public Keywords() {
            Add("int", "Int32");
            Add("bool", "Boolean");
            Add("if", "If");
            Add("else", "Else");
        }
    }
}