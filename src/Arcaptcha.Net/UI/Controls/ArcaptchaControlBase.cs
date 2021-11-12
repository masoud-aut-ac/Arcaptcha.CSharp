using Arcaptcha.Net.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Arcaptcha.Net.UI.Controls
{
    public abstract class ArcaptchaControlBase : WebControl
    {
        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(false)]
        public string SiteKey
        {
            get
            {
                if (true)
                {
                    if (ViewState["ArcaptchaSiteKey"] == null)
                    {
                        var config = ArcaptchaConfigurationManager.GetConfiguration();
                        ViewState["ArcaptchaSiteKey"] = config.SiteKey;
                    }

                    return (String)ViewState["ArcaptchaSiteKey"];
                }
            }
            set
            {
                ViewState["ArcaptchaSiteKey"] = value;
            }
        }
        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(false)]
        public string WidgetScriptUrl {
            get
            {
                if (true)
                {
                    if (ViewState["ArcaptchaWidgetScriptUrl"] == null)
                    {
                        var config = ArcaptchaConfigurationManager.GetConfiguration();
                        ViewState["ArcaptchaWidgetScriptUrl"] = config.WidgetScriptUrl;
                    }

                    return (String)ViewState["ArcaptchaWidgetScriptUrl"];
                }
            }
            set
            {
                ViewState["ArcaptchaWidgetScriptUrl"] = value;
            }
        }
    }
}
