namespace Maui.SmartWindow.Core;

public interface ISmartWindow
{
    event EventHandler<EventArgs> PositionChanged;
    bool IsMDIChild { get; }
    View Content { get; set; }
    IWindow ParentWindow { get; set; }
    void SetPosition(int x, int y);
    void Show();
    void Close();
    double MdiX { get; set; }
    double MdiY { get; set; }
}
