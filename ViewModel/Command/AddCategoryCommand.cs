using PosApp.Model;
using PosApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.ViewModel.Command
{
    public class AddCategoryCommand: CommandBase
    {
        private readonly AddCategoryViewModel _addCategoryViewModel;
        private readonly INavigationService _navigationService;

        public AddCategoryCommand(AddCategoryViewModel addCategoryViewModel, INavigationService navigationService)
        {
            _addCategoryViewModel = addCategoryViewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            Category category = new()
            {
                CategoryName = _addCategoryViewModel.CategoryName,
                DisplayID = _addCategoryViewModel.DispayID,
                Description = _addCategoryViewModel.CategoryDescription,
                ImageUrl= _addCategoryViewModel.CategoryImageUrl,
            };

            _navigationService.Navigate();
        }
    }
}
