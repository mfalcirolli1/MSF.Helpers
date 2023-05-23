using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MSF.Util.AppConfig
{
    public static class AppConfigHelper
    {
        private static readonly Dictionary<string, string> chaves = new Dictionary<string, string>()
        {
            { "chave1", "valor1" },
            { "chave2", "valor2"},
            { "chave3", "valor3"}
        };

        public static string GetChaveAppConfig(string chave)
        {
            chaves.TryGetValue(chave, out string valor);
            return valor;
        }

        public static void UpdateChaveAppConfig(string chave, string movoValor)
        {
            chaves[chave] = movoValor;
        }

        public static void SaveChangesAppConfig()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            foreach (var chave in chaves)
            {
                config.AppSettings.Settings.Remove(chave.Key);
                config.AppSettings.Settings.Add(chave.Key, chave.Value);
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
        }
    }
}
