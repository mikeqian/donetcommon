using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using Framework.Qlh.Common.Infrastructure.Config;
using Framework.Qlh.Common.Infrastructure.Log;
using Framework.Qlh.Common.Infrastructure.Server;

namespace WindowsServer.Configuration
{
    public class WebApiConfigurationContainer : BaseConfigurationContainer
    {
        private Dictionary<string, string> _config = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
        public override string GetSetting(string key)
        {
            var url = string.Empty;

            try
            {
                if (!_config.ContainsKey(key))
                {
                    var configurationService = ConfigurationCenter.Local["ConfigurationService"];

                    if (string.IsNullOrEmpty(configurationService))
                    {
                        return null;
                    }

                    url = HttpUtility.UrlCombine(configurationService, "api/configs/" + key);
                    var client = new HttpClient();
                    var text = client.GetStringAsync(url).Result;
                    var value = JsonConvert.DeserializeObject<string>(text);

                    if (!string.IsNullOrEmpty(value))
                    {
                        _config.Add(key, value);
                    }

                    return value;
                }
                else
                {
                    return _config[key];
                }
            }
            catch (Exception)
            {
                InternalLogger.Info("Can not get the config from webapi: " + url);
            }

            return null;
        }
    }
}