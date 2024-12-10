namespace ConverterPdfEmOcr.Wpf.Interfaces;

public interface IFilePickerSave
{
    Task<string> SavePath(string nomeArquivoOriginal);
}
