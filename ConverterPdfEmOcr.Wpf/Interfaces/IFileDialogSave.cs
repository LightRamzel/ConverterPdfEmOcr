namespace ConverterPdfEmOcr.Wpf.Interfaces;

public interface IFileDialogSave
{
    Task<string> SavePath(string nomeArquivoOriginal);
}
