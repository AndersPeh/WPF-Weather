using System.Collections.ObjectModel;
using System.ComponentModel;
using WPF_Weather.Model;
using WPF_Weather.ViewModel.Commands;
using WPF_Weather.ViewModel.Helpers;

namespace WPF_Weather.ViewModel;

public class WeatherVM : INotifyPropertyChanged
{
    private string query = string.Empty;

    // The Query is binded with the Textbox in WeatherWindow, any changes in the textbox will change the Query.
    public string Query
    {
        get { return query; }
        set
        {
            query = value;
            // When Query is changed due to the textbox, it will trigger PropertyChanged Event.
            OnPropertyChanged("Query");
        }
    }

    // Observable
    public ObservableCollection<City> Cities { get; set; }

    private CurrentConditions? currentConditions;

    public CurrentConditions? CurrentConditions
    {
        get { return currentConditions; }
        set
        {
            currentConditions = value;
            OnPropertyChanged("CurrentConditions");
        }
    }

    private City? selectedCity;

    public City? SelectedCity
    {
        get { return selectedCity; }
        set
        {
            selectedCity = value;
            OnPropertyChanged("SelectedCity");
            GetCurrentConditions();
        }
    }

    public SearchCommand searchCommand { get; set; }

    public WeatherVM()
    {
        if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
        {
            SelectedCity = new City { LocalizedName = "Brisbane" };
            CurrentConditions = new CurrentConditions
            {
                WeatherText = "Sunny",
                Temperature = new Temperature { Metric = new Units { Value = "21" } },
            };
        }

        // this refers to the current instance of WeatherVM class.
        searchCommand = new SearchCommand(this);

        Cities = new ObservableCollection<City>();
    }

    private async Task GetCurrentConditions()
    {
        Query = string.Empty;

        Cities.Clear();

        if (SelectedCity != null)
        {
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }
    }

    public async Task MakeQuery()
    {
        var cities = await AccuWeatherHelper.GetCities(Query);

        Cities.Clear();

        foreach (var city in cities)
        {
            Cities.Add(city);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    // When Query is changed, this is called. propertyName is what is passed to OnPropertyChanged.
    private void OnPropertyChanged(string propertyName)
    {
        // Invoke informs the list of subscribers (UI elements bound to it in Weather Window).
        // and pass the event information (propertyName) to
        // update the corresponding dependency property on the UI element (when UI loses focus).
        // "this" refers to the sender of the event which is the instance of the WeatherVM class stored in the UI key.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
