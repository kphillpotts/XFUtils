using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using PlatformEffects = XFUtils.Effects.iOS;
using RoutingEffects = XFUtils.Effects;

[assembly: ResolutionGroupName("XFUtils")]
[assembly: ExportEffect(typeof(PlatformEffects.ScrollReporterEffect), "ScrollReporterEffect")]
namespace XFUtils.Effects.iOS
{
    public class ScrollReporterEffect : PlatformEffect
    {
        private IDisposable _offsetObserver;
        RoutingEffects.ScrollReporterEffect effect;

        protected override void OnAttached()
        {
            if (Element is ListView == false)
                return;
            effect = (RoutingEffects.ScrollReporterEffect)Element.Effects.FirstOrDefault(e => e is RoutingEffects.ScrollReporterEffect);
            _offsetObserver = ((UITableView)Control).AddObserver("contentOffset", Foundation.NSKeyValueObservingOptions.New, HandleAction);
        }

        private void HandleAction(Foundation.NSObservedChange obj)
        {
            effect.OnScrollEffect(Element, new ScrollEventArgs(((UITableView)Control).ContentOffset.Y));
        }

        protected override void OnDetached()
        {
            _offsetObserver.Dispose();
            _offsetObserver = null;
        }
    }
}
