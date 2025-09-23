using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Weather.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

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

        public event PropertyChangedEventHandler? PropertyChanged;

        // When Query is changed, this is called. propertyName is Query.
        private void OnPropertyChanged(string propertyName)
        {
            // Invoke informs the list of subscribers (UI elements bound to Query).
            // and pass the event information (propertyName which is Query property) to
            // update the corresponding dependency property on the UI element (when UI loses focus).
            // "this" refers to the sender of the event which is the instance of the WeatherVM class stored in the UI key.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
