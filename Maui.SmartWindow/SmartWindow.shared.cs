using Maui.SmartWindow.Core;

namespace Maui.SmartWindow;
public partial class SmartWindow : Window, ISmartWindow, IDisposable
{
    #region Events

    readonly WeakEventManager _weakEventManager = new();

    public event EventHandler<EventArgs> PositionChanged
    {
        add => this._weakEventManager.AddEventHandler(value);
        remove => this._weakEventManager.RemoveEventHandler(value);
    }

    #endregion

    #region Constructor

    public SmartWindow() : this(new ContentPage())
    {

    }

    public SmartWindow(ContentPage page)
    {
        this.Page = page;
    }

    #endregion

    #region Properties

    public bool IsMDIChild => this.ParentWindow != null;

    public static readonly BindableProperty MdiXProperty = BindableProperty.Create(nameof(ISmartWindow.MdiX), typeof(double), typeof(SmartWindow), 0d, BindingMode.OneWay);
    public double MdiX
    {
        get { return (double)GetValue(MdiXProperty); }
        set { SetValue(MdiXProperty, value); }
    }

    public static readonly BindableProperty MdiYProperty = BindableProperty.Create(nameof(MdiY), typeof(double), typeof(SmartWindow), 0d, BindingMode.OneWay);
    public double MdiY
    {
        get { return (double)GetValue(MdiYProperty); }
        set { SetValue(MdiYProperty, value); }
    }

    private View _content;
    public View Content
    {
        get { return _content; }
        set
        {
            if (_content == value)
                return;

            _content = value;
            this.SetPageContent(value);
        }
    }

    private IWindow _parentWindow;
    public IWindow ParentWindow
    {
        get { return _parentWindow; }
        set
        {
            if (_parentWindow == value)
                return;

            if (value == null)
                this.UnHookParentWindowEvents(this._parentWindow);
            else
                this.HookParentWindowEvents(value);

            _parentWindow = value;
        }
    }

    #endregion

    #region Internal Methods

    internal void OnPositionChanged()
    {
        this._weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(this.PositionChanged));
    }

    #endregion

    #region Private Methods

    private void UnHookParentWindowEvents(IWindow parentWindow)
    {
        if (parentWindow is Window window)
            window.Destroying -= ParentWindow_Destroying;
    }

    private void HookParentWindowEvents(IWindow parentWindow)
    {
        if (parentWindow is Window window)
            window.Destroying += ParentWindow_Destroying;
    }

    private void ParentWindow_Destroying(object sender, EventArgs e)
    {
        this.Close();
        this.Dispose();
    }

    private void SetPageContent(View content)
    {
        if (this.Page is ContentPage page)
            page.Content = content;
    }

    #endregion

    #region Public Methods

    public void Show()
    {
        Application.Current.OpenWindow(this);
    }

    public void Close()
    {
        Application.Current.CloseWindow(this);
    }

    public void Dispose()
    {
        this.Handler?.DisconnectHandler();
    }

    public void SetPosition(int x, int y)
    {
        this.Handler?.Invoke(nameof(ISmartWindow.SetPosition), new Point(x, y));
    }

    #endregion
}
