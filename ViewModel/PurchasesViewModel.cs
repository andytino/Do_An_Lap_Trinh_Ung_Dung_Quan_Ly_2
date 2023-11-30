using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using PosApp.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PosApp.ViewModel
{
    public class PurchasesViewModel : ViewModelBase
    {
        private ObservableCollection<Purchase> _dataList;
        private ObservableCollection<bool> _isOpenModal;
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GlobalStore _globalStore;

        public ObservableCollection<Purchase> DataList
        {
            get { return _dataList; }
            set
            {
                if (_dataList != value)
                {
                    _dataList = value;
                    OnPropertyChanged(nameof(DataList));
                }
            }
        }
        public NavigationBarViewModel NavigationBarViewModel { get; }

        public ICommand AddPurchaseCommand { get; }
        public ICommand DeletePurchaseCommand { get; }
        public ICommand EditPurchaseCommand { get; }

        public PurchasesViewModel(NavigationStore navigationStore,
            ModalNavigationStore modalNavigationStore,
            GlobalStore globalStore,
            Func<NavigationBarViewModel> CreateNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _globalStore = globalStore;

            DataList = new ObservableCollection<Purchase>();

            var newPurchaseId = "";

            var addPurchaseModalNavigationService =
                new ModalNavigationService<PurchaseFormViewModel>(
                        _modalNavigationStore,
                        () => new PurchaseFormViewModel(newPurchaseId, _navigationStore,
                        _modalNavigationStore,
                        _globalStore,
                        CreateNavigationBarViewModel)
                    );

            AddPurchaseCommand = new OpenModalCommand(addPurchaseModalNavigationService);
        }
    }
}
