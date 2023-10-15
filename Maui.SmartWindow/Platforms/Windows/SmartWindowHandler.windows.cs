using Maui.Interop;
using Maui.SmartWindow.Core;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;

namespace Maui.SmartWindow;

public partial class SmartWindowHandler : WindowHandler
{
    #region Fields

    private AppWindow _appWindow;
    private ISmartWindow _smartWindow;

    #endregion

    #region Handler Methods

    protected override void ConnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        base.ConnectHandler(platformView);
        this.InitializeMainFields();

        if (this._smartWindow.IsMDIChild)
            this.InitializeWindowAsMDIChild();
    }

    protected override void DisconnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        base.DisconnectHandler(platformView);
    }

    #endregion

    #region Public Methods

    public static void MapXProperty(IWindowHandler handler, IWindow window)
    {
        if (handler is SmartWindowHandler smartWindowHandler && window is ISmartWindow smartWindow)
            smartWindowHandler.UpdatePosition((int)smartWindow.X, (int)smartWindow.Y);
    }

    public static void MapYProperty(IWindowHandler handler, IWindow window)
    {
        if (handler is SmartWindowHandler smartWindowHandler && window is ISmartWindow smartWindow)
            smartWindowHandler.UpdatePosition((int)smartWindow.X, (int)smartWindow.Y);
    }

    #endregion

    #region Internal Methods

    internal void UpdatePosition(int x, int y)
    {
        if (this._smartWindow == null || this._appWindow == null)
            return;

        if (this._smartWindow.IsMDIChild && this._appWindow != null)
            this._appWindow.Move(new Windows.Graphics.PointInt32(x, y));
    }

    #endregion

    #region Private Methods

    private void InitializeMainFields()
    {
        this._appWindow = (this.PlatformView as MauiWinUIWindow).GetAppWindow();
        this._smartWindow = this.VirtualView as ISmartWindow;
    }

    private void InitializeWindowAsMDIChild()
    {
        (this.PlatformView as MauiWinUIWindow).ExtendsContentIntoTitleBar = false;
        if (this._appWindow.Presenter is OverlappedPresenter presenter)
            presenter.SetBorderAndTitleBar(true, false);

        InteropHelper.SetParent(this.VirtualView as Window, this._smartWindow.ParentWindow as Window);
    }

    #endregion
}
