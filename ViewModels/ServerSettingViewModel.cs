using PosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PosApp.ViewModels
{
    public class ServerSettingViewModel : ViewModelBase
    {
        //// Fields
        //private string _server;
        //private string _database;
        //private string _errorMessage;
        
        //public string Server
        //{
        //    get => _server;
        //    set
        //    {
        //        _server = value;
        //        OnPropertyChanged(nameof(Server));
        //    }
        //}
        //public string Database
        //{
        //    get => _database;
        //    set
        //    {
        //        _database = value;
        //        OnPropertyChanged(nameof(Database));
        //    }
        //}

        //public string ErrorMessage
        //{
        //    get => _errorMessage;
        //    set
        //    {
        //        _errorMessage = value;
        //        OnPropertyChanged(nameof(ErrorMessage));
        //    }
        //}

        //// Commands
        //public ICommand SaveSettingCommand { get; }
        
        //// Constructor
        //public ServerSettingViewModel()
        //{
        //    SaveSettingCommand = new ViewModelCommand(ExecuteSaveSetting, CanExecuteSaveSetting);
        //}

        //private bool CanExecuteSaveSetting(object obj)
        //{
        //    bool validData;
        //    if (string.IsNullOrWhiteSpace(_server))
        //    {
        //        validData = false;
        //    }
        //    else
        //    {
        //        validData = true;
        //    }

        //    return validData;
        //}

        //private void ExecuteSaveSetting(object obj)
        //{
            
        //}
    }
}
