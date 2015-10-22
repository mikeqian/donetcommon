using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using Framework.Qlh.Common.Infrastructure.Log;
using Framework.Qlh.Common.Infrastructure.Server;

namespace WindowsServer.Configuration
{
    public class MachineConfigurationContainer : BaseConfigurationContainer
    {
        private const string DefaultPath = "C:\\Program Files\\search\\global.config";
        private static Dictionary<string, string> _config;

        public override string GetSetting(string key)
        {
            try
            {
                if (_config == null)
                {
                    var text = File.ReadAllText(DefaultPath);
                    _config = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
                }

                if (_config != null && _config.ContainsKey(key))
                {
                    return _config[key];
                }
            }
            catch (Exception)
            {
                InternalLogger.Info("Can not read the global config at C:\\Program Files\\search\\global.config");
            }

            return null;
        }
    }
}