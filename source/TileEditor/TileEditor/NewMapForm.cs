using System;
using System.Windows.Forms;

namespace TileEditor
{
    //Detta är formen som används när man vill skapa en ny karta.
    public partial class NewMapForm : Form
    {
        #region Properties

        //Namnet på kartan.
        public string MapName { get; private set; }
        //Bredden på kartans tiles.
        public int TileWidth { get; private set; }
        //Höjden på kartans tiles.
        public int TileHeight { get; private set; }
        //Hur många tiles kartan ska innehålla horisontellt.
        public int TilesHorizontal { get; private set; }
        //Hur många tiles kartan ska innehålla vertikalt.
        public int TilesVertical { get; private set; }

        #endregion

        #region Constructor

        //Komponenter initieras.
        public NewMapForm() { InitializeComponent(); }

        #endregion

        #region Methods

        //Det som händer om man trycker på OK-knappen.
        private void okButton_Click(object sender, EventArgs e)
        {
            //En kontroll sker på alla textboxar och kontrollerar om angivet värde är giltligt.
            if (string.IsNullOrEmpty(mapNameTextBox.Text))
            {
                MessageBox.Show("You must enter a map name.");
                return;
            }

            if (tileWidthMaskedTextBox.Text == "" || int.Parse(tileWidthMaskedTextBox.Text) < 1)
            {
                MessageBox.Show("The tile width must be greater than or equal to one.");
                return;
            }

            if (tileWidthMaskedTextBox.Text == "" || int.Parse(tileHeightMaskedTextBox.Text) < 1)
            {
                MessageBox.Show("The tile height must be greater than or equal to one.");
                return;
            }

            if (tileWidthMaskedTextBox.Text == "" || int.Parse(tilesHorizontalMaskedTextBox.Text) < 1)
            {
                MessageBox.Show("The number of tiles horizontal must be greater than or equal to one.");
                return;
            }

            if (tileWidthMaskedTextBox.Text == "" || int.Parse(tilesVerticalMaskedTextBox.Text) < 1)
            {
                MessageBox.Show("The number of tiles vertical must be greater than or equal to one.");
                return;
            }
            
            MapName = mapNameTextBox.Text;
            TileWidth = int.Parse(tileWidthMaskedTextBox.Text);
            TileHeight = int.Parse(tileHeightMaskedTextBox.Text);
            TilesHorizontal = int.Parse(tilesHorizontalMaskedTextBox.Text);
            TilesVertical = int.Parse(tilesVerticalMaskedTextBox.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //Det som händer om man trycker på Cancel-knappen (Formen stängs ner).
        private void cancelButton_Click(object sender, EventArgs e) { this.Close(); }

        #endregion
    }
}
