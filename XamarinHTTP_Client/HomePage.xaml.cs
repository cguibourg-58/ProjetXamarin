using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinHTTP_Client.ViewModels;

namespace XamarinHTTP_Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
        void Entry_Completed(System.Object sender, System.EventArgs e)
        {
            if (BindingContext is MainViewModel vm)
            {
                vm.GetCommand.Execute(sender);
            }
        }
    }
}