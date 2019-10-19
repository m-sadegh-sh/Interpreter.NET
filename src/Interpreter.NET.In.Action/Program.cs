namespace Interpreter.NET.In.Action {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Interpreter.NET.Interpreters;

    internal class Program {
        private static void Main(string[] args) {
            var executionFailed = false;
            var output = new StringBuilder();

            try {
                const string code = @"int a = 0;
                                      int b = 10;

                                      if a < b
                                        write: a is greater than b.;
                                      else
                                        write: a is not greater than b.;";

                var cleanedCode = CleanUpCode(code);
                var tokenizedCode = TokenizeCode(cleanedCode);

                var variableFlags = new VariableFlags();
                var ifStatementFlags = new IfStatementFlags();

                var definedVariables = new VariableDefinitionList();

                foreach (var token in tokenizedCode) {
                    if (token == ";")
                        variableFlags.Reset();
                    if (variableFlags.NameFounded) {
                        if (!variableFlags.WasVariableAssignOperatorFound && token != "=")
                            throw new InvalidSyntaxException("If you want to assign a value into a variable, value must be followed after an equal-sign (=).");

                        if (variableFlags.WasVariableAssignOperatorFound) {
                            object variableValue = token;

                            definedVariables.Update(variableFlags.Name, variableValue);
                        } else
                            variableFlags.WasVariableAssignOperatorFound = true;
                    } else if (variableFlags.TypeFounded) {
                        variableFlags.Name = token;

                        definedVariables.Add(variableFlags.Type, variableFlags.Name);
                    } else if (token == "int")
                        variableFlags.Type = typeof (int);
                    else if (ifStatementFlags.WriteToOutput) {
                        if (token == ";")
                            ifStatementFlags.WriteToOutput = false;
                        else
                            output.Append(token + " ");
                    } else if (ifStatementFlags.ConditionIsTrue.HasValue) {
                        if (token == "else") {
                            ifStatementFlags.ElseFounded = true;
                            continue;
                        }

                        if (ifStatementFlags.ElseFounded && !ifStatementFlags.ConditionIsTrue.Value) {
                            if (token == "write:")
                                ifStatementFlags.WriteToOutput = true;
                            else
                                throw new InvalidSyntaxException(string.Format("An unrecognized token was found: {0}", token));
                        } else {
                            if (!ifStatementFlags.ElseFounded && ifStatementFlags.ConditionIsTrue.Value) {
                                if (token == "write:")
                                    ifStatementFlags.WriteToOutput = true;
                                else
                                    throw new InvalidSyntaxException(string.Format("An unrecognized token was found: {0}", token));
                            }
                        }
                    } else if (ifStatementFlags.IfFounded) {
                        if (!ifStatementFlags.LeftSideVariableFounded) {
                            if (!definedVariables.IsDeclared(token))
                                throw new UnknownVariableNameException(token);

                            ifStatementFlags.LeftSideVariable = token;
                        } else if (!ifStatementFlags.ConditionalOperatorFounded) {
                            if (!IsOperator(token))
                                throw new InvalidSyntaxException("A conditional operator needed when you want to compare two things.");

                            ifStatementFlags.ConditionalOperator = token;
                        } else if (!ifStatementFlags.RightSideVariableFounded) {
                            if (!definedVariables.IsDeclared(token))
                                throw new UnknownVariableNameException(token);

                            ifStatementFlags.RightSideVariable = token;

                            ComputeProvidedCondition(definedVariables, ifStatementFlags);
                        }
                    } else if (token == "if") {
                        ifStatementFlags.Reset();
                        ifStatementFlags.IfFounded = true;
                    } else if (token == ";")
                        continue;
                    else
                        throw new InvalidSyntaxException(string.Format("An unrecognized token was found: {0}", token));
                }
            } catch (Exception e) {
                executionFailed = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\nAn exception occurs when interpreting your code: " + e.Message);
                Console.ResetColor();
            }

            if (executionFailed) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\nExecution of code was failed.");
                Console.ResetColor();
            } else {
                Console.WriteLine(output);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nExecution of code was succeeded.");
                Console.ResetColor();
            }

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void ComputeProvidedCondition(VariableDefinitionList definedVariables, IfStatementFlags ifStatementFlags) {
            var leftSideVariableValue = definedVariables.ExtractValue<int>(ifStatementFlags.LeftSideVariable);
            var rightSideVariableValue = definedVariables.ExtractValue<int>(ifStatementFlags.RightSideVariable);

            switch (ifStatementFlags.ConditionalOperator) {
                case "<":
                    ifStatementFlags.ConditionIsTrue = leftSideVariableValue < rightSideVariableValue;
                    break;
                case "<=":
                    ifStatementFlags.ConditionIsTrue = leftSideVariableValue <= rightSideVariableValue;
                    break;
                case "==":
                    ifStatementFlags.ConditionIsTrue = leftSideVariableValue == rightSideVariableValue;
                    break;
                case "!=":
                    ifStatementFlags.ConditionIsTrue = leftSideVariableValue != rightSideVariableValue;
                    break;
                case ">":
                    ifStatementFlags.ConditionIsTrue = leftSideVariableValue > rightSideVariableValue;
                    break;
                case ">=":
                    ifStatementFlags.ConditionIsTrue = leftSideVariableValue >= rightSideVariableValue;
                    break;
            }
        }

        private static string CleanUpCode(string code) {
            var cleanedCode = code.Replace("\n", "");
            cleanedCode = cleanedCode.Replace("\r", "");
            cleanedCode = cleanedCode.Replace("\t", "");

            while (cleanedCode.Contains("  "))
                cleanedCode = cleanedCode.Replace("  ", " ");

            cleanedCode = cleanedCode.Trim();

            return cleanedCode;
        }

        private static List<string> TokenizeCode(string cleanedCode) {
            var tokenizedCode = new List<string>();

            foreach (var token in cleanedCode.Split(' ')) {
                if (token.EndsWith(";")) {
                    tokenizedCode.Add(token.Replace(";", ""));
                    tokenizedCode.Add(";");
                } else
                    tokenizedCode.Add(token);
            }

            return tokenizedCode;
        }

        private static bool IsOperator(string token) {
            return new[] {"<", "<=", "==", "!=", ">", ">="}.Any(o => o == token);
        }
    }
}