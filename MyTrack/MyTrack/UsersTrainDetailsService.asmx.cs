using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MyTrack
{
    /// <summary>
    /// Summary description for TrainDetailsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class UsersTrainDetailsService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Entities.UserTrainDetails> GetAllTrainsService()
        {
            Entities.UserTrainDetails objTD = new Entities.UserTrainDetails();
            List<Entities.UserTrainDetails> lstTD = new List<Entities.UserTrainDetails>();
            lstTD = objTD.GetAllTrains();
            return lstTD;
        }

        [WebMethod]
        public List<Entities.UserTrainDetails> GetSpecificTrainsbyFromToService()
        {
            Entities.UserTrainDetails objTD = new Entities.UserTrainDetails();
            List<Entities.UserTrainDetails> lstTD = new List<Entities.UserTrainDetails>();
            lstTD = objTD.GetAllTrains();
            return lstTD;
        }

        [WebMethod]
        public Utilities.Response CreateTrainService()
        {
            Entities.UserTrainDetails objTD = new Entities.UserTrainDetails();
            objTD.TrainName = "Duronto";
            objTD.TrainNumber = 1;
            objTD.Source = "Delhi";
            objTD.Destination = "Hyderabad";
            objTD.Distance = 800;
            objTD.DepartureTime = "20:00";
            objTD.ArrivalTime = "12:00";
            Utilities.Response objRes = objTD.CreateTrain();
            return objRes;
        }

        [WebMethod]
        public Utilities.Response CreateTicketService(object[] obj)
        {
            Entities.Ticket objTK = new Entities.Ticket();
            Utilities.Response objRes = objTK.CreateTicket(obj);
            return objRes;
        }
    }
}
