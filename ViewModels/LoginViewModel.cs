using PosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PosApp.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {

        private ViewModelCommand? showDashboardView;
        public ICommand ShowDashboardView => showDashboardView ??= new ViewModelCommand(PerformShowDashboardView);

        private void PerformShowDashboardView(object? commandParameter)
        {
        }
    }
}
