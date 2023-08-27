using Maui.Interop.Platforms.Windows.Core;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Maui.Interop
{
    // All the code in this file is only included on Windows.
    public static class WindowsNativeMethods
    {
        [DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [SupportedOSPlatform("windows5.0")]
        internal static extern HWND SetParent(HWND hWndChild, HWND hWndNewParent);

        [DllImport("USER32.dll", ExactSpelling = true, EntryPoint = "SetWindowLongW", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [SupportedOSPlatform("windows5.0")]
        internal static extern int SetWindowLong(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex, int dwNewLong);
    }
}