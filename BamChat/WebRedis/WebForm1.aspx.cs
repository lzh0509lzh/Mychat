using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRedis
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebRedis.DoRedisString a = new WebRedis.DoRedisString();
            //a.Set("key", "value");

            Response.Write(a.Get("key"));
        }
    }
}