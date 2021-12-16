using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MMSSTV.Net
{
    class PlaybackDevices
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
        public struct WAVEOUTCAPS
        {
            public short wMid;
            public short wPid;
            public int vDriverVersion;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;

            public int dwFormats;
            public short wChannels;
            public short wReserved;
            public int dwSupport;
        }

        [DllImport("winmm.dll", EntryPoint = "waveOutGetNumDevs")]
        public static extern int waveOutGetNumDevs();

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutGetDevCaps(int hwo, ref WAVEOUTCAPS pwoc, uint cbwoc);

        ArrayList outputDeviceList = new ArrayList();
        //using to store all sound playback devices strings 

        public int Count
        //to return total sound playback devices found
        {
            get { return outputDeviceList.Count; }
        }

        public string this[int indexer]
        //return specific sound playback device name
        {
            get { return (string)outputDeviceList[indexer]; }
        }

        public PlaybackDevices() //fill sound output devices array
        {
            int waveOutDevicesCount = waveOutGetNumDevs(); //get total

            for (int uDeviceID = 0; uDeviceID < waveOutDevicesCount; uDeviceID++)
            {
                WAVEOUTCAPS waveOutCaps = new WAVEOUTCAPS();
                waveOutGetDevCaps(uDeviceID, ref waveOutCaps, (uint)Marshal.SizeOf(typeof(WAVEOUTCAPS)));
                outputDeviceList.Add(waveOutCaps.szPname.Trim());
            }
        }

    }
}
