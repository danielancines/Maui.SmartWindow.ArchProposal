using Maui.SmartWindow.Core;

namespace Maui.SmartWindow;

public partial class SmartWindowHandler
{
    public SmartWindowHandler()
    {
#if WINDOWS || MACCATALYST
        CommandMapper.AppendToMapping(nameof(ISmartWindow.SetPosition), SetPosition);

        //Mapper.AppendToMapping(nameof(ISmartWindow.MdiX), MapMdiXProperty);
        //Mapper.AppendToMapping(nameof(ISmartWindow.MdiY), MapMdiYProperty);

#endif
    }
}
