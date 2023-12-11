using Maui.SmartWindow.ArchProposal.Helpers;
using Maui.SmartWindow.ArchProposal.Services;

namespace Maui.SmartWindow.ArchProposal.Views;

public partial class MyView : ContentView
{
    private ICustomerService _customerService;
    private IServiceScope _scopeProvider;

    public MyView(ICustomerService customerService)
    {
        InitializeComponent();
        this._scopeProvider = ContainerHelper.Provider.CreateScope();

        this.MyLabel.Text = $"MyView Customer Service Hash: {customerService.GetCustomerName()}";
        //this.Loaded += MyView_Loaded;
    }

    private void MyView_Loaded(object sender, EventArgs e)
    {
        this.LastNameLabel.Text = $"MyView MainPage Hash: {ContainerHelper.Provider.GetService<MainPage>().GetHashCode()}"; //Application.Current.MainPage.GetHashCode().ToString(); //this._customerService.LastName;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        this._customerService = ContainerHelper.Provider.GetService<ICustomerService>();
        //this.MyLabel.Text = $"MyView Customer Service Hash: {this._customerService.GetCustomerName()}";
    }
}