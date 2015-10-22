using System.Configuration;
using System.Linq;
using System.Text;

namespace WindowsServer.Configuration
{
    public class FileConfigurationContainer : BaseConfigurationContainer
    {
        public override string GetSetting(string key)
        {
            var v = ConfigurationManager.AppSettings[key];
            if (v == null)
            {
                var conn = ConfigurationManager.ConnectionStrings[key];
                if (conn != null)
                {
                    v = conn.ConnectionString;
                }
            }
            return v;
        }
    }
}
