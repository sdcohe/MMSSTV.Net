using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static MMSSTV.Net.SSTVENGWrapper;

namespace MMSSTV.Net
{
    public partial class frmMain : Form
    {
        // bitmap handles
        private IntPtr hRX;
        private IntPtr hSync;
        private IntPtr hTX;
        private IntPtr hbSpec;
        private IntPtr hbWater;
        private IntPtr hbLevel;

        // drawing rectangles
        private RECT m_rcDest;
        private RECT m_rcRX;
        private RECT m_rcTX;
        private RECT m_rcSpec;

        //private int m_ClipTimer;

        private int m_TuneFreq;
        private int m_SpecBase;
        private int m_SpecWidth;
        private int m_NotchFreq;

        private int m_JobCode;
        private int m_RxCode;
        private int m_TxCode;
        private int m_TuneCode;

        private int m_RXY;
        private int m_TXY;
        private int m_SendY = -1;
        private bool m_TxPic = false;

        private int m_RxMode;
        private int m_TxMode;

        private int m_RxClock;

        private string HistoryFolderPath;
        private List<FileSystemInfo> historyFiles;
        private int m_historyFileIndex = -1;

        public frmMain()
        {
            InitializeComponent();

            // set window title
            string sstvVersion = mmsGetVersion();
            string language = (mmsLanguage(-1) == 0) ? "Japanese" : "English";
            this.Text = string.Format("MMSSTV.Net Library Test - SSTV Version {0} {1}", sstvVersion, language);

            // initialize rectangle to center RX image
            m_rcDest.Left = (picRXImage.ClientRectangle.Width - 320) / 2;
            m_rcDest.Top = (picRXImage.ClientRectangle.Height - 256) / 2;
            m_rcDest.Right = m_rcDest.Left + 320;
            m_rcDest.Bottom = m_rcDest.Top + 256;

            // start off with 320x256 image size
            m_rcRX.Left = 0;
            m_rcRX.Top = 0;
            m_rcRX.Right = 320;
            m_rcRX.Bottom = 256;

            //m_rcSync.Left = 0;
            //m_rcSync.Top = 0;
            //m_rcSync.Right = 320;
            //m_rcSync.Bottom = 256;

            m_rcTX.Left = 0;
            m_rcTX.Top = 0;
            m_rcTX.Right = 320;
            m_rcTX.Bottom = 256;

            // get the size of the spectrum display so we can draw lines for the frequencies
            m_rcSpec.Top = 0;
            m_rcSpec.Left = 0;
            m_rcSpec.Right = picSpectrum.Width;
            m_rcSpec.Bottom = picSpectrum.Height;

            // initialize history
            HistoryFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "SSTV-ReceivedImages");
            historyFiles = GetHistoryFilesList();

            // get the input and output sound devices. Not currently used.
            Console.WriteLine("Input devices");
            RecordingDevices recDev = new RecordingDevices();
            for (int i = 0; i < recDev.Count; i++)
            {
                Console.WriteLine(recDev[i]);
            }

            Console.WriteLine("\nOutput devices");
            PlaybackDevices playDev = new PlaybackDevices();
            for (int i = 0; i < playDev.Count; i++)
            {
                Console.WriteLine(playDev[i]);
            }
        }

        // If you want to receive Windows messages you can override WndProc to watch for messages.
        //  You also need to specify a message number when you call mmsCreate()
        //      example is commented out in the form load event
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == WM_MMSSTV)
        //    {
        //        //Console.WriteLine(m.ToString());
        //        if ((Messages)m.WParam == Messages.TXMS_SCAN)
        //        {
        //            if ((int)m.LParam == 0)
        //            {
        //                Console.WriteLine("Scan end");
        //            }
        //            else if ((int)m.LParam == 1)
        //            {
        //                Console.WriteLine("Scan start");
        //            }
        //        }
        //    }

        //    base.WndProc(ref m);
        //}

        private void frmSSTVTest_Load(object sender, EventArgs e)
        {
            // initialize some stuff 
            m_SpecBase = 700;
            m_SpecWidth = 2000;
            m_TuneFreq = 1750;
            m_JobCode = 0;
            tabControl.SelectedIndex = 1;

            try
            {
                FillModeComboBoxes();
                DispClockInfo();

                // Use the 1st example to not receive Windows messages. Use teh 2nd and uncomment the
                //  WndProc code above to receive messages from MMSSTV engine
                int isCreated = SSTVENGWrapper.mmsCreate(this.Handle, 0);
                // pass a message number to receive Windows messages.
                //int isCreated = SSTVENGWrapper.mmsCreate(this.Handle, WM_MMSSTV);

                if (isCreated != 0)
                {
                    mmsSetSpecRange(m_SpecBase, m_SpecWidth);
                    DrawPanelLabels();
                    m_TxCode = mmsSetTxControl(-1);
                    m_RxCode = mmsSetRxControl(-1);
                    m_TuneCode = mmsSetTuneControl(-1);
                    m_JobCode = 0;

                    // Crate the bitmaps the engine will use for drawing
                    // these are the recommended sizes for Rx, Tx, and sync DIBs
                    hRX = mmsCreateDIB(800, 616);
                    mmsFillDIB(hRX, mmsGetClearColor());
                    hSync = mmsCreateDIB(320, 256);
                    hTX = mmsCreateDIB(800, 616);
                    hbSpec = mmsCreateDIB(picSpectrum.Width, picSpectrum.Height);
                    hbLevel = mmsCreateDIB(picLevel.Width, picLevel.Height);
                    hbWater = mmsCreateDIB(picWaterfall.Width, picWaterfall.Height);

                    // tell the engine to use the bitmaps we jhust created and start up
                    int ret = mmsSetBitmap(hRX, hSync, hTX);
                    ret = mmsSetTuneBitmap(hbSpec, hbWater, hbLevel);
                    mmsStart();

                    // set the toggle buttons
                    if ((m_RxCode & rcAFC) != 0) { chkAFC.Checked = true; }
                    if ((m_RxCode & rcLMS) != 0) { chkLMS.Checked = true; }
                    btnSlant.Enabled = false;

                    // initialize the controls on the form
                    UpdateRxMode();
                    UpdateTxMode();
                    UpdateUIButton();
                    UpdateVM();
                    DispClockInfo();

                    // start the timer that will periodically call mmsDoJob() and respond to what it returns
                    tmrDoJob.Enabled = true;
                }
                else
                {
                    Console.WriteLine("Create() failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception {0}", ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // stop the timer and MMSSTV engine
            tmrDoJob.Enabled = false;
            mmsStop();

            // clean up the bitmap resources we allocated
            mmsDeleteDIB(hRX);
            mmsDeleteDIB(hSync);
            mmsDeleteDIB(hTX);
            mmsDeleteDIB(hbSpec);
            mmsDeleteDIB(hbWater);
            mmsDeleteDIB(hbLevel);

            // delete the engine
            mmsDelete();
        }

        private void tmrDoJob_Tick(object sender, EventArgs e)
        {
            int retValue = mmsDoJob(1);

            // Received image has been updated. Show it in the RX picturebox
            //  and update the display
            if ((retValue & jcUpdateRX) != 0)
            {
                paintRxTxImages();
                if (((m_RxCode & rcAutoSlant) != 0) && (mmsGetSampleFreq(2) != m_RxClock))
                {
                    DispClockInfo();
                }
            }

            // We are in RX mode. Enable the slant button if applicable
            if ((retValue & jcRX) != 0)
            {
                m_RXY = mmsGetPos(0);
                if ((m_RXY > 16) && ((m_RxCode & rcUseBuff) != 0))
                    btnSlant.Enabled = true;
                else
                    btnSlant.Enabled = false;
            }

            // We have started to receive an image. Show what mode we are receiving
            if ((retValue & jcStartRX) != 0)
            {
                m_RxMode = mmsGetMode(0);
                cboRxMode.SelectedItem = mmsGetModeName(m_RxMode);
                UpdateRxMode();
            }

            // We are TXing. Show the progress on the image being sent
            if ((retValue & jcTX) != 0)
            {
                m_TXY = mmsGetPos(1);
                if (tabControl.SelectedIndex == 2) DrawCursor(true);
            }

            // We received an FSKID
            if ((retValue & jcFSKID) != 0)
            {
                lblFSKId.Text = mmsGetFSKID();
            }

            // The spectrum bitmaps heve been updated. Display the waterfall, spectrum, and level
            if ((retValue & jcSpec) != 0)
            {
                //Console.WriteLine("jcSpec");
                Image imgWaterfall = Image.FromHbitmap(hbWater);
                picWaterfall.Image = imgWaterfall;

                Image imgLevel = Image.FromHbitmap(hbLevel);
                picLevel.Image = imgLevel;

                Image imgSpectrum = Image.FromHbitmap(hbSpec);
                picSpectrum.Image = imgSpectrum;

                // Example of using the engine to update the display. I chose to use the Image class instead. This is left
                //      as an example.
                //mmsDrawDIBdc(picWaterfall.CreateGraphics().GetHdc(), (IntPtr)null, hbWater, (IntPtr)null, HALFTONE);
                //mmsDrawDIBdc(picLevel.CreateGraphics().GetHdc(), (IntPtr)null, hbLevel, (IntPtr)null, HALFTONE);
                //mmsDrawDIBdc(picSpectrum.CreateGraphics().GetHdc(), (IntPtr)null, hbSpec, (IntPtr)null, HALFTONE);
            }

            if ((retValue & jcRadioFreq) != 0)
            {
                Console.WriteLine("jcRadioFreq");
            }

            if ((retValue & jcDetRep) != 0)
            {
                Console.WriteLine("jcDetRep");
            }

            if ((retValue & jcDetTone) != 0)
            {
                Console.WriteLine("jcDetTone");
            }

            if ((retValue & jcTick) != 0)
            {
                Console.WriteLine("jcTick");
            }

            // If the return from mmsDoJob() has changed from the last pass through here
            //  we can check to see if we started or stopped receiving or transmitting an image
            if (retValue != m_JobCode)
            {
                int atrs = m_JobCode & (jcRX | jcTX);   // previously RX or TX;
                int ntrs = retValue & (jcRX | jcTX);    // currently RX or TX;

                m_JobCode = retValue;

                //// if start RX display the mode
                //if ((m_JobCode & jcStartRX) != 0) {
                //    m_RxMode = mmsGetMode(0);
                //    cboRxMode.SelectedIndex = m_RxMode;
                //    UpdateRxMode();
                //}

                // if RX or TX status changed
                if (atrs != ntrs) {

                    UpdateUIButton();

                    // if changed from rx to not rx 
                    if (((atrs & jcRX) != 0) && ((ntrs & jcRX) == 0)) {
                        // if we got at least a 65% full image, copy it to the clipboard,
                        //      save it in the history, and set same mode as RX for TX
                        if (m_RXY > (m_rcRX.Bottom * 0.65))
                        {
                            CopyImageToClipboard();
                            SaveImageToHistory();

                            // set the TX mode to be the same as the image we just received
                            cboTxMode.SelectedIndex = m_RxMode;
                        }
                    }

                    // change from TX to not TXing
                    if (((atrs & jcTX) != 0) && ((ntrs & jcTX) == 0)) {
                        DrawCursor(false);
                    }
                }

                // get the FSKID if available
                if ((m_JobCode & jcFSKID) != 0) lblFSKId.Text = mmsGetFSKID();
            }
        }

        private void mnuOptionSetup_Click(object sender, EventArgs e)
        {
            // Display the options dialog provided by the engine. If we get back a successful
            //  return update some variables and the display
            mmsSetOptionTitle("MMSSTV.Net Setup");
            if (mmsOption() != 0)
            {
                m_TxCode = mmsSetTxControl(-1);
                m_RxCode = mmsSetRxControl(-1);
                UpdateUIButton();
                DispClockInfo();
            }
        }

        private void paintRxTxImages()
        {
            // display the sync, rx and tx bitmaps in picture boxes
            Image imgSync = Image.FromHbitmap(hSync);
            picSync.Image = imgSync;

            Image imgRX = Image.FromHbitmap(hRX);
            picRXImage.Image = imgRX;

            Image imgTX = Image.FromHbitmap(hTX);
            picTXImage.Image = imgTX;
        }

        private void ToggleButton_Paint(object sender, PaintEventArgs e)
        {
            // paint event so we can make our checkbox look like a toggle button. Not needed
            //  to use the engine. Just eye candy.
            CheckBox myCheckbox = (CheckBox)sender;
            Rectangle borderRectangle = myCheckbox.ClientRectangle;
            if (myCheckbox.Checked)
            {
                ControlPaint.DrawBorder3D(e.Graphics, borderRectangle,
                    Border3DStyle.Sunken);
            }
            else
            {
                ControlPaint.DrawBorder3D(e.Graphics, borderRectangle,
                    Border3DStyle.Raised);
            }
        }

        private void UpdateRxMode()
        {
            int mode;
            mode = cboRxMode.SelectedIndex;
            m_RxMode = mode;
            uint isize = mmsGetImageSize(mode);
            m_rcRX.Right = (int)isize & 65535;
            m_rcRX.Bottom = (int)(isize / 65536);
        }

        private void UpdateTxMode()
        {
            int mode = cboRxMode.SelectedIndex;
            m_TxMode = mode;
            uint isize = mmsGetImageSize(mode);
            m_rcTX.Right = (int)(isize & 65535);
            m_rcTX.Bottom = (int)(isize / 65536);
            uint msize = mmsGetModeSize(mode);
            int mtime = mmsGetModeLength(mode) / 1000;
            lblTX.Text = (msize & 65535) + "x" + (msize / 65536) + " (" + (mtime) + "s)";
        }

        private void cboRxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_RxMode != cboRxMode.SelectedIndex)
            {
                UpdateRxMode();
                if ((m_JobCode & jcRX) != 0)
                {
                    mmsStopScan(1);
                    mmsStartScan(cboRxMode.SelectedIndex);
                }
            }
        }

        private void DispClockInfo()
        {
            int fm = mmsGetSampleFreq(0);
            int fr = mmsGetSampleFreq(2);
            m_RxClock = fr;
            grpRX.Text = "RX-" + GetFreqString(fr) + "  Mas-" + GetFreqString(fm);
            int ft = mmsGetSampleFreq(1) + fm;
            grpTX.Text = "TX-" + GetFreqString(ft);
        }

        private string GetFreqString(int fq)
        {
            string sA = Convert.ToString((int)fq / 100) + ".";
            fq = (fq % 100) + 100;
            string sB = Convert.ToString(fq);
            return sA + sB.Substring(sB.Length - 2);
        }

        private void UpdateUIButton()
        {
            if ((m_JobCode & jcTX) != 0)
            {
                btnTX.Text = "Stop";
                cboTxMode.Enabled = false;
                btnTune.Enabled = false;
            }
            else
            {
                btnTX.Text = "TX";
                cboTxMode.Enabled = true;
                btnTune.Enabled = true;
            }

            if ((m_JobCode & jcRX) != 0)
            {
                btnRX.Text = "Stop";
                btnSync.Enabled = true;
            }
            else
            {
                btnRX.Text = "RX";
                btnSync.Enabled = false;
            }

            btnTune.Text = m_TuneFreq.ToString();

            if ((m_RxCode & rcFSKID) != 0)
            {
                lblFSKId.Enabled = true;
            }
            else
            {
                lblFSKId.Enabled = false;
            }
        }

        private void UpdateVM()
        {
            spectrumOffToolStripMenuItem.Checked = false;
            spectrumFFTToolStripMenuItem.Checked = false;
            spectrumFMDecoderToolStripMenuItem.Checked = false;

            switch ((int)((m_TuneCode / 16) & 3))
            {
                case 1:
                    spectrumFFTToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    spectrumFMDecoderToolStripMenuItem.Checked = true;
                    break;
                default:
                    spectrumOffToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyImageToClipboard();
        }

        private void CopyImageToClipboard()
        {
            // make a copy of the DIB and place it on the clipboard
            IntPtr hbRx;
            hbRx = mmsCreateDIB(m_rcRX.Right, m_rcRX.Bottom);
            mmsDrawDIBbmp(hbRx, ref m_rcRX, hRX, ref m_rcRX, HALFTONE);
            mmsCopyDIB(hbRx);
            mmsDeleteDIB(hbRx);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadImageFromFile();
        }

        private void LoadImageFromFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.FileName = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tabControl.SelectedIndex = 2;
                    Cursor.Current = Cursors.WaitCursor;
                    // This looks like a lot of extra (unnecessary) bitmap manipulation
                    //  .NET bitmaps support a lot more file formats, so use Bitmap to load the file
                    Bitmap bmp = new Bitmap(openFileDialog.FileName);

                    // MMSSTV requires a DIB so we use its conversion function to create a DIB from the loaded bitmap
                    RECT rc = new RECT();
                    if (mmsGetDIBSize(ref rc, bmp.GetHbitmap()) != 0)
                    {
                        mmsDrawDIBbmp(hTX, ref m_rcTX, bmp.GetHbitmap(), ref rc, HALFTONE);

                        // Now convert the DIB back to a bitmap. This ensures we have the same size bitmap as MMSSTV
                        picTXImage.Image = Image.FromHbitmap(hTX);
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }
        private void LoadAndOverlayImage()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.FileName = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tabControl.SelectedIndex = 2;

                    Cursor.Current = Cursors.WaitCursor;
                    Bitmap bmp = new Bitmap(openFileDialog.FileName);
                    RECT rc = new RECT();

                    if (mmsGetDIBSize(ref rc, bmp.GetHbitmap()) != 0)
                    {
                        int X = (m_rcTX.Right - rc.Right) / 2;
                        int Y = (m_rcTX.Bottom - rc.Bottom) / 2;
                        mmsDrawTransDIBbmp(hTX, X, Y, bmp.GetHbitmap(), -1);
                        picTXImage.Image = Image.FromHbitmap(hTX);
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            mmsReSync();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            PasteTxImage();
        }

        private void PasteTxImage()
        {
            Cursor.Current = Cursors.WaitCursor;
            IntPtr hbTx = mmsPasteDIB();
            if (hbTx != null)
            {
                //m_MainPage = 3;
                tabControl.SelectedIndex = 2;

                RECT rc = new RECT();
                if (mmsGetDIBSize(ref rc, hbTx) != 0)
                {
                    mmsDrawDIBbmp(hTX, ref m_rcTX, hbTx, ref rc, HALFTONE);
                    Image img = Image.FromHbitmap(hTX);
                    picTXImage.Image = img;
                }
                mmsDeleteDIB(hbTx);
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnRX_Click(object sender, EventArgs e)
        {
            if ((m_JobCode & jcRX) != 0)
            {
                mmsStopScan(-1);
            }
            else
            {
                mmsStartScan(cboRxMode.SelectedIndex);
            }
        }

        private void btnTune_Click(object sender, EventArgs e)
        {
            if ((m_JobCode & jcTX) != 0)
            {
                mmsSendStop();
                if (m_TxPic)
                {
                    mmsSendTone(m_TuneFreq);
                    m_TxPic = false;
                }
            }
            else
            {
                mmsSendTone(m_TuneFreq);
                m_TxPic = false;
            }
        }

        private void btnTune_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (m_TuneFreq == 1750)
                    m_TuneFreq = 1200;
                else
                    m_TuneFreq = 1750;
                UpdateUIButton();
            }
        }

        private void btnTX_Click(object sender, EventArgs e)
        {
            if ((m_JobCode & jcTX) != 0)
            {
                mmsSendStop();
            }
            else
            {
                mmsSendPic(cboTxMode.SelectedIndex);
                m_TxPic = true;
            }
        }

        private void cboTxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_TxMode != cboTxMode.SelectedIndex)
            {
                UpdateTxMode();
            }
        }

        private void DrawSpecFreq(int fq, Label lbl)
        {
            string sA = fq.ToString();
            int W = pnlLabels.Width;
            int X = ((fq - m_SpecBase) * W) / m_SpecWidth;
            lbl.Left = X;
            int FW = lbl.Width;
            lbl.Left = X - (FW / 2);
        }

        private void DrawPanelLabels()
        {
            DrawSpecFreq(1200, lbl1200);
            DrawSpecFreq(1500, lbl1500);
            DrawSpecFreq(1900, lbl1900);
            DrawSpecFreq(2300, lbl2300);
        }

        private void picSpectrum_MouseDown(object sender, MouseEventArgs e)
        {
            ToggleNotchFrequency(e);
        }

        private void ToggleNotchFrequency(MouseEventArgs e)
        {
            int freq;
            if (e.Button == MouseButtons.Left)
            {
                freq = m_SpecBase + ((e.X * m_SpecWidth) / m_rcSpec.Right);
                m_NotchFreq = freq;
            }
            else if (mmsSetNotch(-1) != 0)
            {
                freq = 0;
            }
            else
            {
                freq = m_NotchFreq;
            }

            freq = mmsSetNotch(freq);
        }

        private void picSpectrum_MouseMove(object sender, MouseEventArgs e)
        {
            SetNotchFrequency(e);
        }

        private void SetNotchFrequency(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_NotchFreq = m_SpecBase + ((e.X * m_SpecWidth) / m_rcSpec.Right);
                mmsSetNotch(m_NotchFreq);
            }
        }

        private void picWaterfall_MouseDown(object sender, MouseEventArgs e)
        {
            ToggleNotchFrequency(e);
        }

        private void picWaterfall_MouseMove(object sender, MouseEventArgs e)
        {
            SetNotchFrequency(e);
        }

        private void chkAFC_CheckedChanged(object sender, EventArgs e)
        {
            m_RxCode = m_RxCode ^ SSTVENGWrapper.rcAFC;
            if (chkAFC.Checked) m_RxCode = m_RxCode | SSTVENGWrapper.rcAFC;
            mmsSetRxControl(m_RxCode);
        }

        private void chkLMS_CheckedChanged(object sender, EventArgs e)
        {
            m_RxCode = m_RxCode ^ rcLMS;
            if (chkLMS.Checked) m_RxCode = m_RxCode | rcLMS;
            mmsSetRxControl(m_RxCode);
        }

        private void btnSlant_Click(object sender, EventArgs e)
        {
            mmsCorrectSlant();
            DispClockInfo();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImageFromFile();
        }

        private void overlayImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadAndOverlayImage();
        }

        private void copyFromRXImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyImageToClipboard();
        }

        private void pasteToTxImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteTxImage();
        }

        private void spectrumOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_TuneCode = m_TuneCode & (ucTYPE ^ -1);
            mmsSetTuneControl(m_TuneCode);
            UpdateVM();
        }

        private void spectrumFFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: why does the example change ucGain value
            m_TuneCode = m_TuneCode & (ucTYPE ^ -1);
            //m_TuneCode = m_TuneCode & ((ucTYPE | ucGAIN) ^ -1);

            // TODO: define constants
            //m_TuneCode = m_TuneCode | 0x12;
            m_TuneCode = m_TuneCode | 0x10;

            mmsSetTuneControl(m_TuneCode);
            UpdateVM();
        }

        private void spectrumFMDecoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: why does the example change ucGain value
            m_TuneCode = m_TuneCode & (ucTYPE ^ -1);
            //m_TuneCode = m_TuneCode & ((ucTYPE | ucGAIN) ^ -1);

            // TODO: define constants
            //m_TuneCode = m_TuneCode | 0x25;
            m_TuneCode = m_TuneCode | 0x20;
            mmsSetTuneControl(m_TuneCode);
            UpdateVM();
        }

        private void DrawCursorSub(int Y) {
            if (Y >= 0 ) {
                m_SendY = Y;
                Y = Y + m_rcDest.Top;
                Console.WriteLine("Draw a line from {0} to {1} on line {2}", m_rcDest.Left, m_rcDest.Right, Y);
                for(int x = m_rcDest.Left; x < m_rcDest.Right; x++)
                {
                    Bitmap pic = (Bitmap)picTXImage.Image;
                    Color inv = pic.GetPixel(x, Y);
                    inv = Color.FromArgb(inv.A, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(x, Y, inv);
                }
                picTXImage.Refresh();
            }
        }

        private void DrawCursor(bool sw)
        {
            // DEBUG: starts at line 20?
            if ((tabControl.SelectedIndex == 2) && picTXImage.Visible && m_TxPic)
            {
                if (sw)
                {
                    int sy = m_TXY * 256 / m_rcTX.Bottom;
                    if ((m_SendY < 0) || (m_SendY != sy))
                    {
                        // Draw 2 inverted lines. The 1st one inverts the previous line back to normal.
                        //  The second one inverts the current line
                        if (m_SendY >= 0) DrawCursorSub(m_SendY);
                        DrawCursorSub(sy);
                    }
                }
                else
                {
                    paintRxTxImages();
                    m_SendY = -1;
                }

            }
        }

        private List<FileSystemInfo> GetHistoryFilesList()
        {
            // Create the history folder if it doesn't already exist
            DirectoryInfo dir = new DirectoryInfo(HistoryFolderPath);
            if (!dir.Exists) {
                dir.Create();
            }

            List<FileSystemInfo> files = new List<FileSystemInfo>();
            FileSystemInfo[] infos = dir.GetFileSystemInfos();
            foreach(FileSystemInfo fi in infos)
            {
                if (fi is FileInfo)
                {
                    files.Add(fi);
                }
            }

            files.Sort((x, y) => DateTime.Compare(x.CreationTime, y.CreationTime)); 
            
            return files;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageHistory)
            {
                // load history files - order by date
                historyFiles = GetHistoryFilesList();

                // display most recent or stay on currently selected file
                if (historyFiles.Count > 0)
                {
                    //if (m_historyFileIndex == -1)
                    //{
                    // show last file in the list (most recent)
                    m_historyFileIndex = historyFiles.Count - 1;
                    //}
                    //else
                    //{
                    //    // show the currently indexed file in the list, if it exists
                    //    if (m_historyFileIndex > historyFiles.Count)
                    //    {
                    //        // show the last file in the list
                    //        m_historyFileIndex = historyFiles.Count - 1;
                    //    }
                    //}

                    DisplayHistoryFile();
                }
            }    
        }

        private void EnableDisableHistoryNavigation()
        {
            btnHistoryFirst.Enabled = (m_historyFileIndex > 0);
            btnHistoryNext.Enabled = (m_historyFileIndex < historyFiles.Count - 1);
            btnHistoryPrevious.Enabled = (m_historyFileIndex > 0);
            btnHistoryLast.Enabled = (m_historyFileIndex < historyFiles.Count - 1);
        }

        private void DisplayHistoryFile()
        {
            Bitmap bmp = new Bitmap(historyFiles[m_historyFileIndex].FullName);
            picHistory.Image = bmp;

            // enable/disable navigation buttons
            EnableDisableHistoryNavigation();
        }

        private void btnHistoryFirst_Click(object sender, EventArgs e)
        {
            m_historyFileIndex = 0;
            DisplayHistoryFile();
        }

        private void btnHistoryLast_Click(object sender, EventArgs e)
        {
            m_historyFileIndex = historyFiles.Count - 1;
            DisplayHistoryFile();
        }

        private void btnHistoryPrevious_Click(object sender, EventArgs e)
        {
            m_historyFileIndex -= 1;
            DisplayHistoryFile();
        }

        private void btnHistoryNext_Click(object sender, EventArgs e)
        {
            m_historyFileIndex += 1;
            DisplayHistoryFile();
        }

        private void FillModeComboBoxes()
        {
            // fill the RX and TX mode combo boxes
            for (int i = 0; i < 99; i++)
            {
                string mode = mmsGetModeName(i);
                if (mode != "")
                {
                    cboRxMode.Items.Add(mode);
                    cboTxMode.Items.Add(mode);
                }

            }
            if (cboRxMode.Items.Contains("Scottie 1"))
            {
                cboRxMode.SelectedItem = "Scottie 1";
                cboTxMode.SelectedItem = "Scottie 1";
            }
        }
        private void SaveImageToHistory()
        {
            // save image in history
            string fileName = Path.Combine(HistoryFolderPath,
                String.Format("{0}-{1}.jpg", DateTime.Now.ToString("yyyyMMdd-HHmmss"), cboRxMode.SelectedItem).Replace(' ', '-'));

            // save as correct size.
            IntPtr dibToSave = mmsCreateDIB(m_rcRX.Width, m_rcRX.Height);
            mmsDrawDIBbmp(dibToSave, ref m_rcRX, hRX, ref m_rcRX, HALFTONE);
            mmsSaveDIB(fileName, dibToSave);
            mmsDeleteDIB(dibToSave);

            historyFiles = GetHistoryFilesList();
        }

    }
}
