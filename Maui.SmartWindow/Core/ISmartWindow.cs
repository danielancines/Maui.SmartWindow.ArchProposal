namespace Maui.SmartWindow.Core;

public interface ISmartWindow
{
    bool IsMDIChild { get; }
    View Content { get; set; }
    IWindow ParentWindow { get; set; }
    void Show();
    void Close();
    double X { get; set; }
    double Y { get; set; }
}
