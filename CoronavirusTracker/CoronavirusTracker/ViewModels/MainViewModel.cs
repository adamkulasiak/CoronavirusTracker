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
            var countries = await GetCountries();
            Countries = countries;
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
