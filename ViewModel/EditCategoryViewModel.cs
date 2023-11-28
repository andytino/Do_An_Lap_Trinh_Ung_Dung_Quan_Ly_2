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
    public class EditCategoryViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GlobalStore _globalStore;
        private readonly Category _category;
        private string _displayID;
        private string _categoryName;
        private string _categoryDescription;
        private string _categoryImageUrl;
        private ImageSource _categoryImageSource;

        public string DisplayID
        {
            get { return _displayID; }
            set
            {
                _displayID = value;
                OnPropertyChanged(nameof(DisplayID));
            }
        }

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

        public EditCategoryViewModel(Category category, 
            NavigationStore navigationStore,
            ModalNavigationStore modalNavigationStore,
            GlobalStore globalStore,
            Func<NavigationBarViewModel> CreateNavigationBarViewModel)
        {
            DisplayID = category.DisplayID;
            CategoryName = category.CategoryName;
            CategoryDescription = category.Description;
            CategoryImageUrl = category.ImageUrl;

            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _globalStore = globalStore;

            var closeService = new CloseModalNavigationService(_modalNavigationStore);
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
    }
}
