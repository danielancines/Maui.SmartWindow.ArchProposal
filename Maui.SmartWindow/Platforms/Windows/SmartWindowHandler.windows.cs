﻿using Maui.Interop;
using Maui.SmartWindow.Core;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;

namespace Maui.SmartWindow;

public partial class SmartWindowHandler : WindowHandler
{
    #region Fields

    private bool _updatingPositionByPointer;
    private AppWindow _appWindow;
    private ISmartWindow _smartWindow;

    #endregion

    #region Handler Methods

    protected override void ConnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        base.ConnectHandler(platformView);
        this.InitializeMainFields();
        this.HookEvents();

        if (this._smartWindow.IsMDIChild)
            this.InitializeWindowAsMDIChild();
    }

    protected override void DisconnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        this.UnHookEvents();
        base.DisconnectHandler(platformView);
    }

    #endregion

    #region Public Methods

    public static void MapMdiXProperty(IWindowHandler handler, IWindow window)
    {
        if (handler is SmartWindowHandler smartWindowHandler && window is ISmartWindow smartWindow)
        {
            if (smartWindowHandler._updatingPositionByPointer)
                return;

            if (!smartWindow.IsMDIChild)
                throw new InvalidOperationException("MdiX cannot be used if SmartWindow is not a MDIChild, use X property");

            smartWindowHandler.UpdatePosition((int)smartWindow.MdiX, (int)smartWindow.MdiY);
        }
    }

    public static void MapMdiYProperty(IWindowHandler handler, IWindow window)
    {
        if (handler is SmartWindowHandler smartWindowHandler && window is ISmartWindow smartWindow)
        {
            if (smartWindowHandler._updatingPositionByPointer)
                return;

            if (!smartWindow.IsMDIChild)
                throw new InvalidOperationException("MdiY cannot be used if SmartWindow is not a MDIChild, use Y property");

            smartWindowHandler.UpdatePosition((int)smartWindow.MdiX, (int)smartWindow.MdiY);
        }
    }

    #endregion

    #region Private Methods

    private void UpdatePosition(int x, int y)
    {
        if (this._smartWindow == null || this._appWindow == null)
            return;

        if (this._smartWindow.IsMDIChild && this._appWindow != null)
            this._appWindow.Move(new Windows.Graphics.PointInt32(x, y));
    }

    private void InitializeMainFields()
    {
        this._appWindow = this.PlatformView.GetAppWindow();
        this._smartWindow = this.VirtualView as ISmartWindow;
    }

    private void InitializeWindowAsMDIChild()
    {
        (this.PlatformView as MauiWinUIWindow).ExtendsContentIntoTitleBar = false;
        if (this._appWindow.Presenter is OverlappedPresenter presenter)
            presenter.SetBorderAndTitleBar(true, false);

        InteropHelper.SetParent(this.VirtualView as Window, this._smartWindow.ParentWindow as Window);
    }

    private void SetPosition(IWindowHandler handler, IWindow window, object parameter)
    {
        if (handler is SmartWindowHandler smartWindowHandler && parameter is Point position && window is ISmartWindow smartWindow)
        {
            smartWindow.MdiX = position.X;
            smartWindow.MdiY = position.Y;
        }
    }

    private void HookEvents()
    {
        if (this.VirtualView is Window window)
            window.Created += Window_Created;
    }

    private void Window_Created(object sender, EventArgs e)
    {
        if (this.VirtualView is Window window)
            window.SizeChanged += Window_SizeChanged;
    }

    private void UnHookEvents()
    {
        if (this.VirtualView is Window window)
        {
            window.Created -= Window_Created;
            window.SizeChanged -= Window_SizeChanged;
        }
    }

    private void Window_SizeChanged(object sender, EventArgs e)
    {
        if (this._smartWindow == null || this._smartWindow.ParentWindow == null)
            return;

        this._updatingPositionByPointer = true;
        var relativeTo = InteropHelper.GetWindowRelativePositionTo(this.VirtualView as Window, this._smartWindow.ParentWindow as Window);
        if (this._smartWindow is SmartWindow smartWindow)
        {
            smartWindow.MdiX = relativeTo.X;
            smartWindow.MdiY = relativeTo.Y;
            smartWindow.OnPositionChanged();
        }

        this._updatingPositionByPointer = false;
    }

    #endregion
}
