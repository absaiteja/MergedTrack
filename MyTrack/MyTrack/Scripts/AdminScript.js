$(document).ready(function () {
    $('#tabs').tabs();
    $(".chosen").data("placeholder", "Select Station...").chosen({width:60});
    debugger;
    $("#list").on("click", function () {
        $(location).attr('href', "Home.aspx");
    });
    $('#ddlFareDetails').change(function () {
        ajaxCallerFare("FaresService.asmx/GetFareService", JSON.stringify({ objFaresValues: $('#ddlFareDetails').val() }), SuccessFare, FailureCallBack);
    });
    function ajaxCallerFare(url, dataToSend, SuccessFare, FailureCallBack) {
        debugger;
        $.ajax({
            url: url,
            async: true,
            type: 'POST',
            data: dataToSend,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: SuccessFare,
            error: FailureCallBack
        })
    }
    function SuccessFare(data) {
        $('#txtFare').val(data.d.Fare);
    }

   /*Binding Train Names into dropdown*/
    AjaxCallForDDL('AdminHome.aspx?GetTrains=true', 'POST', BindTrains, FailureCallBack);
    function BindTrains(data) {
        var TrainDetails = JSON.parse(data);
        BindDropDown("#ddlFareDetails", TrainDetails, "TrainName", "TrainNumber");
    }
    function AjaxCallForDDL(url, callType, successCall, FailureCallBack) {
        $.ajax({
            url: url,
            type: callType,
            async: true,
            success: successCall,
            error: FailureCallBack
        });
    }
    function BindDropDown(selector, data, dataMember, valueMember) {
        $(selector).empty();
        for (var obj in data) {
            $(selector).append("<option value = " + data[obj][valueMember] + ">" + data[obj][dataMember] + "</option>")
        }
        $(selector).trigger("chosen:updated");
    }

    $('#btnSubmitTrain').val("Submit");

    /*Binding Train Details into the Grid*/
    $('#TrainsDetailsGrid').jqxGrid({
        
        width: '60%',
        height: '300px',
        columns: [
        { text: 'Train Number', datafield: 'TrainNumber', hidden: true},
        { text: 'Train Name', datafield: 'TrainName'},
        { text: 'Source', datafield: 'Source'},
        { text: 'Destination', datafield: 'Destination'},
        { text: 'Distance', datafield: 'Distance'},
        { text: 'Arrival Time', datafield: 'ArrivalTime'},
        { text: 'Departure Time', datafield: 'DepartureTime'},
        {
            text: 'Select Train', datafield: 'Edit', columntype: 'button', width: 50, cellsrenderer: function () {
                return "Edit";
            }, buttonclick: function (row) {
                editrowindex = row;
               var varTrainNumber = $("#TrainsDetailsGrid").jqxGrid('getcellvalue', row, "TrainNumber");
                var varTrainName = $("#TrainsDetailsGrid").jqxGrid('getcellvalue', row, "TrainName");
                var varSource = $("#TrainsDetailsGrid").jqxGrid('getcellvalue', row, "Source");
                var varDestination = $("#TrainsDetailsGrid").jqxGrid('getcellvalue', row, "Destination");
                var varDistance = $("#TrainsDetailsGrid").jqxGrid('getcellvalue', row, "Distance");
                var varArrivalTime = $("#TrainsDetailsGrid").jqxGrid('getcellvalue', row, "ArrivalTime");
                var varDepartureTime = $("#TrainsDetailsGrid").jqxGrid('getcellvalue', row, "DepartureTime");
                $('#txtTrainNumber').val(varTrainNumber);
                $('#txtTrainName').val(varTrainName);
                $('#txtSource').val(varSource);
                $('#txtDestination').val(varDestination);
                $('#txtDistance').val(varDistance);
                $('#txtArrivalTime').val(varArrivalTime);
                $('#txtDepartureTime').val(varDepartureTime);
                $('#btnSubmitTrain').val("Update");
                $('#btnClear').val("Delete")
            }
        }
        ],
        theme: 'metro',
        sortable: true,
        pageable: true,
        source: null
    });
    BindTrainDetails();
    /*Binding Schedule details into the grid*/
    $('#TrainScheduleGrid').jqxGrid({

        width: '60%',
        height: '300px',
        columns: [
        { text: 'Train Number', datafield: 'TrainNumber', hidden: true },
        { text: 'Train Name', datafield: 'TrainName' },
        { text: 'Source', datafield: 'Source' },
        { text: 'Destination', datafield: 'Destination' },
        { text: 'Distance', datafield: 'Distance' },
        { text: 'Arrival Time', datafield: 'ArrivalTime' },
        { text: 'Departure Time', datafield: 'DepartureTime' },
      
              ],
        theme: 'metro',
        sortable: true,
        pageable: true,
        source: null
    });
    BindTrainDetailsSchedule();
    

    
    /*submit and update details of Train*/

    $('#btnSubmitTrain').click(function () {
        if ($('#btnSubmitTrain').val() == "Submit") {
            $('#TrainsDetailsGrid').show();
            var object = JSON.stringify({ "obj": [$('#txtTrainName').val(), $('#txtSource').val(), $('#txtDestination').val(), $('#txtDistance').val(), $('#txtArrivalTime').val(), $('#txtDepartureTime').val()] })
            ajaxCaller("TrainDetailsService.asmx/CreateTrainService", object, SuccessCallBack, FailureCallBack);
        }
        else {
            var object = JSON.stringify({ "objUpdatedTrains": [$('#txtTrainNumber').val(), $('#txtTrainName').val(), $('#txtSource').val(), $('#txtDestination').val(), $('#txtDistance').val(), $('#txtArrivalTime').val(), $('#txtDepartureTime').val()] })
                ajaxCaller("TrainDetailsService.asmx/UpdateTrainService", object, SuccessCallBack, FailureCallBack);
                $('#btnSubmitTrain').val("Submit");
        }
    })

    /*clear and delete Train Details*/

    $('#btnClear').click(function () {
        if ($('#btnClear').val() == "Clear") {
            $('#txtTrainNumber').val(' ');
            $('#txtTrainName').val(' ');
            $('#txtSource').val(' ');
            $('#txtDestination').val(' ');
            $('#txtDistance').val(' ');
            $('#txtArrivalTime').val(' ');
            $('#txtDepartureTime').val(' ');
        }
        else {
            var object = JSON.stringify({ "objDeleteTrainDetails": [$('#txtTrainNumber').val(), $('#txtTrainName').val(), $('#txtSource').val(), $('#txtDestination').val(), $('#txtDistance').val(), $('#txtArrivalTime').val(), $('#txtDepartureTime').val()] });
            ajaxCaller("TrainDetailsService.asmx/DeleteTrainService", object, SuccessCallBack, FailureCallBack);
            $('#btnClear').val("Clear");
        }
    })

    //$('#btnSubmitFare').click(function () {
    //    if ($('#btnSubmitFare').val() == "Submit") {
    //        var ddlSelectedValue = $('#ddlFareDetails').options[$('#ddlFareDetails').selectedIndex].text
    //        var object = JSON.stringify({ "objectFare": [$('#txtFare').val(), ddlSelectedValue] })
    //        ajaxCaller("TrainDetailsService.asmx/CreateTrainService", object, SuccessCallBack, FailureCallBack);
    //    }
    //    else {
    //        var object = JSON.stringify({ "objUpdatedTrains": [$('#txtTrainNumber').val(), $('#txtTrainName').val(), $('#txtSource').val(), $('#txtDestination').val(), $('#txtDistance').val(), $('#txtArrivalTime').val(), $('#txtDepartureTime').val()] })
    //        ajaxCaller("TrainDetailsService.asmx/UpdateTrainService", object, SuccessCallBack, FailureCallBack);
    //        $('#btnSubmitTrain').val("Submit");
    //    }
    //})
    $('#btnClearFare').click(function () {
        $('#txtFare').val(' ');
        $('#ddlFareDetails').val("-1");
    })
    
});


function SuccessCallBack(data) {
    alert(data.d.Message)
}
function FailureCallBack(XHR, msg, exception) {

    debugger;
    alert(msg);
}
function ajaxCaller(url, dataToSend, SuccessCallBack, FailureCallBack) {
    $.ajax({
        url: url,
        async: true,
        type: 'POST',
        data: dataToSend,
        contentType: "application/json; charset = utf-8",
        datatype: "json",
        success: SuccessCallBack,
        error: FailureCallBack
    })
}
function SuccessCallForSechedule(data) {
    var cities = data.d;
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
        localData: cities
    };
    var dataAdapter = new $.jqx.dataAdapter(source);
    $('#TrainScheduleGrid').jqxGrid('source', dataAdapter);
}

function ajaxCallerForSchedule(url, dataToSend, SuccessCallForSechedule, FailureCallBack) {
    $.ajax({
        url: url,
        async: true,
        type: 'POST',
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SuccessCallForSechedule,
        error: FailureCallBackForSchedule
    });
}

function FailureCallBackForSchedule(xhr, msg, exception) {
    alert(msg);
}
function BindTrainDetailsSchedule() {
    var objData = JSON.stringify({});
    ajaxCallerForSchedule("TrainDetailsService.asmx/GetAllTrainsService", objData, SuccessCallForSechedule, FailureCallBackForSchedule);
}
function SuccessCallForGrid(data) {
    var cities = data.d;
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
        localData: cities
    };
    var dataAdapter = new $.jqx.dataAdapter(source);
    $('#TrainsDetailsGrid').jqxGrid('source', dataAdapter);

}

function FailureCallBack(xhr, msg, exception) {
    alert(msg);
}
function ajaxCallerFOrGrid(url, dataToSend, SuccessCallForGrid, FailureCallBack) {
    $.ajax({
        url: url,
        async: true,
        type: 'POST',
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SuccessCallForGrid,
        error: FailureCallBack
    });
}


function BindTrainDetails() {
    var objData = JSON.stringify({});
    ajaxCallerFOrGrid("TrainDetailsService.asmx/GetAllTrainsService", objData, SuccessCallForGrid, FailureCallBack);
}

