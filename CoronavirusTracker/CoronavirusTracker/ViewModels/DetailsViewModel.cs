using CoronavirusTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoronavirusTracker.ViewModels
{
    public class DetailsViewModel: BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly CountryModel _countryModel;

        public DetailsViewModel(INavigation navigation, CountryModel country)
        {
            _navigation = navigation;
            _countryModel = country;
            Initialize();
        }

        private List<Stats> _stats;
        public List<Stats> Stats
        {
            get => _stats;
            set => SetProperty(ref _stats, value);
        }

        private string _country;
        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        private int _confirmed;
        public int Confirmed
        {
            get => _confirmed;
            set => SetProperty(ref _confirmed, value);
        }

        private int _deaths;
        public int Deaths
        {
            get => _deaths;
            set => SetProperty(ref _deaths, value);
        }

        private int _recovered;
        public int Recovered
        {
            get => _recovered;
            set => SetProperty(ref _recovered, value);
        }

        private int _active;
        public int Active
        {
            get => _active;
            set => SetProperty(ref _active, value);
        }

        private string _datetime;
        public string Datetime
        {
            get => _datetime;
            set => SetProperty(ref _datetime, value);
        }

        private bool _isContentVisible;
        public bool IsContentVisible
        {
            get => _isContentVisible;
            set => SetProperty(ref _isContentVisible, value);
        }

        private bool _isLoaderVisible;
        public bool IsLoaderVisible
        {
            get => _isLoaderVisible;
            set => SetProperty(ref _isLoaderVisible, value);
        }

        private string _flagUrl;
        public string FlagUrl
        {
            get => _flagUrl;
            set => SetProperty(ref _flagUrl, value);
        }

        private async Task Initialize()
        {
            IsContentVisible = false;
            IsLoaderVisible = true;
            var stats = await GetResponse<List<Stats>>($"/total/country/{_countryModel.ISO2}");
            FlagUrl = string.Format(App.FlagUrl, _countryModel.ISO2);
            Country = stats.Select(x => x.Country).FirstOrDefault();
            Confirmed = stats.Select(x => x.Confirmed).Last();
            Deaths = stats.Select(x => x.Deaths).Last();
            Recovered = stats.Select(x => x.Recovered).Last();
            Active = stats.Select(x => x.Active).Last();
            Datetime = stats.Select(x => x.Date).Last().ToString("dd-MM-yyyy");
            IsContentVisible = true;
            IsLoaderVisible = false;
        }

    }
}
