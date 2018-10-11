using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XFUtils.Effects
{
    public class ScrollEventArgs : EventArgs
    {
        public ScrollEventArgs(double ScrollY)
        {
            Y = ScrollY;
        }
        public double Y { get; }
    }

    public class ScrollReporterEffect : RoutingEffect
    {
        public event Action<Object, ScrollEventArgs> ScrollChanged;
        public ScrollReporterEffect() : base("XFUtils.ScrollReporterEffect") { }

        public void OnScrollEffect(Object sender, ScrollEventArgs e)
        {
            ScrollChanged?.Invoke(sender, e);
        }
    }
}
