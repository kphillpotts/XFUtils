using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFUtilsSample
{
    public partial class MainPage : ContentPage
    {
        List<string> listItems = new List<string>();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            for (int i = 0; i < 100; i++)
            {
                listItems.Add($"List item {i}");
            }
            MyListView.ItemsSource = listItems;

            var eff = new XFUtils.Effects.ScrollReporterEffect();
            MyListView.Effects.Add(eff);
            eff.ScrollChanged += Eff_ScrollChanged;
        }

        private void Eff_ScrollChanged(object arg1, XFUtils.Effects.ScrollEventArgs e)
        {
            this.Title = $"ScrollY = {e.Y}";

            var scale = 2.5;

            if (e.Y < 1)
                HeaderImage.TranslationY = 0;
            else
                HeaderImage.TranslationY = (int)e.Y / scale;
        }
    }
}
