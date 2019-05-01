using BlazorEventPropagation.ViewModel;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEventPropagation.Shared
{
    public class EventControlledComponent : BlazorComponent, IDisposable
    {
        public async void EventControlledClick(Func<UIMouseEventArgs, bool, Task> onEvent, UIMouseEventArgs click, bool preventBubbling = false)
        {
            System.Diagnostics.Trace.WriteLine($"Received EventControlledClick {click.ClientX}|{click.ClientY} {preventBubbling}");

            var shouldAction = !InCurrentDisablePropagationList(click);

            if(shouldAction)
            {
                if (preventBubbling)
                {
                    AddToDisablePropagationList(click);
                }
            }

            await onEvent(click, shouldAction);

            this.StateHasChanged();
        }

        public void ClearEventControlledClick(UIMouseEventArgs click)
        {
            System.Diagnostics.Trace.WriteLine($"ClearEventControlledClick {click.ClientX}|{click.ClientY}");
            ClearPreventBubblingIfExists(click);
        }

        private static List<string> _currentDisablePropagationList = new List<string>();
        public static void AddToDisablePropagationList(UIMouseEventArgs click)
        {
            System.Diagnostics.Trace.WriteLine($"AddToDisablePropagationList {click.ClientX}|{click.ClientY}");
            _currentDisablePropagationList.Add($"{click.ScreenX}|{click.ScreenY}");
        }

        public static void ClearPreventBubblingIfExists(UIMouseEventArgs click)
        {
            System.Diagnostics.Trace.WriteLine($"ClearPreventBubblingIfExists {click.ClientX}|{click.ClientY}");
            _currentDisablePropagationList.Remove($"{click.ScreenX}|{click.ScreenY}");
        }

        public static bool InCurrentDisablePropagationList(UIMouseEventArgs click)
        {
            var result = _currentDisablePropagationList.Contains($"{click.ScreenX}|{click.ScreenY}");
            System.Diagnostics.Trace.WriteLine($"InCurrentDisablePropagationList {click.ClientX}|{click.ClientY} {result}");
            return result;
        }
        public void Dispose()
        {

        }
    }
}
