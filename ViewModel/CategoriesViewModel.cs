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
using System.Windows;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data.Common;

namespace PosApp.ViewModel
{
    public class CategoriesViewModel : ViewModelBase
    {
        private ObservableCollection<Category> _dataList;
        private ObservableCollection<bool> _isOpenModal;
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GlobalStore _globalStore;

        public ObservableCollection<bool> IsOpenModal
        {
            get { return _isOpenModal; }
            set
            {
                _isOpenModal = value;
                OnPropertyChanged(nameof(IsOpenModal));
            }
        }
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
        public ICommand DeleteCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }

        public CategoriesViewModel(NavigationStore navigationStore,
            ModalNavigationStore modalNavigationStore,
            GlobalStore globalStore,
            Func<NavigationBarViewModel> CreateNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _globalStore = globalStore;

            DataList = new ObservableCollection<Category>();

            var newCategoryId = "";
            var addCategoryModalNavigationService =
                new ModalNavigationService<AddCategoryViewModel>(
                        _modalNavigationStore,
                        () => new AddCategoryViewModel(newCategoryId,_navigationStore,
                        _modalNavigationStore,
                        _globalStore,
                        CreateNavigationBarViewModel)
                    );

            var mainLayoutNavigationService = new MainLayoutNavigationService<CategoriesViewModel>(
                    _navigationStore,
                    () => new CategoriesViewModel(_navigationStore, _modalNavigationStore, _globalStore, CreateNavigationBarViewModel),
                    CreateNavigationBarViewModel
                );

            var editCategoryModalNavigationService =
                new ModalParameterNavigationService<string, AddCategoryViewModel>(
                _modalNavigationStore,
                (parameter) => new AddCategoryViewModel(parameter,
                        _navigationStore,
                        _modalNavigationStore,
                        _globalStore,
                        CreateNavigationBarViewModel)
                    );

            AddCategoryCommand = new OpenModalCommand(addCategoryModalNavigationService);
            EditCategoryCommand = new OpenUpdateCategoryModalCommand(editCategoryModalNavigationService);
            DeleteCategoryCommand = new DeleteCategoryCommand(mainLayoutNavigationService, _globalStore);

            LoadCategories();

        }

        private void LoadCategories()
        {
            string sql = """
                        SELECT * 
                        FROM Categories
                        JOIN Images ON Categories.ImageID = Images.ImageID
                        WHERE DeletedAt IS NULL;
                """;

            var connection = _globalStore.CurrentGlobal.Connection;
            if (connection != null)
            {
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();
                var _dataList = new ObservableCollection<Category>();

                while (reader.Read())
                {
                    Category category = new Category()
                    {
                        CategoryID = (string)reader["CategoryID"],
                        CategoryName = (string)reader["CategoryName"],
                        Description = (string)reader["Description"],
                        ImageUrl = (string)reader["ImagePath"]
                    };

                    _dataList.Add(category);

                }

                reader.Close();

                DataList = _dataList;
            }
        }
    }
}
