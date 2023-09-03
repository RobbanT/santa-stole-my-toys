namespace TileEditor
{
    partial class NewTileSheetForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tileSheetPathTextBox = new System.Windows.Forms.TextBox();
            this.tileSheetPathLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.findTileSheetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tileSheetPathTextBox
            // 
            this.tileSheetPathTextBox.Location = new System.Drawing.Point(96, 17);
            this.tileSheetPathTextBox.Name = "tileSheetPathTextBox";
            this.tileSheetPathTextBox.Size = new System.Drawing.Size(134, 20);
            this.tileSheetPathTextBox.TabIndex = 0;
            // 
            // tileSheetPathLabel
            // 
            this.tileSheetPathLabel.AutoSize = true;
            this.tileSheetPathLabel.Location = new System.Drawing.Point(10, 20);
            this.tileSheetPathLabel.Name = "tileSheetPathLabel";
            this.tileSheetPathLabel.Size = new System.Drawing.Size(83, 13);
            this.tileSheetPathLabel.TabIndex = 1;
            this.tileSheetPathLabel.Text = "Tile Sheet Path:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(50, 46);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(160, 46);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // findTileSheetButton
            // 
            this.findTileSheetButton.Location = new System.Drawing.Point(236, 17);
            this.findTileSheetButton.Name = "findTileSheetButton";
            this.findTileSheetButton.Size = new System.Drawing.Size(36, 19);
            this.findTileSheetButton.TabIndex = 4;
            this.findTileSheetButton.Text = "...";
            this.findTileSheetButton.UseVisualStyleBackColor = true;
            this.findTileSheetButton.Click += new System.EventHandler(this.findTileSheetButton_Click);
            // 
            // NewTileSheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 77);
            this.Controls.Add(this.findTileSheetButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tileSheetPathLabel);
            this.Controls.Add(this.tileSheetPathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTileSheetForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Tile Sheet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tileSheetPathTextBox;
        private System.Windows.Forms.Label tileSheetPathLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button findTileSheetButton;
    }
}