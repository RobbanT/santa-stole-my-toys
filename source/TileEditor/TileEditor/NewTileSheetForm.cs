using System;
using System.Windows.Forms;

namespace TileEditor
{
    //Form som används när man vill lägga till ett tile sheet.
    public partial class NewTileSheetForm : Form
    {
        #region Properties
        //Sökvägen till tilesheetet.
        public string TileSheetPath { get; private set; }

        #endregion

        #region Constructor

        //Komponenter initieras.
        public NewTileSheetForm() { InitializeComponent(); }

        #endregion

        #region Button Events

        //Det som händer när man trycker på findTileSheet-knappen.
        private void findTileSheetButton_Click(object sender, EventArgs e)
        {
            //En ny form öppnas där spelaren får ange sökvägen till sitt tile sheet.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.BMP;*.GIF;*.JPG;*.TGA;*.PNG";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                tileSheetPathTextBox.Text = openFileDialog.FileName;
        }

        //Det som händer om man trycker på OK-knappen.
        private void okButton_Click(object sender, EventArgs e)
        {
            //Spelaren ska inte kunna fortsätta om han/hon inte har angett en sökväg.
            if (string.IsNullOrEmpty(tileSheetPathTextBox.Text))
            {
                MessageBox.Show("You must select a path for the tile sheet.");
                return;
            }
            TileSheetPath = tileSheetPathTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //Det som händer om man trycker på Cancel-knappen (Formen stängs ner).
        private void cancelButton_Click(object sender, EventArgs e) { this.Close(); }

        #endregion
    }
}
