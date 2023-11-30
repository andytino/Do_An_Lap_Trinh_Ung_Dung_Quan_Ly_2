using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;

namespace PosApp.ViewModel
{
    public class ViewWithErrorModelBase : INotifyPropertyChanged, INotifyDataErrorInfo, IDisposable
    {
        public bool HasErrors => throw new NotImplementedException();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose() { }

        public IEnumerable GetErrors(string? propertyName)
        {
            throw new NotImplementedException();
        }
    }
}