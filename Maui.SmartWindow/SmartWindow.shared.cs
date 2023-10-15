using Maui.SmartWindow.Core;

namespace Maui.SmartWindow;
public partial class SmartWindow : Window, ISmartWindow, IDisposable
{
    #region Constructor

    public SmartWindow() : this(new ContentPage())
    {

    }

    public SmartWindow(ContentPage page)
    {
        this.Page = page;
    }

    #endregion

    #region ISmartWindow Members

    private double _x;
    public new double X
    {
        get
        {
            return this._x;
        }
        set
        {
            if (this._x == value)
                return;

            this._x = value;
            base.X = value;
        }
    }

    private double _y;
    public new double Y
    {
        get
        {
            return _y;
        }
        set
        {
            if (this._y == value)
                return;

            this._y = value;
            base.Y = value;
        }
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

            _parentWindow = value;
        }
    }

    public bool IsMDIChild => this.ParentWindow != null;

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
        this.Close();
        this.Handler?.DisconnectHandler();
    }

    #endregion

    #region Private Methods
    private void SetPageContent(View content)
    {
        if (this.Page is ContentPage page)
            page.Content = content;
    }

    #endregion
}
