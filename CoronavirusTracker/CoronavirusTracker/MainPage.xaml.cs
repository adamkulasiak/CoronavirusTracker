using CoronavirusTracker.Models;
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
                                                x.Slug.Contains(e.NewTextValue))
                                                .OrderBy(x => x.Country);
            CountriesList.EndRefresh();
        }

        private void CountriesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var context = BindingContext as MainViewModel;
            context.GoToDetailsCommand.Execute(e.Item);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var context = BindingContext as MainViewModel;
            CountriesList.BeginRefresh();
            string iso2 = ((Button)sender).BindingContext as string;

            string currentFavourites = string.Empty;
            try
            {
                currentFavourites = Application.Current.Properties["favourites"].ToString();
            }
            catch (KeyNotFoundException) { }

            if (currentFavourites == string.Empty)
                currentFavourites = iso2;
            else if (currentFavourites.Split(',').Any(x => x == iso2))
                Application.Current.Properties.Remove(iso2);
            else
                currentFavourites = currentFavourites + $",{iso2}";

            Application.Current.Properties["favourites"] = currentFavourites;
            Application.Current.SavePropertiesAsync();
            if (CountriesList.ItemsSource != null)
            {
                CountriesList.ItemsSource = context.SortCountries();
            }
            
            CountriesList.EndRefresh();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var context = BindingContext as MainViewModel;
            CountriesList.BeginRefresh();
            Application.Current.Properties.Clear();
            Application.Current.SavePropertiesAsync();
            CountriesList.ItemsSource = context.SortCountries();
            CountriesList.EndRefresh();
        }
    }
}
