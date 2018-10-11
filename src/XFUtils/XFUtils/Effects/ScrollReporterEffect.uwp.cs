using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using PlatformEffects = XFUtils.Effects.UWP;
using RoutingEffects = XFUtils.Effects;

[assembly: ResolutionGroupName("XFUtils")]
[assembly: ExportEffect(typeof(PlatformEffects.ScrollReporterEffect), "ScrollReporterEffect")]
namespace XFUtils.Effects.UWP
{
    public class ScrollReporterEffect : PlatformEffect
    {
        RoutingEffects.ScrollReporterEffect effect;
        ScrollViewer scrollViewer;
        protected override void OnAttached()
        {
            var listView = Control as Windows.UI.Xaml.Controls.ListView;
            effect = (RoutingEffects.ScrollReporterEffect)Element.Effects.FirstOrDefault((object e) => e is RoutingEffects.ScrollReporterEffect);
            //scrollViewer = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(listView, 0), 0) as ScrollViewer;
            scrollViewer = GetScrollViewer(listView);

            scrollViewer.ViewChanging += ScrollViewer_ViewChanging;
        }

        private ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer)
                return depObj as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = GetScrollViewer(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void ScrollViewer_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            effect.OnScrollEffect(Element, new ScrollEventArgs((int)e.NextView.VerticalOffset));
        }

        protected override void OnDetached()
        {
            var listView = Control as Windows.UI.Xaml.Controls.ListView;
            if (listView == null)
                return;
            else
                scrollViewer.ViewChanging -= ScrollViewer_ViewChanging;
        }
    }
}
