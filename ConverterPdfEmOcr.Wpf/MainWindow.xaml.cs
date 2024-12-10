using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using MudBlazor.Services;
using ConverterPdfEmOcr.Wpf.Interfaces;
using ConverterPdfEmOcr.Wpf.Utils;
using ConverterPdfEmOcr.Wpf.Services;

namespace ConverterPdfEmOcr.Wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMudServices();

        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddScoped<IFileDialogRead, FileDialogRead>();
        serviceCollection.AddScoped<IFileDialogSave, FileDialogSave>();
        serviceCollection.AddScoped<IConverterArquivoPdf, ConverterArquivoPdf>();

#if DEBUG
        serviceCollection.AddBlazorWebViewDeveloperTools();
#endif

        Resources.Add("services", serviceCollection.BuildServiceProvider());
    }
}