using PosApp.Model;
using PosApp.Stores;
using PosApp.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PosApp.ViewModel
{
    public class PurchaseDetailFormViewModel : ViewModelBase
    {
        private ObservableCollection<PurchaseDetail> _dataList;

        public ObservableCollection<PurchaseDetail> DataList
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
        public ICommand RemovePurchaseDetailCommand { get; }

        public PurchaseDetailFormViewModel(ObservableCollection<PurchaseDetail> dataList)
        {
            DataList = dataList;
            RemovePurchaseDetailCommand = new ViewModelCommand(ExecuteRemovePurchaseDetailCommand, CanExecuteRemovePurchaseDetailCommand);
        }
        private void ExecuteRemovePurchaseDetailCommand(object parameter)
        {
            MessageBox.Show("aaa");

            var a = parameter as PurchaseDetail;
            DataList.Remove(a);
        }

        private bool CanExecuteRemovePurchaseDetailCommand(object parameter)
        {
            return true;
        }
    }
}
