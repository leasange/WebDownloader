//url:http://kzgm.bbshjz.cn:8000/ncms/mask/book-view
//name:抢口罩
//alert(window.navigator.userAgent.toLowerCase());

if (window.location.href.indexOf("http://kzgm.bbshjz.cn:8000") == -1) {
    window.location.href="http://kzgm.bbshjz.cn:8000/ncms/mask/book-view";
}
var count = 1;
setTimeout(function () {
    console.log("name element = " + document.getElementById("name"));
    document.title = "##{real_name}##";
    var lb = document.getElementById("cefMsg");
    if (lb==undefined) {
        lb = document.createElement("label");
        lb.setAttribute("id", "cefMsg");
        lb.setAttribute("style", "font-size:20px;color:red;background-color:black;");
        lb.innerHTML = "暂无消息，请等待...";
        document.body.insertBefore(lb, document.body.childNodes[0]);
    }
    lb = document.getElementById("cefMsg");
    lb.innerHTML = "暂无消息，请等待...";
    if (document.getElementById("captcha") == undefined) {
        lb.innerHTML = "当前非枪口罩页面-等待自动刷新...";
        setInterval(function () {
            if (document.getElementById("captcha") == undefined) {
                var now = new Date();
                var time = new Date();
                time.setHours(16, 59, 59);
                console.log(now > time);
                if (now > time) {
                    var sp = (now.getTime() - time.getTime()) / 1000;
                    console.log(sp);
                    if (sp > 200) {
                        lb.innerHTML = "当前枪口罩已过时间：" + sp + "秒,今天不抢了，明天17:00请打开，再见！！！！";
                        return;
                    }
                    window.location.reload();
                }
            }
        }, 500);
        return;
    }

    //var lb = $("<label id=\"cefMsg\" style=\"font-size:20px;color:red;background-color:black;\">暂无消息</label>");
    //$("body").prepend(lb);

    document.getElementById("name").value = "##{real_name}##";
    document.getElementById("pharmacyName").value = "##{pharmacy_name}##";
    document.getElementById("pharmacyCode").value = "##{pharmacy_code}##";
    document.getElementById("cardNo").value = "##{card_code}##";
    document.getElementById("phone").value = "##{phone}##";
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
                codeErrorRetry();
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

function codeErrorRetry() {
    document.getElementById("validateImg").src = "";
    refreshImg();
    getCodeInterval = setInterval("getCodeImage()", 100);
}