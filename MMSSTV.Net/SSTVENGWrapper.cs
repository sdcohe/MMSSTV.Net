using System;
using System.Runtime.InteropServices;
using System.Text;

// Some references I used in creating this file
// http://www.pinvoke.net/
// https://www.displayfusion.com/Discussions/View/converting-c-data-types-to-c/?ID=38db6001-45e5-41a3-ab39-8004450204b3

namespace MMSSTV.Net
{
    #region RECT
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

        public int X
        {
            get { return Left; }
            set { Right -= (Left - value); Left = value; }
        }

        public int Y
        {
            get { return Top; }
            set { Bottom -= (Top - value); Top = value; }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set { X = value.X; Y = value.Y; }
        }

        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT)obj);
            else if (obj is System.Drawing.Rectangle)
                return Equals(new RECT((System.Drawing.Rectangle)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return ((System.Drawing.Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }
    #endregion

    class SSTVENGWrapper
    {
        #region Definitions and Enumerations
        public const int WM_USER = 0x400;
        public const int WM_MMSSTV = WM_USER + 1009;

        // mmsDoJob()
        public const int jcUpdateRX  = 0x00000001;
        public const int jcRX        = 0x00000002;
        public const int jcStartRX   = 0x00000004;
        public const int jcTX        = 0x00000008;
        public const int jcFSKID     = 0x00000010;
        public const int jcSpec      = 0x00000020;
        public const int jcRadioFreq = 0x00000040;
        public const int jcDetRep    = 0x00000080;
        public const int jcDetTone   = 0x00000100;
        public const int jcTick      = 0x00000200;

        // mmsSetRxControl()
		public const int rcFSKID       = 0x00000001;
		public const int rcAFC         = 0x00000002;
		public const int rcLMS         = 0x00000004;
		public const int rcAutoRestart = 0x00000008;
		public const int rcAutoStop    = 0x00000010;
		public const int rcAutoSync    = 0x00000020;
		public const int rcAutoSlant   = 0x00000040;
		public const int rcAutoClear   = 0x00000080;
		public const int rcFirstSync   = 0x00030000;
		public const int rcUseBuff	   = 0x00300000;
		public const int rcDemType	   = 0x03000000;
		public const int rcBPF		   = 0x30000000;

        // mmsSetTxControl()
		public const int tcFSKID = 0x00000001;
		public const int tcCWID  = 0x00000002;
		public const int tcECHO  = 0x00030000;

        // mmsSetTuneControl()
		public const int ucGAIN     = 0x00000007;
		public const int ucTYPE     = 0x00000030;
		public const int ucRESP     = 0x00000300;
		public const int ucPERSIST  = 0x00003000;
		public const int ucAGC      = 0x00010000;
		public const int ucLSYNC    = 0x00100000;
		public const int ucPRIORITY = 0x30000000;

        // mmsDisableOption()
		public const int doPTT       = 0x00000001;
		public const int doRadio     = 0x00000002;
		public const int doVari      = 0x00000004;
		public const int doFSKID     = 0x00000008;
		public const int doCWID      = 0x00000010;
		public const int doSpecCol   = 0x00000020;
		public const int doWaterCol  = 0x00000040;
		public const int doAutoSlant = 0x00000080;
		public const int doAutoClear = 0x00000100;

        public const int HALFTONE = 4;

        // Messages
        public enum Messages {
            TXMS_WAVE,
            TXMS_PTT,
            TXMS_SCAN,
            TXMS_FSKID,
            TXMS_DETREP,
            TXMS_CWID,
            TXMS_CLOCKCHANGE,
        };
        #endregion

        public SSTVENGWrapper() { }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetVersion",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr private_mmsGetVersion();

        // Get the version string and copy it to a managed string
        public static string mmsGetVersion()
        {
            IntPtr ret = private_mmsGetVersion();
            return CopyStringPointerToManagedString(ret);
        }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsStrCopy",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern long mmsStrCopy(StringBuilder pDest, int cbSize, IntPtr pSrc);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsCreate",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int mmsCreate(IntPtr hWnd, uint msg);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDelete",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsDelete();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsStart",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsStart();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsStop",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsStop();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsOption",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int mmsOption();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetBitmap",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int mmsSetBitmap(IntPtr hRX, IntPtr hSync, IntPtr hTX);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetTuneBitmap",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int mmsSetTuneBitmap(IntPtr hbSpec, IntPtr hbWater, IntPtr hbLevel);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsCreateDIB",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mmsCreateDIB(Int32 width, Int32 height);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDeleteDIB",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsDeleteDIB(IntPtr hbDest);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDrawDIBdc",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsDrawDIBdc(IntPtr hDC, ref RECT prcDest, IntPtr hbSrc, ref RECT prcSrc, int smode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDrawDIBwnd",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsDrawDIBwnd(IntPtr hWnd, ref RECT prcDest, IntPtr hbSrc, ref RECT prcSrc, int smode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDoJob",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsDoJob(Int32 sw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsLanguage",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsLanguage(Int32 lang);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetOptionTitle",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSetOptionTitle([MarshalAs(UnmanagedType.LPUTF8Str)] string pTitle);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetLevel",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetLevel();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetModeName",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr private_mmsGetModeName(Int32 mode);
        public static string mmsGetModeName(Int32 mode)
        {
            IntPtr ret = private_mmsGetModeName(mode);
            if (ret != (IntPtr)0)
            {
                return CopyStringPointerToManagedString(ret);
            }
            else
            {
                return "";
            }
        }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetModeSize",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 mmsGetModeSize(Int32 mode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetImageSize",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 mmsGetImageSize(Int32 mode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetModeLength",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetModeLength(Int32 mode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsStartScan",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsStartScan(Int32 mode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsStopScan",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsStopScan(Int32 enb);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetMode",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetMode(Int32 tx);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSendPic",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSendPic(Int32 mode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSendTone",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSendTone(Int32 freq);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSendStop",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSendStop();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetPTT",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSetPTT(Int32 tx);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetPos",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetPos(Int32 tx);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetLMS",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsSetLMS(Int32 sw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetRxControl",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsSetRxControl(Int32 sw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetTxControl",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsSetTxControl(Int32 sw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetTuneControl",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsSetTuneControl(Int32 sw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetAFCFQ",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 mmsGetAFCFQ();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetWWVFQ",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetWWVFQ();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetSpecRange",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSetSpecRange(Int32 fl, Int32 fw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetSampleFreq",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetSampleFreq(Int32 sw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetSampleFreq",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSetSampleFreq(Int32 sw, Int32 freq);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsReSync",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsReSync();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsCorrectSlant",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsCorrectSlant();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsAdjustPhase",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsAdjustPhase(Int32 x, Int32 xw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDisableOption",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsDisableOption(Int32 sw);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetRepeater",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSetRepeater(Int32 sw, Int32 tone, Int32 sense, Int32 t1, Int32 t2, Int32 sq);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetRepSQ",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetRepSQ();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetNotch",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsSetNotch(Int32 freq);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetModeTiming",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 mmsGetModeTiming(Int32 mode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsPasteDIB",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mmsPasteDIB();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsLoadDIB",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mmsLoadDIB([MarshalAs(UnmanagedType.LPUTF8Str)] string pName);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetParent",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mmsSetParent(IntPtr hWnd);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetMessage",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mmsSetMessage(IntPtr hWnd, UInt32 msg);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDrawDIBdc",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsDrawDIBdc(IntPtr hDC, IntPtr prcDest, IntPtr hbSrc, IntPtr prcSrc, UInt32 smode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "private_mmsGetFSKID",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr private_mmsGetFSKID();
        public static string mmsGetFSKID()
        {
            IntPtr ret = private_mmsGetFSKID();
            if (ret != (IntPtr)0)
            {
                return CopyStringPointerToManagedString(ret);
            }
            else
            {
                return "";
            }
        }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "private_mmsSetFSKID",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr private_mmsSetFSKID([MarshalAs(UnmanagedType.LPUTF8Str)] string pCall);
        public static string mmsSetFSKID(string pCall)
        {
            IntPtr ret = private_mmsSetFSKID(pCall);
            if (ret != (IntPtr)0)
            {
                return CopyStringPointerToManagedString(ret);
            }
            else
            {
                return "";
            }
        }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "private_mmsSetCWID",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr private_mmsSetCWID([MarshalAs(UnmanagedType.LPUTF8Str)] string pText);
        public static string mmsSetCWID(string pText)
        {
            IntPtr ret = private_mmsSetCWID(pText);
            if (ret != (IntPtr)0)
            {
                return CopyStringPointerToManagedString(ret);
            }
            else
            {
                return "";
            }
        }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "private_mmsSetPTTPort",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr private_mmsSetPTTPort([MarshalAs(UnmanagedType.LPUTF8Str)] string pCom);
        public static string mmsSetPTTPort(string pCom)
        {
            IntPtr ret = private_mmsSetPTTPort(pCom);
            if (ret != (IntPtr)0)
            {
                return CopyStringPointerToManagedString(ret);
            }
            else
            {
                return "";
            }
        }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "private_mmsSetRadioPort",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr private_mmsSetRadioPort([MarshalAs(UnmanagedType.LPUTF8Str)] string pCom);
        public static string mmsSetRadioPort(string pCom)
        {
            IntPtr ret = private_mmsSetRadioPort(pCom);
            if (ret != (IntPtr)0)
            {
                return CopyStringPointerToManagedString(ret);
            }
            else
            {
                return "";
            }
        }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "private_mmsGetRadioFreq",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr private_mmsGetRadioFreq();
        public static string mmsGetRadioFreq()
        {
            IntPtr ret = private_mmsGetRadioFreq();
            if (ret != (IntPtr)0)
            {
                return CopyStringPointerToManagedString(ret);
            }
            else
            {
                return "";
            }
        }

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetHDC",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        private static extern void mmsSetHDC(IntPtr hSync, IntPtr hSpec, IntPtr hWater, IntPtr hLevel);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsCopyDIB",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsCopyDIB(IntPtr hbSrc);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSendCWID",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSendCWID([MarshalAs(UnmanagedType.LPUTF8Str)] string pText);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetSpec",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetSpec(ref Int32[] pStore);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetSpecDraw",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetSpecDraw(ref Int32[] pStore, Int32 width, Int32 max);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetSpecPersistence",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsGetSpecPersistence(ref Int32[] pStore);

        //Actually, there's no COLORREF structure in native Win32. It is typedef-ed to DWORD, which means that
        //in the managed world its direct counterpart is [System.Int32] (aka int in C#). So, when faced with
        //interop involving COLORREF'S you'd better treat them as int's.Also have in mind that the color components
        //are stored in reverse order, i.e.the Red component is in the lowest-byte. In short, the format is 0x00BBGGRR.
        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetSpecColor",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSetSpecColor(Int32 back, Int32 sig, Int32 persist, Int32 marksync, Int32 marksig);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetWaterColor",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSetWaterColor(Int32 back, Int32 sig);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSetClearColor",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsSetClearColor(Int32 col);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetClearColor",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetClearColor();

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDrawDIBbmp",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsDrawDIBbmp(IntPtr hbDest, ref RECT prcDest, IntPtr hbSrc, ref RECT prcSrc, Int32 smode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDrawDCbmp",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsDrawDCbmp(IntPtr hbDest, ref RECT prcDest, IntPtr hbSrc, ref RECT prcSrc, Int32 smode);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDrawTransDIBdc",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsDrawTransDIBdc(IntPtr hDC, Int32 x, Int32 y, IntPtr hbSrc, Int32 key);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsDrawTransDIBbmp",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsDrawTransDIBbmp(IntPtr hbDest, Int32 x, Int32 y, IntPtr hbSrc, Int32 key);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsSaveDIB",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsSaveDIB([MarshalAs(UnmanagedType.LPUTF8Str)] string pName, IntPtr hbSrc);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsFillDIB",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void mmsFillDIB(IntPtr hbDest, Int32 col);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsGetDIBSize",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsGetDIBSize(ref RECT prc, IntPtr hbSrc);

        [DllImport("SSTVENG.DLL",
            EntryPoint = "mmsStrWCopy",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mmsStrWCopy([MarshalAs(UnmanagedType.LPWStr)] StringBuilder pDest, Int32 cwSize, [MarshalAs(UnmanagedType.LPUTF8Str)] string pSrc);

        // Copy a string pointer to a managed string
        private static string CopyStringPointerToManagedString(IntPtr lngSTR)
        {
            StringBuilder builder = new StringBuilder(256);
            long ret;

            ret = mmsStrCopy(builder, builder.Capacity, lngSTR);
            return builder.ToString().Substring(0, (int)ret);
        }

    }
}
