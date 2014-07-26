using SqlConnectors;
using MyTrack.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MyTrack.Entities
{
    public class Ticket
    {
        const string Success_MSG = "Successfully {0} a Ticket";
        const string Failure_MSG = "Unable to {0} a Ticket";

        [Display(Name = "SId")]
        public int SId { get; set; }

        [Display(Name = "TickerId", Description = "Primarykey")]
        public int TickerId { get; set; }

        [Display(Name = "PNRNumber")]
        public double PNRNumber { get; set; }

        [Display(Name = "Source")]
        public string Source { get; set; }

        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Display(Name = "DateOfJourney")]
        public string DateOfJourney { get; set; }

        [Display(Name = "DateOfBooking")]
        public string DateOfBooking { get; set; }

        [Display(Name = "NoOfPassengers")]
        public int NoOfPassengers { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "BerthPreference")]
        public string BerthPreference { get; set; }

        [Display(Name = "Fare")]
        public double Fare { get; set; }

        public Response CreateTicket(object[] objCreateTicket)
        {
            string strConnection = Properties.Settings.Default.Connection;
            string[] strArrParameterName = { "PNRNumber", "Source","Destination",
                                            "DateOfJourney","DateOfBooking","NoOfPassengers","ArrivalTime"
                                            ,"DepartureTime","Fare","Name",
                                            "Age","Gender","BerthPreference" };
            object[] objArrParameterValue = objCreateTicket;

            string strQuery = @"INSERT INTO [Ticket]([PNRNumber],
                                [Source],[Destination],[DateOfJourney],
                                [DateOfBooking],[NoOfPassengers],[ArrivalTime],
                                [DepartureTime],[Fare],[Name],
                                [Age],[Gender],[BerthPreference])
                                    VALUES(@PNRNumber,@Source
                                ,@Destination,@DateOfJourney,@DateOfBooking
                                ,@NoOfPassengers,@ArrivalTime,@DepartureTime,@Fare,@Name
                                ,@Age,@Gender
                                ,@BerthPreference)";
            DBOperations objDBOperations = new DBOperations(strConnection);
            bool blnResult = objDBOperations.ExecuteQuery(strQuery, strArrParameterName, objArrParameterValue);


            if (!blnResult)
            {
                objDBOperations.CloseConnection();
                return new Response(9999, string.Format(Failure_MSG, "insert"));
            }
            objDBOperations.CloseConnection();
            return new Response(5555, string.Format(Success_MSG, "inserted"));


        }

        public Response Update()
        {
            string strConnection = Properties.Settings.Default.Connection;
            string strQuery = @"UPDATE [Ticket] SET [PNRNumber] = @PNRNumber
                              ,[Source] = @Source,
                              ,[Destination] = @Destination,[DateOfJourney] = @DateOfJourney
                              ,[DateOfBooking] = @DateOfBooking
                              ,[NoOfPassengers] = @NoOfPassengers
                              ,[Name] = @Name
                              ,[Age] = @Age
                              ,[Gender] = @Gender
                              ,[BerthPreference] = @BerthPreference
                              ,[Fare] = @Fare
                              WHERE TicketId = @TicketId";
            string[] strArrParameterName = { "SId", "TicketId", "PNRNumber", "Source","Destination",
                                            "DateOfJourney","DateOfBooking","NoOfPassengers","Name",
                                            "Age","Gender","BerthPreference","Fare" };
            object[] objArrParameterValue = { this.SId,this.TickerId,this.PNRNumber,this.Source,this.Destination,
                                             this.DateOfJourney,this.DateOfBooking,this.NoOfPassengers,this.Name,
                                             this.Age,this.Gender,this.BerthPreference,this.Fare };
            DBOperations objDBOperations = new DBOperations(strConnection);
            bool blnResult = objDBOperations.ExecuteQuery(strQuery, strArrParameterName, objArrParameterValue);

            if (!blnResult)
            {
                objDBOperations.CloseConnection();
                return new Response(9999, string.Format(Failure_MSG, "update"));
            }
            objDBOperations.CloseConnection();
            return new Response(5555, string.Format(Success_MSG, "updated"));
        }

        public Response Delete()
        {
            string strConnection = Properties.Settings.Default.Connection;
            string strQuery = @"DELETE FROM [Ticket] WHERE TicketId = @TicketId";
            string[] strArrParameterName = { "SId", "TicketId", "PNRNumber", "Source","Destination",
                                            "DateOfJourney","DateOfBooking","NoOfPassengers","Name",
                                            "Age","Gender","BerthPreference","Fare" };
            object[] objArrParameterValue = { this.SId,this.TickerId,this.PNRNumber,this.Source,this.Destination,
                                             this.DateOfJourney,this.DateOfBooking,this.NoOfPassengers,this.Name,
                                             this.Age,this.Gender,this.BerthPreference,this.Fare };
            DBOperations objDBOperations = new DBOperations(strConnection);
            bool blnResult = objDBOperations.ExecuteQuery(strQuery, strArrParameterName, objArrParameterValue);

            if (!blnResult)
            {
                objDBOperations.CloseConnection();
                return new Response(9999, string.Format(Failure_MSG, "delete"));
            }
            objDBOperations.CloseConnection();
            return new Response(5555, string.Format(Success_MSG, "deleted"));
        }

        public static Ticket Get(int TicketId)
        {
            Ticket objTicket = new Ticket();
            string strQuery = @"SELECT [SId],[TicketId],[PNRNumber]
                              ,[Source],[Destination],[DateOfJourney]
                              ,[DateOfBooking],[NoOfPassengers],[Name]
                              ,[Age],[Gender],[BerthPreference],[Fare]
                              FROM [Ticket] TicketId = @TicketId";
            string[] strArrParameterName = { "TicketId" };
            object[] objArrparameterValue = { TicketId };
            DataTable dtRetVal = new DataTable();
            DBOperations objoperations = new DBOperations(Properties.Settings.Default.Connection);
            dtRetVal = DBOperations.ExecuteQueryForAll(Properties.Settings.Default.Connection, strQuery, strArrParameterName, objArrparameterValue);
            int intSId;
            int intTicketId;
            double dblPNRNumber;
            int intNoOfPassengers;
            int intAge;
            double dblFare;

            if (dtRetVal.Rows.Count > 0)
            {
                int.TryParse(dtRetVal.Rows[0]["SId"].ToString(), out intSId);
                objTicket.SId = intSId;
                int.TryParse(dtRetVal.Rows[0]["TicketId"].ToString(), out intTicketId);
                objTicket.TickerId = intTicketId;
                double.TryParse(dtRetVal.Rows[0]["PNRNumber"].ToString(), out dblPNRNumber);
                objTicket.PNRNumber = dblPNRNumber;
                objTicket.Source = dtRetVal.Rows[0]["Source"] != null ? dtRetVal.Rows[0]["Source"].ToString() : string.Empty;
                objTicket.Destination = dtRetVal.Rows[0]["Destination"] != null ? dtRetVal.Rows[0]["Destination"].ToString() : string.Empty;
                objTicket.DateOfJourney = dtRetVal.Rows[0]["DateOfJourney"] != null ? dtRetVal.Rows[0]["DateOfJourney"].ToString() : string.Empty;
                objTicket.DateOfBooking = dtRetVal.Rows[0]["DateOfBooking"] != null ? dtRetVal.Rows[0]["DateOfBooking"].ToString() : string.Empty;
                int.TryParse(dtRetVal.Rows[0]["NoOfPassengers"].ToString(), out intNoOfPassengers);
                objTicket.NoOfPassengers = intNoOfPassengers;
                objTicket.Source = dtRetVal.Rows[0]["Name"] != null ? dtRetVal.Rows[0]["Name"].ToString() : string.Empty;
                int.TryParse(dtRetVal.Rows[0]["Age"].ToString(), out intAge);
                objTicket.Age = intAge;
                objTicket.Source = dtRetVal.Rows[0]["Gender"] != null ? dtRetVal.Rows[0]["Gender"].ToString() : string.Empty;
                objTicket.Source = dtRetVal.Rows[0]["BerthPreference"] != null ? dtRetVal.Rows[0]["BerthPreference"].ToString() : string.Empty;
                double.TryParse(dtRetVal.Rows[0]["Fare"].ToString(), out dblFare);
                objTicket.Fare = dblFare;
            }
            objoperations.CloseConnection();
            return objTicket;
        }

        public static Ticket GetFromPnr(string PNRNumber)
        {
            Ticket objTicket = new Ticket();
            string strQuery = @"SELECT [SId]
                              ,[TicketId]
                              ,[PNRNumber]
                              ,[Source]
                              ,[Destination]
                              ,[DateOfJourney]
                              ,[DateOfBooking]
                              ,[NoOfPassengers]
                              ,[ArrivalTime]
                              ,[DepartureTime]
                              ,[Fare]
                              ,[Name]
                              ,[Age]
                              ,[Gender]
                              ,[BerthPreference]
                          FROM [Ticket] where [PNRNumber] = @PNRNumber";
            string[] strArrParameterName = { "PNRNumber" };
            object[] objArrparameterValue = { PNRNumber };
            DataTable dtRetVal = new DataTable();
            DBOperations objoperations = new DBOperations(Properties.Settings.Default.Connection);
            dtRetVal = DBOperations.ExecuteQueryForAll(Properties.Settings.Default.Connection, strQuery, strArrParameterName, objArrparameterValue);
            int intSId;
            int intTicketId;
            double dblPNRNumber;
            int intNoOfPassengers;
            int intAge;
            double dblFare;

            if (dtRetVal.Rows.Count > 0)
            {
                int.TryParse(dtRetVal.Rows[0]["SId"].ToString(), out intSId);
                objTicket.SId = intSId;
                int.TryParse(dtRetVal.Rows[0]["TicketId"].ToString(), out intTicketId);
                objTicket.TickerId = intTicketId;
                double.TryParse(dtRetVal.Rows[0]["PNRNumber"].ToString(), out dblPNRNumber);
                objTicket.PNRNumber = dblPNRNumber;
                objTicket.Source = dtRetVal.Rows[0]["Source"] != null ? dtRetVal.Rows[0]["Source"].ToString() : string.Empty;
                objTicket.Destination = dtRetVal.Rows[0]["Destination"] != null ? dtRetVal.Rows[0]["Destination"].ToString() : string.Empty;
                objTicket.DateOfJourney = dtRetVal.Rows[0]["DateOfJourney"] != null ? dtRetVal.Rows[0]["DateOfJourney"].ToString() : string.Empty;
                objTicket.DateOfBooking = dtRetVal.Rows[0]["DateOfBooking"] != null ? dtRetVal.Rows[0]["DateOfBooking"].ToString() : string.Empty;
                int.TryParse(dtRetVal.Rows[0]["NoOfPassengers"].ToString(), out intNoOfPassengers);
                objTicket.NoOfPassengers = intNoOfPassengers;
                objTicket.Name = dtRetVal.Rows[0]["Name"] != null ? dtRetVal.Rows[0]["Name"].ToString() : string.Empty;
                int.TryParse(dtRetVal.Rows[0]["Age"].ToString(), out intAge);
                objTicket.Age = intAge;
                objTicket.Gender = dtRetVal.Rows[0]["Gender"] != null ? dtRetVal.Rows[0]["Gender"].ToString() : string.Empty;
                objTicket.BerthPreference = dtRetVal.Rows[0]["BerthPreference"] != null ? dtRetVal.Rows[0]["BerthPreference"].ToString() : string.Empty;
                double.TryParse(dtRetVal.Rows[0]["Fare"].ToString(), out dblFare);
                objTicket.Fare = dblFare;
            }
            objoperations.CloseConnection();
            return objTicket;
        }

        public static List<Ticket> GetAllDetails()
        {
            List<Ticket> lstTicket = new List<Ticket>();
            Ticket objTicket = null;

            string strQuery = @"SELECT [SId],[TicketId],[PNRNumber]
                              ,[Source],[Destination],[DateOfJourney]
                              ,[DateOfBooking],[NoOfPassengers],[Name]
                              ,[Age],[Gender],[BerthPreference],[Fare]
                              FROM [Ticket]";
            string[] strArrColNames = new string[] { };
            object[] objArrColValue = new object[] { };
            DataTable dtRetVal = new DataTable();
            DBOperations objoperations = new DBOperations(Properties.Settings.Default.Connection);
            string str = Properties.Settings.Default.Connection;
            dtRetVal = DBOperations.ExecuteQueryForAll(str, strQuery, strArrColNames, objArrColValue);
            int intSId;
            int intTicketId;
            double dblPNRNumber;
            int intNoOfPassengers;
            int intAge;
            double dblFare;
            for (int i = 0; i < dtRetVal.Rows.Count; i++)
            {
                objTicket = new Ticket();

                int.TryParse(dtRetVal.Rows[i]["SId"].ToString(), out intSId);
                objTicket.SId = intSId;
                int.TryParse(dtRetVal.Rows[i]["TicketId"].ToString(), out intTicketId);
                objTicket.TickerId = intTicketId;
                double.TryParse(dtRetVal.Rows[i]["PNRNumber"].ToString(), out dblPNRNumber);
                objTicket.PNRNumber = dblPNRNumber;
                objTicket.Source = dtRetVal.Rows[i]["Source"] != null ? dtRetVal.Rows[i]["Source"].ToString() : string.Empty;
                objTicket.Destination = dtRetVal.Rows[i]["Destination"] != null ? dtRetVal.Rows[i]["Destination"].ToString() : string.Empty;
                objTicket.DateOfJourney = dtRetVal.Rows[i]["DateOfJourney"] != null ? dtRetVal.Rows[i]["DateOfJourney"].ToString() : string.Empty;
                objTicket.DateOfBooking = dtRetVal.Rows[i]["DateOfBooking"] != null ? dtRetVal.Rows[i]["DateOfBooking"].ToString() : string.Empty;
                int.TryParse(dtRetVal.Rows[i]["NoOfPassengers"].ToString(), out intNoOfPassengers);
                objTicket.NoOfPassengers = intNoOfPassengers;
                objTicket.Source = dtRetVal.Rows[i]["Name"] != null ? dtRetVal.Rows[i]["Name"].ToString() : string.Empty;
                int.TryParse(dtRetVal.Rows[i]["Age"].ToString(), out intAge);
                objTicket.Age = intAge;
                objTicket.Source = dtRetVal.Rows[i]["Gender"] != null ? dtRetVal.Rows[i]["Gender"].ToString() : string.Empty;
                objTicket.Source = dtRetVal.Rows[i]["BerthPreference"] != null ? dtRetVal.Rows[i]["BerthPreference"].ToString() : string.Empty;
                double.TryParse(dtRetVal.Rows[i]["Fare"].ToString(), out dblFare);
                objTicket.Fare = dblFare;


                lstTicket.Add(objTicket);
            }
            return lstTicket;
        }


    }
}


