using System;


namespace R5T.F0104
{
    public static class Instances
    {
        public static Z0000.ICharacters Characters => Z0000.Characters.Instance;
        public static F0000.IIndexOperator IndexOperator => F0000.IndexOperator.Instance;
        public static Z0029.IKeywords Keywords => Z0029.Keywords.Instance;
        public static IParameterNameOperator ParameterNameOperator => F0104.ParameterNameOperator.Instance;
        public static F0000.IStringOperator StringOperator => F0000.StringOperator.Instance;
        public static Z0000.IStrings Strings => Z0000.Strings.Instance;
        public static F0000.ITypeNameOperator TypeNameOperator => F0000.TypeNameOperator.Instance;
    }
}