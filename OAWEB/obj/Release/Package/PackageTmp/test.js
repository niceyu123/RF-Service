jQuery(document).ready(function () {

    checkCustomize = function () { //提交前执行
        var time = {
            manid: jQuery("#field8755").val(),
            startTime: jQuery("#field8742").val() + " " + jQuery("#field8743").val(),
            endTime: jQuery("#field8744").val() + " " + jQuery("#field8745").val(),
            qjlx: jQuery("#field9061").val(),
            oaNo: jQuery("#field8733").val(),
            dptID: jQuery("#field8735").val()
        };
        var data = JSON.stringify(time);
        $.ajax({
            url: "http://172.16.11.19:1915/NewOAWebService.asmx/SaveLeaveTime?", type: "post", data: "time=" + data, dataType: "text",
            //contentType: "application/json",
            success: function (result) {
                //var c = $.parseXML(result).find("string").text();
                var tempArr = result.split("\">")[1];
                var tem = tempArr.split("</")[0];
                var t = tem;

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert("失败");
            }
        })
        return true;
    }

});

jQuery(document).ready(function () {

    checkCustomize = function () { //提交前执行
        var time = {
            mainid: jQuery("#field25202").val(),
            pm1: jQuery("#field20301").val(),
            tsbs1: jQuery("#field20302").val(),
            bk1: jQuery("#field20303").val(),
            dg1: jQuery("#field20304").val(),
            zt1: jQuery("#field20305").val(),
            lt1: jQuery("#field20306").val(),
            sb1: jQuery("#field20307").val(),
            zh1: jQuery("#field20308").val(),
            wg1: jQuery("#field20309").val(),
            jp1: jQuery("#field20310").val(),
            sz1: jQuery("#field20311").val(),
            rt1: jQuery("#field20312").val(),
            sl1: jQuery("#field20313").val(),
            fj1: jQuery("#field20314").val()
        };
        var data = JSON.stringify(time);
        $.ajax({
            url: "http://172.16.11.19:1915/NewOAWebService.asmx/SaveLeaveTime?", type: "post", data: "time=" + data, dataType: "text",
            //contentType: "application/json",
            success: function (result) {
                //var c = $.parseXML(result).find("string").text();
                var tempArr = result.split("\">")[1];
                var tem = tempArr.split("</")[0];
                var t = tem;

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert("失败");
            }
        })
        return true;
    }

});