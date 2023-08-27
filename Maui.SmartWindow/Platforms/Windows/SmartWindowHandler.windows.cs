using Maui.Interop;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;

namespace Maui.SmartWindow;

public partial class SmartWindowHandler : WindowHandler
{
    #region Handler Methods

    protected override void ConnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        base.ConnectHandler(platformView);

        (platformView as MauiWinUIWindow).ExtendsContentIntoTitleBar = false;
        var appWindow = (platformView as MauiWinUIWindow).GetAppWindow();
        if (appWindow.Presenter is OverlappedPresenter presenter)
            presenter.SetBorderAndTitleBar(true, false);
    }

    protected override void DisconnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        base.DisconnectHandler(platformView);
    }

    #endregion

    #region Public Methods

    public static void SetParent(IWindowHandler handler, IWindow window, object parent)
    {
        if (parent is Window parentWindow)
            InteropHelper.SetParent(handler.VirtualView as Window, parentWindow);
    }

    #endregion
}
