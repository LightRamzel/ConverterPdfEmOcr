using ConverterPdfEmOcr.Wpf.Interfaces;
using System.IO;

namespace ConverterPdfEmOcr.Wpf.Utils;

public class FileDialogRead : IFileDialogRead
{
    private readonly OpenFileDialog _openFileDialog = new()
    {
        Multiselect = false,
        Filter = "Arquivo PDF | *.pdf",
        Title = "Selecione o arquivo PDF"
    };

    public async Task<string> ReadPathAsync()
    {
        var result = _openFileDialog.ShowDialog();

        if (result != DialogResult.OK || PdfInvalido(_openFileDialog.FileName))
            return string.Empty;

        return await Task.FromResult(_openFileDialog.FileName);
    }

    private static bool PdfInvalido(string filename)
    {
        return string.IsNullOrEmpty(filename) || !Path.Exists(filename) || Path.GetExtension(filename).ToLower() != ".pdf";
    }
}
