using CoronavirusTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoronavirusTracker
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(Navigation);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var context = BindingContext as MainViewModel;
            CountriesList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                CountriesList.ItemsSource = context.Countries;
            else
                CountriesList.ItemsSource = context.Countries
                                                .Where(x => x.Country.Contains(e.NewTextValue) ||
                                                x.ISO2.Contains(e.NewTextValue) ||
                                                x.Slug.Contains(e.NewTextValue));
            CountriesList.EndRefresh();
        }
    }
}
