using Maui.Interop;
using Maui.SmartWindow.Core;
using Microsoft.Maui.Handlers;

namespace Maui.SmartWindow;

public partial class SmartWindowHandler : WindowHandler, ISmartWindowHandler
{
    #region Handler Methods

    protected override void ConnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(Microsoft.UI.Xaml.Window platformView)
    {
        base.DisconnectHandler(platformView);
    }

    #endregion

    #region ISmartWindowHandler Members

    public void SetParent(Window parent)
    {
        InteropHelper.SetParent(this.VirtualView as Window, parent);
    }

    #endregion
}
