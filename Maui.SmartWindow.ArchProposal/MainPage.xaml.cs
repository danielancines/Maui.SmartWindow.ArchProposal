using Maui.SmartWindow.ArchProposal.Helpers;
using Maui.SmartWindow.ArchProposal.Services;
using Maui.SmartWindow.ArchProposal.Views;
using Maui.SmartWindow.Core;
using System.Diagnostics;

namespace Maui.SmartWindow.ArchProposal
{
    public partial class MainPage : ContentPage
    {
        ISmartWindow _window;
        private readonly IServiceProvider _serviceProvider;

        public MainPage(ICustomerService customerService, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            this.MyWindowHashLabel.Text = $"customerService Hash: {customerService.GetCustomerName()}";
            this._serviceProvider = serviceProvider;
            this.MyContentView.Content = this._serviceProvider.GetService<MyView>();

            //this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, EventArgs e)
        {
            this.Loaded -= MainPage_Loaded;

            //var mainPage = ContainerHelper.Provider.GetService<MainPage>();
            this.MyWindowHashLabel.Text = $"MainPage Hash: {this.GetHashCode()}";
        }

        private void AddContentButton_Clicked(object sender, EventArgs e)
        {
            this._window.Content = new Grid() { Background = Colors.Gray };
        }

        private void OpenNewWindowButton_Clicked(object sender, EventArgs e)
        {
            var contentPage = new ContentPage();
            contentPage.Content = ContainerHelper.Provider.GetService<MyView>();
            this._window = new SmartWindow(contentPage);
            this._window.PositionChanged += _window_PositionChanged;
            this._window.ParentWindow = this.Window;
            this._window.MdiX = 200;
            this._window.MdiY = 200;
            this._window.Width = 600;
            this._window.Height = 200;

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

        private void OpenNewExternalWindowButton_Clicked(object sender, EventArgs e)
        {
            var window = new Window(this._serviceProvider.GetService<AppShell>());
            Application.Current.OpenWindow(window);

        }
    }
}