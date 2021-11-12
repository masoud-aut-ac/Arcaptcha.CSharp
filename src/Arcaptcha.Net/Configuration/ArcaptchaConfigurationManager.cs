using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcaptcha.Core.Configuration;

namespace Arcaptcha.Net.Configuration
{
    /// <summary>
    /// Represents a singleton class that manages Arcaptcha configuration.
    /// </summary>
    public static class ArcaptchaConfigurationManager
    {
        private static ArcaptchaConfiguration arcConfig;
        /// <summary>
        /// Gets the configuration from the default source.
        /// </summary>
        /// <returns>Returns configuration as an instance of the <see cref="ArcaptchaConfiguration"/> class.</returns>
        public static ArcaptchaConfiguration GetConfiguration()
        {
            if (arcConfig != null)
                return arcConfig;
            string secretKey = "", siteKey = "", verficationUrl = "", widgetScriptUrl = "";
            if (ConfigurationManager.AppSettings.AllKeys.Contains("ArcaptchaSecretKey"))
            {
                secretKey = ConfigurationManager.AppSettings["ArcaptchaSecretKey"];
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains("ArcaptchaSiteKey"))
            {
                siteKey = ConfigurationManager.AppSettings["ArcaptchaSiteKey"];
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains("ArcaptchaVerificationUrl"))
            {
                verficationUrl = ConfigurationManager.AppSettings["ArcaptchaVerificationUrl"];
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains("ArcaptchaWidgetScriptUrl"))
            {
                widgetScriptUrl = ConfigurationManager.AppSettings["ArcaptchaWidgetScriptUrl"];
            }
            arcConfig = new ArcaptchaConfiguration(siteKey, secretKey, verficationUrl, widgetScriptUrl);
            
            return arcConfig;
        }
    }
}
