//url:http://kzgm.bbshjz.cn:8000/ncms/mask/book-view
//name:抢口罩
if (window.location.href.indexOf("http://kzgm.bbshjz.cn:8000") == -1) {
    window.location.href="http://kzgm.bbshjz.cn:8000/ncms/mask/book-view";
}
var count = 1;
setTimeout(function () {
    console.log("name element = " + document.getElementById("name"));
    document.title = "##{real_name}##";
    if (document.getElementById("name") == undefined) {
        setInterval(function () {
            if (document.getElementById("name") == undefined) {
                var now = new Date();
                var time = new Date();
                time.setHours(16, 59, 59);
                console.log(now > time);
                if (now > time) {
                    var sp = (now.getTime() - time.getTime()) / 1000;
                    console.log(sp);
                    if (sp>30) {
                        return;
                    }
                    window.location.reload();
                }
            }
        }, 500);
        return;
    }

    var lb = $("<label id=\"cefMsg\" style=\"font-size:20px;color:red;background-color:black;\">暂无消息</label>");
    $("body").prepend(lb);

    document.getElementById("name").value = "##{real_name}##";
    document.getElementById("pharmacyName").value = "国胜大药房香御公馆店";
    document.getElementById("pharmacyCode").value = "10175";
    document.getElementById("cardNo").value = "##{card_code}##";
    document.getElementById("phone").value = "17352959982";
    document.getElementById("reservationNumber").value = "5";
    document.getElementById("pharmacyPhaseName").value = "9:00-13:00";
    document.getElementById("pharmacyPhase").value = "9:00-13:00";
    $(":button").removeAttr("disabled");
}, 100);
var getCodeInterval;
function getCodeImage() {
    if (document.getElementById("validateImg") == undefined) {
        return;
    }
    var src = document.getElementById("validateImg").src;
    if (src != "" && src != null && src != undefined) {
        if (src.indexOf("data:image/jpg;base64,") != -1) {
            clearInterval(getCodeInterval);
            var code = jsCallObject.GetValidateCode(src);
            if (code.length != 4) {
                document.getElementById("validateImg").src = "";
                refreshImg();
                getCodeInterval = setInterval("getCodeImage()", 100);
                return;
            }
            document.getElementById("captcha").value = code;//验证码
            setTimeout("bindNext()",count==1? 2000:200);
        }
    }
    return null;
}
getCodeInterval = setInterval("getCodeImage()", 100);
function afternoonNext() {
    if (document.getElementById("pharmacyPhase").value == "9:00-13:00") {
        document.getElementById("pharmacyPhase").value = "13:00-17:00";
        document.getElementById("validateImg").src = "";
        refreshImg();
        count = 2;
        getCodeInterval = setInterval("getCodeImage()", 100);
    }
}