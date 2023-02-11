using System;


namespace R5T.F0104
{
    public class PropertyNameOperator : IPropertyNameOperator
    {
        #region Infrastructure

        public static IPropertyNameOperator Instance { get; } = new PropertyNameOperator();


        private PropertyNameOperator()
        {
        }

        #endregion
    }
}
