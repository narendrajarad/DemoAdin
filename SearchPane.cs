using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demo_Addin
{
    public partial class SearchPane : UserControl
    {
        private CancellationTokenSource _cts;

        public SearchPane()
        {
            InitializeComponent();
        }

        private void btnFindWord_Click(object sender, EventArgs e)
        {
            
            lstResults.Items.Clear();

            string searchFont = txtSearchWord.Text.Trim();
            if (string.IsNullOrEmpty(searchFont))
            {
                MessageBox.Show("Please enter a font name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //  Cancel previous search before starting a new one
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            _=System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    Microsoft.Office.Interop.Word.Application wordApp = Globals.ThisAddIn.Application;
                    Document doc = wordApp.ActiveDocument;

                    //Remove previous highlights before starting the new search
                    //RemovePreviousHighlights(doc);

                    bool found = false;
                    List<string> results = new List<string>();

                    foreach (var result in SearchAndHighlightFontInDocument(searchFont, token))
                    {
                        if (token.IsCancellationRequested)
                        {
                            results.Add("Search Canceled");
                            break;
                        }

                        found = true;
                        results.Add(result);
                    }

                    // Batch update UI once instead of updating every iteration
                    Invoke((MethodInvoker)(() =>
                    {
                        lstResults.Items.AddRange(results.ToArray());
                        if (!found) lstResults.Items.Add("Font not found in document.");
                    }));
                }
                catch (Exception ex)
                {
                    Invoke((MethodInvoker)(() => MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                }
            });



        }

        private IEnumerable<string> SearchAndHighlightFontInDocument(string searchFont, CancellationToken token)
        {
            Microsoft.Office.Interop.Word.Application wordApp = Globals.ThisAddIn.Application;
            Document doc = wordApp.ActiveDocument;
            HashSet<int> processedPages = new HashSet<int>(); // Avoids duplicate results

            //Process larger text chunks for speed
            foreach (Range paragraphRange in doc.Content.Paragraphs.Cast<Paragraph>().Select(p => p.Range))
            {
                if (token.IsCancellationRequested)
                {
                    yield return "Search Canceled";
                    yield break;
                }

                string fontName = string.Empty;
                int pageNumber = -1;

                try
                {
                    fontName = paragraphRange.Font.Name;
                    pageNumber = paragraphRange.Information[WdInformation.wdActiveEndPageNumber];
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Skipping paragraph due to error: {ex.Message}");
                    continue; // Skip and move to the next paragraph
                }

                //  Ensure valid page number and avoid duplicates
                if (!string.IsNullOrEmpty(fontName) && fontName.Equals(searchFont, StringComparison.OrdinalIgnoreCase) && pageNumber > 0)
                {
                    if (!processedPages.Contains(pageNumber))
                    {
                        processedPages.Add(pageNumber);
                        yield return $"Page {pageNumber}: {searchFont}";
                    }

                    // Highlight matched text
                    try
                    {
                        paragraphRange.HighlightColorIndex = WdColorIndex.wdYellow;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Highlight error: {ex.Message}");
                    }
                }

                // Keep UI responsive
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void RemovePreviousHighlights(Document doc)
        {
            foreach (Range wordRange in doc.Words)
            {
                wordRange.HighlightColorIndex = WdColorIndex.wdNoHighlight;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void btnFindFonts_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();

            _cts?.Cancel(); // Cancel any previous operation
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            _ = System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    bool isCanceled = false; // Track if cancellation occurred

                    foreach (var result in GetFontsPerPage(token))
                    {
                        if (result == "Search Canceled")
                        {
                            isCanceled = true;
                        }

                        Invoke((MethodInvoker)(() => lstResults.Items.Add(result)));

                        if (token.IsCancellationRequested) break; // Stop processing further
                    }

                    // Ensure "Search Canceled" is visible if needed
                    if (isCanceled && lstResults.Items[lstResults.Items.Count - 1].ToString() != "Search Canceled")
                    {
                        Invoke((MethodInvoker)(() => lstResults.Items.Add("Search Canceled")));
                    }
                }
                catch (Exception ex)
                {
                    Invoke((MethodInvoker)(() => MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                }
            });



        }
        private IEnumerable<string> GetFontsPerPage(CancellationToken token)
        {
            Microsoft.Office.Interop.Word.Application wordApp = Globals.ThisAddIn.Application;
            Document doc = wordApp.ActiveDocument;
            Dictionary<int, HashSet<string>> fontsPerPage = new Dictionary<int, HashSet<string>>();

            foreach (Range wordRange in doc.Words)
            {
                if (token.IsCancellationRequested)
                {
                    yield return "Search Canceled";  // Ensure message is returned
                    yield break;
                }

                int pageNumber = -1;
                string fontName = string.Empty;

                try
                {
                    pageNumber = wordRange.Information[WdInformation.wdActiveEndPageNumber];
                    fontName = wordRange.Font.Name;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Skipping word due to error: {ex.Message}");
                    continue;
                }

                if (pageNumber == -1 || string.IsNullOrEmpty(fontName))
                    continue;

                if (!fontsPerPage.ContainsKey(pageNumber))
                {
                    fontsPerPage[pageNumber] = new HashSet<string>();
                }

                if (!fontsPerPage[pageNumber].Contains(fontName))
                {
                    fontsPerPage[pageNumber].Add(fontName);

                    if (token.IsCancellationRequested)
                    {
                        yield return "Search Canceled";  // Ensure "Search Canceled" is properly returned
                        yield break;
                    }

                    yield return $"Page {pageNumber}: {fontName}";
                }

                System.Threading.Thread.Sleep(75);
            }
        }



    }
}
