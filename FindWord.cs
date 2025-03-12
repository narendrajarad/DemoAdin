using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using Microsoft.Office.Core;

namespace Demo_Addin
{
    public partial class FindWord
    {
        private SearchPane searchPane;
        private Microsoft.Office.Tools.CustomTaskPane taskPane;

        private void FindWord_Load(object sender, RibbonUIEventArgs e)
        {
            if (taskPane == null)
            {
                searchPane = new SearchPane();
                taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(searchPane, "Search Pane");
                taskPane.Visible = false; // Start as hidden
            }
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            if (taskPane != null)
            {
                taskPane.Visible = !taskPane.Visible; // Toggle visibility
            }
        }
    }
}
