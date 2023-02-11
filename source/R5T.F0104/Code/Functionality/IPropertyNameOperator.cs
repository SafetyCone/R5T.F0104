using System;

using R5T.T0132;


namespace R5T.F0104
{
    [FunctionalityMarker]
    public partial interface IPropertyNameOperator : IFunctionalityMarker
    {
        /// <summary>
        /// The property name is just the type name, or the type name stem in the case of interface type names.
        /// </summary>
        public string GetPropertyNameForTypeName(string typeName)
        {
            var isInterfaceIndicated = Instances.TypeNameOperator.Is_InterfaceIndicatedTypeName(typeName);

            var output = isInterfaceIndicated
                ? Instances.TypeNameOperator.GetTypeNameStemForInterfaceName(typeName)
                : typeName
                ;

            return output;
        }
    }
}
