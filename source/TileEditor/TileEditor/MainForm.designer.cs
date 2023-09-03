namespace TileEditor
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTileSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTileSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cyanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileBrushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom75ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tileCoordinatesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mapDisplay = new TileEditor.XNA.MapDisplay();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tileSheetPictureBox = new System.Windows.Forms.PictureBox();
            this.currentLayerLabel = new System.Windows.Forms.Label();
            this.currentLayerCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.currentTileModeLabel = new System.Windows.Forms.Label();
            this.currentTileModeUpDown = new System.Windows.Forms.DomainUpDown();
            this.currentTileSheetLabel = new System.Windows.Forms.Label();
            this.currentTileSheetUpDown = new System.Windows.Forms.DomainUpDown();
            this.mapDisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileSheetPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapToolStripMenuItem,
            this.layerToolStripMenuItem,
            this.tileSheetToolStripMenuItem,
            this.gridToolStripMenuItem,
            this.tileBrushToolStripMenuItem,
            this.zoomToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.openMapToolStripMenuItem,
            this.saveMapToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.newMapToolStripMenuItem.Text = "New Map";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // openMapToolStripMenuItem
            // 
            this.openMapToolStripMenuItem.Name = "openMapToolStripMenuItem";
            this.openMapToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.openMapToolStripMenuItem.Text = "Open Map";
            this.openMapToolStripMenuItem.Click += new System.EventHandler(this.openMapToolStripMenuItem_Click);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Enabled = false;
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.saveMapToolStripMenuItem.Text = "Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLayerToolStripMenuItem,
            this.removeLayerToolStripMenuItem});
            this.layerToolStripMenuItem.Enabled = false;
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // newLayerToolStripMenuItem
            // 
            this.newLayerToolStripMenuItem.Name = "newLayerToolStripMenuItem";
            this.newLayerToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newLayerToolStripMenuItem.Text = "New Layer";
            this.newLayerToolStripMenuItem.Click += new System.EventHandler(this.newLayerToolStripMenuItem_Click);
            // 
            // removeLayerToolStripMenuItem
            // 
            this.removeLayerToolStripMenuItem.Name = "removeLayerToolStripMenuItem";
            this.removeLayerToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.removeLayerToolStripMenuItem.Text = "Remove Layer";
            this.removeLayerToolStripMenuItem.Click += new System.EventHandler(this.removeLayerToolStripMenuItem_Click);
            // 
            // tileSheetToolStripMenuItem
            // 
            this.tileSheetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTileSheetToolStripMenuItem,
            this.removeTileSheetToolStripMenuItem});
            this.tileSheetToolStripMenuItem.Enabled = false;
            this.tileSheetToolStripMenuItem.Name = "tileSheetToolStripMenuItem";
            this.tileSheetToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.tileSheetToolStripMenuItem.Text = "Tile Sheet";
            // 
            // newTileSheetToolStripMenuItem
            // 
            this.newTileSheetToolStripMenuItem.Name = "newTileSheetToolStripMenuItem";
            this.newTileSheetToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.newTileSheetToolStripMenuItem.Text = "New Tile Sheet";
            this.newTileSheetToolStripMenuItem.Click += new System.EventHandler(this.newTileSheetToolStripMenuItem_Click);
            // 
            // removeTileSheetToolStripMenuItem
            // 
            this.removeTileSheetToolStripMenuItem.Name = "removeTileSheetToolStripMenuItem";
            this.removeTileSheetToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.removeTileSheetToolStripMenuItem.Text = "Remove Tile Sheet";
            this.removeTileSheetToolStripMenuItem.Click += new System.EventHandler(this.removeTileSheetToolStripMenuItem_Click);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.CheckOnClick = true;
            this.gridToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayGridToolStripMenuItem,
            this.gridColorToolStripMenuItem});
            this.gridToolStripMenuItem.Enabled = false;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.gridToolStripMenuItem.Text = "Grid";
            // 
            // displayGridToolStripMenuItem
            // 
            this.displayGridToolStripMenuItem.Checked = true;
            this.displayGridToolStripMenuItem.CheckOnClick = true;
            this.displayGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayGridToolStripMenuItem.Name = "displayGridToolStripMenuItem";
            this.displayGridToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.displayGridToolStripMenuItem.Text = "Display Grid";
            this.displayGridToolStripMenuItem.Click += new System.EventHandler(this.displayGridToolStripMenuItem_Click);
            // 
            // gridColorToolStripMenuItem
            // 
            this.gridColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whiteToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.yellowToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.redToolStripMenuItem,
            this.blackToolStripMenuItem,
            this.magentaToolStripMenuItem,
            this.cyanToolStripMenuItem});
            this.gridColorToolStripMenuItem.Name = "gridColorToolStripMenuItem";
            this.gridColorToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.gridColorToolStripMenuItem.Text = "Grid Color";
            // 
            // whiteToolStripMenuItem
            // 
            this.whiteToolStripMenuItem.Checked = true;
            this.whiteToolStripMenuItem.CheckOnClick = true;
            this.whiteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            this.whiteToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.whiteToolStripMenuItem.Text = "White";
            this.whiteToolStripMenuItem.Click += new System.EventHandler(this.whiteToolStripMenuItem_Click);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.blueToolStripMenuItem_Click);
            // 
            // yellowToolStripMenuItem
            // 
            this.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem";
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.yellowToolStripMenuItem.Text = "Yellow";
            this.yellowToolStripMenuItem.Click += new System.EventHandler(this.yellowToolStripMenuItem_Click);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.greenToolStripMenuItem.Text = "Green";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.greenToolStripMenuItem_Click);
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.redToolStripMenuItem.Text = "Red";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.redToolStripMenuItem_Click);
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.blackToolStripMenuItem.Text = "Black";
            this.blackToolStripMenuItem.Click += new System.EventHandler(this.blackToolStripMenuItem_Click);
            // 
            // magentaToolStripMenuItem
            // 
            this.magentaToolStripMenuItem.Name = "magentaToolStripMenuItem";
            this.magentaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.magentaToolStripMenuItem.Text = "Magenta";
            this.magentaToolStripMenuItem.Click += new System.EventHandler(this.magentaToolStripMenuItem_Click);
            // 
            // cyanToolStripMenuItem
            // 
            this.cyanToolStripMenuItem.Name = "cyanToolStripMenuItem";
            this.cyanToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.cyanToolStripMenuItem.Text = "Cyan";
            this.cyanToolStripMenuItem.Click += new System.EventHandler(this.cyanToolStripMenuItem_Click);
            // 
            // tileBrushToolStripMenuItem
            // 
            this.tileBrushToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x1ToolStripMenuItem,
            this.x2ToolStripMenuItem,
            this.x4ToolStripMenuItem,
            this.x8ToolStripMenuItem,
            this.x16ToolStripMenuItem});
            this.tileBrushToolStripMenuItem.Enabled = false;
            this.tileBrushToolStripMenuItem.Name = "tileBrushToolStripMenuItem";
            this.tileBrushToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.tileBrushToolStripMenuItem.Text = "Tile Brush";
            // 
            // x1ToolStripMenuItem
            // 
            this.x1ToolStripMenuItem.Checked = true;
            this.x1ToolStripMenuItem.CheckOnClick = true;
            this.x1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.x1ToolStripMenuItem.Name = "x1ToolStripMenuItem";
            this.x1ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.x1ToolStripMenuItem.Text = "1 x 1";
            this.x1ToolStripMenuItem.Click += new System.EventHandler(this.x1ToolStripMenuItem_Click);
            // 
            // x2ToolStripMenuItem
            // 
            this.x2ToolStripMenuItem.Name = "x2ToolStripMenuItem";
            this.x2ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.x2ToolStripMenuItem.Text = "2 x 2";
            this.x2ToolStripMenuItem.Click += new System.EventHandler(this.x2ToolStripMenuItem_Click);
            // 
            // x4ToolStripMenuItem
            // 
            this.x4ToolStripMenuItem.Name = "x4ToolStripMenuItem";
            this.x4ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.x4ToolStripMenuItem.Text = "4 x 4";
            this.x4ToolStripMenuItem.Click += new System.EventHandler(this.x4ToolStripMenuItem_Click);
            // 
            // x8ToolStripMenuItem
            // 
            this.x8ToolStripMenuItem.Name = "x8ToolStripMenuItem";
            this.x8ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.x8ToolStripMenuItem.Text = "8 x 8";
            this.x8ToolStripMenuItem.Click += new System.EventHandler(this.x8ToolStripMenuItem_Click);
            // 
            // x16ToolStripMenuItem
            // 
            this.x16ToolStripMenuItem.Name = "x16ToolStripMenuItem";
            this.x16ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.x16ToolStripMenuItem.Text = "16 x 16";
            this.x16ToolStripMenuItem.Click += new System.EventHandler(this.x16ToolStripMenuItem_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoom50ToolStripMenuItem,
            this.zoom75ToolStripMenuItem,
            this.zoom100ToolStripMenuItem});
            this.zoomToolStripMenuItem.Enabled = false;
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // zoom50ToolStripMenuItem
            // 
            this.zoom50ToolStripMenuItem.Name = "zoom50ToolStripMenuItem";
            this.zoom50ToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.zoom50ToolStripMenuItem.Text = "50%";
            this.zoom50ToolStripMenuItem.Click += new System.EventHandler(this.zoom50ToolStripMenuItem_Click);
            // 
            // zoom75ToolStripMenuItem
            // 
            this.zoom75ToolStripMenuItem.Name = "zoom75ToolStripMenuItem";
            this.zoom75ToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.zoom75ToolStripMenuItem.Text = "75%";
            this.zoom75ToolStripMenuItem.Click += new System.EventHandler(this.zoom75ToolStripMenuItem_Click);
            // 
            // zoom100ToolStripMenuItem
            // 
            this.zoom100ToolStripMenuItem.Checked = true;
            this.zoom100ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.zoom100ToolStripMenuItem.Name = "zoom100ToolStripMenuItem";
            this.zoom100ToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.zoom100ToolStripMenuItem.Text = "100%";
            this.zoom100ToolStripMenuItem.Click += new System.EventHandler(this.zoom100ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileCoordinatesStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tileCoordinatesStatusLabel
            // 
            this.tileCoordinatesStatusLabel.Name = "tileCoordinatesStatusLabel";
            this.tileCoordinatesStatusLabel.Size = new System.Drawing.Size(96, 17);
            this.tileCoordinatesStatusLabel.Text = "Tile Coordinates:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mapDisplay);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 636);
            this.splitContainer1.SplitterDistance = 960;
            this.splitContainer1.TabIndex = 2;
            // 
            // mapDisplay
            // 
            this.mapDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapDisplay.Location = new System.Drawing.Point(0, 0);
            this.mapDisplay.Name = "mapDisplay";
            this.mapDisplay.Size = new System.Drawing.Size(960, 636);
            this.mapDisplay.TabIndex = 2;
            this.mapDisplay.Text = "mapDisplay";
            this.mapDisplay.OnInitialize += new System.EventHandler(this.mapDisplay_OnInitialize);
            this.mapDisplay.OnDraw += new System.EventHandler(this.mapDisplay_OnDraw);
            this.mapDisplay.SizeChanged += new System.EventHandler(this.mapDisplay_SizeChanged);
            this.mapDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapDisplay_MouseDown);
            this.mapDisplay.MouseEnter += new System.EventHandler(this.mapDisplay_MouseEnter);
            this.mapDisplay.MouseLeave += new System.EventHandler(this.mapDisplay_MouseLeave);
            this.mapDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapDisplay_MouseMove);
            this.mapDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapDisplay_MouseUp);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.tileSheetPictureBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.currentLayerLabel);
            this.splitContainer2.Panel2.Controls.Add(this.currentLayerCheckedListBox);
            this.splitContainer2.Panel2.Controls.Add(this.currentTileModeLabel);
            this.splitContainer2.Panel2.Controls.Add(this.currentTileModeUpDown);
            this.splitContainer2.Panel2.Controls.Add(this.currentTileSheetLabel);
            this.splitContainer2.Panel2.Controls.Add(this.currentTileSheetUpDown);
            this.splitContainer2.Size = new System.Drawing.Size(300, 636);
            this.splitContainer2.SplitterDistance = 532;
            this.splitContainer2.TabIndex = 3;
            // 
            // tileSheetPictureBox
            // 
            this.tileSheetPictureBox.Enabled = false;
            this.tileSheetPictureBox.Location = new System.Drawing.Point(0, 0);
            this.tileSheetPictureBox.Name = "tileSheetPictureBox";
            this.tileSheetPictureBox.Size = new System.Drawing.Size(300, 532);
            this.tileSheetPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.tileSheetPictureBox.TabIndex = 8;
            this.tileSheetPictureBox.TabStop = false;
            this.tileSheetPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.tileSheetPictureBox_Paint);
            this.tileSheetPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tileSheetPictureBox_MouseClick);
            // 
            // currentLayerLabel
            // 
            this.currentLayerLabel.AutoSize = true;
            this.currentLayerLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.currentLayerLabel.Location = new System.Drawing.Point(0, 2);
            this.currentLayerLabel.Name = "currentLayerLabel";
            this.currentLayerLabel.Size = new System.Drawing.Size(73, 13);
            this.currentLayerLabel.TabIndex = 1;
            this.currentLayerLabel.Text = "Current Layer:";
            // 
            // currentLayerCheckedListBox
            // 
            this.currentLayerCheckedListBox.BackColor = System.Drawing.Color.White;
            this.currentLayerCheckedListBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.currentLayerCheckedListBox.Enabled = false;
            this.currentLayerCheckedListBox.FormattingEnabled = true;
            this.currentLayerCheckedListBox.Location = new System.Drawing.Point(0, 15);
            this.currentLayerCheckedListBox.Name = "currentLayerCheckedListBox";
            this.currentLayerCheckedListBox.ScrollAlwaysVisible = true;
            this.currentLayerCheckedListBox.Size = new System.Drawing.Size(300, 19);
            this.currentLayerCheckedListBox.TabIndex = 6;
            // 
            // currentTileModeLabel
            // 
            this.currentTileModeLabel.AutoSize = true;
            this.currentTileModeLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.currentTileModeLabel.Location = new System.Drawing.Point(0, 34);
            this.currentTileModeLabel.Name = "currentTileModeLabel";
            this.currentTileModeLabel.Size = new System.Drawing.Size(94, 13);
            this.currentTileModeLabel.TabIndex = 5;
            this.currentTileModeLabel.Text = "Current Tile Mode:";
            // 
            // currentTileModeUpDown
            // 
            this.currentTileModeUpDown.AutoSize = true;
            this.currentTileModeUpDown.BackColor = System.Drawing.Color.White;
            this.currentTileModeUpDown.CausesValidation = false;
            this.currentTileModeUpDown.Cursor = System.Windows.Forms.Cursors.Default;
            this.currentTileModeUpDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.currentTileModeUpDown.Enabled = false;
            this.currentTileModeUpDown.Items.Add("Passable");
            this.currentTileModeUpDown.Items.Add("Impassable (Red)");
            this.currentTileModeUpDown.Items.Add("Door tile (Magenta)");
            this.currentTileModeUpDown.Items.Add("Enemy start position tile (Yellow)");
            this.currentTileModeUpDown.Items.Add("Entrance tile (Green)");
            this.currentTileModeUpDown.Items.Add("Player start position tile (Blue)");
            this.currentTileModeUpDown.Location = new System.Drawing.Point(0, 47);
            this.currentTileModeUpDown.Name = "currentTileModeUpDown";
            this.currentTileModeUpDown.ReadOnly = true;
            this.currentTileModeUpDown.Size = new System.Drawing.Size(300, 20);
            this.currentTileModeUpDown.TabIndex = 4;
            this.currentTileModeUpDown.Text = "Passable";
            // 
            // currentTileSheetLabel
            // 
            this.currentTileSheetLabel.AutoSize = true;
            this.currentTileSheetLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.currentTileSheetLabel.Location = new System.Drawing.Point(0, 67);
            this.currentTileSheetLabel.Name = "currentTileSheetLabel";
            this.currentTileSheetLabel.Size = new System.Drawing.Size(95, 13);
            this.currentTileSheetLabel.TabIndex = 3;
            this.currentTileSheetLabel.Text = "Current Tile Sheet:";
            // 
            // currentTileSheetUpDown
            // 
            this.currentTileSheetUpDown.BackColor = System.Drawing.Color.White;
            this.currentTileSheetUpDown.Cursor = System.Windows.Forms.Cursors.Default;
            this.currentTileSheetUpDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.currentTileSheetUpDown.Enabled = false;
            this.currentTileSheetUpDown.Location = new System.Drawing.Point(0, 80);
            this.currentTileSheetUpDown.Name = "currentTileSheetUpDown";
            this.currentTileSheetUpDown.ReadOnly = true;
            this.currentTileSheetUpDown.Size = new System.Drawing.Size(300, 20);
            this.currentTileSheetUpDown.TabIndex = 2;
            this.currentTileSheetUpDown.SelectedItemChanged += new System.EventHandler(this.currentTileSheetUpDown_SelectedItemChanged);
            // 
            // mapDisplayTimer
            // 
            this.mapDisplayTimer.Enabled = true;
            this.mapDisplayTimer.Interval = 17;
            this.mapDisplayTimer.Tick += new System.EventHandler(this.mapDisplayTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tile Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tileSheetPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTileSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem magentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cyanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileBrushToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x16ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tileCoordinatesStatusLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private XNA.MapDisplay mapDisplay;
        private System.Windows.Forms.PictureBox tileSheetPictureBox;
        private System.Windows.Forms.Timer mapDisplayTimer;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label currentTileModeLabel;
        private System.Windows.Forms.DomainUpDown currentTileModeUpDown;
        private System.Windows.Forms.Label currentTileSheetLabel;
        private System.Windows.Forms.ToolStripMenuItem removeLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTileSheetToolStripMenuItem;
        private System.Windows.Forms.Label currentLayerLabel;
        private System.Windows.Forms.DomainUpDown currentTileSheetUpDown;
        private System.Windows.Forms.CheckedListBox currentLayerCheckedListBox;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom50ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom75ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom100ToolStripMenuItem;
    }
}

