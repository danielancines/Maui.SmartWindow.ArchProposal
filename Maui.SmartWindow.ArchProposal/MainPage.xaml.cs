using Maui.SmartWindow.Core;
using System.Diagnostics;

namespace Maui.SmartWindow.ArchProposal
{
    public partial class MainPage : ContentPage
    {
        ISmartWindow _window;

        public MainPage()
        {
            InitializeComponent();
        }

        private void AddContentButton_Clicked(object sender, EventArgs e)
        {
            this._window.Content = new Grid() { Background = Colors.Gray };
        }

        private void OpenNewWindowButton_Clicked(object sender, EventArgs e)
        {
            this._window = new SmartWindow();
            this._window.PositionChanged += _window_PositionChanged;
            this._window.ParentWindow = this.Window;
            this._window.MdiX = 200;
            this._window.MdiY = 200;
            this._window?.Show();
        }

        private void _window_PositionChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(this._window.MdiY);
        }

        private void CloseWindowButton_Clicked(object sender, EventArgs e)
        {
            this._window?.Close();
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
}