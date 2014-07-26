using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyTrack
{
    public partial class UsersHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string strTemp="<label>Test Date Of Journey Test Test:<span>*</span></label>";
            //lblDisplayHtml.Text = strTemp;
            hdnPnr.Value = GenerateRandomNumber();
            Session["Pnr"] = hdnPnr.Value;

        }
        public string GenerateRandomNumber()
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;
        }
    }
}