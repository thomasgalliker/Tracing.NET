using System;

namespace Tracing
{
    internal static class Guard
    {
        public static void ArgumentNotNullOrEmpty(string paramName, string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ArgumentNotNull(string paramName, object paramValue)
        {
            if (paramValue == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }

    /// <summary>
    /// ValidatedNotNullAttribute signals to static code analysis (CA1062)
    /// to trust that we're really checking the marked parameters for null references.
    /// </summary>
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
