using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEventPropagation.ViewModel
{
    public static class EventManager
    {
        private static List<string> _uIMouseEventArgsToStopBubbling = new List<string>();
        public static void PreventBubbling(UIMouseEventArgs uiMouseEventArg)
        {
            System.Diagnostics.Trace.WriteLine($"PreventBubbling Called {uiMouseEventArg.ScreenX}|{uiMouseEventArg.ScreenY}");
            _uIMouseEventArgsToStopBubbling.Add($"{uiMouseEventArg.ScreenX}|{uiMouseEventArg.ScreenY}");
        }

        public static void ClearPreventBubbling(UIMouseEventArgs uiMouseEventArg)
        {
            _uIMouseEventArgsToStopBubbling.Remove($"{uiMouseEventArg.ScreenX}|{uiMouseEventArg.ScreenY}");
        }

        public static bool ShouldAction(UIMouseEventArgs uiMouseEventArg)
        {
            System.Diagnostics.Trace.WriteLine($"ShouldAction Called {uiMouseEventArg.ScreenX}|{uiMouseEventArg.ScreenY}");
            var result = !_uIMouseEventArgsToStopBubbling.Contains($"{uiMouseEventArg.ScreenX}|{uiMouseEventArg.ScreenY}");
            System.Diagnostics.Trace.WriteLine($"ShouldAction Result {result}");
            return result;
        }

        public static async void ManageClick(DOnUIMouseEvent onUIMouseEvent, UIMouseEventArgs args)
        {
            System.Diagnostics.Trace.WriteLine("ClickedX " + args.ScreenX);
            System.Diagnostics.Trace.WriteLine("ClickedY " + args.ScreenY);
            await new Task(() => onUIMouseEvent(args));
        }

        public static void Log(DOnUIMouseEvent onUIMouseEvent, UIMouseEventArgs args)
        {
            
        }
    }
}
