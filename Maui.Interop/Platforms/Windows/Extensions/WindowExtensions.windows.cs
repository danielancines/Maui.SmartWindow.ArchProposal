using Maui.Interop.Platforms.Windows.Core;
using Microsoft.Maui.Platform;

namespace Maui.Interop.Extensions;

public static partial class WindowExtensions
{
    public static HWND GetHandle(this Window window)
    {
        if (window == null)
            throw new ArgumentNullException("Window cannot be null");

        if (window.Handler == null)
            throw new ArgumentNullException("Window handler cannot be null");

        if (window.Handler.PlatformView is MauiWinUIWindow mauiWinUIWindow)
            return new HWND(mauiWinUIWindow.GetWindowHandle());

        throw new InvalidOperationException("Window is not ready yet");
    }
}
