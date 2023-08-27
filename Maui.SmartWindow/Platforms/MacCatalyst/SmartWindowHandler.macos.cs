using Microsoft.Maui.Handlers;

namespace Maui.SmartWindow
{
    // All the code in this file is only included on Mac Catalyst.
    public partial class SmartWindowHandler : WindowHandler
    {
        public static void SetParent(IWindowHandler handler, IWindow window, object parent)
        {
            //TODO MacOS doenst have MDI
        }
    }
}