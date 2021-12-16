using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MMSSTV.Net
{
    class RecordingDevices
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct WAVEINCAPS
        {
            public ushort wMid;
            public ushort wPid;
            public uint vDriverVersion;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string szPname;

            public uint dwFormats;
            public ushort wChannels;
            public ushort wReserved1;
        }

        [DllImport("winmm.dll", EntryPoint = "waveInGetNumDevs")]
        public static extern int waveInGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "waveInGetDevCaps")]
        public static extern int waveInGetDevCapsA(int uDeviceID,
                             ref WAVEINCAPS lpCaps, int uSize);

        ArrayList inputDeviceList = new ArrayList();
        //using to store all sound recording devices strings 

        public int Count
        //to return total sound recording devices found
        {
            get { return inputDeviceList.Count; }
        }
        public string this[int indexer]
        //return specific sound recording device name
        {
            get { return (string)inputDeviceList[indexer]; }
        }
        public RecordingDevices() //fill sound recording devices array
        {
            int waveInDevicesCount = waveInGetNumDevs(); //get total
            if (waveInDevicesCount > 0)
            {
                for (int uDeviceID = 0; uDeviceID < waveInDevicesCount; uDeviceID++)
                {
                    WAVEINCAPS waveInCaps = new WAVEINCAPS();
                    waveInGetDevCapsA(uDeviceID, ref waveInCaps,
                                      Marshal.SizeOf(typeof(WAVEINCAPS)));
                    inputDeviceList.Add(waveInCaps.szPname.Trim());
                    //clean garbage
                }
            }
        }
    }
}
