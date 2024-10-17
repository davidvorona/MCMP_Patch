using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMP_Patch
{
    internal static class HelperTools
    {
        public static void AddChatMessage(string text)
        {
            HUDManager.Instance.AddTextToChatOnServer(text);
        }
    }
}
