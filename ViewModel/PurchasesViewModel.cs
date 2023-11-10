using PosApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.ViewModel
{
    public class PurchasesViewModel : ViewModelBase
    {
        private ObservableCollection<Purchase> _dataList;

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

        public PurchasesViewModel()
        {
            DataList = new ObservableCollection<Purchase>();
            DataList.Add(new Purchase
            {
                DisplayID = "1",
                SupplierName = "Test 1",
                Date = "10/10/2020",
                Total = "20",
                Description = "Day la description"
            });

            DataList.Add(new Purchase
            {
                DisplayID = "2",
                SupplierName = "Test 2",
                Date = "10/10/2023",
                Total = "30",
                Description = "Day la description  2"
            });
        }
    }
}
