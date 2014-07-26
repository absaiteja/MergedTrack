using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyTrack
{
    public partial class PrintTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPnr = Session["Pnr"].ToString();
            Entities.Ticket objEnt = Entities.Ticket.GetFromPnr(strPnr);
            lblDispTrainName.Text = "Duronto";
            lblPnr.Text = objEnt.PNRNumber.ToString();
            lblTrainId.Text = "1";
            lblTransactionId.Text = "34436";
            lblDateOfBooking.Text = objEnt.DateOfBooking;
            lblClass.Text = "First AC";
            lblFromNew.Text = objEnt.Source;
            lblDojNew.Text = objEnt.DateOfJourney;
            lblDoj.Text = objEnt.DateOfJourney;
            lblFrom.Text = objEnt.Source;
            lblToNew.Text = objEnt.Destination;
            lblTo.Text = objEnt.Destination;
            lblDistance.Text = "600";
            lblNoOfPassengers.Text = Convert.ToString(objEnt.NoOfPassengers);
            lblMobile.Text = "9885436347";
            lblEmailId.Text = "test@test.com";
            lblFare.Text = objEnt.Fare.ToString();
            lblNameNew.Text = objEnt.Name;
            lblAgeNew.Text = objEnt.Age.ToString();
            lblGender.Text = objEnt.Gender;
            lblSeatNumber.Text = "24";
        }
    }
}