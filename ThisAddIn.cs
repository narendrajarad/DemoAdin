using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;

namespace Demo_Addin
{
    public partial class ThisAddIn
    {
        private SearchPane searchPane;
        public CustomTaskPane taskPane;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            searchPane = new SearchPane();
            taskPane = this.CustomTaskPanes.Add(searchPane, "Word Finder");
            taskPane.Visible = true;
        }

        public void ShowTaskPane()
        {
            taskPane.Visible = true;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        


        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
