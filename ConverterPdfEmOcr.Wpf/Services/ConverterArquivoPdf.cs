using ConverterPdfEmOcr.Wpf.Interfaces;
using ImageMagick;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using TesseractOCR;
using TesseractOCR.Enums;

namespace ConverterPdfEmOcr.Wpf.Services;

public class ConverterArquivoPdf : IConverterArquivoPdf
{
    public async Task FazerConversaoSalvarAsync(string arquivoLeitura, string arquivoSalvar)
    {
        string tessdataPath = "./tessdata";

        using var images = new MagickImageCollection();
        
        await Task.Factory.StartNew(() => images.Read(arquivoLeitura));

        using var reader = new PdfReader(arquivoLeitura);
        using var fileStream = new FileStream(arquivoSalvar, FileMode.Create, FileAccess.Write);
        using var stamper = new PdfStamper(reader, fileStream);

        var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        var size = 12;

        var folderPathImagesTmp = CreateTmpFolderImages();

        await Task.Factory.StartNew(() =>
        {
            for (int i = 0; i < images.Count; i++)
            {
                using var image = images[i];

                string outputPath = Path.Combine(folderPathImagesTmp, $"pagina_{i + 1}.png");
                image.Write(outputPath);

                // Realizar OCR na imagem
                using var ocrEngine = new Engine(tessdataPath, Language.Portuguese, EngineMode.Default);
                using var img = TesseractOCR.Pix.Image.LoadFromFile(outputPath);
                using var ocrPage = ocrEngine.Process(img);

                // Adicionar o texto OCR ao PDF como uma camada transparente
                var canvas = CreateCanvas(stamper, baseFont, size, i + 1);
                
                var paragraphs = ocrPage.Layout.SelectMany(x => x.Paragraphs);

                foreach (var paragraph in paragraphs)
                {
                    if (paragraph.Baseline is null)
                        continue;

                    var baselineValue = paragraph.Baseline.Value;

                    float x = baselineValue.X1 * 595 / img.Width;  // 595 é a largura de uma página A4 em pontos
                    float y = (img.Height - baselineValue.Y1) * 842 / img.Height; // 842 é a altura de uma página A4 em pontos

                    canvas.ShowTextAligned(Element.ALIGN_LEFT, paragraph.Text, x, y, 0);
                }

                canvas.EndText();

                // Remover a imagem temporária
                File.Delete(outputPath);
            }
        });
    }

    private static string CreateTmpFolderImages()
    {
        const string folderTmp = "ImagesTmp";

        if (!Directory.Exists(folderTmp))
            Directory.CreateDirectory(folderTmp);

        return Path.GetFullPath(folderTmp);
    }

    private static PdfContentByte CreateCanvas(PdfStamper pdfStamper, BaseFont baseFont, int fontSize,int pageNum)
    {
        var canvas = pdfStamper.GetOverContent(pageNum);
        canvas.BeginText();
        canvas.SetColorFill(new BaseColor(Color.Transparent));
        canvas.SetFontAndSize(baseFont, fontSize);

        return canvas;
    }
}
