// A csharp class that allows updating of screen resolutions for an infinite number of screens
// Project Page: https://github.com/whitehotlogic/Resolution Author: Dustin Johnson, whitehotlogic
//
// SOURCES:
// Reference Project Page: http://www.codeproject.com/Articles/6810/Dynamic-Screen-Resolution Author: sreejith ss nair
// Reference Project Page: http://www.codeproject.com/Articles/36664/Changing-Display-Settings-Programmatically Author: Mohammad Elsheimy


using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;










[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct DEVMODE
{
    // You can define the following constant
    // but OUTSIDE the structure because you know
    // that size and layout of the structure is very important
    // CCHDEVICENAME = 32 = 0x50
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string dmDeviceName;
    // In addition you can define the last character array
    // as following:
    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    //public Char[] dmDeviceName;

    // After the 32-bytes array
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 dmSpecVersion;

    [MarshalAs(UnmanagedType.U2)]
    public UInt16 dmDriverVersion;

    [MarshalAs(UnmanagedType.U2)]
    public UInt16 dmSize;

    [MarshalAs(UnmanagedType.U2)]
    public UInt16 dmDriverExtra;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmFields;

    public POINTL dmPosition;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmDisplayOrientation;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmDisplayFixedOutput;

    [MarshalAs(UnmanagedType.I2)]
    public Int16 dmColor;

    [MarshalAs(UnmanagedType.I2)]
    public Int16 dmDuplex;

    [MarshalAs(UnmanagedType.I2)]
    public Int16 dmYResolution;

    [MarshalAs(UnmanagedType.I2)]
    public Int16 dmTTOption;

    [MarshalAs(UnmanagedType.I2)]
    public Int16 dmCollate;

    // CCHDEVICENAME = 32 = 0x50
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string dmFormName;
    // Also can be defined as
    //[MarshalAs(UnmanagedType.ByValArray, 
    //    SizeConst = 32, ArraySubType = UnmanagedType.U1)]
    //public Byte[] dmFormName;

    [MarshalAs(UnmanagedType.U2)]
    public UInt16 dmLogPixels;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmBitsPerPel;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmPelsWidth;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmPelsHeight;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmDisplayFlags;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmDisplayFrequency;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmICMMethod;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmICMIntent;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmMediaType;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmDitherType;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmReserved1;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmReserved2;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmPanningWidth;

    [MarshalAs(UnmanagedType.U4)]
    public UInt32 dmPanningHeight;

    /// <summary>
    /// Initializes the structure variables.
    /// </summary>
    public void Initialize()
    {
        this.dmDeviceName = new string(new char[32]);
        this.dmFormName = new string(new char[32]);
        this.dmSize = (ushort)Marshal.SizeOf(this);
    }
}





[StructLayout(LayoutKind.Sequential)]
public struct POINTL
{
    [MarshalAs(UnmanagedType.I4)]
    public int x;
    [MarshalAs(UnmanagedType.I4)]
    public int y;
}





[Flags()]
public enum ChangeDisplaySettingsFlags : uint
{
    CDS_NONE = 0,
    CDS_UPDATEREGISTRY = 0x00000001,
    CDS_TEST = 0x00000002,
    CDS_FULLSCREEN = 0x00000004,
    CDS_GLOBAL = 0x00000008,
    CDS_SET_PRIMARY = 0x00000010,
    CDS_VIDEOPARAMETERS = 0x00000020,
    CDS_ENABLE_UNSAFE_MODES = 0x00000100,
    CDS_DISABLE_UNSAFE_MODES = 0x00000200,
    CDS_RESET = 0x40000000,
    CDS_RESET_EX = 0x20000000,
    CDS_NORESET = 0x10000000
}





class User_32
{
    /*
	[DllImport("user32.dll")]
	public static extern int EnumDisplaySettingsEx (string deviceName, int modeNum, ref DEVMODE devMode, int flags );     
    */

    /*
    [DllImport("user32.dll")]
	public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);
    */


    [DllImport("user32.dll")]
    public static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, ChangeDisplaySettingsFlags dwflags, IntPtr lParam);



    [DllImport("User32.dll", SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean EnumDisplaySettings(
            byte[] lpszDeviceName,  // display device name (\\.\DISPLAY1, \\.\DISPLAY2, etc.)
            [param: MarshalAs(UnmanagedType.U4)]
                Int32 iModeNum,         // graphics mode
            [In, Out]
                ref DEVMODE lpDevMode       // graphics mode settings
            );

    public const int ENUM_CURRENT_SETTINGS = -1;
	public const int CDS_UPDATEREGISTRY = 0x01;
	public const int CDS_TEST = 0x02;
	public const int DISP_CHANGE_SUCCESSFUL = 0;
	public const int DISP_CHANGE_RESTART = 1;
	public const int DISP_CHANGE_FAILED = -1;
}


namespace Resolution
{

    // need this class to convert the lpszDeviceName into a byte array readable by EnumDisplaySettings
    public static class StringExtensions
    {
        public static byte[] ToLPTStr(string str)
        {
            var lptArray = new byte[str.Length + 1];
            var index = 0;
            foreach (char c in str.ToCharArray())
                lptArray[index++] = Convert.ToByte(c);
            lptArray[index] = Convert.ToByte('\0');
            return lptArray;
        }
    }


    class CResolution
	{


		public CResolution(uint w,uint h,Screen s)
		{
            Screen screen = s;
            uint iWidth = w;
            uint iHeight = h;
            

            DEVMODE dm = new DEVMODE();
            dm.Initialize();
			//dm.dmDeviceName = new String (new char[32]);
			//dm.dmFormName = new String (new char[32]);
			//dm.dmSize = (ushort)Marshal.SizeOf (dm);

            // make sure we can read current monitor settings
			if (User_32.EnumDisplaySettings (StringExtensions.ToLPTStr(screen.DeviceName), User_32.ENUM_CURRENT_SETTINGS, ref dm))
            {

				
				dm.dmPelsWidth = iWidth;
				dm.dmPelsHeight = iHeight;

                // check to see if requested graphics mode can be set
                int iRet = User_32.ChangeDisplaySettingsEx (screen.DeviceName, ref dm, IntPtr.Zero, ChangeDisplaySettingsFlags.CDS_TEST, IntPtr.Zero);
                if (iRet == User_32.DISP_CHANGE_FAILED)
				{
					MessageBox.Show("DISP_CHANGE_FAILED", "FAILURE",MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
				else
				{
                    //have to use ChangeDisplaySettingsEx() instead of ChangeDisplaySettings() so we can specify device name (for multi-monitor support)
                    iRet = User_32.ChangeDisplaySettingsEx(screen.DeviceName, ref dm, IntPtr.Zero, ChangeDisplaySettingsFlags.CDS_UPDATEREGISTRY, IntPtr.Zero);
                    
                    switch (iRet) 
					{
						case User_32.DISP_CHANGE_SUCCESSFUL:
						{
							break;

							//successfull change
						}
						case User_32.DISP_CHANGE_RESTART:
						{
							
							MessageBox.Show("Resolution Change Requires Reboot.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
							break;
							//windows 9x series you have to restart
						}
						default:
						{
							
							MessageBox.Show("Something has gone horribly wrong.","Unknown Failure",MessageBoxButtons.OK,MessageBoxIcon.Information);
							break;
							//failed to change
						}
					}
				}
				
			}
		}

	}
}
