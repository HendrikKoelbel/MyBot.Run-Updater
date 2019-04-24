namespace MyBot.Run_Updater
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_checkUpdate = new System.Windows.Forms.Button();
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.labelstatus = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_checkUpdate
            // 
            this.btn_checkUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_checkUpdate.Font = new System.Drawing.Font("Arial", 11.25F);
            this.btn_checkUpdate.Location = new System.Drawing.Point(12, 303);
            this.btn_checkUpdate.Name = "btn_checkUpdate";
            this.btn_checkUpdate.Size = new System.Drawing.Size(136, 26);
            this.btn_checkUpdate.TabIndex = 0;
            this.btn_checkUpdate.Text = "Check for Update";
            this.btn_checkUpdate.UseVisualStyleBackColor = true;
            this.btn_checkUpdate.Click += new System.EventHandler(this.Btn_checkUpdate_Click);
            // 
            // progressBarMain
            // 
            this.progressBarMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMain.Location = new System.Drawing.Point(12, 348);
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(425, 23);
            this.progressBarMain.TabIndex = 1;
            // 
            // labelstatus
            // 
            this.labelstatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelstatus.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelstatus.Location = new System.Drawing.Point(0, 374);
            this.labelstatus.Name = "labelstatus";
            this.labelstatus.Size = new System.Drawing.Size(449, 48);
            this.labelstatus.TabIndex = 2;
            this.labelstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Font = new System.Drawing.Font("Arial", 11.25F);
            this.btn_cancel.Location = new System.Drawing.Point(301, 303);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(136, 26);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.Btn_cancel_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.Location = new System.Drawing.Point(13, 13);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedHorizontal;
            this.richTextBoxLog.Size = new System.Drawing.Size(424, 270);
            this.richTextBoxLog.TabIndex = 3;
            this.richTextBoxLog.Text = "";
            this.richTextBoxLog.TextChanged += new System.EventHandler(this.richTextBoxLog_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 422);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.labelstatus);
            this.Controls.Add(this.progressBarMain);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_checkUpdate);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(465, 530);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(325, 390);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyBot Updater by Hendrik Kölbel";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_checkUpdate;
        private System.Windows.Forms.ProgressBar progressBarMain;
        private System.Windows.Forms.Label labelstatus;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
    }
}

