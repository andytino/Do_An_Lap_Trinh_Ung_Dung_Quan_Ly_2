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
        public ServerSetting _serverSetting;
        public ServerSetting ServerSetting
        {
            get => _serverSetting;
            set
            {
                _serverSetting = value;
            }
        }
    }
}
