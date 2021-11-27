using Arcaptcha.Core;
using Arcaptcha.NetCore.Configuration;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Arcaptcha.NetCore.MVC
{
    /// <summary>
    /// Represents the Arcaptcha method extensions container for the <see cref="System.Web.Mvc.HtmlHelper"/> and <see cref="System.Web.Mvc.Controller"/> classes.
    /// </summary>
    public static class ArcaptchaMvcExtenstions
    {
        #region Public Methods
        /// <summary>
        /// Renders the Arcaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="siteKey">Sets the site key of Arcaptcha.</param>
        /// <param name="widgetScriptUrl">Sets the widget script of Arcaptcha.</param>
        /// <param name="renderApiScript">Determines if the API script call is to be rendered.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        public static IHtmlContent ArcaptchaWidget(
            this IHtmlHelper htmlHelper,
            string siteKey = null,
            string widgetScriptUrl = null,
            bool renderApiScript = true)
        {
            var config = ArcaptchaConfigurationManager.GetConfiguration();

            var rHtmlHelper = new ArcaptchaHtmlHelper(siteKey ?? config.SiteKey, widgetScriptUrl?? config.WidgetScriptUrl);
            return new HtmlString(rHtmlHelper.CreateWidgetHtml(renderApiScript));
        }

        /// <summary>
        /// Renders the Arcaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="siteKey">Sets the site key of Arcaptcha.</param>
        /// <param name="widgetScriptUrl">Sets the widget script of Arcaptcha.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        public static IHtmlContent ArcaptchaApiScript(
            this IHtmlHelper htmlHelper,
            string siteKey = null,
            string widgetScriptUrl = null
            )
        {
            var config = ArcaptchaConfigurationManager.GetConfiguration();

            var rHtmlHelper = new ArcaptchaHtmlHelper(siteKey ?? config.SiteKey, widgetScriptUrl?? config.WidgetScriptUrl);
            return new HtmlString(rHtmlHelper.CreateApiScripttHtml());
        }

        /// <summary>
        /// Gets an instance of the <see cref="ArcaptchaVerificationHelper"/> class that can be used to verify user's response to the Arcaptcha's challenge. 
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <param name="secretKey">The secret key required for making the Arcaptcha verification request.</param>
        /// <param name="siteKey">The site key required for making the Arcaptcha verification request.</param>
        /// <param name="verificationUrl">The verification Url required for making the Arcaptcha verification request.</param>
        /// <returns>Returns an instance of the <see cref="ArcaptchaVerificationHelper"/> class.</returns>
        public static ArcaptchaVerificationHelper GetArcaptchaVerificationHelper(this ControllerBase controller, string secretKey, string siteKey, string verificationUrl)
        {
            return new ArcaptchaVerificationHelper(controller.HttpContext, secretKey, siteKey, verificationUrl);
        }
        /// <summary>
        /// Verifies whether the user's response to the Arcaptcha request is correct.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="secretKey">The secret key required for making the Arcaptcha verification request.</param>
        /// <param name="siteKey">The site key required for making the Arcaptcha verification request.</param>
        /// <param name="verificationUrl">The verification Url required for making the Arcaptcha verification request.</param>
        /// <returns>Returns the result as a value of the <see cref="ArcaptchaVerificationResult"/> enum.</returns>

        public static ArcaptchaVerificationResult VerifyArcaptchaResponse(this ControllerBase controller, string secretKey, string siteKey, string verificationUrl)
        {
            var verificationHelper = new ArcaptchaVerificationHelper(controller.HttpContext, secretKey, siteKey, verificationUrl);
            return verificationHelper.VerifyArcaptchaResponse();
        }

        /// <summary>
        /// Verifies whether the user's response to the Arcaptcha request is correct.
        /// <param name="controller"></param>
        /// <param name="secretKey">The secret key required for making the Arcaptcha verification request.</param>
        /// <param name="siteKey">The site key required for making the Arcaptcha verification request.</param>
        /// <param name="verificationUrl">The verification Url required for making the Arcaptcha verification request.</param>
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="ArcaptchaVerificationResult"/> enum.</returns>
        public static Task<ArcaptchaVerificationResult> VerifyArcaptchaResponseAsync(this ControllerBase controller, string secretKey, string siteKey, string verificationUrl)
        {
            var verificationHelper = new ArcaptchaVerificationHelper(controller.HttpContext, secretKey, siteKey, verificationUrl);
            return verificationHelper.VerifyArcaptchaResponseAsync();
        }

        /// <summary>
        /// Gets an instance of the <see cref="ArcaptchaVerificationHelper"/> class that can be used to verify user's response to the Arcaptcha's challenge. 
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <returns>Returns an instance of the <see cref="ArcaptchaVerificationHelper"/> class.</returns>
        public static ArcaptchaVerificationHelper GetArcaptchaVerificationHelper(this ControllerBase controller)
        {
            var verificationHelper = CreateArcaptchaVerificationHelper(controller);
            return verificationHelper;
        }

        /// <summary>
        /// Verifies whether the user's response to the Arcaptcha request is correct.
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>Returns the result as a value of the <see cref="ArcaptchaVerificationResult"/> enum.</returns>
        public static ArcaptchaVerificationResult VerifyArcaptchaResponse(this ControllerBase controller)
        {
            var verificationHelper = CreateArcaptchaVerificationHelper(controller);
            return verificationHelper.VerifyArcaptchaResponse();
        }

        /// <summary>
        /// Verifies whether the user's response to the Arcaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="ArcaptchaVerificationResult"/> enum.</returns>
        public static Task<ArcaptchaVerificationResult> VerifyArcaptchaResponseAsync(this ControllerBase controller)
        {
            var verificationHelper = CreateArcaptchaVerificationHelper(controller);
            return verificationHelper.VerifyArcaptchaResponseAsync();
        }


        #endregion Public Methods

        #region Private Methods
        private static ArcaptchaVerificationHelper CreateArcaptchaVerificationHelper(ControllerBase controller)
        {
            var config = ArcaptchaConfigurationManager.GetConfiguration();
            return new ArcaptchaVerificationHelper(controller.HttpContext, config.SecretKey, config.SiteKey, config.VerificationUrl);
        }
        #endregion
    }
}
