using CoronavirusTracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            var countries = await GetCountries();
            Countries = countries;
            IsSearchBarVisible = true;
            IsRunning = false;
            IsVisible = false;
        }

        private static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(App.ApiKey);

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            return client;
        }

        private async Task<List<CountryModel>> GetCountries()
        {
            try
            {
                var client = GetHttpClient();
                var response = await client.GetAsync("countries");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var countries = JsonConvert.DeserializeObject<List<CountryModel>>(content);
                    return countries;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}
