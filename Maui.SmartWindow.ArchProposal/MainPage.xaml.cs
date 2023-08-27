using Maui.SmartWindow.Core;

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
            this._window?.Show();
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
            (this._window as Window).X = 10;
            (this._window as Window).Y = 10;
        }
    }
}