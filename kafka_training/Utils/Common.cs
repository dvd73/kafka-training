using System.Text;

namespace kafka_training.Utils;

public class Common
{
    public static string? ComposeFullExceptionMessage(Exception ex)
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