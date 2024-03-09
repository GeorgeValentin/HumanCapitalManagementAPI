using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace HumanCapitalManagement.Utilities.Logging
{
    public static class LoggingHelper
    {
        public static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;

        public static string CreateLogMessageForController<T>(
            HttpOperationType httpVerb,
            string endpoint,
            string className,
            string methodName,
            T? entityObject = default)
        {
            switch (httpVerb)
            {
                case HttpOperationType.GET:
                case HttpOperationType.DELETE:
                case HttpOperationType.PATCH:
                    return $"[{className}.{methodName}] Request for {httpVerb} {endpoint} endpoint.";

                case HttpOperationType.POST:
                case HttpOperationType.PUT:
                    return $"[{className}.{methodName}] Request for {httpVerb} {endpoint} endpoint, having the following details: {JsonConvert.SerializeObject(entityObject)}.";

                case HttpOperationType.UNKNOWN:
                    throw new InvalidOperationException("The specified HTTP operation is unknown!");

                default:
                    throw new InvalidOperationException("The specified HTTP operation is invalid!");
            }
        }

    }
}