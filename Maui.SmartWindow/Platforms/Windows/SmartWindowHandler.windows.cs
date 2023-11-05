using Maui.Interop;
using Maui.SmartWindow.Core;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
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
        this.HookEvents(platformView);

        if (this._smartWindow.IsMDIChild)
            this.InitializeWindowAsMDIChild();
    }

    protected override void DisconnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        this.UnHookEvents(platformView);
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

        var window = this.VirtualView as Window;
        if (window == null)
            return;

        //Error on resize on Windows 11
        //https://github.com/microsoft/microsoft-ui-xaml/issues/8707
        var width = window.Width;
        var height = window.Height;
        window.Height = 4000;
        window.Width = 4000;

        InteropHelper.SetParent(this.VirtualView as Window, this._smartWindow.ParentWindow as Window);

        //Error on resize on Windows 11
        //https://github.com/microsoft/microsoft-ui-xaml/issues/8707
        this._appWindow.Resize(new Windows.Graphics.SizeInt32((int)width, (int)height));


        if (this.VirtualView is SmartWindow smartWindow)
        {
            //var nativeWindow = (uiXaml.Window)App.Current.Windows.First().Handler.PlatformView;
            //var nativeElement = (uiXaml.UIElement)element.Handler.PlatformView;
            //var coordinates = nativeElement.TransformToVisual(nativeWindow.Content)
            //                                .TransformPoint(new Windows.Foundation.Point(0, 0));

            //var navBarHeight = (App.Current.MainPage is NavigationPage navPage && navPage.CurrentPage != null)
            //                 ? navPage.Height - navPage.CurrentPage.Height : 0;

            //return new Point((int)Math.Round(coordinates.X),
            //                 (int)Math.Round(coordinates.Y - navBarHeight));

            var parentWindow = (smartWindow.ParentWindow.Handler.PlatformView as Microsoft.UI.Xaml.Window);
            var parentPage = (smartWindow.ParentWindow as Window).Page as Shell;

            var currentPage = parentPage.CurrentPage as ContentPage;
            var horizontalStackLayout = currentPage.Content as HorizontalStackLayout;
            var button = horizontalStackLayout.Children[0] as Button;


            var buttonPlatformView = button.Handler.PlatformView as Microsoft.Maui.Platform.MauiButton;
            var buttonPosition = buttonPlatformView.TransformToVisual(parentWindow.Content).TransformPoint(new Windows.Foundation.Point(0, 0));

            var navBarHeight = (currentPage as NavigationPage).Height;



            var containerPlatformView = smartWindow.ParentWindow.Content.Handler.PlatformView as Microsoft.UI.Xaml.UIElement;
            var point = containerPlatformView.TransformToVisual(parentWindow.Content).TransformPoint(new Windows.Foundation.Point(0, 0));


            var a = this.PlatformView.Content.TransformToVisual(this.PlatformView.Content).TransformPoint(new Windows.Foundation.Point(10,10));
        }
    }

    private void SetPosition(IWindowHandler handler, IWindow window, object parameter)
    {
        if (handler is SmartWindowHandler smartWindowHandler && parameter is Point position && window is ISmartWindow smartWindow)
        {
            smartWindow.MdiX = position.X;
            smartWindow.MdiY = position.Y;
        }
    }

    private void HookEvents(Microsoft.UI.Xaml.Window platformView)
    {
        if (this.VirtualView is Window window)
            window.Created += Window_Created;

        if (platformView is MauiWinUIWindow mauiWinUIWindow && mauiWinUIWindow.Content is Microsoft.UI.Xaml.UIElement uiElement)
            uiElement.PointerPressed += Content_PointerPressed;
    }

    private void UnHookEvents(Microsoft.UI.Xaml.Window platformView)
    {
        if (this.VirtualView is Window window)
        {
            window.Created -= Window_Created;
            window.SizeChanged -= Window_SizeChanged;
        }

        if (platformView is MauiWinUIWindow mauiWinUIWindow && mauiWinUIWindow.Content is Microsoft.UI.Xaml.UIElement uiElement)
            uiElement.PointerPressed -= Content_PointerPressed;
    }

    private void Content_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        if (this.PlatformView is MauiWinUIWindow mauiWinUIWindow && mauiWinUIWindow.Content is Microsoft.UI.Xaml.UIElement uiElement)
        {
            var initialClickPoint = e.GetCurrentPoint(uiElement);
            while (InteropHelper.IsMouseLeftButtonDown)
            {
                var point = InteropHelper.CursorPosition;
                var relativePoint = InteropHelper.GetRelativePositionToWindow(this._smartWindow.ParentWindow as Window, point);

                //7 px are some kind of border on every window
                this.UpdatePosition((int)relativePoint.X - (int)initialClickPoint.Position.X - 7, (int)relativePoint.Y - (int)initialClickPoint.Position.Y - 7);
            }
        }
    }

    private void Window_Created(object sender, EventArgs e)
    {
        if (this.VirtualView is Window window)
            window.SizeChanged += Window_SizeChanged;
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
