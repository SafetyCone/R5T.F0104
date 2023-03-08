using System;


namespace R5T.F0104.F001
{
    public class MethodNameOperator : IMethodNameOperator
    {
        #region Infrastructure

        public static IMethodNameOperator Instance { get; } = new MethodNameOperator();


        private MethodNameOperator()
        {
        }

        #endregion
    }
}
