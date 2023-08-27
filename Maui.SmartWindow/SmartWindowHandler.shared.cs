using Maui.SmartWindow.Core;

namespace Maui.SmartWindow;

public partial class SmartWindowHandler
{
    public SmartWindowHandler()
    {
#if WINDOWS || MACCATALYST
        CommandMapper.AppendToMapping(nameof(SetParent), SetParent);
#endif
    }
}
