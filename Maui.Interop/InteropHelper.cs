using Maui.Interop.Extensions;
#if WINDOWS
using Maui.Interop.Platforms.Windows.Core;
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
            WindowsNativeMethods.SetParent(childHandle, parent.GetHandle());
            WindowsNativeMethods.SetWindowLong(childHandle, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, -20);
#endif
        }
    }
}