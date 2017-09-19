using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    // Quelle: http://stackoverflow.com/questions/6150644/change-default-app-config-at-runtime
    public abstract class AppConfig : IDisposable
    {
        /// <summary>
        /// Lädt eine gegebene App.config, behält die Default-Config bei ungültigen Pfad bei.
        /// </summary>
        /// <param name="path">Absoluter Pfad zur *.config</param>
        /// <returns>Anwendung in using</returns>
        public static AppConfig Change(string path)
        {
            return new ChangeAppConfig(path);
        }

        public abstract void Dispose();

        private class ChangeAppConfig : AppConfig
        {
            private readonly string oldConfig =
                AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE").ToString();

            private bool disposedValue;
            public ChangeAppConfig(string path)
            {
                // Auf ungültigen Pfad prüfen
                if (!File.Exists(path))
                    throw new FileNotFoundException(string.Format("Die Datei '{0}' wurde nicht gefunden.", path));

                AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", path);
                Debug.WriteLine(AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE").ToString());
                ResetConfigMechanism();
            }

            public override void Dispose()
            {
                if (!disposedValue)
                {
                    AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", oldConfig);
                    ResetConfigMechanism();


                    disposedValue = true;
                }
                GC.SuppressFinalize(this);
            }

            private static void ResetConfigMechanism()
            {
                typeof(ConfigurationManager)
                    .GetField("s_initState", BindingFlags.NonPublic |
                                             BindingFlags.Static)
                    .SetValue(null, 0);

                typeof(ConfigurationManager)
                    .GetField("s_configSystem", BindingFlags.NonPublic |
                                                BindingFlags.Static)
                    .SetValue(null, null);

                typeof(ConfigurationManager)
                    .Assembly.GetTypes()
                    .Where(x => x.FullName ==
                                "System.Configuration.ClientConfigPaths")
                    .First()
                    .GetField("s_current", BindingFlags.NonPublic |
                                           BindingFlags.Static)
                    .SetValue(null, null);
            }
        }
    }
}
