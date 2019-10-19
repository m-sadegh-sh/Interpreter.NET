namespace Interpreter.NET.Interpreters {
    public class IfStatementFlags {
        public IfStatementFlags() {
            Reset();
        }

        public bool IfFounded { get; set; }
        //public bool IfBodyExecuted { get; set; }

        public bool LeftSideVariableFounded { get { return LeftSideVariable != null; } }
        public string LeftSideVariable { get; set; }

        public bool ConditionalOperatorFounded { get { return ConditionalOperator != null; } }
        public string ConditionalOperator { get; set; }

        public bool RightSideVariableFounded { get { return RightSideVariable != null; } }
        public string RightSideVariable { get; set; }

        public bool? ConditionIsTrue { get; set; }

        public bool ElseFounded { get; set; }
        //public bool ElseBodyExecuted { get; set; }

        public bool WriteToOutput { get; set; }

        public void Reset() {
            IfFounded = false;
            LeftSideVariable = null;
            ConditionalOperator = null;
            RightSideVariable = null;
            ConditionIsTrue = null;
            ElseFounded = false;
            WriteToOutput = false;
        }
    }
}