using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BamChat.myRedis;

namespace BamChat
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RedisHelper.CreateClient("104.224.167.94", 26935, "Ms2688");
            RedisHelper.setvaluestring("sbc", "Success");
            string a = RedisHelper.getValueString("sbc");
            Response.Write(a);
        }
    }
}