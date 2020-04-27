using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinHTTP_Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPageMaster : ContentPage
    {
        public ListView ListView;

        private string source;
        private int numPhoto = 0;


        public RootPageMaster()
        {
            InitializeComponent();

            BindingContext = new RootPageMasterViewModel();
            numPhoto = Int32.Parse(Preferences.Get("numPhoto", "0"));
            name.Text = Preferences.Get("name", "Stan");
            lastname.Text = Preferences.Get("lastname", "Lee");
            source = Preferences.Get("photoProfile", "hulk.jpg");
            Preferences.Get("birth", "Né le 28/12/1922");
            photoProfile.Source = source;
            //ListView = MenuItemsListView;
        }

        class RootPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<RootPageMasterMenuItem> MenuItems { get; set; }


            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
        public void Name_Changed(System.Object sender, System.EventArgs e)
        {
            Preferences.Set("name", name.Text);
        }
        public void Lastname_Changed(System.Object sender, System.EventArgs e)
        {
            Preferences.Set("lastname", lastname.Text);
        }

        public void Birth_Changed(System.Object sender, System.EventArgs e)
        {
            if (changeBirth.Text.Length == 8 && Int32.Parse(changeBirth.Text.Substring(0, 2)) >=1 && Int32.Parse(changeBirth.Text.Substring(0, 2)) <= 31 
                && Int32.Parse(changeBirth.Text.Substring(2, 2)) >= 1 && Int32.Parse(changeBirth.Text.Substring(2, 2)) <= 12
                && Int32.Parse(changeBirth.Text.Substring(2, 2)) >= 1 && Int32.Parse(changeBirth.Text.Substring(2, 2)) <= 12
                && Int32.Parse(changeBirth.Text.Substring(4, 4)) >= 1900 && Int32.Parse(changeBirth.Text.Substring(4, 4)) <= 2020)
            {
                string format = changeBirth.Text.Substring(0, 2) + "/" + changeBirth.Text.Substring(2, 2) + "/" + changeBirth.Text.Substring(4, 4);
                birth.Text = "Né le " + format;
                Preferences.Set("birth", birth.Text);
                changeBirth.Text = "";
            }
        }

        public void ChangePP(object sender, EventArgs e)
        {
            numPhoto++;
            if (numPhoto >= 9)
            {
                numPhoto = 0;
            }
            switch(numPhoto)
            {
                case 0: 
                    source = "hulk.jpg";
                    break;
                case 1:
                    source = "thor.jpg";
                    break;
                case (2):
                    source = "ironman.jpg";
                    break;
                case (3):
                    source = "spiderman.jpg";
                    break;
                case (4):
                    source = "captainmarvel.jpg";
                    break;
                case (5):
                    source = "thanos.jpg";
                    break;
                case (6):
                    source = "blackpanther.jpg";
                    break;
                case (7):
                    source = "loki.jpg";
                    break;
                case (8):
                    source = "nickfury.jpg";
                    break;
            }
            Preferences.Set("numPhoto", ""+numPhoto);
            Preferences.Set("photoProfile", source);
            photoProfile.Source = source;
        }
    }
}