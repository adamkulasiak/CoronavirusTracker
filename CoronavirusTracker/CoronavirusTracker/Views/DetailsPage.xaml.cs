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

namespace CoronavirusTracker.Views
{
    [DesignTimeVisible(false)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(CountryModel country)
        {
            InitializeComponent();
            BindingContext = new DetailsViewModel(Navigation, country);
        }
    }
}