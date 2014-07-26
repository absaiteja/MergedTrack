using MyTrack.Entities;
using MyTrack.Utilities;
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
    public class TrainDetailsService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

     
        [WebMethod]
        public Response CreateTrainService(object[] obj)
        {
            TrainDetails objTD = new Entities.TrainDetails();
            Response objResponse = objTD.CreateTrain(obj);
            if (objResponse.id == Properties.Settings.Default.SuccessId)
                return objResponse;
            else
                return objResponse;
        }
        [WebMethod]
        public Response UpdateTrainService(object[] objUpdatedTrains)
        {
            TrainDetails objTD = new TrainDetails();

            Response objResponse = objTD.UpdateTrain(objUpdatedTrains);
            if (objResponse.id == Properties.Settings.Default.SuccessId)
                return objResponse;
            else
                return objResponse;
        }
        [WebMethod]
        public object GetTrainService(object[] objGetTrainDetails)
        {
            Entities.TrainDetails objTD = new Entities.TrainDetails();
            int intTrainNumber = Convert.ToInt32(objGetTrainDetails[0]);
            objTD = TrainDetails.Get(intTrainNumber);
            return objTD;
        }
        [WebMethod]
        public Response DeleteTrainService(object[] objDeleteTrainDetails)
        {
            TrainDetails objTD = new TrainDetails();
            Response objResponse = objTD.DeleteTrain(objDeleteTrainDetails);
            if (objResponse.id == Properties.Settings.Default.SuccessId)
                return objResponse;
            else
                return objResponse;
        }
      
        [WebMethod]
        public List<TrainDetails> GetAllTrainsService()
        {
            Entities.TrainDetails objTD = new Entities.TrainDetails();
            List<Entities.TrainDetails> lstTD = new List<Entities.TrainDetails>();
            lstTD = objTD.GetAllTrains();
            return lstTD;
        }
    }
}
