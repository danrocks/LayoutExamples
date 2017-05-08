using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
namespace LayoutExamples
{
    //[ViewType(typeof(ImagePage))]
    class ImagePageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Implementing INotifyPropertyChanged by adding this event.
        /// Use-case is that View causes this ViewModel to up date. Setter on the updated property raises the Poperty changed event, 
        /// which other things migh tbe listending to
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// CallerMemberName is complier attribute that causes the name of the calling method to be captured.
        /// It means all the setters can pass this info by "magic".
        /// </summary>
        /// <param name="name"></param>
        void OnPropertyChanged([CallerMemberName] string name= "") {
            // ?. is the null new coalescing operator
            //Invoke the PropertyChanged event.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Image> images = new ObservableCollection<Image>();       

        public ImagePageViewModel()
        {           
            List<string> urls = new List<string>() {
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRgCcmRWT2cX761A6HdybauEKigYB6WVtuE4oJ1-FXlrqe0VYD4"
                ,"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/CoCo3system.jpg/220px-CoCo3system.jpg"
                ,"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTMhiU6igl5OeCZ4BtmIk0kGtkixJ4wqHUZUR3Ybx64noiNNx1T"
                ,"https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRkfmuN8u67jcxya3UwHZdwAJ4DJ-0LscqcO4DBr_WjLojggKhuZg"
            };

            foreach (string s in urls)
            {
                images.Add(new Image { Source = ImageSource.FromUri(new Uri(s)) });
            }

            //urls.Select((u) => (images.Add(new Image { Source = ImageSource.FromUri(new Uri(u)) })));
        }

        private string _SomeText;

        public string SomeText {
            get { return _SomeText; }
                set { _SomeText = value;
                OnPropertyChanged();
                // nameof is C#6.
                // Previously we might have had to pass string to reference the property we are interested.
                // nameof means the compiler provides the string, so checks the poperty actually exists.
                // The actual logic here is capturing the fact that chanding "SomeText" means a change in SomeNotes.
                OnPropertyChanged( nameof(SomeNotes));
            } }

        public string SomeNotes { get{
                return $"You entered: {SomeText}";
            } }

        private Command _AddImage;

        public Command AddImage
        {
            get
            {
                string s = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQU4rfzY0k55sCJFWkTuXWrM6luEkDZuyTEZNBbVI1hPz9wda58";

                return _AddImage ?? (_AddImage= new Command(execute:()=> images.Add(new Image { Source = ImageSource.FromUri(new Uri(s)) })));                
            }
        }
    }
}
