using System.Text;

namespace DiceServiceApplication.Extensions;

public static class ExceptionExtensions
{
    public static string? ComposeFullExceptionMessage(this Exception ex)
    {
        StringBuilder builder = new StringBuilder();
        Exception innerException = ex;
        do
        {
            builder.Append(innerException.GetType().ToString() + ": ").Append(innerException.Message).Append("; ");
            innerException = innerException.InnerException;
        }
        while (innerException != null);            
        return builder.ToString();
    }
}