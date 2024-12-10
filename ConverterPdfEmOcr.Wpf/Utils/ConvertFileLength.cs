namespace ConverterPdfEmOcr.Wpf.Utils;

public static class ConvertFileLength
{
    public static string ConvertToBestFormat(long bytes)
    {
        if (bytes >= 1_099_511_627_776) // Maior ou igual a 1 TB
        {
            return $"{ConvertToTerabytes(bytes):F2} TB";
        }
        else if (bytes >= 1_073_741_824) // Maior ou igual a 1 GB
        {
            return $"{ConvertToGigabytes(bytes):F2} GB";
        }
        else if (bytes >= 1_048_576) // Maior ou igual a 1 MB
        {
            return $"{ConvertToMegabytes(bytes):F2} MB";
        }
        else // Menor que 1 MB, permanece em bytes
        {
            return $"{bytes} Bytes";
        }
    }

    private static double ConvertToMegabytes(long bytes)
    {
        return bytes / 1_048_576.0; // 1 MB = 1024^2 bytes
    }

    private static double ConvertToGigabytes(long bytes)
    {
        return bytes / 1_073_741_824.0; // 1 GB = 1024^3 bytes
    }

    private static double ConvertToTerabytes(long bytes)
    {
        return bytes / 1_099_511_627_776.0; // 1 TB = 1024^4 bytes
    }
}
