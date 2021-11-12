using Arcaptcha.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Arcaptcha.Net.UI.Controls
{
    [ToolboxData("<{0}:ArcaptchaApiScript runat=server></{0}:ArcaptchaApiScript>")]
    public class ArcaptchaApiScript : ArcaptchaControlBase
    {
        #region Control Events
        /// <summary>
        /// Calls the OnLoad method of the parent class <see cref="System.Web.UI.WebControls.WebControl"/>
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object passed to the Load event of the control.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
                output.Write("<p>Arcaptcha API Script Control</p>");
            }
            else
            {
                    var htmlHelper = new ArcaptchaHtmlHelper(SiteKey,WidgetScriptUrl);
                    output.Write(htmlHelper.CreateApiScripttHtml());
            }
        }

        #endregion Control Events
    }
}
