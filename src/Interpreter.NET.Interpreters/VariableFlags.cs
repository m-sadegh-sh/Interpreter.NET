namespace Interpreter.NET.Interpreters
{
    using System;

    public class VariableFlags
    {
       public VariableFlags() {
           Reset();
       }

       public bool TypeFounded { get { return Type != null; } }

       public Type Type { get; set; }
        
       public bool NameFounded { get { return Name != null; } }

       public string Name { get; set; }

       public bool WasVariableAssignOperatorFound { get; set; }

       public void Reset() {
           Type = null;
           Name = null;
           WasVariableAssignOperatorFound = false;
       }
    }
}
