using PosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PosApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private ViewModelBase _currentChildView;

        //Properties
        public ViewModelBase CurrentChildView {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        //Commands
        public ICommand ShowDashboardView { get; } 
        public ICommand ShowCategoriesView { get; }
        public ICommand ShowLoginView { get; }
        public ICommand ShowServerSetting { get; }

        public MainViewModel()
        {
            ShowDashboardView = new ViewModelCommand(ExecuteShowDashboardViewCommand);
            ShowCategoriesView = new ViewModelCommand(ExecuteShowCategoriesViewCommand);
            ShowLoginView = new ViewModelCommand(ExecuteShowLoginViewCommand);
            ShowServerSetting = new ViewModelCommand(ExecuteShowServerSettingViewCommand);

            ExecuteShowLoginViewCommand(null);

        }

        private void ExecuteShowCategoriesViewCommand(object obj)
        {
            CurrentChildView = new CategoriesViewModel();
        }

        private void ExecuteShowDashboardViewCommand(object obj)
        {
            CurrentChildView = new DashboardViewModel();
        }

        private void ExecuteShowLoginViewCommand(object? obj)
        {
            CurrentChildView = new LoginViewModel();
        }

        private void ExecuteShowServerSettingViewCommand(object? obj)
        {
            MessageBox.Show("test");
            CurrentChildView = new ServerSettingViewModel();
        }
    }
}
