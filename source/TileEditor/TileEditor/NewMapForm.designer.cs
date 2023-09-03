namespace TileEditor
{
    partial class NewMapForm
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
            this.mapNameLabel = new System.Windows.Forms.Label();
            this.tileWidthLabel = new System.Windows.Forms.Label();
            this.tileHeightLabel = new System.Windows.Forms.Label();
            this.tilesHorizontalLabel = new System.Windows.Forms.Label();
            this.tilesVerticalLabel = new System.Windows.Forms.Label();
            this.mapNameTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tileWidthMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.tileHeightMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.tilesHorizontalMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.tilesVerticalMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // mapNameLabel
            // 
            this.mapNameLabel.AutoSize = true;
            this.mapNameLabel.Location = new System.Drawing.Point(13, 13);
            this.mapNameLabel.Name = "mapNameLabel";
            this.mapNameLabel.Size = new System.Drawing.Size(62, 13);
            this.mapNameLabel.TabIndex = 0;
            this.mapNameLabel.Text = "Map Name:";
            // 
            // tileWidthLabel
            // 
            this.tileWidthLabel.AutoSize = true;
            this.tileWidthLabel.Location = new System.Drawing.Point(13, 36);
            this.tileWidthLabel.Name = "tileWidthLabel";
            this.tileWidthLabel.Size = new System.Drawing.Size(58, 13);
            this.tileWidthLabel.TabIndex = 1;
            this.tileWidthLabel.Text = "Tile Width:";
            // 
            // tileHeightLabel
            // 
            this.tileHeightLabel.AutoSize = true;
            this.tileHeightLabel.Location = new System.Drawing.Point(13, 59);
            this.tileHeightLabel.Name = "tileHeightLabel";
            this.tileHeightLabel.Size = new System.Drawing.Size(61, 13);
            this.tileHeightLabel.TabIndex = 2;
            this.tileHeightLabel.Text = "Tile Height:";
            // 
            // tilesHorizontalLabel
            // 
            this.tilesHorizontalLabel.AutoSize = true;
            this.tilesHorizontalLabel.Location = new System.Drawing.Point(13, 82);
            this.tilesHorizontalLabel.Name = "tilesHorizontalLabel";
            this.tilesHorizontalLabel.Size = new System.Drawing.Size(82, 13);
            this.tilesHorizontalLabel.TabIndex = 3;
            this.tilesHorizontalLabel.Text = "Tiles Horizontal:";
            // 
            // tilesVerticalLabel
            // 
            this.tilesVerticalLabel.AutoSize = true;
            this.tilesVerticalLabel.Location = new System.Drawing.Point(13, 105);
            this.tilesVerticalLabel.Name = "tilesVerticalLabel";
            this.tilesVerticalLabel.Size = new System.Drawing.Size(70, 13);
            this.tilesVerticalLabel.TabIndex = 4;
            this.tilesVerticalLabel.Text = "Tiles Vertical:";
            // 
            // mapNameTextBox
            // 
            this.mapNameTextBox.Location = new System.Drawing.Point(73, 10);
            this.mapNameTextBox.Name = "mapNameTextBox";
            this.mapNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.mapNameTextBox.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 128);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(98, 128);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // tileWidthMaskedTextBox
            // 
            this.tileWidthMaskedTextBox.Location = new System.Drawing.Point(73, 33);
            this.tileWidthMaskedTextBox.Mask = "0000";
            this.tileWidthMaskedTextBox.Name = "tileWidthMaskedTextBox";
            this.tileWidthMaskedTextBox.PromptChar = ' ';
            this.tileWidthMaskedTextBox.Size = new System.Drawing.Size(33, 20);
            this.tileWidthMaskedTextBox.TabIndex = 12;
            // 
            // tileHeightMaskedTextBox
            // 
            this.tileHeightMaskedTextBox.Location = new System.Drawing.Point(73, 56);
            this.tileHeightMaskedTextBox.Mask = "0000";
            this.tileHeightMaskedTextBox.Name = "tileHeightMaskedTextBox";
            this.tileHeightMaskedTextBox.PromptChar = ' ';
            this.tileHeightMaskedTextBox.Size = new System.Drawing.Size(33, 20);
            this.tileHeightMaskedTextBox.TabIndex = 13;
            // 
            // tilesHorizontalMaskedTextBox
            // 
            this.tilesHorizontalMaskedTextBox.Location = new System.Drawing.Point(94, 79);
            this.tilesHorizontalMaskedTextBox.Mask = "0000";
            this.tilesHorizontalMaskedTextBox.Name = "tilesHorizontalMaskedTextBox";
            this.tilesHorizontalMaskedTextBox.PromptChar = ' ';
            this.tilesHorizontalMaskedTextBox.Size = new System.Drawing.Size(33, 20);
            this.tilesHorizontalMaskedTextBox.TabIndex = 14;
            // 
            // tilesVerticalMaskedTextBox
            // 
            this.tilesVerticalMaskedTextBox.Location = new System.Drawing.Point(82, 102);
            this.tilesVerticalMaskedTextBox.Mask = "0000";
            this.tilesVerticalMaskedTextBox.Name = "tilesVerticalMaskedTextBox";
            this.tilesVerticalMaskedTextBox.PromptChar = ' ';
            this.tilesVerticalMaskedTextBox.Size = new System.Drawing.Size(33, 20);
            this.tilesVerticalMaskedTextBox.TabIndex = 15;
            // 
            // NewMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 163);
            this.Controls.Add(this.tilesVerticalMaskedTextBox);
            this.Controls.Add(this.tilesHorizontalMaskedTextBox);
            this.Controls.Add(this.tileHeightMaskedTextBox);
            this.Controls.Add(this.tileWidthMaskedTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.mapNameTextBox);
            this.Controls.Add(this.tilesVerticalLabel);
            this.Controls.Add(this.tilesHorizontalLabel);
            this.Controls.Add(this.tileHeightLabel);
            this.Controls.Add(this.tileWidthLabel);
            this.Controls.Add(this.mapNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewMapForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mapNameLabel;
        private System.Windows.Forms.Label tileWidthLabel;
        private System.Windows.Forms.Label tileHeightLabel;
        private System.Windows.Forms.Label tilesHorizontalLabel;
        private System.Windows.Forms.Label tilesVerticalLabel;
        private System.Windows.Forms.TextBox mapNameTextBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.MaskedTextBox tileWidthMaskedTextBox;
        private System.Windows.Forms.MaskedTextBox tileHeightMaskedTextBox;
        private System.Windows.Forms.MaskedTextBox tilesHorizontalMaskedTextBox;
        private System.Windows.Forms.MaskedTextBox tilesVerticalMaskedTextBox;
    }
}