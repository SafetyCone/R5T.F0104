using System;


namespace R5T.F0104
{
    public class ParameterNameOperator : IParameterNameOperator
    {
        #region Infrastructure

        public static IParameterNameOperator Instance { get; } = new ParameterNameOperator();


        private ParameterNameOperator()
        {
        }

        #endregion
    }
}
