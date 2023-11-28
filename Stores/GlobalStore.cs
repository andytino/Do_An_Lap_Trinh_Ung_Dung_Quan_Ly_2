using PosApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Stores
{
    public class GlobalStore
    {
        public Global _currentGlobal = new() { }
        ;
        public Global CurrentGlobal
        {
            get => _currentGlobal;
            set
            {
                _currentGlobal = value;
                CurrentGlobalChanged?.Invoke();
            }

        }

        public bool IsLoggedIn => CurrentGlobal != null;
        public event Action CurrentGlobalChanged;

        public void Logout()
        {
            CurrentGlobal = null;
        }
    }
}
