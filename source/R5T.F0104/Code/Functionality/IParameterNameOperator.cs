using System;

using R5T.T0132;


namespace R5T.F0104
{
    [FunctionalityMarker]
    public partial interface IParameterNameOperator : IFunctionalityMarker
    {
        /// <summary>
        /// Removes the leading 'I' from interface type names to get the type name stem, then lowers the first character of the type name stem.
        /// </summary>
        public string GetDefaultParameterNameForTypeName_HandleInterface(string typeName)
        {
            var typeNameStem = Instances.TypeNameOperator.GetTypeNameStem_HandleInterface(typeName);

            var output = this.GetDefaultParameterNameForTypeNameStem(typeNameStem);
            return output;
        }

        /// <summary>
        /// Simply lower the first letter of the type name stem.
        /// For example, the interface "IEnumerable" would have the type name stem "Enumerable", and the default parameter name for that type name stem would be "enumerable".
        /// </summary>
        public string GetDefaultParameterNameForTypeNameStem(string typeNameStem)
        {
            if(Instances.StringOperator.IsNullOrEmpty(typeNameStem))
            {
                throw new ArgumentException($"The type name stem cannot be null or empty, found: '{typeNameStem}'.", nameof(typeNameStem));
            }

            var output = typeNameStem[0].ToString().ToLowerInvariant() + typeNameStem[1..];
            return output;
        }

        /// <summary>
        /// Quality-of-life name for <see cref="GetDefaultParameterNameForTypeName_HandleInterface(string)"/>.
        /// </summary>
        public string GetDefaultParameterNameForTypeName(string typeName)
        {
            var output = this.GetDefaultParameterNameForTypeName_HandleInterface(typeName);
            return output;
        }

        /// <summary>
        /// For an extension parameter (example: "this IEnumerable enumerable"), get the type name (in the example, "IEnumerable").
        /// </summary>
        public string GetExtensionTypeName(string extensionParameter)
        {
            var tokens = extensionParameter.Split(Instances.Characters.Space);

            if (tokens.Length != 3)
            {
                throw new Exception($"Extension parameter '{extensionParameter}' did not include:" +
                    $"\n1) 'this' extension parameter modifier," +
                    $"\n2) the parameter type, and" +
                    $"\n3) the parameter name.");
            }

            var output = tokens[1];
            return output;
        }
    }
}
