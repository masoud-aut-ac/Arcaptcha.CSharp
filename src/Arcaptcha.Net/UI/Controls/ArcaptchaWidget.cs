using Arcaptcha.Core;
using Arcaptcha.Net.Configuration;
using Arcaptcha.NetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Arcaptcha.Net.UI.Controls
{
    [ToolboxData("<{0}:ArcaptchaWidget runat=server></{0}:ArcaptchaWidget>")]
    public class ArcaptchaWidget : ArcaptchaControlBase
    {
        #region Fields

        private ArcaptchaVerificationHelper _verificationHelper = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the secret key of the Arcaptcha widget.
        /// </summary>
        /// <remarks>The value of the <see cref="SecretKey"/> property is required when Arcaptcha response is to be verified. The key can be set either directly through this property or as an appSettings key (Arcaptcha:secretkey) in the application configuration file.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(false)]
        public string SecretKey
        {
            get
            {
                if (ViewState["ArcaptchaSecretKey"] == null)
                {
                    var config = ArcaptchaConfigurationManager.GetConfiguration();
                    ViewState["ArcaptchaSecretKey"] = config.SecretKey;
                }

                return (String)ViewState["ArcaptchaSecretKey"];
            }
            set
            {
                ViewState["ArcaptchaSecretKey"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Verification URL of the Arcaptcha widget.
        /// </summary>
        /// <remarks>The value of the <see cref="SecretKey"/> property is required when Arcaptcha response is to be verified. The key can be set either directly through this property or as an appSettings key (Arcaptcha:secretkey) in the application configuration file.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(false)]
        public string VerificationUrl
        {
            get
            {
                if (ViewState["ArcaptchaVerification"] == null)
                {
                    var config = ArcaptchaConfigurationManager.GetConfiguration();
                    ViewState["ArcaptchaVerification"] = config.VerificationUrl;
                }

                return (String)ViewState["ArcaptchaVerification"];
            }
            set
            {
                ViewState["ArcaptchaVerification"] = value;
            }
        }

        /// <summary>
        /// Determines whether to render API script.
        /// </summary>
        /// <remarks>The default value is true.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Localizable(false)]
        public bool RenderApiScript
        {
            get
            {
                if (ViewState["ArcaptchaRenderApiScript"] == null)
                {
                    ViewState["ArcaptchaRenderApiScript"] = true;
                }

                return (bool)ViewState["ArcaptchaRenderApiScript"];
            }

            set
            {
                ViewState["ArcaptchaRenderApiScript"] = value;
            }
        }

        /// <summary>
        /// Gets the user's response to the Arcaptcha challenge.
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [Localizable(false)]
        public string Response
        {
            get
            {
                if (_verificationHelper != null)
                {
                    return _verificationHelper.Response;
                }

                return String.Empty;
            }
        }

        #endregion Properties

        #region Control Events

        /// <summary>
        /// Calls the OnLoad method of the parent class <see cref="System.Web.UI.WebControls.WebControl"/> and initializes the internal state of the <see cref="ArcaptchaWidget"/> control for verification of the user's response to the Arcaptcha challenge.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object passed to the Load event of the control.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.Page.IsPostBack)
            {
                _verificationHelper = new ArcaptchaVerificationHelper(SecretKey, SiteKey, VerificationUrl);
            }
        }

        /// <summary>
        /// Redners the HTML output. This method is automatically called by ASP.NET during the rendering process.
        /// </summary>
        /// <param name="output">The output object to which the method will write HTML to.</param>
        /// <exception cref="InvalidOperationException">The exception is thrown if the public key is not set.</exception>
        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.DesignMode)
            {
                output.Write("<p>Arcaptcha Control</p>");
            }
            else
            {
                var htmlHelper = new ArcaptchaHtmlHelper(SiteKey, WidgetScriptUrl);
                output.Write(htmlHelper.CreateWidgetHtml(RenderApiScript));
            }
        }

        #endregion Control Events

        #region Public Methods

        /// <summary>
        /// Verifies the user's answer to the Arcaptcha challenge.
        /// </summary>
        /// <returns>Returns the verification result as <see cref="ArcaptchaVerificationResult"/> enum value.</returns>
        ///<exception cref="InvalidOperationException">The private key is null or empty.</exception>
        ///<exception cref="System.Net.WebException">The time-out period for the Arcaptcha verification request expired.</exception>
        public ArcaptchaVerificationResult Verify()
        {
            if (_verificationHelper == null)
            {
                _verificationHelper = new ArcaptchaVerificationHelper(SecretKey, SiteKey, VerificationUrl);
            }

            return _verificationHelper.VerifyArcaptchaResponse();
        }

        /// <summary>
        /// Verifies the user's answer to the Arcaptcha challenge.
        /// </summary>
        /// <returns>Returns the verification result as <see cref="ArcaptchaVerificationResult"/> enum value.</returns>
        ///<exception cref="InvalidOperationException">The private key is null or empty.</exception>
        ///<exception cref="System.Net.WebException">The time-out period for the Arcaptcha verification request expired.</exception>
        public Task<ArcaptchaVerificationResult> VerifyAsync()
        {
            if (_verificationHelper == null)
            {
                _verificationHelper = new ArcaptchaVerificationHelper(SecretKey, SiteKey, VerificationUrl);
            }

            return _verificationHelper.VerifyArcaptchaResponseAsync();
        }

        #endregion Public Methods
    }
}
