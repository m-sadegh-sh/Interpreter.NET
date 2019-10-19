namespace Interpreter.NET.Interpreters {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class VariableDefinitionList : List<VariableDefinition> {
        public bool IsDeclared(string name) {
            return this.Any(vd => vd.Name == name);
        }

        public void Add(Type type, string name) {
            if (IsDeclared(name))
                throw new DuplicateVariableNameException(name);

            Add(new VariableDefinition {
                Type = type,
                Name = name
            });
        }

        public void Update(string name, object value) {
            if (!IsDeclared(name))
                throw new UnknownVariableNameException(name);

            var definition = this.First(vd => vd.Name == name);

            definition.Value = value;
        }

        public T ExtractValue<T>(string name) {
            if (!IsDeclared(name))
                throw new UnknownVariableNameException(name);

            var definition = this.First(vd => vd.Name == name);

            var converter = TypeDescriptor.GetConverter(definition.Type);

            if (converter == null)
                throw new NotSupportedException(string.Format("No type converter exists for type \"{0}\".", definition.Type.FullName));

            var convertedValue = converter.ConvertFrom(definition.Value);

            return (T)convertedValue;
        }
    }
}