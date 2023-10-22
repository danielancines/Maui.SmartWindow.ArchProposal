using Maui.Interop.Core;
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

        public static bool IsMouseLeftButtonDown
        {
            get
            {
#if WINDOWS
                return PInvoke.GetKeyState(VirtualKeyCodes.VK_LBUTTON) < 0;
#else
                return false;
#endif
            }
        }

        public static bool IsMouseRightButtonDown
        {
            get
            {
#if WINDOWS
                return PInvoke.GetKeyState(VirtualKeyCodes.VK_RBUTTON) < 0;
#else
                return false;
#endif
            }
        }

        public static Point CursorPosition
        {
            get
            {
#if WINDOWS
                PInvoke.GetCursorPos(out System.Drawing.Point cursorPosition);
                return new Point(cursorPosition.X, cursorPosition.Y);
#else
                return new Point();
#endif
            }
        }

        public static Point GetRelativePositionToWindow(Window window, Point location)
        {
#if WINDOWS
            var windowHandle = window.GetHandle();
            System.Drawing.Point locationPoint = new System.Drawing.Point((int)location.X, (int)location.Y);
            PInvoke.ScreenToClient(windowHandle, ref locationPoint);

            return new Point(locationPoint.X, locationPoint.Y);
#else
            return new Point();
#endif
        }
    }
}