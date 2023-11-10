using PosApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.ViewModel
{

    public class ProductsViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _dataList;

        public ObservableCollection<Product> DataList
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

        public ProductsViewModel()
        {
            DataList = new ObservableCollection<Product>();
            DataList.Add(new Product
            {
                DisplayID = "1",
                ProductName = "Test TestTe stTestTest 1",
                Unit="cai",
                Price = "10/10/2020",
                Quantity = "20",
                Quality= "1",
                ImageUrl="",
                Description = "Day la description"
            });

            DataList.Add(new Product
            {
                DisplayID = "2",
                ProductName = "Test 2",
                Unit = "cai",
                Price = "10/10/2020",
                Quantity = "20",
                Quality = "1",
                ImageUrl = "",
                Description = "Day la description"
            });
        }
    }
}
