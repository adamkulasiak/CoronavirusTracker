using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoronavirusTracker.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;

            RaisePropertyChanged(propertyName);
            return true;
        }

        protected static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(App.ApiKey);

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            return client;
        }

        protected async Task<T> GetResponse<T>(string path)
        {
            try
            {
                var client = GetHttpClient();
                var response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error occured: {e}");
            }
            return default;
        }
    }
}
