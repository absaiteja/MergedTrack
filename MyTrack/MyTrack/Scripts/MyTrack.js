$(document).ready(PageLoad);
function PageLoad() {
    $('#fdsShowTrains').hide();
    $('#fdsAddPassengers').hide();
    $('#logOut').on("click", function () {
        $(location).attr('href', "Home.aspx");
    });
    $(function () {
        $("#Usertabs").tabs();
    });
    $(".chosen").data("placeholder", "Select Station...").chosen();
    $(function () {
        $("#datepicker").datepicker();
    });
    //GetAllTrains();
    //CreateTrain();
    $('#TrainsGrid').jqxGrid({
        width: '70%',
        height: '300px',
        columns: [
        { text: 'Train Number', datafield: 'TrainNumber', width: 80, hidden: true },
        { text: 'Train', datafield: 'TrainName', width: 100 },
        { text: 'From', datafield: 'Source', width: 100 },
        { text: 'To', datafield: 'Destination', width: 100 },
        { text: 'Distance', datafield: 'Distance', width: 60 },
        { text: 'Arrival Time', datafield: 'ArrivalTime', width: 100 },
        { text: 'Departure Time', datafield: 'DepartureTime', width: 100 },
        {
            text: 'Select Train', datafield: 'Edit', columntype: 'button', width: 50, cellsrenderer: function () {
                return "Edit";
            }, buttonclick: function (row) {
                editrowindex = row;
                var varTrainName = $("#TrainsGrid").jqxGrid('getcellvalue', row, "TrainName");
                var varSource = $("#TrainsGrid").jqxGrid('getcellvalue', row, "Source");
                var varDestination = $("#TrainsGrid").jqxGrid('getcellvalue', row, "Destination");
                var varDistance = $("#TrainsGrid").jqxGrid('getcellvalue', row, "Distance");
                $('#txtAvailableTrain').val(varTrainName);
                $('#txtDistance').val(varDistance);
                $('#txtTicketFrom').val(varSource);
                $('#txtTicketTo').val(varDestination);
                $('#txtFare').val("1000");
                $('#fdsAddPassengers').show();
                $('#fdsAddPassengers').focus();
                //window.location = '/Customers/Edit/?id=' + id;
            }
        }
        ],
        theme: 'metro',
        sortable: true,
        pageable: true,
        source: null
    });
    BindTrains();
    $("#btnSearchTrains").click(function () {
        $('#fdsShowTrains').show();
        $('#fdsShowTrains').focus();
    });
    $("#btnAddPassenger").click(function () {
        CreateTicket();
        var url = "PrintTicket.aspx";
        $(location).attr('href', url);
    });
}




//function GetAllTrains() {
//    ajaxCaller("UsersTrainDetailsService.asmx/GetSpecificTrainsbyFromToService", "{}", SuccessCallOnSpecificTrain, FailureCallOnSpecificTrain);
//      }

//function CreateTrain() {
//    ajaxCaller("UsersTrainDetailsService.asmx/CreateTrainService", "{}", SuccessCallOnCreateTrain, FailureCallOnCreateTrain);
//}

function CreateTicket() {
    debugger
    //var varPnr = (Math.floor(Math.random() * 90000) + 10000).toString();
    var varPnr = $('#hdnPnr').val();
    var ddlFromVal = $('#ddlFrom').find('option:selected');
    var ddlFromText = ddlFromVal.text();
    var ddlToVal = $('#ddlTo').find('option:selected');
    var ddlToText = ddlToVal.text();
    var dateJourneyDate = $('#datepicker').val();
    var date = new Date(dateJourneyDate);
    var dateFormattedJourneyDate = date.getFullYear() + "-" + date.getMonth() + "-" + date.getDate();
    var currDate = new Date();
    var dateFormattedCurrDate = currDate.getFullYear() + "-" + currDate.getMonth() + "-" + currDate.getDate();
    var ddllNoOfPassengersVal = $('#ddllNoOfPassengers').find('option:selected');
    var ddllNoOfPassengersText = ddllNoOfPassengersVal.text();
    var varArrivalTime = "12:00";
    var varDepartureTime = "12:00";
    var varFare = "1000";
    var varName = $('#txtPassengerName').val();
    var varAge = $('#txtAge').val();
    var varGender = $("input[name='gender']:checked").val();
    var ddlBerthVal = $('#ddlBerth').find('option:selected');
    var ddlBerthText = ddlBerthVal.text();
    var objTicket = JSON.stringify({ "obj": [varPnr, ddlFromText, ddlToText, dateFormattedJourneyDate, dateFormattedCurrDate, ddllNoOfPassengersText, varArrivalTime, varDepartureTime, varFare, varName, varAge, varGender, ddlBerthText] });
    ajaxCaller("UsersTrainDetailsService.asmx/CreateTicketService", objTicket, SuccessCallOnCreateTicket, FailureCallOnCreateTicket);
}


function BindTrains() {
    var objData = JSON.stringify({});
    ajaxCaller("UsersTrainDetailsService.asmx/GetAllTrainsService", objData, SuccessCallOnBindTrains, FailureCallOnBindTrains);
}

function SuccessCallOnBindTrains(data) {
    var Trains = data.d;
    var source = {
        datatype: 'json',
        datafields: [
            { name: 'TrainNumber', type: 'int' },
            { name: 'TrainName', type: 'string' },
            { name: 'Source', type: 'string' },
            { name: 'Destination', type: 'string' },
            { name: 'Distance', type: 'int' },
             { name: 'ArrivalTime', type: 'string' },
            { name: 'DepartureTime', type: 'string' }
        ],
        id: 'TrainNumber',
        localData: Trains
    };
    var dataAdapter = new $.jqx.dataAdapter(source);
    $('#TrainsGrid').jqxGrid('source', dataAdapter);
}

function FailureCallOnBindTrains(xhr, msg, exception) {
    alert(msg);
}

function SuccessCallOnCreateTicket(Response) {
    alert(Response.d.Message);
}
function FailureCallOnCreateTicket(Response) {
    alert(Response.d.Message);
}

function ajaxCaller(url, dataToSend, SuccessCallBack, FailureCallBack) {
    $.ajax({
        url: url,
        async: true,
        type: 'POST',
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SuccessCallBack,
        error: FailureCallBack
    });
}




