namespace ConverterPdfEmOcr.Wpf.Interfaces;

public interface IConverterArquivoPdf
{
    Task FazerConversaoSalvarAsync(string arquivoLeitura, string arquivoSalvar);
}
