using Arcaptcha.Core.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcaptcha.NetCore.Configuration
{
    /// <summary>
    /// Represents a class that manages Arcaptcha configuration.
    /// </summary>
    public static class ArcaptchaConfigurationManager
    {
        private static IConfiguration _configuration = null;

        private static ArcaptchaConfiguration arcConfig = null;
        /// <summary>
        /// Initializes the configuration context.
        /// </summary>
        /// <param name="configuration">The configuration context of the application.</param>
        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            IConfigurationSection section = _configuration.GetSection("Arcaptcha");
            string widgetScriptUrl = (section.AsEnumerable().Any(x => x.Key == "WidgetScriptUrl")) ? section["WidgetScriptUrl"] : "";
            arcConfig = new ArcaptchaConfiguration(section["SiteKey"], section["SecretKey"], section["VerificationUrl"], widgetScriptUrl);
        }


        /// <summary>
        /// Gets the configuration from the default source.
        /// </summary>
        /// <returns>Returns configuration as an instance of the <see cref="ArcaptchaConfiguration"/> class.</returns>
        public static ArcaptchaConfiguration GetConfiguration()
        {
            return arcConfig;
        }
    }
}
