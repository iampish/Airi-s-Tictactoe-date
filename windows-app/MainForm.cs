using System.Reflection;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace MomoiTicTacToe;

public class MainForm : Form
{
    private readonly WebView2 _webView = new();
    private string? _tempHtmlPath;

    public MainForm()
    {
        Text = "the momoi tictactoe";
        Width = 560;
        Height = 780;
        MinimumSize = new Size(420, 620);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = ColorTranslator.FromHtml("#FFF6F2");

        _webView.Dock = DockStyle.Fill;
        Controls.Add(_webView);

        Load += MainForm_Load;
        FormClosed += (_, _) => CleanUpTempFile();
    }

    private async void MainForm_Load(object? sender, EventArgs e)
    {
        try
        {
            await _webView.EnsureCoreWebView2Async(null);
        }
        catch (WebView2RuntimeNotFoundException)
        {
            MessageBox.Show(
                this,
                "This game needs the free Microsoft Edge WebView2 Runtime to run.\n\n" +
                "It's preinstalled on most Windows 10/11 PCs. If you're seeing this, " +
                "download it from:\nhttps://developer.microsoft.com/microsoft-edge/webview2/",
                "WebView2 Runtime Required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            Close();
            return;
        }

        // The whole game (HTML/CSS/JS/image) lives as one embedded resource inside the .exe.
        // We extract it to a temp file once per run and navigate WebView2 to it.
        _tempHtmlPath = ExtractEmbeddedHtmlToTempFile();
        _webView.CoreWebView2.Navigate(_tempHtmlPath);

        // Nice little touches: no right-click context menu, no dev-tools popup, no zoom bar.
        _webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        _webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
        _webView.CoreWebView2.Settings.IsZoomControlEnabled = false;
    }

    private static string ExtractEmbeddedHtmlToTempFile()
    {
        var assembly = Assembly.GetExecutingAssembly();
        const string resourceName = "MomoiTicTacToe.Assets.game.html";

        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");

        var tempPath = Path.Combine(Path.GetTempPath(), $"momoi_ttt_{Guid.NewGuid():N}.html");
        using (var fileStream = File.Create(tempPath))
        {
            stream.CopyTo(fileStream);
        }

        return tempPath;
    }

    private void CleanUpTempFile()
    {
        if (_tempHtmlPath != null && File.Exists(_tempHtmlPath))
        {
            try { File.Delete(_tempHtmlPath); } catch { /* best effort */ }
        }
    }
}
