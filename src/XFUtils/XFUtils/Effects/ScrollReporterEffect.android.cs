using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PlatformEffects = XFUtils.Effects.droid;
using RoutingEffects = XFUtils.Effects;

[assembly: ResolutionGroupName("XFUtils")]
[assembly: ExportEffect(typeof(PlatformEffects.ScrollReporterEffect), "ScrollReporterEffect")]
namespace XFUtils.Effects.droid
{
    public class ScrollReporterEffect : PlatformEffect
    {
        RoutingEffects.ScrollReporterEffect effect;
        float Density;
        protected override void OnAttached()
        {
            if (Element is Xamarin.Forms.ListView == false)
                return;

            effect = (RoutingEffects.ScrollReporterEffect)Element.Effects.FirstOrDefault(e => e is RoutingEffects.ScrollReporterEffect);

            Density = ((Android.Widget.ListView)Control).Context.Resources.DisplayMetrics.Density;

            ((Android.Widget.ListView)Control).Scroll += ScrollReporterEffect_Scroll;
        }

        private void ScrollReporterEffect_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            int pos = GetListViewYPosition();

            float dp = pos / Density;

            effect.OnScrollEffect(Element, new ScrollEventArgs(dp));
        }

        private int mLastFirstVisibleItem;
        private bool processingSwipe = false;
        private Dictionary<Int32, Int32> listViewItemHeights = new Dictionary<Int32, Int32>();
        private Double CellHeight = 0;

        private int GetListViewYPosition()
        {
            var listView = ((Android.Widget.ListView)Control);

            if (listView != null)
            {
                var c = listView.GetChildAt(0); //this is the first visible row
                if (c != null)
                {
                    int scrollY = -c.Top;
                    if (listViewItemHeights.ContainsKey(listView.FirstVisiblePosition) == false)
                    {
                        CellHeight = c.Height;
                        listViewItemHeights.Add(listView.FirstVisiblePosition, c.Height);
                    }
                    for (int i = 0; i < listView.FirstVisiblePosition; ++i)
                    {
                        if (listViewItemHeights.ContainsKey(i) && listViewItemHeights[i] != 0)
                            scrollY += listViewItemHeights[i];
                    }
                    return scrollY;
                }
            }
            return 0;
        }

        protected override void OnDetached()
        {
            ((Android.Widget.ListView)Control).Scroll -= ScrollReporterEffect_Scroll;
        }
    }
}
