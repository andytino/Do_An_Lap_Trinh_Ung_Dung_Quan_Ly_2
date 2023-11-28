using PosApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.ViewModel.Command
{
    public class OpenModalCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public OpenModalCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }

    }
}
