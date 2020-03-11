using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.Settings
{
    public class SysSettingManager
    {
        private static SysSettingManager _instance = null;
        public static SysSettingManager Instance
        {
            get
            {
                if (_instance==null)
                {
                    _instance = new SysSettingManager();
                }
                return _instance;
            }
        }

        private bool _needReloadCef = false;

        public bool NeedReloadCef
        {
            get { return _needReloadCef; }
            set { _needReloadCef = value; }
        }

        private string _userAgent = null;
        public string UserAgent
        {
            get
            {
                return _userAgent;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(_userAgent)&&string.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                if (value!=_userAgent)
                {
                    _userAgent = value;
                    SunCreate.Common.ConfigHelper.SetConfigValue("UserAgent", _userAgent);
                    if (!_needReloadCef)
                    {
                        _needReloadCef = true;
                    }
                }
            }
        }
        public SysSettingManager()
        {
            _userAgent = SunCreate.Common.ConfigHelper.GetConfigString("UserAgent");
        }
    }
}
