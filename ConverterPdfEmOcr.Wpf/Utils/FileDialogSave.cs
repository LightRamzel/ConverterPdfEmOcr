using ConverterPdfEmOcr.Wpf.Interfaces;
using System.IO;

namespace ConverterPdfEmOcr.Wpf.Utils;

public class FileDialogSave : IFileDialogSave
{
    private readonly SaveFileDialog _saveFileDialog = new()
    {
        AddExtension = true,
        //Filter = "Arquivo PDF | *.pdf",
        Title = "Selecione o caminho para salvar o arquivo PDF"
    };

    public async Task<string> SavePath(string nomeArquivoOriginal)
    {
        var retorno = string.Empty;

        _saveFileDialog.FileName = $"{Path.GetFileNameWithoutExtension(nomeArquivoOriginal)}_OCR.pdf";

        var result = _saveFileDialog.ShowDialog();
        
        if (result == DialogResult.OK)
            retorno = _saveFileDialog.FileName;

        return await Task.FromResult(retorno);
    }
}
