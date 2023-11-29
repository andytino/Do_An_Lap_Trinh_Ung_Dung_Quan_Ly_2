﻿using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using PosApp.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PosApp.ViewModel
{
    public class AddCategoryViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GlobalStore _globalStore;
        
        private string _categoryName;
        private string _categoryDescription;
        private string _categoryImageUrl;
        private ImageSource _categoryImageSource;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
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

        public ImageSource CategoryImageSource
        {
            get { return _categoryImageSource; }
            set
            {

                _categoryImageSource = value;
                OnPropertyChanged(nameof(CategoryImageSource));

            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ICommand UploadCommand { get; }

        public AddCategoryViewModel(string categoryId, NavigationStore navigationStore,
            ModalNavigationStore modalNavigationStore,
            GlobalStore globalStore,
            Func<NavigationBarViewModel> CreateNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _globalStore = globalStore;

            var closeService = new CloseModalNavigationService(_modalNavigationStore);

            CompositeNavigationService navigationService = new CompositeNavigationService(
                closeService,
               new MainLayoutNavigationService<CategoriesViewModel>(
                _navigationStore,
                () => new CategoriesViewModel(_navigationStore, _modalNavigationStore, _globalStore, CreateNavigationBarViewModel),
                CreateNavigationBarViewModel
                )
             );

            if (!categoryId.IsNullOrEmpty())
            {
                LoadCategory(categoryId);
            }

            SaveCommand = new SaveCategoryCommand(this, navigationService, globalStore);
            CancelCommand = new CloseModalCommand(closeService);

            UploadCommand = new ViewModelCommand(ExecuteUploadCommand, CanExecuteUploadCommand);
        }

        private void ExecuteUploadCommand(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                CategoryImageUrl = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                CategoryImageSource = new BitmapImage(fileUri);
            }
        }

        private bool CanExecuteUploadCommand(object parameter)
        {
            return true;
        }


        private void LoadCategory(string catergoryId)
        {
            string getCategorySql = $"""
                        SELECT *
                        FROM Categories
                        JOIN Images ON Categories.ImageID = Images.ImageID
                        WHERE CategoryID = '{catergoryId}'
                """
            ;
            var connection = _globalStore._currentGlobal.Connection;

            if (connection != null)
            {
                var getCategoryCommand = new SqlCommand(getCategorySql, connection);
                var reader = getCategoryCommand.ExecuteReader();
                if (reader.Read())
                {

                    CategoryName = (string)reader["CategoryName"];
                    CategoryDescription = (string)reader["Description"];
                    CategoryImageUrl = (string)reader["ImagePath"];

                }

                reader.Close();
            }
        }
    }
}
