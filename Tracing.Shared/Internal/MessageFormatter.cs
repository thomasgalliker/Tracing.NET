using System.Globalization;

namespace Tracing.Internal
{
    internal static class MessageFormatter
    {
        internal static string FormatArguments(this string message, params object[] arguments)
        {
            string formattedMessage;
            if (message != null && arguments != null && arguments.Length > 0)
            {
                formattedMessage = string.Format(CultureInfo.InvariantCulture, message, arguments);
            }
            else
            {
                formattedMessage = message ?? string.Empty;
            }

            return formattedMessage;
        }
    }
}