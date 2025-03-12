namespace Demo_Addin
{
    partial class SearchPane
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSearchWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFindWord = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFindFonts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSearchWord
            // 
            this.txtSearchWord.Location = new System.Drawing.Point(70, 24);
            this.txtSearchWord.Name = "txtSearchWord";
            this.txtSearchWord.Size = new System.Drawing.Size(137, 20);
            this.txtSearchWord.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search Font";
            // 
            // btnFindWord
            // 
            this.btnFindWord.Location = new System.Drawing.Point(65, 50);
            this.btnFindWord.Name = "btnFindWord";
            this.btnFindWord.Size = new System.Drawing.Size(50, 23);
            this.btnFindWord.TabIndex = 2;
            this.btnFindWord.Text = "Search";
            this.btnFindWord.UseVisualStyleBackColor = true;
            this.btnFindWord.Click += new System.EventHandler(this.btnFindWord_Click);
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Location = new System.Drawing.Point(16, 125);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(267, 550);
            this.lstResults.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(157, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFindFonts
            // 
            this.btnFindFonts.Location = new System.Drawing.Point(65, 79);
            this.btnFindFonts.Name = "btnFindFonts";
            this.btnFindFonts.Size = new System.Drawing.Size(142, 23);
            this.btnFindFonts.TabIndex = 5;
            this.btnFindFonts.Text = "Font Analysis";
            this.btnFindFonts.UseVisualStyleBackColor = true;
            this.btnFindFonts.Click += new System.EventHandler(this.btnFindFonts_Click);
            // 
            // SearchPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnFindFonts);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnFindWord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearchWord);
            this.Name = "SearchPane";
            this.Size = new System.Drawing.Size(311, 722);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindWord;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFindFonts;
        private System.Windows.Forms.ListBox lstResults;
    }
}
