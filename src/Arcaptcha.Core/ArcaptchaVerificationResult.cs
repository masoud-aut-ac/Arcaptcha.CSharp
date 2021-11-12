using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcaptcha.NetCore
{

    /// <summary>
    /// Represents the JSON result for Arcaptcha.
    /// </summary>
    public class ArcaptchaVerificationResult
    {
        #region Properties

        /// <summary>
        /// Determines if the Arcaptcha verification was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success
        {
            get;
            set;
        }
        #endregion Properties
    }
}
