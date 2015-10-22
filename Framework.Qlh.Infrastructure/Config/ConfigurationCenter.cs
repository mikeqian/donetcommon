namespace WindowsServer.Configuration
{
    public class ConfigurationCenter
    {
        static ConfigurationCenter()
        {
            Local = new ContainerWrapper(new FileConfigurationContainer(), new MachineConfigurationContainer());
            Global = new ContainerWrapper(new FileConfigurationContainer(), new MachineConfigurationContainer(), new WebApiConfigurationContainer());
        }

        public static ContainerWrapper Global { get; private set; }
        public static ContainerWrapper Local { get; private set; }

        public class ContainerWrapper
        {
            private BaseConfigurationContainer _container;
            private BaseConfigurationContainer _fallbackContainer;
            private BaseConfigurationContainer _remoteConainer;

            internal ContainerWrapper(BaseConfigurationContainer container, BaseConfigurationContainer fallbackContainer = null, BaseConfigurationContainer remoteContainer = null)
            {
                _container = container;
                _fallbackContainer = fallbackContainer;
                _remoteConainer = remoteContainer;
            }

            public string this[string key]
            {
                get
                {
                    var v = _container.GetSetting(key);
                    if (!string.IsNullOrEmpty(v))
                    {
                        return v;
                    }

                    if (_fallbackContainer != null)
                    {
                        v = _fallbackContainer.GetSetting(key);
                        if (!string.IsNullOrEmpty(v))
                        {
                            return v;
                        }
                    }

                    if (_remoteConainer != null)
                    {
                        v = _remoteConainer.GetSetting(key);
                        if (!string.IsNullOrEmpty(v))
                        {
                            return v;
                        }
                    }

                    return v;
                }
            }
        }
    }
}
