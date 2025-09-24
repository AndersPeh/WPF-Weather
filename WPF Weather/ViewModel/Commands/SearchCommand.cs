using System.Windows.Input;

namespace WPF_Weather.ViewModel.Commands;

public class SearchCommand : ICommand
{
    public WeatherVM VM { get; set; }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public SearchCommand(WeatherVM vm)
    {
        VM = vm;
    }

    public bool CanExecute(object? parameter)
    {
        var query = parameter as string;

        if (string.IsNullOrEmpty(query))
            return false;

        return true;
    }

    // Send GET Request to get list of cities matching the query.
    public async void Execute(object? parameter)
    {
        await VM.MakeQuery();
    }
}
