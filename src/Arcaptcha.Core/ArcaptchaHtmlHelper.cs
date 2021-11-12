using System;
using System.Collections.Generic;
using System.Text;

namespace Arcaptcha.Core
{

    /// <summary>
    /// Represents the functionality to generate HTML for Arcaptcha API.
    /// </summary>
    public class ArcaptchaHtmlHelper
    {
        #region Fields
        private const string PARAM_SITEKEY = "site-key";
        private string widgetScriptURL;
        #endregion Fields

        #region Constructors
        private ArcaptchaHtmlHelper()
        { }

        /// <summary>
        /// Creates an instance of the <see cref="ArcaptchaHtmlHelper"/> class.
        /// </summary>
        /// <param name="siteKey">Sets the site key for the Arcaptcha widget.</param>
        public ArcaptchaHtmlHelper(string siteKey, string widgetScriptUrl)
        {
            SiteKey = siteKey;
            WidgetScriptURL = widgetScriptUrl;
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the site key of the Arcaptcha widget.
        /// </summary>
        public string SiteKey
        {
            get;
            set;
        }
        public string WidgetScriptURL
        {
            get { return widgetScriptURL; }
            set
            {
                if (value.StartsWith("http"))
                    widgetScriptURL = value;
                else
                    widgetScriptURL = "https://" + value;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Creates the Arcaptcha HTML that needs to be rendered.
        /// </summary>
        /// <param name="renderApiScript">Determines if the API script is to be rendered.</param>
        /// <returns>Returns the Arcaptcha HTML as an instance of the <see cref="string"/> type.</returns>
        public string CreateWidgetHtml(bool renderApiScript)
        {
            var dictAttributes = new Dictionary<string, string>
            {
                { "data-" + PARAM_SITEKEY, SiteKey }
            };

            var sbAttributes = new StringBuilder();
            foreach (var key in dictAttributes.Keys)
            {
                sbAttributes.Append($"{key}=\"{dictAttributes[key]}\" ");
            }

            StringBuilder sbHtml = new StringBuilder();

            if (renderApiScript)
            {
                sbHtml.Append(CreateApiScripttHtml());
            }

            sbHtml.Append($"<div class=\"arcaptcha\" {sbAttributes}></div>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// Creates the HTML that can be used to render arcaptcha API script..
        /// </summary>
        /// <returns>Returns the HTML as an instance of the <see cref="string"/> type.</returns>
        public string CreateApiScripttHtml()
        {
            return $"<script src=\"{WidgetScriptURL}\" async defer></script>";
        }

        #endregion Public Methods
    }
}
