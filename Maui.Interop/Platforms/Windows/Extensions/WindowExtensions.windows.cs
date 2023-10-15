using Microsoft.Maui.Platform;

namespace Maui.Interop.Extensions;

public static partial class WindowExtensions
{
    internal static Windows.Win32.Foundation.HWND GetHandle(this Window window)
    {
        if (window == null)
            throw new ArgumentNullException("Window cannot be null");

        if (window.Handler == null)
            throw new ArgumentNullException("Window handler cannot be null");

        if (window.Handler.PlatformView is MauiWinUIWindow mauiWinUIWindow)
            return new Windows.Win32.Foundation.HWND(mauiWinUIWindow.GetWindowHandle());

        throw new InvalidOperationException("Window is not ready yet, OpenWindow must to be called");
    }
}
