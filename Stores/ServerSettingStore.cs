using PosApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Stores
{
    public class ServerSettingStore
    {
        private ServerSetting? _serverSetting;
        public ServerSetting ServerSetting
        {
            get => _serverSetting;
            set
            {


                _serverSetting = value;
                CurrentServerSettingChanged?.Invoke();


            }
        }

        public event Action CurrentServerSettingChanged;
    }
}
