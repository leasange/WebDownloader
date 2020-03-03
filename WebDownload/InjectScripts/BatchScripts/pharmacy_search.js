//url:http://kzgm.bbshjz.cn:8000/ncms/monthMuffleStock/select
//name:药店查询
if (window.location.href.indexOf("http://kzgm.bbshjz.cn:8000/ncms/monthMuffleStock") == -1) {
    window.location.href = "http://kzgm.bbshjz.cn:8000/ncms/monthMuffleStock/select";
}

function clickPullUpRefresh2() {
    count = 0;
    noMoreData = false
    var table = document.body.querySelector('.mui-table-view');
    table.innerHTML = '';
    pullupRefresh2();
}

/**
 * 下拉刷新
 */
function pulldownRefresh2() {
    //禁用下拉
    return;
    //_refresh('pulldownRefresh')
}

/**
 * 上拉加载
 */
function pullupRefresh2() {
    _refresh2('pullupRefresh')
}

function _refresh2(type) {
    count = count + 1;
    showLoading();
    setTimeout(function () {
        var table = document.body.querySelector('.mui-table-view');
        var stockDate = $('input[name="stockDate"]').val();
        var name = $('input[name="name"]').val();
        var areaId = $('input[name="areaId"]').val();
        var nextDate = $('#nextDate').val();
        if (stockDate < nextDate) {
            alert('查询时间预约活动已结束，请重新选择日期');
            completeLoading();
            return;
        }
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: urlFirst + '/monthMuffleStock/search',
            data: { stockDate: stockDate, name: name, areaId: areaId, pageNum: count, pageSize: 100 },
            success: function (data) {
                var cells = data.data;
                //table.innerHTML = '';
                if (count === 1 && cells.length <= 0) {
                    alert('没有找到您查找的药店');
                    completeLoading();
                    return;
                }
                if (cells.length <= 0) {
                    noMoreData = true;
                }
                for (var i = 0, len = cells.length; i < len; i++) {
                    var status = '';
                    var msg = ''
                    if (cells[i].stock === 0) {
                        status = '已约满';
                    } else if (cells[i].stock === 1) {
                        status = '<span style="color: #2ac845">可预约</span>';
                        msg = '<span style="color: #2ac845">剩余' + cells[i].remainStock + '</span>';
                    } else {
                        status = '未开始';
                    }
                    var li = document.createElement('li');
                    li.className = 'mui-table-view-cell';
                    li.style.paddingRight = '10px';
                    li.style.setProperty("-webkit-user-select", "-webkit-user-select:text !important;");
                    li.innerHTML = '<div class="mui-table" style="cursor:default;-webkit-user-select:text !important;">' +
                            '<div class="mui-table-cell mui-col-xs-7" style="-webkit-user-select:text !important;">' +
                            '<h4 class="name" style="-webkit-user-select:text !important;color:red;">' + (cells[i].name) + '</h4>' +
                            '<p class="mui-h6" style="-webkit-user-select:text !important;">地址: ' + (cells[i].address === null ? '无' : cells[i].address) + '</p>' +
                            '<p class="mui-h6 mui-ellipsis" style="-webkit-user-select:text !important;">联系电话: ' + (cells[i].phone === null ? '无' : cells[i].phone) + '</p>' +
                            '<p class="mui-h6 mui-ellipsis" style="-webkit-user-select:text !important;color:red;">编码: ' + (cells[i].code === null ? '无' : cells[i].code) + '</p>' +
                            '</div>' +
                            '<div class="mui-table-cell mui-col-xs-3 mui-text-right" style="-webkit-user-select:text !important;">' +
                            '<h5 class="mui-ellipsis" style="-webkit-user-select:text !important;">' + status + '</h5>' +
                            '<h6 style="-webkit-user-select:text !important;">' + msg + '</h6>' +
                            '</div>' +
                            '</div>';

                   
                    /* var li = document.createElement('li');
                    li.style.paddingRight = '10px';
                    li.innerHTML = '<div>' +
                            '<div>' +
                            '<h4>' + (cells[i].name) + '</h4>' +
                            '<p>地址: ' + (cells[i].address === null ? '无' : cells[i].address) + '</p>' +
                            '<p>联系电话: ' + (cells[i].phone === null ? '无' : cells[i].phone) + '</p>' +
                            '<p>编码: ' + (cells[i].code === null ? '无' : cells[i].code) + '</p>' +
                            '</div>' +
                            '<div>' +
                            '<h5>' + status + '</h5>' +
                            '<h6>' + msg + '</h6>' +
                            '</div>' +
                            '</div>';*/


                    if (type === 'pulldownRefresh') {
                        table.insertBefore(li, table.firstChild);
                    } else if (type === 'pullupRefresh') {
                        table.appendChild(li);
                    }
                }
                if (type === 'pullupRefresh') {
                    mui('#pullrefresh').pullRefresh().endPullupToRefresh(noMoreData);
                }
                if (type === 'pulldownRefresh') {
                    mui('#pullrefresh').pullRefresh().endPulldownToRefresh(noMoreData);
                }
                completeLoading();
            },
            error: function (err) {
                completeLoading();
            }
        });
    }, 1500);
}

$(":button").bind("click", clickPullUpRefresh2);