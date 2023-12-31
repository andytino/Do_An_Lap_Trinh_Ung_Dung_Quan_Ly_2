﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;

namespace PosApp.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose() { }

        public event EventHandler<string> ErrorOccured;

        protected void RaiseError(string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }

    }
}