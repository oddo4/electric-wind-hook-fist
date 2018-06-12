using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mice.Classes
{
    class Win32API
    {
        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);

        public const UInt32 SPI_SETMOUSESPEED = 0x0071;
        public const UInt32 SPI_SETWHEELSCROLLLINES = 0x0069;
        public const UInt32 SPI_SETDOUBLECLICKTIME = 0x0020;

        public void SetMouseSpeed(int speedValue)
        {
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, (uint)speedValue, 0);
        }
        public void SetScrollWheelSpeed(int speedValue)
        {
            SystemParametersInfo(SPI_SETWHEELSCROLLLINES, (uint)speedValue, 0, 0);
        }
        public void SetDoubleClickSpeed(int speedValue)
        {
            SystemParametersInfo(SPI_SETDOUBLECLICKTIME, (uint)speedValue, 0, 0);
        }
    }
}
