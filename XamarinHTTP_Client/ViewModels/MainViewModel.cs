using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinHTTP_Client.Service;

namespace XamarinHTTP_Client.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string errorMessage, city, windSpeed, humidity, visibility, temperature;
        string querry;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string WindSpeed
        {
            get { return windSpeed; }
            set { windSpeed = value; }
        }
        public string Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }

        public string Visibility
        {
            get { return visibility; }
            set { visibility = value; }
        }

        public string Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        public string Querry
        {
            get { return querry; }
            set { SetProperty(ref querry, value); }
        }
        string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }
        public ICommand GetCommand => new Command(() => Task.Run(LoadWeatherData));
        async Task LoadWeatherData()
        {
            if (IsBusy) return;
            IsBusy = true;
            var client = HttpService.GetInstance();
            var result = await
            client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={Querry}&APPID=6fcb5a969e58b25ffb37b7426ac18d12&units=metric&lang=fr");
            var serializedResponse = await result.Content.ReadAsStringAsync();
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(serializedResponse);
            if (weatherResponse?.Weather != null && weatherResponse.Weather.Any())
            {
                ErrorMessage = "";
                City = weatherResponse.Name;
                WindSpeed = $"{weatherResponse.Wind.Speed} km/h";
                Humidity = $"{weatherResponse.Main.Humidity}%";
                Visibility = $"{weatherResponse.Visibility}m";
                Temperature = $"{weatherResponse.Main.Temp}°";
            }
            else
            {
                ErrorMessage = weatherResponse?.Message ?? "Unknown error";
                City = "unknown";
                WindSpeed = "unknown";
                Humidity = "unknown";
                Visibility = "unknown";
                Temperature = "unknown";
            }
            IsBusy = false;
        }
    }
}
