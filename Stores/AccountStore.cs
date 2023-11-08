using PosApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Stores
{
    public class AccountStore
    {
        public Account _currentAccount = new() { }
        ;
        public Account CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                CurrentAccountChanged?.Invoke();
            }

        }

        public bool IsLoggedIn => CurrentAccount != null;
        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentAccount = null;
        }
    }
}
