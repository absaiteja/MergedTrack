$(document).ready(function () {
    $('input[type = button]').button();
    $('.tabs').tabs();

    function SuccessCallBack(data) {
        if (data.d == true) {
            $(location).attr('href', 'UsersHome.aspx');
        }
        else
            alert("Please Enter Correct Password");
    }
    function FailureCallBack(XHR, msg, exception) {
        debugger;
        alert(msg);
    }
    $('#btnSubmit').click(function () {
        if (($('#txtEmail').val() == "admin") && ($('#txtPassword').val() == "admin")) {
            $(location).attr('href', 'AdminHome.aspx');
        }
    })

    $('#btnUSubmit').click(function () {
        var object = JSON.stringify({ strEmailId: $('#txtUEmail').val(), strPassword: $('#txtUPassword').val() });
        ajaxCaller("UsersService.asmx/GetService", object, SuccessCallBack, FailureCallBack);
    })

    function ajaxCaller(url, dataToSend, SuccessCallBack, FailureCallBack) {
        debugger;
        $.ajax({
            url: url,
            async: true,
            type: 'POST',
            data: dataToSend,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: SuccessCallBack,
            error: FailureCallBack
        })

    }
    $('#btnRsubmit').click(function () {
        var object = JSON.stringify({ "obj": [$('#txtFirstName').val(), $('#txtContactNumber').val(), $('#txtUserName').val(), $('#txtRPassword').val(), "Male"] });
        ajaxCaller("UsersService.asmx/CreateService", object, SuccessCallBack, FailureCallBack);
    })
    $('#ddlSelector').change(function () {
        var trainname = JSON.stringify({ strEmailId: $('#ddlSelector').val() });
        ajaxCallerFare("FaresService.asmx/GetFareService", trainname, SuccessFare, FailureCallBack);
    })
    function ajaxCallerFare(url, dataToSend, SuccessFare, FailureCallBack) {
        debugger;
        $.ajax({
            url: url,
            async: true,
            type: 'POST',
            data: dataToSend,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: SuccessCallBack,
            error: FailureCallBack
        })
    }
    function SuccessFare(data) {
        $('#txtFare').val(data.d.Fare);
    }
});
