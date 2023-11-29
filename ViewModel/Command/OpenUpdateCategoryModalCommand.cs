using Microsoft.Data.SqlClient;
using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.ViewModel.Command
{
    public class OpenUpdateCategoryModalCommand : CommandBase
    {
        private readonly IParameterNavigationService<string> _navigationService;

        public OpenUpdateCategoryModalCommand(IParameterNavigationService<string> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            var id = parameter as string;
            _navigationService.Navigate(id);
        }
    }
}
