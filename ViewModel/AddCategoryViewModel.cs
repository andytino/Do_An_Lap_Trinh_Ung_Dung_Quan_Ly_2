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
    public class AddCategoryViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Category _category;
        private string _displayID;
        private string _categoryName;
        private string _categoryDescription;
        private string _categoryImageUrl;

        public string DispayID
        {
            get { return _displayID; }
            set
            {
                _displayID = value;
                OnPropertyChanged(nameof(DispayID));
            }
        }

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _displayID = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        public string CategoryDescription
        {
            get => _categoryDescription;
            set
            {
                _categoryDescription = value;
                OnPropertyChanged(nameof(CategoryDescription));
            }
        }

        public string CategoryImageUrl
        {
            get { return _categoryImageUrl; }
            set
            {
                _categoryImageUrl = value;
                OnPropertyChanged(nameof(CategoryImageUrl));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddCategoryViewModel(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
            var closeService = new CloseModalNavigationService(_modalNavigationStore);
            SaveCommand = new AddCategoryCommand(this, closeService);
            CancelCommand = new CloseModalCommand(closeService);
        }
    }
}
