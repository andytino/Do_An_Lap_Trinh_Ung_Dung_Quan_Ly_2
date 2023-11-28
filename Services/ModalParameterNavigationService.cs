using PosApp.Stores;
using PosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Services
{
    public class ModalParameterNavigationService<TParameter, TViewModel>: IParameterNavigationService<TParameter> where TViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Func<TParameter, TViewModel> _createViewModel;

        public ModalParameterNavigationService(
            ModalNavigationStore modalNavigationStore,
            Func<TParameter, TViewModel> createViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(TParameter parameter)
        {
            _modalNavigationStore.CurrentViewModel = _createViewModel(parameter);
        }
    }
}
