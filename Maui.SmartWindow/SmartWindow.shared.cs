using Maui.SmartWindow.Core;

namespace Maui.SmartWindow;
public partial class SmartWindow : Window, ISmartWindow
{
    public SmartWindow() : this(new ContentPage())
    {

    }

    public SmartWindow(ContentPage page)
    {
        this.Page = page;
    }

    #region ISmartWindow Members

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

    private Window _parentWindow;
    public Window ParentWindow
    {
        get { return _parentWindow; }
        set
        {
            if (_parentWindow == value)
                return;

            _parentWindow = value;
            this.SetParent(value);
        }
    }

    public void Show()
    {
        Application.Current.OpenWindow(this);
    }

    public void Close()
    {
        Application.Current.CloseWindow(this);
    }

    #endregion

    #region Private Methods
    private void SetPageContent(View content)
    {
        if (this.Page is ContentPage page)
            page.Content = content;
    }

    private void SetParent(Window parentWindow)
    {
        this.Handler.Invoke(nameof(SmartWindowHandler.SetParent), parentWindow);
    }

    #endregion
}
