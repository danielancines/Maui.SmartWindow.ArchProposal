namespace Maui.SmartWindow.Core;

public interface ISmartWindow
{
    View Content { get; set; }
    Window ParentWindow { get; set; }
    void Show();
    void Close();
}
