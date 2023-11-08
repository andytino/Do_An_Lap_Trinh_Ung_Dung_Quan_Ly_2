using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.ViewModel
{

    public class CategoriesViewModel : ViewModelBase
    {
        public NavigationBarViewModel NavigationBarViewModel { get; }

        public CategoriesViewModel(NavigationBarViewModel navigationBarViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
        }
    }
}
