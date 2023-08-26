using Maui.Interop.Extensions;
using Microsoft.Maui.Platform;

namespace Maui.Interop
{
    // All the code in this file is included in all platforms.
    public static class InteropHelper
    {
        public static void SetParent(Window child, Window parent)
        {
            
#if WINDOWS
            NativeMethods.SetParent(child.GetHandle(), parent.GetHandle());
#endif
        }
    }
}