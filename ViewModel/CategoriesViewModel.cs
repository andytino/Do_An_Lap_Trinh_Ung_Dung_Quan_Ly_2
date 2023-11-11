using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PosApp.ViewModel.Command;

namespace PosApp.ViewModel
{
    public class CategoriesViewModel : ViewModelBase
    {
        private ObservableCollection<Category> _dataList;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Func<AddCategoryViewModel> _createAddCategoryViewModel;
        private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;

        public ObservableCollection<Category> DataList
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

        public ICommand AddCategoryCommand { get; }

        public CategoriesViewModel(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;

            DataList = new ObservableCollection<Category>();
            DataList.Add(new Category
            {
                DisplayID = "1",
                CategoryName = "name 1",
                Description = "Day la description",
                ImageUrl = "/Assets/Images/laptop.png"
            });

            DataList.Add(new Category
            {
                DisplayID = "2",
                CategoryName = "name 2",
                Description = "Day la description",
                ImageUrl = "/Assets/Images/smartwatch.png"
            });

            var addCategoryModalNavigationService =
                new ModalNavigationService<AddCategoryViewModel>(
                    _modalNavigationStore,
                    () => new AddCategoryViewModel(_modalNavigationStore));

            AddCategoryCommand = new OpenModalCommand(addCategoryModalNavigationService);
        }
    }
}
