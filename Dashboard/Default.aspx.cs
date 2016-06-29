using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsCallback && !this.IsPostBack)
        {
            string aUser_id = this.Request.Params["user"];
            if (string.IsNullOrEmpty(aUser_id))
                aUser_id = "1";

            Table aOuterTable = (Table)FindDeepControl(this, "OuterTable");
            aOuterTable.Rows[0].Cells[0].Text =
"<script language=\"JavaScript\" type=\"text/javascript\">" +
"AC_FL_RunContent(" +
"\t'codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0'," +
"\t'width', '1353'," +
"\t'height', '900'," +
"\t'src', 'DashboardTest'," +
"\t'quality', 'high'," +
"\t'pluginspage', 'http://www.adobe.com/go/getflashplayer'," +
"\t'align', 'middle'," +
"\t'play', 'true'," +
"\t'loop', 'true'," +
"\t'scale', 'showall'," +
"\t'wmode', 'window'," +
"\t'devicefont', 'false'," +
"\t'id', 'DashboardTest'," +
"\t'bgcolor', '#ffffff'," +
"\t'name', 'Dashboard'," +
"\t'menu', 'true'," +
"\t'allowFullScreen', 'false'," +
"\t'allowScriptAccess','sameDomain'," +
"\t'movie', 'swf/DashboardTest'," +
"\t'salign', ''," +
"\t'FlashVars', 'user='" + aUser_id + "&broker=" + HttpUtility.UrlEncode("broker.aspx") +
"); //end AC code" +
"</script>";
/*
<noscript>
    <object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0\" width=\"1353\" height=\"900\" id=\"Dashboard\" align=\"middle\">
    <param name=\"allowScriptAccess\" value=\"sameDomain\" />
    <param name=\"allowFullScreen\" value=\"false\" />
    <param name=\"movie\" value=\"swf/DashboardTest.swf\" />
    <param name=\"quality\" value=\"high\" /><param name=\"bgcolor\" value=\"#ffffff\" />	
    <embed src=\"swf/DashboardTest.swf\" quality=\"high\" bgcolor=\"#ffffff\" width=\"1353\" height=\"900\" name=\"Dashboard\" align=\"middle\" allowScriptAccess=\"sameDomain\" allowFullScreen=\"false\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.adobe.com/go/getflashplayer\" />
    </object>
</noscript>
*/
        }
    }

    static private Control FindDeepControl(Control aParent, string aID)
    {
        Control aCtrl = null;
        try
        {
            aCtrl = aParent.FindControl(aID);
        }
        catch
        {
            aCtrl = null;
        }
        if (aCtrl == null)
        {
            try
            {
                for (int i = 0; i < aParent.Controls.Count; i++)
                {
                    aCtrl = FindDeepControl(aParent.Controls[i], aID);
                    if (aCtrl != null)
                        return aCtrl;
                }
            }
            catch
            {
                aCtrl = null;
            }
        }
        return aCtrl;
    }

}
