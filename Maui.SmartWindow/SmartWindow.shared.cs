using Maui.SmartWindow.Core;

namespace Maui.SmartWindow;
public partial class SmartWindow : ISmartWindowBehaviors
{
    #region ISmartWindowBehaviors Members
    public void Show()
    {
        this.Handler?.OpenWindow();
    }

    #endregion
}
