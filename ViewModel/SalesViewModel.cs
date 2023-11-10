using PosApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.ViewModel
{
    public class SalesViewModel : ViewModelBase
    {
        private ObservableCollection<Sale> _dataList;

        public ObservableCollection<Sale> DataList
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

        public SalesViewModel()
        {
            DataList = new ObservableCollection<Sale>();
            DataList.Add(new Sale
            {
                DisplayID = "1",
                CustomerName = "Test 1",
                Date = "10/10/2020",
                Total = "20",
                Description = "Day la description"
            });

            DataList.Add(new Sale
            {
                DisplayID = "2",
                CustomerName = "Test 2",
                Date = "10/10/2023",
                Total = "30",
                Description = "Day la description  2"
            });
        }
    }
}
