using System;
using System.Collections.Generic;
using System.Text;

namespace Arcaptcha.Core.Configuration
{

    /// <summary>
    /// Represents ArcaptchaConfiguration configuration.
    /// </summary>
    public class ArcaptchaConfiguration
    {
        /// <summary>
        /// Creates an instance of the <see cref="ArcaptchaConfiguration"/> class.
        /// </summary>
        /// <param name="siteKey">The site key.</param>
        /// <param name="secretKey">The secret key.</param>
        /// <param name="verificationUrl">The verification URL.</param>
        /// <param name="widgetScriptUrl">The widget script URL.</param>
        /// 
        public ArcaptchaConfiguration(string siteKey, string secretKey, string verificationUrl, string widgetScriptUrl)
        {
            SiteKey = siteKey;
            SecretKey = secretKey;
            VerificationUrl = verificationUrl.StartsWith("http") ? verificationUrl : "https://" + verificationUrl;
            WidgetScriptUrl = widgetScriptUrl.StartsWith("http") ? widgetScriptUrl : "https://" + widgetScriptUrl;
        }

        /// <summary>
        /// The site key.
        /// </summary>
        public string SiteKey
        {
            get;
            private set;
        }

        /// <summary>
        /// The secret key
        /// </summary>
        public string SecretKey
        {
            get;
            private set;
        }

        /// <summary>
        /// The Verification URL
        /// </summary>
        public string VerificationUrl
        {
            get;
            private set;
        }
        /// <summary>
        /// The Widget Script URL
        /// </summary>
        public string WidgetScriptUrl
        {
            get;
            private set;
        }
    }
}
