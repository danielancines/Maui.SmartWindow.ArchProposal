using Maui.SmartWindow.Core;
using System.Diagnostics;

namespace Maui.SmartWindow.ArchProposal;


public class MyWindow : SmartWindow
{
    public MyWindow(ContentPage page) : base(page)
    {
        
    }
}

public partial class MainPage : ContentPage
{
    ISmartWindow _window;

    public MainPage()
    {
        InitializeComponent();
    }

    private void AddContentButton_Clicked(object sender, EventArgs e)
    {
        this._window.Content = new Grid() { Background = Color.FromRgb(Random.Shared.Next(0,255), Random.Shared.Next(0, 255), Random.Shared.Next(0, 255)) };
    }

    private void OpenNewWindowButton_Clicked(object sender, EventArgs e)
    {
        for (int i = 0; i < 3; i++)
        {
            var contentPage = new ContentPage();
            var horizontalLayout = new HorizontalStackLayout();

            for (int j = 0; j < 33; j++)
            {
                horizontalLayout.Children.Add(new Label() { Text = "NewLabel" });
            }

            contentPage.Content = horizontalLayout;
            Application.Current.OpenWindow(new SmartWindow(contentPage));
            //var parentWindow = this._window as IWindow ?? this.Window;
            //this._window = new SmartWindow();
            //this._window.PositionChanged += _window_PositionChanged;
            ////this._window.ParentWindow = parentWindow;
            ////this._window.MdiX = 200;
            ////this._window.MdiY = 200;
            //this._window.Width = 600;
            //this._window.Height = 200;

            //this._window?.Show();
        }

        //var parentWindow = this._window as IWindow ?? this.Window;
        //this._window = new SmartWindow();
        //this._window.PositionChanged += _window_PositionChanged;
        ////this._window.ParentWindow = parentWindow;
        ////this._window.MdiX = 200;
        ////this._window.MdiY = 200;
        //this._window.Width = 600;
        //this._window.Height = 200;

        //this._window?.Show();
    }

    private void _window_PositionChanged(object sender, EventArgs e)
    {
        Debug.WriteLine(this._window.MdiY);
    }

    private void CloseWindowButton_Clicked(object sender, EventArgs e)
    {
        var windows = Application.Current.Windows.Where(w => w is SmartWindow).ToList();

        foreach (SmartWindow window in windows)
            Application.Current.CloseWindow(window);

        //GC.SuppressFinalize(this);
        //GC.Collect();
    }

    private void SetParentButton_Clicked(object sender, EventArgs e)
    {
        this._window.ParentWindow = this.Window;
    }

    private void SetPositionButton_Clicked(object sender, EventArgs e)
    {
        //this._window.SetPosition((int)this._window.MdiX + 10, (int)this._window.MdiY + 10);
        this._window.MdiX += 10;
        this._window.MdiY += 10;
    }
}