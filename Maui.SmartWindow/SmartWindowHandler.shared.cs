using Maui.SmartWindow.Core;
using Microsoft.Maui.Handlers;

namespace Maui.SmartWindow;

public partial class SmartWindowHandler
{
    public SmartWindowHandler()
    {
#if WINDOWS || MACCATALYST
        //CommandMapper.AppendToMapping(nameof(SetParent), SetParent);

        Mapper.AppendToMapping(nameof(IWindow.X), MapXProperty);
        Mapper.AppendToMapping(nameof(IWindow.Y), MapYProperty);

#endif
    }
}
