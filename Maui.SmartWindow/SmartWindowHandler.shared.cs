using Maui.SmartWindow.Core;
using Microsoft.Maui.Handlers;

namespace Maui.SmartWindow;

public partial class SmartWindowHandler
{
    public SmartWindowHandler()
    {
#if WINDOWS
        SmartWindowHandler.CommandMapper.AppendToMapping(nameof(SmartWindowHandler.SetParent), SetParent);
#endif
    }

    public static void SetParent(IWindowHandler handler, IWindow window, object arg3)
    {
        if (handler is ISmartWindowHandler smartWindowHandler && arg3 is Window parentWindow)
            smartWindowHandler.SetParent(parentWindow);
    }
}
