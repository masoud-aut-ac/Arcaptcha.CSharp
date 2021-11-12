using Arcaptcha.NetCore.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Arcaptcha.NetCore
{
    /// <summary>
    /// Represents the functionality for verifying user's response to the Arcaptcha challenge.
    /// </summary>
    public class ArcaptchaVerificationHelper
    {
        #region Constructors

        private ArcaptchaVerificationHelper()
        { }

        /// <summary>
        /// Creates an instance of the <see cref="ArcaptchaVerificationHelper"/> class.
        /// </summary>
        /// <param name="secretKey">Sets the secret key of the Arcapcha verification request.</param>
        /// <param name="siteKey">Sets the site key of the Arcapcha verification request.</param>
        /// <param name="verificationUrl">Sets the verification Url of the Arcapcha verification request.</param>
        internal ArcaptchaVerificationHelper(HttpContext httpContext, string secretKey, string siteKey, string verificationUrl)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("Secret key cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(siteKey))
            {
                throw new InvalidOperationException("Site key cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(verificationUrl))
            {
                throw new InvalidOperationException("Verification URL cannot be null or empty.");
            }

            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var request = httpContext.Request;


            SecretKey = secretKey;
            SiteKey = siteKey;
            VerificationUrl = verificationUrl.StartsWith("http") ? verificationUrl : "https://" + verificationUrl;
            Response = request.Form["arcaptcha-token"];
        }

        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the secret key of the Arcaptcha verification request.
        /// </summary>
        public string SecretKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the site key of the Arcaptcha verification request.
        /// </summary>
        public string SiteKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the verification URL of the Arcaptcha verification request.
        /// </summary>
        public string VerificationUrl
        {
            get ;
            private set;
        }

        /// <summary>
        /// Gets the user's response to the Arcaptcha challenge of the Arcaptcha verification request.
        /// </summary>
        public string Response
        {
            get;
            private set;
        }

        #endregion Properties

        #region Public Methods
        /// <summary>
        /// Verifies whether the user's response to the Arcaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="ArcaptchaVerificationResult"/> enum.</returns>
        public ArcaptchaVerificationResult VerifyArcaptchaResponse()
        {
            string postData = $"secret_key={SecretKey}&challenge_id={Response}&site_key={SiteKey}";

            byte[] postDataBuffer = Encoding.ASCII.GetBytes(postData);
            Uri verifyUri = new Uri(VerificationUrl, UriKind.Absolute);
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(verifyUri);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = postDataBuffer.Length;
                webRequest.Method = "POST";

                var proxy = WebRequest.GetSystemWebProxy();
                proxy.Credentials = CredentialCache.DefaultCredentials;

                webRequest.Proxy = proxy;

                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(postDataBuffer, 0, postDataBuffer.Length);
                }

                var webResponse = (HttpWebResponse)webRequest.GetResponse();

                string sResponse = null;

                using (var sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    sResponse = sr.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<ArcaptchaVerificationResult>(sResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifies whether the user's response to the Arcaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="ArcaptchaVerificationResult"/> enum.</returns>
        public Task<ArcaptchaVerificationResult> VerifyArcaptchaResponseTaskAsync()
        {
            return Task.Run(() => VerifyArcaptchaResponse());
        }

        #endregion Public Methods

    }
}
