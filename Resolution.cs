// A csharp class that allows updating of screen resolutions for an infinite number of screens

// Original Project Page: http://www.codeproject.com/Articles/6810/Dynamic-Screen-Resolution Author: sreejith ss nair
// Updated Project Page: https://github.com/whitehotlogic/Resolution Author: Dustin Johnson, whitehotlogic

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct DEVMODE1 
{
	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=32)] public string dmDeviceName;
	public short  dmSpecVersion;
	public short  dmDriverVersion;
	public short  dmSize;
	public short  dmDriverExtra;
	public int    dmFields;

	public short dmOrientation;
	public short dmPaperSize;
	public short dmPaperLength;
	public short dmPaperWidth;

	public short dmScale;
	public short dmCopies;
	public short dmDefaultSource;
	public short dmPrintQuality;
	public short dmColor;
	public short dmDuplex;
	public short dmYResolution;
	public short dmTTOption;
	public short dmCollate;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string dmFormName;
	public short dmLogPixels;
	public short dmBitsPerPel;
	public int   dmPelsWidth;
	public int   dmPelsHeight;

	public int   dmDisplayFlags;
	public int   dmDisplayFrequency;

	public int   dmICMMethod;
	public int   dmICMIntent;
	public int   dmMediaType;
	public int   dmDitherType;
	public int   dmReserved1;
	public int   dmReserved2;

	public int   dmPanningWidth;
	public int   dmPanningHeight;
};

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
	[DllImport("user32.dll")]
	public static extern int EnumDisplaySettings (string deviceName, int modeNum, ref DEVMODE1 devMode );         
	[DllImport("user32.dll")]
	public static extern int ChangeDisplaySettings(ref DEVMODE1 devMode, int flags);
    [DllImport("user32.dll")]
    public static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE1 lpDevMode, IntPtr hwnd, ChangeDisplaySettingsFlags dwflags, IntPtr lParam);


    public const int ENUM_CURRENT_SETTINGS = -1;
	public const int CDS_UPDATEREGISTRY = 0x01;
	public const int CDS_TEST = 0x02;
	public const int DISP_CHANGE_SUCCESSFUL = 0;
	public const int DISP_CHANGE_RESTART = 1;
	public const int DISP_CHANGE_FAILED = -1;
}


namespace Resolution
{
	class CResolution
	{
		public CResolution(int w,int h,Screen s)
		{
            Screen screen = s;
            int iWidth = w;
            int iHeight = h;
            

            DEVMODE1 dm = new DEVMODE1();
			dm.dmDeviceName = new String (new char[32]);
			dm.dmFormName = new String (new char[32]);
			dm.dmSize = (short)Marshal.SizeOf (dm);

            // make sure we can read current monitor settings
			if (0 != User_32.EnumDisplaySettings (null, User_32.ENUM_CURRENT_SETTINGS, ref dm))
            {

				
				dm.dmPelsWidth = iWidth;
				dm.dmPelsHeight = iHeight;

                // send a test to see if we can change the display settings
                int iRet = User_32.ChangeDisplaySettings (ref dm, User_32.CDS_TEST);
                if (iRet == User_32.DISP_CHANGE_FAILED)
				{
					MessageBox.Show("DISP_CHANGE_FAILED", "FAILURE",MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
				else
				{
                    //have to use ChangeDisplaySettingsEx() instead of ChangeDisplaySettings(), or only one monitor will be affected
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
