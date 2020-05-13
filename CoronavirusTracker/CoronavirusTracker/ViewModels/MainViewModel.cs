using CoronavirusTracker.Models;
using CoronavirusTracker.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoronavirusTracker.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        private readonly INavigation _navigation;

        private List<CountryModel> _countries;
        public List<CountryModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private bool _isSearchBarVisible;
        public bool IsSearchBarVisible
        {
            get => _isSearchBarVisible;
            set => SetProperty(ref _isSearchBarVisible, value);
        }

        private string _iconPath;
        public string IconPath
        {
            get => _iconPath;
            set => SetProperty(ref _iconPath, value);
        }

        public MainViewModel()
        {
            Initialize();
        }

        public MainViewModel(INavigation navigation): this()
        {
            _navigation = navigation;
        }

        private async Task Initialize()
        {
            IsSearchBarVisible = false;
            IsRunning = true;
            IsVisible = true;
            var countries = await GetResponse<List<CountryModel>>("countries");
            var sorted = SortCountries(countries);
            Countries = sorted;
            IconPath = "https://img.icons8.com/ios/50/000000/star.png";
            IsSearchBarVisible = true;
            IsRunning = false;
            IsVisible = false;
        }

        public List<CountryModel> SortCountries(IEnumerable<CountryModel> countries = null)
        {
            if (countries == null)
            {
                countries = Countries;
            }
            string favourites = string.Empty;
            try
            {
                favourites = Application.Current.Properties["favourites"].ToString();
            }
            catch (KeyNotFoundException) { }

            var listOfFavourites = favourites.Split(',').ToList();
            foreach (var country in countries)
            {
                if (listOfFavourites.Any(x => country.ISO2 == x))
                {
                    country.IsFavourite = true;
                    country.IconPath = "https://img.icons8.com/ios-filled/50/000000/star.png";
                } else
                {
                    country.IsFavourite = false;
                    country.IconPath = "https://img.icons8.com/ios/50/000000/star.png";
                }
            }

            return countries.OrderByDescending(x => x.IsFavourite).ThenBy(x => x.Country).ToList();
        }

        private ICommand _goToDetailsCommand;
        public ICommand GoToDetailsCommand => _goToDetailsCommand ?? (_goToDetailsCommand = new Command<CountryModel>(country => OnGoToDetails(country)));

        private void OnGoToDetails(CountryModel country)
        {
            _navigation.PushAsync(new DetailsPage(country));
        }
    }
}
