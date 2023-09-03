using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TileEditor
{
    public partial class NewEntranceTileForm : Form
    {
        #region Properties

        //Alla namn på kartorna som anges sparas i denna array.
        public string[] MapNames { get; private set; }

        #endregion

        #region Constructor

        //Komponenter initieras.
        public NewEntranceTileForm() { InitializeComponent(); }

        #endregion

        #region Methods

        private void okButton_Click(object sender, EventArgs e)
        {
            //Vi kontrollerar vilka textorboxar som innehåller text.
            int numberOfMapNames = 0;
            if(!string.IsNullOrEmpty(mapName1TextBox.Text))
                numberOfMapNames++;
            if(!string.IsNullOrEmpty(mapName2TextBox.Text))
                numberOfMapNames++;
            if(!string.IsNullOrEmpty(mapName3TextBox.Text))
                numberOfMapNames++;
            if(!string.IsNullOrEmpty(mapName4TextBox.Text))
                numberOfMapNames++;
            if(!string.IsNullOrEmpty(mapName5TextBox.Text))
                numberOfMapNames++;

            //Har inget namn angivits så skrivs ett felmeddelande ut.
            if (numberOfMapNames == 0)
            {
                MessageBox.Show("You must enter at least one map name.");
                return;
            }
            MapNames = new string[numberOfMapNames];
            if (!string.IsNullOrEmpty(mapName1TextBox.Text))
                MapNames[0] = mapName1TextBox.Text;
            if (!string.IsNullOrEmpty(mapName2TextBox.Text))
                MapNames[1] = mapName2TextBox.Text;
            if (!string.IsNullOrEmpty(mapName3TextBox.Text))
                MapNames[2] = mapName3TextBox.Text;
            if (!string.IsNullOrEmpty(mapName4TextBox.Text))
                MapNames[3] = mapName4TextBox.Text;
            if (!string.IsNullOrEmpty(mapName5TextBox.Text))
                MapNames[4] = mapName5TextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        //Det som händer om man trycker på Cancel-knappen (Formen stängs ner).
        private void cancelButton_Click(object sender, EventArgs e) { this.Close(); }

        #endregion
    }
}
