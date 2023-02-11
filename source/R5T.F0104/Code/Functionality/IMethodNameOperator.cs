using System;

using R5T.F0000;
using R5T.T0132;


namespace R5T.F0104
{
    [FunctionalityMarker]
    public partial interface IMethodNameOperator : IFunctionalityMarker
    {
        public string GetTypedMethodName(
            string typeName,
            string methodName)
        {
            var output = $"{typeName}{Instances.Strings.Period}{methodName}";
            return output;
        }

        /// <summary>
        /// A full method name (or assemblied, namespaced, typed, parameterized, method name) is:
        /// R5T.T0054 - System.IEntryOperatorExtensions.HasEntryByIdentity(entries: IEnumerable&lt;Entry&gt;, entryIdentity: string)
        /// A typed method name is: IEntryOperatorExtensions.HasEntryByIdentity(), which includes the terminating empty paired parentheses to indicate the name is a method name.
        /// </summary>
        public string GetTypedMethodNameFromFullMethodName(
            string fullMethodName)
        {
            var typeName = this.GetTypeNameFromFullMethodName(fullMethodName);
            var methodName = this.GetMethodNameFromFullMethodName(fullMethodName);

            var output = this.GetTypedMethodName(
                typeName,
                methodName);

            return output;
        }

        /// <summary>
        /// A full method name (or assemblied, namespaced, typed, parameterized, method name) is:
        /// R5T.T0054 - System.IEntryOperatorExtensions.HasEntryByIdentity(entries: IEnumerable&lt;Entry&gt;, entryIdentity: string)
        /// A method name is: HasEntryByIdentity(), which includes the terminating empty paired parentheses to indicate the name is a method name.
        /// </summary>
        public string GetMethodNameFromFullMethodName(
            string fullMethodName)
        {
            // The method name is between the last period and open-parenthesis.
            var indexOfFirstOpenParenthesis = this.GetIndexOfFirstOpenParenthesis(fullMethodName);
            if (!Instances.StringOperator.IsFound(indexOfFirstOpenParenthesis))
            {
                throw new Exception($"Open parenthesis ('(') not found in full method name:\n{fullMethodName}");
            }

            var indexOfFirstOpenAngleBrace = this.GetIndexOfFirstOpenAngleBraceOrNotFound(fullMethodName);

            var indexOfMethodNameStop = Instances.IndexOperator.IsFound(indexOfFirstOpenAngleBrace) && indexOfFirstOpenAngleBrace < indexOfFirstOpenParenthesis
                ? indexOfFirstOpenAngleBrace
                : indexOfFirstOpenParenthesis
                ;

            var indexOfLastPeriod = fullMethodName.LastIndexOf(Instances.Characters.Period, indexOfFirstOpenParenthesis);
            if (!Instances.StringOperator.IsFound(indexOfLastPeriod))
            {
                throw new Exception($"Period ('.') not found in full method name:\n{fullMethodName}");
            }

            var methodLength = indexOfMethodNameStop - 1 - indexOfLastPeriod;

            var methodNameWithoutParentheses = fullMethodName.Substring(indexOfLastPeriod + 1, methodLength);

            var output = $"{methodNameWithoutParentheses}{Instances.Strings.PairedParentheses}";
            return output;
        }

        public string GetMethodNameFromFullMethodNameWithoutParentheses(
            string fullMethodName)
        {
            var output = this.GetMethodNameFromFullMethodName(fullMethodName)
                .TrimEnd(
                    Instances.Characters.OpenParenthesis,
                    Instances.Characters.CloseParenthesis)
                ;

            return output;
        }

        public int GetIndexOfFirstOpenParenthesis(
            string parameterizedMethodName)
        {
            var output = parameterizedMethodName.IndexOf(Instances.Characters.OpenParenthesis);
            if (!Instances.StringOperator.IsFound(output))
            {
                throw new Exception($"Open parenthesis not found in method name:\n{parameterizedMethodName}");
            }

            return output;
        }

        public int GetIndexOfFirstOpenAngleBraceOrNotFound(
            string parameterizedMethodName)
        {
            var output = parameterizedMethodName.IndexOf(Instances.Characters.OpenAngleBracket);
            return output;
        }

        public int GetIndexOfLastOpenParenthesis(
            string parameterizedMethodName)
        {
            var output = parameterizedMethodName.LastIndexOf(Instances.Characters.OpenParenthesis);
            if (!Instances.StringOperator.IsFound(output))
            {
                throw new Exception($"Open parenthesis not found in method name:\n{parameterizedMethodName}");
            }

            return output;
        }

        public int GetIndexOfNextCloseParenthesis(
            string parameterizedMethodName,
            int startIndex)
        {
            var output = parameterizedMethodName.IndexOf(Instances.Characters.CloseParenthesis, startIndex);
            if (!Instances.StringOperator.IsFound(output))
            {
                throw new Exception($"Close parenthesis not found in method name:\n{parameterizedMethodName}");
            }

            return output;
        }

        public int GetIndexOfLastCloseParenthesis(
            string parameterizedMethodName)
        {
            var output = parameterizedMethodName.LastIndexOf(Instances.Characters.CloseParenthesis);
            if (!Instances.StringOperator.IsFound(output))
            {
                throw new Exception($"Close parenthesis not found in method name:\n{parameterizedMethodName}");
            }

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetParameterListParenthesied(string)"/> as the default.
        /// </summary>
        public string GetParameterList(
            string parameterizedMethodName)
        {
            var output = this.GetParameterListParenthesied(parameterizedMethodName);
            return output;
        }

        public string GetParameterListParenthesied(
            string parameterizedMethodName)
        {
            // The parameter list (parenthsied) extends from the last open parenthesis of the parameterized method name to the end.
            var indexOfFirstOpenParenthesis = this.GetIndexOfFirstOpenParenthesis(parameterizedMethodName);

            var output = parameterizedMethodName[indexOfFirstOpenParenthesis..];
            return output;
        }

        public string GetParameterListUnParenthesied(
            string fullMethodName)
        {
            var output = this.GetParameterListParenthesied(fullMethodName)
                .TrimStart(Instances.Characters.OpenParenthesis)
                .TrimEnd(Instances.Characters.CloseParenthesis)
                ;

            return output;
        }

        public string GetParameterizedMethodName(
            string fullMethodName)
        {
            var methodName = this.GetMethodNameFromFullMethodNameWithoutParentheses(fullMethodName);
            var parameterList = this.GetParameterList(fullMethodName);

            var output = $"{methodName}{parameterList}";
            return output;
        }

        /// <summary>
        /// A full method name (or assemblied, namespaced, typed, parameterized, method name) is:
        /// R5T.T0054 - System.IEntryOperatorExtensions.HasEntryByIdentity(entries: IEnumerable&lt;Entry&gt;, entryIdentity: string)
        /// The type name is: IEntryOperatorExtensions.
        /// </summary>
        public string GetTypeNameFromFullMethodName(
            string fullMethodName)
        {
            // The type name is between the second-to-last and last period.
            var indexOfLastPeriod = fullMethodName.LastIndexOf(Instances.Characters.Period);
            if (!Instances.StringOperator.IsFound(indexOfLastPeriod))
            {
                throw new Exception($"Period not found in full method name:\n{fullMethodName}");
            }

            var lengthOfSubstringBeforeLastPeriod = indexOfLastPeriod; // Length is index plus one.

            var subStringBeforeLastPeriod = fullMethodName[..lengthOfSubstringBeforeLastPeriod];

            var indexOfSecondToLastPeriod = subStringBeforeLastPeriod.LastIndexOf(Instances.Characters.Period);
            if (!Instances.StringOperator.IsFound(indexOfSecondToLastPeriod))
            {
                throw new Exception($"Second to last period not found in full method name:\n{fullMethodName}");
            }

            var lengthOfTypeName = indexOfLastPeriod - 1 - indexOfSecondToLastPeriod;

            var typeName = fullMethodName.Substring(indexOfSecondToLastPeriod + 1, lengthOfTypeName);
            return typeName;
        }

        public string GetNamespacedTypedMethodName(
            string namespacedTypeName,
            string methodName)
        {
            var output = $"{namespacedTypeName}{Instances.Strings.Period}{methodName}";
            return output;
        }

        public string GetNamespacedTypedParameterizedMethodName(
            string namespacedTypeName,
            string parameterizedMethodName)
        {
            var output = $"{namespacedTypeName}{Instances.Strings.Period}{parameterizedMethodName}";
            return output;
        }

        public string GetNamespacedTypedParameterizedMethodNameFromFullMethodName(
            string namespacedTypeName,
            string fullMethodName)
        {
            var parameterizedMethodName = this.GetParameterizedMethodName(fullMethodName);

            var output = this.GetNamespacedTypedParameterizedMethodName(
                namespacedTypeName,
                parameterizedMethodName);

            return output;
        }

        public WasFound<string> HasFirstParameter(
            string parameterizedMethodName)
        {
            var indexOfOpenParenthesis = this.GetIndexOfFirstOpenParenthesis(parameterizedMethodName);
            var indexOfNextCloseParenthesis = this.GetIndexOfNextCloseParenthesis(parameterizedMethodName, indexOfOpenParenthesis);

            // Is there even a single parameter?
            if (indexOfNextCloseParenthesis == indexOfOpenParenthesis + 1)
            {
                return WasFound.NotFound<string>();
            }

            // Get the first parameter.
            var indexOfFirstNextCommaOrNotFound = parameterizedMethodName.IndexOf(Instances.Characters.Comma, indexOfOpenParenthesis);
            var indexOfFirstNextCommaOrNextCloseParenthesis = Instances.StringOperator.IsFound(indexOfFirstNextCommaOrNotFound)
                ? indexOfFirstNextCommaOrNotFound
                : indexOfNextCloseParenthesis
                ;

            var firstParameter = parameterizedMethodName.Substring(indexOfOpenParenthesis + 1, indexOfFirstNextCommaOrNextCloseParenthesis - indexOfOpenParenthesis);

            var output = WasFound.From(firstParameter);
            return output;
        }

        public WasFound<string> HasExtensionParameter(
            string parameterizedMethodName)
        {
            // Does the method even have a single parameter?
            var hasFirstParameter = this.HasFirstParameter(parameterizedMethodName);
            if (!hasFirstParameter)
            {
                return hasFirstParameter;
            }

            // Ok, at least the method has a parameter.
            // Is the first parameter modified with the extension parameter signifier, 'this'?
            var tokens = hasFirstParameter.Result.Split(Instances.Characters.Space);

            var firstToken = tokens[0];

            var firstTokenIsExtensionParameterSignifier = firstToken == Instances.Keywords.This;

            var output = firstTokenIsExtensionParameterSignifier
                ? hasFirstParameter
                : WasFound.NotFound<string>()
                ;

            return output;
        }

        public string GetExtensionParameter(
            string parameterizedMethodName)
        {
            var hasExtensionParameter = this.HasExtensionParameter(parameterizedMethodName);

            var output = hasExtensionParameter.GetOrExceptionIfNotFound($"No extension parameter found on parameterized method name:\n{parameterizedMethodName}");
            return output;
        }

        public string GetExtensionTypedMethodName(
            string typedParameterizedExtensionMethodName)
        {
            var extensionParameter = this.GetExtensionParameter(typedParameterizedExtensionMethodName);

            var extensionTypeName = Instances.ParameterNameOperator.GetExtensionTypeName(extensionParameter);

            var methodName = this.GetMethodNameFromFullMethodName(typedParameterizedExtensionMethodName);

            var output = $"{extensionTypeName}.{methodName}";
            return output;
        }

        public string GetParameterListWithoutExtensionParameterUnParenthesied(
            string parameterizedMethodName)
        {
            var parameterListUnParenthesied = this.GetParameterListUnParenthesied(parameterizedMethodName);

            var hasExtensionParameter = this.HasExtensionParameter(parameterizedMethodName);
            if (hasExtensionParameter)
            {
                var indexOfFirstCommaOrNotFound = parameterListUnParenthesied.IndexOf(Instances.Strings.Comma);

                var restOfParameterList = Instances.StringOperator.IsFound(indexOfFirstCommaOrNotFound)
                    ? parameterListUnParenthesied[(indexOfFirstCommaOrNotFound + 1)..].TrimStart()
                    : Instances.Strings.Empty
                    ;

                return restOfParameterList;
            }
            else
            {
                return parameterListUnParenthesied;
            }
        }

        public string GetParameterListWithoutExtensionParameter(
            string parameterizedMethodName)
        {
            var parameterListWithoutExtensionParameterUnParenthesied = this.GetParameterListWithoutExtensionParameterUnParenthesied(parameterizedMethodName);

            var output = $"({parameterListWithoutExtensionParameterUnParenthesied})";
            return output;
        }

        public string GetMethodNameWithoutExtensionParameter(
            string parameterizedMethodName)
        {
            var indexOfFirstOpenParenthesisOrNotFound = parameterizedMethodName.IndexOf(Instances.Characters.OpenParenthesis);

            if (Instances.StringOperator.NotFound(indexOfFirstOpenParenthesisOrNotFound))
            {
                return parameterizedMethodName;
            }

            var methodRoot = parameterizedMethodName[..indexOfFirstOpenParenthesisOrNotFound];

            var parameterListWithoutExtensionParameter = this.GetParameterListWithoutExtensionParameter(parameterizedMethodName);

            var output = $"{methodRoot}{parameterListWithoutExtensionParameter}";
            return output;
        }
    }
}
