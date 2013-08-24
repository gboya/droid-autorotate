using MultiMonitorHelper.Common;
using MultiMonitorHelper.Common.Interfaces;

namespace MultiMonitorHelper.DisplayModels.Win7
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using MultiMonitorHelper.Common.Enum;

    public class Win7Display : AbstractDisplay, IDisplay
    {
        /// <summary>
        /// Initialize new instance of Win7Display
        /// </summary>
        public Win7Display(DisplaySettings settings)
        {
            Settings = settings;
        }

        #region IDisplay Members

        /// <summary>
        /// Holds all settings related to a display.
        /// The structure is immutable, so you can not change it directly.
        /// 
        /// You can however call interface specific methods, in order to change some values.
        /// </summary>
        public DisplaySettings Settings { get; private set; }

        /// <summary>
        /// The rotate.
        /// </summary>
        /// <param name="newRotation">
        /// The new rotation.
        /// </param>
        public void Rotate(DisplayRotation newRotation)
        {
            SafeNativeMethods.DEVMODE mode = GetDeviceMode();
            uint temp = mode.dmPelsWidth;
            mode.dmPelsWidth = mode.dmPelsHeight;
            mode.dmPelsHeight = temp;
            mode.dmDisplayOrientation = (uint)newRotation;
            var result = SafeNativeMethods.ChangeDisplaySettings(ref mode, 0);
        }

        /// <summary>
        /// A private helper method used to retrieve current display settings as a DEVMODE object.
        /// </summary>
        /// <remarks>
        /// Internally calls EnumDisplaySettings() native function with the value ENUM_CURRENT_SETTINGS (-1) to retrieve the current settings.
        /// </remarks>
        private static SafeNativeMethods.DEVMODE GetDeviceMode()
        {
            SafeNativeMethods.DEVMODE mode = new SafeNativeMethods.DEVMODE();

            mode.Initialize();

            if (SafeNativeMethods.EnumDisplaySettings(null, SafeNativeMethods.ENUM_CURRENT_SETTINGS, ref mode))
                return mode;
            else
                throw new InvalidOperationException(GetLastError());
        }

        private static string GetLastError()
        {
            int err = Marshal.GetLastWin32Error();

            string msg;

            if (SafeNativeMethods.FormatMessage(
                SafeNativeMethods.FORMAT_MESSAGE_FLAGS,
                SafeNativeMethods.FORMAT_MESSAGE_FROM_HMODULE,
                (uint)err,
                0,
                out msg,
                0,
                0) == 0) return "Error";
            else return msg;
        }
        #endregion
    }
}