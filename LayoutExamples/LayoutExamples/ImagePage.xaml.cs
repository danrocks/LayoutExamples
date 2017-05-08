using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LayoutExamples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage : ContentPage
    {
        public ImagePage()
        {
            InitializeComponent();
            BindingContext = new ImagePageViewModel();
            AddImageListView();
        }

        /// <summary>
        /// I am not overly convinced by Xaml approach.
        /// I can't see that it will alllow me to add 1,2,03 images and place them on the screen 
        /// in a way that takes into account orientation to use space efficiently.
        /// So I am seeing how the world looks if creating the listview in c#.
        /// </summary>
        private void AddImageListView()
        {

            ListView lv = new ListView();
            lv.ItemsSource = (BindingContext as ImagePageViewModel).images;

            lv.HasUnevenRows = true;
            DataTemplate dt = new DataTemplate(() =>
            {
                System.Diagnostics.Debug.WriteLine("View Template...");
                ViewCell vc = new ViewCell();
                StackLayout sl = new StackLayout();
                sl.Padding = new Thickness(0, 20, 0, 0);

                Image i = new Image { Aspect = Aspect.AspectFit };

                i.BackgroundColor = Xamarin.Forms.Color.Orange;
                i.SetBinding(Image.SourceProperty, "Source");
                sl.Children.Add(i);
                vc.View = sl;
                return vc;
            });
            lv.ItemTemplate = dt;

            RouteStack.Children.Add(lv);
        }
    }
}
