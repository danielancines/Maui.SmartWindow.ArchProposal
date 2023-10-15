using Maui.Interop.Extensions;
#if WINDOWS
using Windows.Win32;
using Windows.Win32.Foundation;
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

        public static Rect GetWindowRect(Window window)
        {
#if WINDOWS
            var windowHandle = window.GetHandle();
            PInvoke.GetWindowRect(windowHandle, out RECT rect);

            return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
#else
            return new Rect(0, 0, 0, 0);
#endif
        }

        public static Rect GetWindowRelativePositionTo(Window window, Window parentWindow)
        {
#if WINDOWS
            var windowHandle = window.GetHandle();
            var parentWindowHandle = parentWindow.GetHandle();

            PInvoke.GetWindowRect(windowHandle, out RECT rect);

            var location = new System.Drawing.Point(rect.left, rect.top);
            PInvoke.ScreenToClient(parentWindowHandle, ref location);

            return new Rect(location.X, location.Y, rect.right - rect.left, rect.bottom - rect.top);
#else
            return new Rect(0, 0, 0, 0);
#endif
        }
    }
}