using Maui.Interop.Extensions;
#if WINDOWS
using Windows.Win32;
#endif

namespace Maui.Interop
{
    // All the code in this file is included in all platforms.
    public static class InteropHelper
    {
        public static void SetParent(Window child, Window parent)
        {

#if WINDOWS
            var childHandle = child.GetHandle();
            var parentHandle = parent.GetHandle();
            PInvoke.SetParent(childHandle, parentHandle);
            PInvoke.SetWindowLong(childHandle, Windows.Win32.UI.WindowsAndMessaging.WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, -20);
#endif
        }
    }
}