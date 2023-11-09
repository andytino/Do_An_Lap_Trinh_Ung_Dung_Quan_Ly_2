using PosApp.Stores;
using PosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.Services
{
    public class MainLayoutNavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;

        public MainLayoutNavigationService(
            NavigationStore navigationStore,
            Func<TViewModel> createViewModel,
            Func<NavigationBarViewModel> createNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createNavigationBarViewModel = createNavigationBarViewModel;
        }

        public void Navigate()
        {
            MessageBox.Show("aa");
            _navigationStore.CurrentViewModel = new MainLayoutViewModel(_createNavigationBarViewModel(), _createViewModel());
        }
    }
}
