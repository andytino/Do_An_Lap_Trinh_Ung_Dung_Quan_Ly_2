using Microsoft.Win32;
using PosApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PosApp.ViewModel.Command
{

    public class UploadCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public UploadCommand()
        {
            //_navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
        }
    }
}
