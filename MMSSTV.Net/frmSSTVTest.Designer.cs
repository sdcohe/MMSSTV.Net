
namespace MMSSTV.Net
{
    partial class frmMain
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
            this.picRXImage = new System.Windows.Forms.PictureBox();
            this.picSync = new System.Windows.Forms.PictureBox();
            this.picTXImage = new System.Windows.Forms.PictureBox();
            this.picWaterfall = new System.Windows.Forms.PictureBox();
            this.picLevel = new System.Windows.Forms.PictureBox();
            this.picSpectrum = new System.Windows.Forms.PictureBox();
            this.tmrDoJob = new System.Windows.Forms.Timer(this.components);
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overlayImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFromRXImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastToTxImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumFFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumFMDecoderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSync = new System.Windows.Forms.TabPage();
            this.tabPageRX = new System.Windows.Forms.TabPage();
            this.tabPageTX = new System.Windows.Forms.TabPage();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHistoryLast = new System.Windows.Forms.Button();
            this.btnHistoryFirst = new System.Windows.Forms.Button();
            this.btnHistoryNext = new System.Windows.Forms.Button();
            this.btnHistoryPrevious = new System.Windows.Forms.Button();
            this.picHistory = new System.Windows.Forms.PictureBox();
            this.grpRX = new System.Windows.Forms.GroupBox();
            this.btnSlant = new System.Windows.Forms.Button();
            this.chkLMS = new System.Windows.Forms.CheckBox();
            this.chkAFC = new System.Windows.Forms.CheckBox();
            this.lblFSKId = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.btnRX = new System.Windows.Forms.Button();
            this.cboRxMode = new System.Windows.Forms.ComboBox();
            this.grpTX = new System.Windows.Forms.GroupBox();
            this.lblTX = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnTune = new System.Windows.Forms.Button();
            this.btnTX = new System.Windows.Forms.Button();
            this.cboTxMode = new System.Windows.Forms.ComboBox();
            this.pnlLabels = new System.Windows.Forms.Panel();
            this.lbl2300 = new System.Windows.Forms.Label();
            this.lbl1900 = new System.Windows.Forms.Label();
            this.lbl1500 = new System.Windows.Forms.Label();
            this.lbl1200 = new System.Windows.Forms.Label();
            this.HistoryImageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picRXImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSync)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTXImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWaterfall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpectrum)).BeginInit();
            this.mnuMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageSync.SuspendLayout();
            this.tabPageRX.SuspendLayout();
            this.tabPageTX.SuspendLayout();
            this.tabPageHistory.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHistory)).BeginInit();
            this.grpRX.SuspendLayout();
            this.grpTX.SuspendLayout();
            this.pnlLabels.SuspendLayout();
            this.SuspendLayout();
            // 
            // picRXImage
            // 
            this.picRXImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picRXImage.Location = new System.Drawing.Point(3, 3);
            this.picRXImage.Margin = new System.Windows.Forms.Padding(0);
            this.picRXImage.Name = "picRXImage";
            this.picRXImage.Size = new System.Drawing.Size(321, 297);
            this.picRXImage.TabIndex = 1;
            this.picRXImage.TabStop = false;
            // 
            // picSync
            // 
            this.picSync.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSync.Location = new System.Drawing.Point(3, 3);
            this.picSync.Margin = new System.Windows.Forms.Padding(0);
            this.picSync.Name = "picSync";
            this.picSync.Size = new System.Drawing.Size(321, 297);
            this.picSync.TabIndex = 2;
            this.picSync.TabStop = false;
            // 
            // picTXImage
            // 
            this.picTXImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTXImage.Location = new System.Drawing.Point(3, 3);
            this.picTXImage.Margin = new System.Windows.Forms.Padding(0);
            this.picTXImage.Name = "picTXImage";
            this.picTXImage.Size = new System.Drawing.Size(321, 297);
            this.picTXImage.TabIndex = 3;
            this.picTXImage.TabStop = false;
            // 
            // picWaterfall
            // 
            this.picWaterfall.Location = new System.Drawing.Point(376, 159);
            this.picWaterfall.Name = "picWaterfall";
            this.picWaterfall.Size = new System.Drawing.Size(256, 34);
            this.picWaterfall.TabIndex = 5;
            this.picWaterfall.TabStop = false;
            this.picWaterfall.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picWaterfall_MouseDown);
            this.picWaterfall.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picWaterfall_MouseMove);
            // 
            // picLevel
            // 
            this.picLevel.Location = new System.Drawing.Point(353, 49);
            this.picLevel.Name = "picLevel";
            this.picLevel.Size = new System.Drawing.Size(21, 144);
            this.picLevel.TabIndex = 6;
            this.picLevel.TabStop = false;
            // 
            // picSpectrum
            // 
            this.picSpectrum.Location = new System.Drawing.Point(376, 49);
            this.picSpectrum.Name = "picSpectrum";
            this.picSpectrum.Size = new System.Drawing.Size(256, 105);
            this.picSpectrum.TabIndex = 7;
            this.picSpectrum.TabStop = false;
            this.picSpectrum.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picSpectrum_MouseDown);
            this.picSpectrum.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picSpectrum_MouseMove);
            // 
            // tmrDoJob
            // 
            this.tmrDoJob.Interval = 200;
            this.tmrDoJob.Tick += new System.EventHandler(this.tmrDoJob_Tick);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(639, 24);
            this.mnuMain.TabIndex = 9;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem,
            this.overlayImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.loadImageToolStripMenuItem.Text = "Load Image";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // overlayImageToolStripMenuItem
            // 
            this.overlayImageToolStripMenuItem.Name = "overlayImageToolStripMenuItem";
            this.overlayImageToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.overlayImageToolStripMenuItem.Text = "Overlay Image";
            this.overlayImageToolStripMenuItem.Click += new System.EventHandler(this.overlayImageToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyFromRXImageToolStripMenuItem,
            this.pastToTxImageToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyFromRXImageToolStripMenuItem
            // 
            this.copyFromRXImageToolStripMenuItem.Name = "copyFromRXImageToolStripMenuItem";
            this.copyFromRXImageToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.copyFromRXImageToolStripMenuItem.Text = "Copy from RX image";
            this.copyFromRXImageToolStripMenuItem.Click += new System.EventHandler(this.copyFromRXImageToolStripMenuItem_Click);
            // 
            // pastToTxImageToolStripMenuItem
            // 
            this.pastToTxImageToolStripMenuItem.Name = "pastToTxImageToolStripMenuItem";
            this.pastToTxImageToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.pastToTxImageToolStripMenuItem.Text = "Paste to Tx image";
            this.pastToTxImageToolStripMenuItem.Click += new System.EventHandler(this.pasteToTxImageToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spectrumOffToolStripMenuItem,
            this.spectrumFFTToolStripMenuItem,
            this.spectrumFMDecoderToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // spectrumOffToolStripMenuItem
            // 
            this.spectrumOffToolStripMenuItem.Name = "spectrumOffToolStripMenuItem";
            this.spectrumOffToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.spectrumOffToolStripMenuItem.Text = "Spectrum Off";
            this.spectrumOffToolStripMenuItem.Click += new System.EventHandler(this.spectrumOffToolStripMenuItem_Click);
            // 
            // spectrumFFTToolStripMenuItem
            // 
            this.spectrumFFTToolStripMenuItem.Name = "spectrumFFTToolStripMenuItem";
            this.spectrumFFTToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.spectrumFFTToolStripMenuItem.Text = "Spectrum FFT";
            this.spectrumFFTToolStripMenuItem.Click += new System.EventHandler(this.spectrumFFTToolStripMenuItem_Click);
            // 
            // spectrumFMDecoderToolStripMenuItem
            // 
            this.spectrumFMDecoderToolStripMenuItem.Name = "spectrumFMDecoderToolStripMenuItem";
            this.spectrumFMDecoderToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.spectrumFMDecoderToolStripMenuItem.Text = "Spectrum FM Decoder";
            this.spectrumFMDecoderToolStripMenuItem.Click += new System.EventHandler(this.spectrumFMDecoderToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptionSetup});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // mnuOptionSetup
            // 
            this.mnuOptionSetup.Name = "mnuOptionSetup";
            this.mnuOptionSetup.Size = new System.Drawing.Size(171, 22);
            this.mnuOptionSetup.Text = "Setup SSTV Engine";
            this.mnuOptionSetup.Click += new System.EventHandler(this.mnuOptionSetup_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageSync);
            this.tabControl.Controls.Add(this.tabPageRX);
            this.tabControl.Controls.Add(this.tabPageTX);
            this.tabControl.Controls.Add(this.tabPageHistory);
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(335, 329);
            this.tabControl.TabIndex = 10;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageSync
            // 
            this.tabPageSync.Controls.Add(this.picSync);
            this.tabPageSync.Location = new System.Drawing.Point(4, 22);
            this.tabPageSync.Name = "tabPageSync";
            this.tabPageSync.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSync.Size = new System.Drawing.Size(327, 303);
            this.tabPageSync.TabIndex = 0;
            this.tabPageSync.Text = "Sync";
            this.tabPageSync.UseVisualStyleBackColor = true;
            // 
            // tabPageRX
            // 
            this.tabPageRX.Controls.Add(this.picRXImage);
            this.tabPageRX.Location = new System.Drawing.Point(4, 22);
            this.tabPageRX.Name = "tabPageRX";
            this.tabPageRX.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRX.Size = new System.Drawing.Size(327, 303);
            this.tabPageRX.TabIndex = 1;
            this.tabPageRX.Text = "RX";
            this.tabPageRX.UseVisualStyleBackColor = true;
            // 
            // tabPageTX
            // 
            this.tabPageTX.Controls.Add(this.picTXImage);
            this.tabPageTX.Location = new System.Drawing.Point(4, 22);
            this.tabPageTX.Name = "tabPageTX";
            this.tabPageTX.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTX.Size = new System.Drawing.Size(327, 303);
            this.tabPageTX.TabIndex = 2;
            this.tabPageTX.Text = "TX";
            this.tabPageTX.UseVisualStyleBackColor = true;
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.panel1);
            this.tabPageHistory.Controls.Add(this.picHistory);
            this.tabPageHistory.Location = new System.Drawing.Point(4, 22);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHistory.Size = new System.Drawing.Size(327, 303);
            this.tabPageHistory.TabIndex = 3;
            this.tabPageHistory.Text = "History";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnHistoryLast);
            this.panel1.Controls.Add(this.btnHistoryFirst);
            this.panel1.Controls.Add(this.btnHistoryNext);
            this.panel1.Controls.Add(this.btnHistoryPrevious);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 277);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 23);
            this.panel1.TabIndex = 1;
            // 
            // btnHistoryLast
            // 
            this.btnHistoryLast.Location = new System.Drawing.Point(102, 0);
            this.btnHistoryLast.Name = "btnHistoryLast";
            this.btnHistoryLast.Size = new System.Drawing.Size(33, 23);
            this.btnHistoryLast.TabIndex = 3;
            this.btnHistoryLast.Text = ">>";
            this.btnHistoryLast.UseVisualStyleBackColor = true;
            this.btnHistoryLast.Click += new System.EventHandler(this.btnHistoryLast_Click);
            // 
            // btnHistoryFirst
            // 
            this.btnHistoryFirst.Location = new System.Drawing.Point(15, -1);
            this.btnHistoryFirst.Name = "btnHistoryFirst";
            this.btnHistoryFirst.Size = new System.Drawing.Size(33, 23);
            this.btnHistoryFirst.TabIndex = 2;
            this.btnHistoryFirst.Text = "<<";
            this.btnHistoryFirst.UseVisualStyleBackColor = true;
            this.btnHistoryFirst.Click += new System.EventHandler(this.btnHistoryFirst_Click);
            // 
            // btnHistoryNext
            // 
            this.btnHistoryNext.Location = new System.Drawing.Point(74, 0);
            this.btnHistoryNext.Name = "btnHistoryNext";
            this.btnHistoryNext.Size = new System.Drawing.Size(33, 23);
            this.btnHistoryNext.TabIndex = 1;
            this.btnHistoryNext.Text = ">";
            this.btnHistoryNext.UseVisualStyleBackColor = true;
            this.btnHistoryNext.Click += new System.EventHandler(this.btnHistoryNext_Click);
            // 
            // btnHistoryPrevious
            // 
            this.btnHistoryPrevious.Location = new System.Drawing.Point(44, 0);
            this.btnHistoryPrevious.Name = "btnHistoryPrevious";
            this.btnHistoryPrevious.Size = new System.Drawing.Size(33, 23);
            this.btnHistoryPrevious.TabIndex = 0;
            this.btnHistoryPrevious.Text = "<";
            this.btnHistoryPrevious.UseVisualStyleBackColor = true;
            this.btnHistoryPrevious.Click += new System.EventHandler(this.btnHistoryPrevious_Click);
            // 
            // picHistory
            // 
            this.picHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picHistory.Location = new System.Drawing.Point(3, 3);
            this.picHistory.Name = "picHistory";
            this.picHistory.Size = new System.Drawing.Size(321, 297);
            this.picHistory.TabIndex = 0;
            this.picHistory.TabStop = false;
            // 
            // grpRX
            // 
            this.grpRX.Controls.Add(this.btnSlant);
            this.grpRX.Controls.Add(this.chkLMS);
            this.grpRX.Controls.Add(this.chkAFC);
            this.grpRX.Controls.Add(this.lblFSKId);
            this.grpRX.Controls.Add(this.btnCopy);
            this.grpRX.Controls.Add(this.btnSync);
            this.grpRX.Controls.Add(this.btnRX);
            this.grpRX.Controls.Add(this.cboRxMode);
            this.grpRX.Location = new System.Drawing.Point(350, 200);
            this.grpRX.Name = "grpRX";
            this.grpRX.Size = new System.Drawing.Size(282, 77);
            this.grpRX.TabIndex = 11;
            this.grpRX.TabStop = false;
            this.grpRX.Text = "RX";
            // 
            // btnSlant
            // 
            this.btnSlant.Location = new System.Drawing.Point(90, 47);
            this.btnSlant.Name = "btnSlant";
            this.btnSlant.Size = new System.Drawing.Size(36, 23);
            this.btnSlant.TabIndex = 7;
            this.btnSlant.Text = "SLT";
            this.btnSlant.UseVisualStyleBackColor = true;
            this.btnSlant.Click += new System.EventHandler(this.btnSlant_Click);
            // 
            // chkLMS
            // 
            this.chkLMS.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkLMS.AutoSize = true;
            this.chkLMS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkLMS.Location = new System.Drawing.Point(47, 48);
            this.chkLMS.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.chkLMS.Name = "chkLMS";
            this.chkLMS.Size = new System.Drawing.Size(39, 23);
            this.chkLMS.TabIndex = 6;
            this.chkLMS.Text = "LMS";
            this.chkLMS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkLMS.UseVisualStyleBackColor = true;
            this.chkLMS.CheckedChanged += new System.EventHandler(this.chkLMS_CheckedChanged);
            this.chkLMS.Paint += new System.Windows.Forms.PaintEventHandler(this.ToggleButton_Paint);
            // 
            // chkAFC
            // 
            this.chkAFC.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAFC.AutoSize = true;
            this.chkAFC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAFC.Location = new System.Drawing.Point(7, 48);
            this.chkAFC.Name = "chkAFC";
            this.chkAFC.Size = new System.Drawing.Size(37, 23);
            this.chkAFC.TabIndex = 5;
            this.chkAFC.Text = "AFC";
            this.chkAFC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkAFC.UseVisualStyleBackColor = true;
            this.chkAFC.CheckedChanged += new System.EventHandler(this.chkAFC_CheckedChanged);
            this.chkAFC.Paint += new System.Windows.Forms.PaintEventHandler(this.ToggleButton_Paint);
            // 
            // lblFSKId
            // 
            this.lblFSKId.AutoSize = true;
            this.lblFSKId.Location = new System.Drawing.Point(132, 53);
            this.lblFSKId.Name = "lblFSKId";
            this.lblFSKId.Size = new System.Drawing.Size(38, 13);
            this.lblFSKId.TabIndex = 4;
            this.lblFSKId.Text = "FSKID";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(232, 20);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(44, 23);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(175, 20);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(40, 23);
            this.btnSync.TabIndex = 2;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnRX
            // 
            this.btnRX.Location = new System.Drawing.Point(135, 20);
            this.btnRX.Name = "btnRX";
            this.btnRX.Size = new System.Drawing.Size(39, 23);
            this.btnRX.TabIndex = 1;
            this.btnRX.Text = "RX";
            this.btnRX.UseVisualStyleBackColor = true;
            this.btnRX.Click += new System.EventHandler(this.btnRX_Click);
            // 
            // cboRxMode
            // 
            this.cboRxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRxMode.FormattingEnabled = true;
            this.cboRxMode.Location = new System.Drawing.Point(7, 20);
            this.cboRxMode.Name = "cboRxMode";
            this.cboRxMode.Size = new System.Drawing.Size(121, 21);
            this.cboRxMode.TabIndex = 0;
            this.cboRxMode.SelectedIndexChanged += new System.EventHandler(this.cboRxMode_SelectedIndexChanged);
            // 
            // grpTX
            // 
            this.grpTX.Controls.Add(this.lblTX);
            this.grpTX.Controls.Add(this.btnLoad);
            this.grpTX.Controls.Add(this.btnPaste);
            this.grpTX.Controls.Add(this.btnTune);
            this.grpTX.Controls.Add(this.btnTX);
            this.grpTX.Controls.Add(this.cboTxMode);
            this.grpTX.Location = new System.Drawing.Point(350, 283);
            this.grpTX.Name = "grpTX";
            this.grpTX.Size = new System.Drawing.Size(282, 73);
            this.grpTX.TabIndex = 12;
            this.grpTX.TabStop = false;
            this.grpTX.Text = "TX";
            // 
            // lblTX
            // 
            this.lblTX.AutoSize = true;
            this.lblTX.Location = new System.Drawing.Point(7, 47);
            this.lblTX.Name = "lblTX";
            this.lblTX.Size = new System.Drawing.Size(0, 13);
            this.lblTX.TabIndex = 7;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(232, 43);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(44, 23);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(232, 17);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(44, 23);
            this.btnPaste.TabIndex = 6;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnTune
            // 
            this.btnTune.Location = new System.Drawing.Point(175, 17);
            this.btnTune.Name = "btnTune";
            this.btnTune.Size = new System.Drawing.Size(40, 23);
            this.btnTune.TabIndex = 5;
            this.btnTune.Text = "1750";
            this.btnTune.UseVisualStyleBackColor = true;
            this.btnTune.Click += new System.EventHandler(this.btnTune_Click);
            this.btnTune.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTune_MouseDown);
            // 
            // btnTX
            // 
            this.btnTX.Location = new System.Drawing.Point(135, 17);
            this.btnTX.Name = "btnTX";
            this.btnTX.Size = new System.Drawing.Size(39, 23);
            this.btnTX.TabIndex = 4;
            this.btnTX.Text = "TX";
            this.btnTX.UseVisualStyleBackColor = true;
            this.btnTX.Click += new System.EventHandler(this.btnTX_Click);
            // 
            // cboTxMode
            // 
            this.cboTxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTxMode.FormattingEnabled = true;
            this.cboTxMode.Location = new System.Drawing.Point(6, 19);
            this.cboTxMode.Name = "cboTxMode";
            this.cboTxMode.Size = new System.Drawing.Size(121, 21);
            this.cboTxMode.TabIndex = 1;
            this.cboTxMode.SelectedIndexChanged += new System.EventHandler(this.cboTxMode_SelectedIndexChanged);
            // 
            // pnlLabels
            // 
            this.pnlLabels.Controls.Add(this.lbl2300);
            this.pnlLabels.Controls.Add(this.lbl1900);
            this.pnlLabels.Controls.Add(this.lbl1500);
            this.pnlLabels.Controls.Add(this.lbl1200);
            this.pnlLabels.Location = new System.Drawing.Point(376, 27);
            this.pnlLabels.Name = "pnlLabels";
            this.pnlLabels.Size = new System.Drawing.Size(256, 20);
            this.pnlLabels.TabIndex = 13;
            // 
            // lbl2300
            // 
            this.lbl2300.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl2300.AutoSize = true;
            this.lbl2300.Location = new System.Drawing.Point(117, 4);
            this.lbl2300.Name = "lbl2300";
            this.lbl2300.Size = new System.Drawing.Size(31, 13);
            this.lbl2300.TabIndex = 3;
            this.lbl2300.Text = "2300";
            // 
            // lbl1900
            // 
            this.lbl1900.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl1900.AutoSize = true;
            this.lbl1900.Location = new System.Drawing.Point(80, 4);
            this.lbl1900.Name = "lbl1900";
            this.lbl1900.Size = new System.Drawing.Size(31, 13);
            this.lbl1900.TabIndex = 2;
            this.lbl1900.Text = "1900";
            // 
            // lbl1500
            // 
            this.lbl1500.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl1500.AutoSize = true;
            this.lbl1500.Location = new System.Drawing.Point(40, 4);
            this.lbl1500.Name = "lbl1500";
            this.lbl1500.Size = new System.Drawing.Size(31, 13);
            this.lbl1500.TabIndex = 1;
            this.lbl1500.Text = "1500";
            // 
            // lbl1200
            // 
            this.lbl1200.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl1200.AutoSize = true;
            this.lbl1200.Location = new System.Drawing.Point(3, 4);
            this.lbl1200.Name = "lbl1200";
            this.lbl1200.Size = new System.Drawing.Size(31, 13);
            this.lbl1200.TabIndex = 0;
            this.lbl1200.Text = "1200";
            // 
            // HistoryImageList
            // 
            this.HistoryImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.HistoryImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.HistoryImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 368);
            this.Controls.Add(this.pnlLabels);
            this.Controls.Add(this.grpTX);
            this.Controls.Add(this.grpRX);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.picSpectrum);
            this.Controls.Add(this.picLevel);
            this.Controls.Add(this.picWaterfall);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.frmSSTVTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picRXImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSync)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTXImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWaterfall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpectrum)).EndInit();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageSync.ResumeLayout(false);
            this.tabPageRX.ResumeLayout(false);
            this.tabPageTX.ResumeLayout(false);
            this.tabPageHistory.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHistory)).EndInit();
            this.grpRX.ResumeLayout(false);
            this.grpRX.PerformLayout();
            this.grpTX.ResumeLayout(false);
            this.grpTX.PerformLayout();
            this.pnlLabels.ResumeLayout(false);
            this.pnlLabels.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picRXImage;
        private System.Windows.Forms.PictureBox picSync;
        private System.Windows.Forms.PictureBox picTXImage;
        private System.Windows.Forms.PictureBox picWaterfall;
        private System.Windows.Forms.PictureBox picLevel;
        private System.Windows.Forms.PictureBox picSpectrum;
        private System.Windows.Forms.Timer tmrDoJob;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overlayImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFromRXImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastToTxImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrumOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrumFFTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrumFMDecoderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionSetup;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSync;
        private System.Windows.Forms.TabPage tabPageRX;
        private System.Windows.Forms.TabPage tabPageTX;
        private System.Windows.Forms.GroupBox grpRX;
        private System.Windows.Forms.GroupBox grpTX;
        private System.Windows.Forms.Label lblFSKId;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnRX;
        private System.Windows.Forms.ComboBox cboRxMode;
        private System.Windows.Forms.CheckBox chkAFC;
        private System.Windows.Forms.CheckBox chkLMS;
        private System.Windows.Forms.Label lblTX;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnTune;
        private System.Windows.Forms.Button btnTX;
        private System.Windows.Forms.ComboBox cboTxMode;
        private System.Windows.Forms.Panel pnlLabels;
        private System.Windows.Forms.Label lbl2300;
        private System.Windows.Forms.Label lbl1900;
        private System.Windows.Forms.Label lbl1500;
        private System.Windows.Forms.Label lbl1200;
        private System.Windows.Forms.Button btnSlant;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.PictureBox picHistory;
        private System.Windows.Forms.ImageList HistoryImageList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnHistoryLast;
        private System.Windows.Forms.Button btnHistoryFirst;
        private System.Windows.Forms.Button btnHistoryNext;
        private System.Windows.Forms.Button btnHistoryPrevious;
    }
}

