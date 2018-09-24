$(function () {
    //marker类型（1：摄像机  2：区域  3：人员   4：车辆）
    // 百度地图API功能
    var map = new BMap.Map("allmap", { enableMapClick: false });
    map.centerAndZoom("练市", 16);
    map.enableScrollWheelZoom();
    var top_left_control = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT });// 左上角，添加比例尺
    var top_left_navigation = new BMap.NavigationControl();  //左上角，添加默认缩放平移控件
    var top_right_navigation = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_RIGHT, type: BMAP_NAVIGATION_CONTROL_SMALL }); //右

    //map.addControl(top_left_control);
    //map.addControl(top_left_navigation);
    map.addControl(top_right_navigation);
    
    var content1 = "<h4 style='margin:0 0 5px 0;padding:0.2em 0'>湖滨花园</h4>";
    content1 += "<table width='450px'  borderColor='#808080' cellSpacing='0'  style='border-collapse:collapse' borderColorDark='#ffffff' cellPadding='3' borderColorLight='#808080' border='1'>";
    content1 += "<tr><td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>网格名称：</td><td style='width:100px;text-align:center;'>湖滨花园</td>";
    content1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>所属单位：</td><td style='width:100px;text-align:center;'>派出所</td></tr><tr>";
    content1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>常住人口：</td><td style='width:100px;text-align:center;'>1万</td>";
    content1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>区域人数：</td><td style='width:100px;text-align:center;'>2万</td></tr><tr>";
    content1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>暂住人口：</td><td style='width:100px;text-align:center;'>1万</td>";
    content1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>所属街道：</td><td style='width:100px;text-align:center;'>南浔区振兴东路</td></tr><tr>";
    content1 += "</table>";
    var content2 = "<h4 style='margin:0 0 5px 0;padding:0.2em 0'>练市医院</h4>";
    content2 += "<table width='450px'  borderColor='#808080' cellSpacing='0'  style='border-collapse:collapse' borderColorDark='#ffffff' cellPadding='3' borderColorLight='#808080' border='1'>";
    content2 += "<tr><td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>网格名称：</td><td style='width:100px;text-align:center;'>练市医院</td>";
    content2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>所属单位：</td><td style='width:100px;text-align:center;'>派出所</td></tr><tr>";
    content2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>常住人口：</td><td style='width:100px;text-align:center;'>0.5万</td>";
    content2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>区域人数：</td><td style='width:100px;text-align:center;'>1.5万</td></tr><tr>";
    content2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>暂住人口：</td><td style='width:100px;text-align:center;'>1万</td>";
    content2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>所属街道：</td><td style='width:100px;text-align:center;'>南浔区中心北路</td></tr><tr>";
    content2 += "</table>";
    var content3 = "<h4 style='margin:0 0 5px 0;padding:0.2em 0'>练市人民法庭</h4>";
    content3 += "<table width='450px'  borderColor='#808080' cellSpacing='0'  style='border-collapse:collapse' borderColorDark='#ffffff' cellPadding='3' borderColorLight='#808080' border='1'>";
    content3 += "<tr><td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>网格名称：</td><td style='width:100px;text-align:center;'>练市人民法庭</td>";
    content3 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>所属单位：</td><td style='width:100px;text-align:center;'>派出所</td></tr><tr>";
    content3 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>常住人口：</td><td style='width:100px;text-align:center;'>3万</td>";
    content3 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>区域人数：</td><td style='width:100px;text-align:center;'>4万</td></tr><tr>";
    content3 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>暂住人口：</td><td style='width:100px;text-align:center;'>2万</td>";
    content3 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>所属街道：</td><td style='width:100px;text-align:center;'>南浔区练市镇</td></tr><tr>";
    content3 += "</table>";

    var sContent1 = "<h4 style='margin:0 0 5px 0;padding:0.2em 0'>摄像机信息</h4>";
    sContent1 += "<table width='450px'  borderColor='#808080' cellSpacing='0'  style='border-collapse:collapse' borderColorDark='#ffffff' cellPadding='3' borderColorLight='#808080' border='1'>";
    sContent1 += "<tr><td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>名称：</td><td style='width:100px;text-align:center;'>湖滨花园出口</td>";
    sContent1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>所属单位：</td><td style='width:100px;text-align:center;'>派出所</td></tr><tr>";
    sContent1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>经度：</td><td style='width:100px;text-align:center;'>120.4238268410</td>";
    sContent1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>机型：</td><td style='width:100px;text-align:center;'>球机</td></tr><tr>";
    sContent1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>纬度：</td><td style='width:100px;text-align:center;'>30.7179782019</td>";
    sContent1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>品牌：</td><td style='width:100px;text-align:center;'>海康IPC</td></tr><tr>";
    sContent1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>建设时间：</td><td style='width:100px;text-align:center;'>2016-01-10</td>";
    sContent1 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>像素：</td><td style='width:100px;text-align:center;'>500万</td></tr>";
    sContent1 += "<tr><td colspan='2' style='text-align:center;' bgcolor='#bbe1e1'><a href='###'>实时视频</a></td>";
    sContent1 += "<td colspan='2' style='text-align:center;' bgcolor='#bbe1e1'><a href='###'>录像回放</a></td></tr>";
    sContent1 += "</table>";

    var sContent2 = "<h4 style='margin:0 0 5px 0;padding:0.2em 0'>摄像机信息</h4>";
    sContent2 += "<table width='450px'  borderColor='#808080' cellSpacing='0'  style='border-collapse:collapse' borderColorDark='#ffffff' cellPadding='3' borderColorLight='#808080' border='1'>";
    sContent2 += "<tr><td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>名称：</td><td style='width:100px;text-align:center;'>湖滨花园入口</td>";
    sContent2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>所属单位：</td><td style='width:100px;text-align:center;'>派出所</td></tr><tr>";
    sContent2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>经度：</td><td style='width:100px;text-align:center;'>120.4230436360</td>";
    sContent2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>机型：</td><td style='width:100px;text-align:center;'>球机</td></tr><tr>";
    sContent2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>纬度：</td><td style='width:100px;text-align:center;'>30.7163178612</td>";
    sContent2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>品牌：</td><td style='width:100px;text-align:center;'>海康IPC</td></tr><tr>";
    sContent2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>建设时间：</td><td style='width:100px;text-align:center;'>2016-01-10</td>";
    sContent2 += "<td style='width:100px;text-align:center;' bgcolor='#bbe1e1'>像素：</td><td style='width:100px;text-align:center;'>500万</td></tr>";
    sContent2 += "<tr><td colspan='2' style='text-align:center;' bgcolor='#bbe1e1'><a href='###'>实时视频</a></td>";
    sContent2 += "<td colspan='2' style='text-align:center;' bgcolor='#bbe1e1'><a href='###'>录像回放</a></td></tr>";
    sContent2 += "</table>";

    //addClickHandler(content1, polygon1, p);
    var polygon1 = new BMap.Polygon([
    new BMap.Point(120.4212994962, 30.7166258690),
    new BMap.Point(120.4249480044, 30.7157644079),
    new BMap.Point(120.4251518523, 30.7177752729),
    new BMap.Point(120.4220834052, 30.7187622418)
    ], { strokeColor: "#000093", strokeWeight: 1, strokeOpacity: 0.1, fillColor: "orange", fillOpacity: 0.3 });  //创建多边形（湖滨花园）
    //{strokeColor: "#000093",fillColor: "#b9b9ff",strokeWeight: 1,strokeOpacity: 0.7,fillOpacity: 0.5}

    var polygon2 = new BMap.Polygon([
    new BMap.Point(120.4135210901,30.7131436837),
    new BMap.Point(120.4157526880, 30.7126870700),
    new BMap.Point(120.4162676721, 30.7145919191),
   new BMap.Point(120.4138536840, 30.7150715846)
    ], { strokeColor: "#000093", strokeWeight: 1, strokeOpacity: 0.1, fillColor: "red", fillOpacity: 0.3 });  //创建多边形（练市人民医院）

    var polygon3 = new BMap.Polygon([
    new BMap.Point(120.4240200000, 30.7130260000),
    new BMap.Point(120.4242710000, 30.7064130000),
    new BMap.Point(120.4413390000, 30.7054810000),
    new BMap.Point(120.4418060000, 30.7097660000)
    ], { strokeColor: "#000093", strokeWeight: 1, strokeOpacity: 0.1, fillColor: "green", fillOpacity: 0.3 });  //创建多边形

    var opts = {
        width: 460,     // 信息窗口宽度
        height: 140,     // 信息窗口高度
        title: "", // 信息窗口标题
        enableMessage: true//设置允许信息窗发送短息
    };
    var opts2 = {
        width: 460,     // 信息窗口宽度
        height: 180,     // 信息窗口高度
        title: "", // 信息窗口标题
        enableMessage: true//设置允许信息窗发送短息
    };
    function addClickHandler(content, marker, p) {
        marker.addEventListener("click", function (e) {
            var infoWindow = new BMap.InfoWindow(content, opts);  // 创建信息窗口对象
            map.openInfoWindow(infoWindow, p); //开启信息窗口
        }
        );
    }
    var s = [];
    var options = {
        renderOptions: { map: map },
        pageCapacity: 10,
        onSearchComplete: function (results) {
            // 判断状态是否正确
            if (local.getStatus() == BMAP_STATUS_SUCCESS) {
                s = [];
                for (var i = 0; i < results.getCurrentNumPois() ; i++) {
                    s.push(results.getPoi(i).title + ", " + results.getPoi(i).address);
                }
            }
        }
    };
    var local = new BMap.LocalSearch(map, options);

    function addRyCl(objectNames) {
        $.ajax({
            url: "./GetNewGpsInfo",
            data: {
                objectNames: objectNames
            },
            type: "post",
            async: false,
            success: function (data) {
                removeRy();
                $.each(data, function (index, item) {
                    var marker = new BMap.Marker(new BMap.Point(item.Lon, item.Lat), {
                        icon: myIcon_Person
                    });
                    // 创建标注
                    marker.type = 3;
                    var label = new BMap.Label(item.Name, { offset: new BMap.Size(20, -10) });
                    marker.setLabel(label);
                    map.addOverlay(marker);
                });
            }
        });
    }
    function addRy() {
        $.ajax({
            url: "../Map/GetPersonList",
            dataType: "json",
            type: "post",
            success: function (data) {
                removeRy();
                $.each(data, function (index, item) {
                    var marker = new BMap.Marker(new BMap.Point(item.Lon, item.Lat), {
                        icon: myIcon_Person
                    });  // 创建标注
                    marker.type = 3;
                    var label = new BMap.Label(item.Name, { offset: new BMap.Size(20, -10) });
                    marker.setLabel(label);
                    map.addOverlay(marker);
                });
            }
        });
    }
    function removeRy() {
        var overlays = map.getOverlays();
        $.each(overlays, function (index, item) {
            if (typeof (overlays[index].type) == "undefined") {

            } else {
                if (overlays[index].type == 3) {
                    map.removeOverlay(overlays[index]);
                }
            }
        });
    }

    function addCl() {
        $.ajax({
            url: "../Map/GetCarList",
            dataType: "json",
            type: "post",
            success: function (data) {
                removeCl();
                $.each(data, function (index, item) {
                    var marker = new BMap.Marker(new BMap.Point(item.Lon, item.Lat), {
                        icon: myIcon_Car
                    });  // 创建标注
                    marker.type = 4;
                    var label = new BMap.Label(item.Name, { offset: new BMap.Size(20, -10) });
                    marker.setLabel(label);
                    map.addOverlay(marker);

                    //报警
                    alarm(item.Lon, item.Lat, polygon3);
                });
            }
        });
    }
    function removeCl() {
        var overlays = map.getOverlays();
        $.each(overlays, function (index, item) {
            if (typeof (overlays[index].type) == "undefined") {

            } else {
                if (overlays[index].type == 4) {
                    map.removeOverlay(overlays[index]);
                }
            }
        });
    }

    function addWg() {
        polygon1.type = 2;
        polygon2.type = 2;
        polygon3.type = 2;
        map.addOverlay(polygon1);
        map.addOverlay(polygon2);
        map.addOverlay(polygon3);
        var bounds1 = polygon1.getBounds();
        var p1 = bounds1.getCenter();
        var bounds2 = polygon2.getBounds();
        var p2 = bounds2.getCenter();
        var bounds3 = polygon3.getBounds();
        var p3 = bounds3.getCenter();
        addClickHandler(content1, polygon1, p1);
        addClickHandler(content2, polygon2, p2);
        addClickHandler(content3, polygon3, p3);
    }
    function removeWg() {
        map.removeOverlay(polygon1);
        map.removeOverlay(polygon2);
        map.removeOverlay(polygon3);
    }

    var data_info = [[120.4230436360, 30.7163178612, "湖滨花园入口"],
                 [120.4238268410, 30.7179782019, "湖滨花园出口"]
    ];

    var myIcon = new BMap.Icon("/res/images/i/zpcamera16_jk.gif", new BMap.Size(16, 16));
    var myIcon_Car = new BMap.Icon("/res/images/i/car.gif", new BMap.Size(25, 25));
    var myIcon_Person = new BMap.Icon("/res/images/i/person.gif", new BMap.Size(17, 26));

    function addSxj() {
        var marker = new BMap.Marker(new BMap.Point(data_info[0][0], data_info[0][1]), { icon: myIcon });  // 创建标注
        var content = data_info[0][2];
        marker.type = 1;
        map.addOverlay(marker);               // 将标注添加到地图中
        addMarkClickHandler(sContent1, marker);
        var marker = new BMap.Marker(new BMap.Point(data_info[1][0], data_info[1][1]), { icon: myIcon });  // 创建标注
        var content = data_info[1][2];
        marker.type = 1;
        map.addOverlay(marker);               // 将标注添加到地图中
        addMarkClickHandler(sContent2, marker);
    }
    function removeSxj() {
        var overlays = map.getOverlays();
        $.each(overlays, function (index, item) {
            if (typeof (overlays[index].type) == "undefined") {

            } else {
                if (overlays[index].type == 1) {
                    map.removeOverlay(overlays[index]);
                }
            }
        });
    }

    function addMarkClickHandler(content, marker) {
        marker.addEventListener("click", function (e) {
            openInfo(content, e)
        }
		);
    }

    function openInfo(content, e) {
        var p = e.target;
        var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
        var infoWindow = new BMap.InfoWindow(content, opts2);  // 创建信息窗口对象 
        map.openInfoWindow(infoWindow, point); //开启信息窗口
    }
    $("#sole-input").autocomplete({
        source: function (request, response) {
            local.search($("#sole-input").val());
            setTimeout(function () {
                if (s.length > 0) {
                    response(s);
                }
            }, 500);

        }
    });

    $("#cbRy").change(function () {
        var self = this;
        if ($("#cbRy").prop('checked')) {
            self.timeoutId = setInterval(function () {
                addRy()
            },2000);
        } else {
            clearInterval(self.timeoutId);
            removeRy();
        }
    });
    $("#cbCl").change(function () {
        var self = this;
        if ($("#cbCl").prop('checked')) {
            self.timeoutId = setInterval(function () {
                addCl();
            }, 2000);
        } else {
            clearInterval(self.timeoutId);
            removeCl();
        }
    });
    $("#cbWg").change(function () {
        if ($("#cbWg").prop('checked')) {
            addWg();
        } else {
            removeWg();
        }
    });
    $("#cbSxj").change(function () {
        if ($("#cbSxj").prop('checked')) {
            addSxj();
        } else {
            removeSxj();
        }
    });
    $("#img_pan").click(function () {
        myDrawingManagerObject.close();
        $("#img_pan").addClass("spancolor");
        $("#img_rect").removeClass("spancolor");
        $("#img_circle").removeClass("spancolor");
        $("#img_polygon").removeClass("spancolor");
    });

    $("#img_rect").click(function () {
        var currentTool = "img_rect";
        $("#img_rect").addClass("spancolor");
        $("#img_pan").removeClass("spancolor");
        $("#img_circle").removeClass("spancolor");
        $("#img_polygon").removeClass("spancolor");
        myDrawingManagerObject.setDrawingMode(BMAP_DRAWING_RECTANGLE);
        myDrawingManagerObject.open();
    });
    $("#img_circle").click(function () {
        var currentTool = "img_rect";
        $("#img_rect").removeClass("spancolor");
        $("#img_pan").removeClass("spancolor");
        $("#img_circle").addClass("spancolor");
        $("#img_polygon").removeClass("spancolor");
        myDrawingManagerObject.setDrawingMode(BMAP_DRAWING_CIRCLE);
        myDrawingManagerObject.open();
    });
    $("#img_polygon").click(function () {
        var currentTool = "img_rect";
        $("#img_rect").removeClass("spancolor");
        $("#img_pan").removeClass("spancolor");
        $("#img_circle").removeClass("spancolor");
        $("#img_polygon").addClass("spancolor");
        myDrawingManagerObject.setDrawingMode(BMAP_DRAWING_POLYGON);
        myDrawingManagerObject.open();
    });
    $("#img_unselected").click(function () {
        if (drawOverlay != null) {
            map.removeOverlay(drawOverlay);
        }
    });
    //map.addEventListener("click", function (e) {
    //    alert(e.point.lng + "," + e.point.lat);
    //});
    var drawOverlay = null;
    var myDrawingManagerObject = new BMapLib.DrawingManager(map, {
        isOpen: false,
        drawingType: BMAP_DRAWING_MARKER,
        enableDrawingTool: false,
        enableCalculate: false,
        drawingToolOptions: {
            anchor: BMAP_ANCHOR_TOP_LEFT,
            //offset: new BMap.Size(5, 5),
            drawingTypes: [
            BMAP_DRAWING_MARKER,
            BMAP_DRAWING_CIRCLE,
            BMAP_DRAWING_POLYLINE,
            BMAP_DRAWING_POLYGON,
            BMAP_DRAWING_RECTANGLE
            ]
        },
        polylineOptions: {
            strokeColor: "#000093",
            strokeWeight: 1,
            strokeOpacity: 0.7
        },
        polygonOptions: {
            strokeColor: "#000093",
            fillColor: "#b9b9ff",
            strokeWeight: 1,
            strokeOpacity: 0.7,
            fillOpacity: 0.5
        },
        circleOptions: {
            strokeColor: "#000093",
            fillColor: "#b9b9ff",
            strokeWeight: 1,
            strokeOpacity: 0.7,
            fillOpacity: 0.5
        },
        rectangleOptions: {
            strokeColor: "#000093",
            fillColor: "#b9b9ff",
            strokeWeight: 1,
            strokeOpacity: 0.7,
            fillOpacity: 0.5
        }
    });

    //获取当前时间
    function getNowDate() {
        var date = new Date();
        var sign1 = "-";
        var sign2 = ":";
        var year = date.getFullYear() // 年
        var month = date.getMonth() + 1; // 月
        var day = date.getDate(); // 日
        var hour = date.getHours(); // 时
        var minutes = date.getMinutes(); // 分
        var seconds = date.getSeconds() //秒
        var weekArr = ['星期一', '星期二', '星期三', '星期四', '星期五', '星期六', '星期天'];
        var week = weekArr[date.getDay()];
        // 给一位数数据前面加 “0”
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (day >= 0 && day <= 9) {
            day = "0" + day;
        }
        if (hour >= 0 && hour <= 9) {
            hour = "0" + hour;
        }
        if (minutes >= 0 && minutes <= 9) {
            minutes = "0" + minutes;
        }
        if (seconds >= 0 && seconds <= 9) {
            seconds = "0" + seconds;
        }
        var currentdate = year + sign1 + month + sign1 + day + " " + hour + sign2 + minutes + sign2 + seconds;;
        return currentdate;
    }

    //越格报警
    function alarm(lon,lat,polygon) {
        var point = new BMap.Point(lon,lat);
        if (BMapLib.GeoUtils.isPointInPolygon(point, polygon)==false) {
            $.toast({
                heading: '越界提醒',
                text: '浙E00000于' + getNowDate() + '超越练市人民法庭网格边界',
                icon: 'warning',
                loader: true,        // Change it to false to disable loader
                //loaderBg: '#9EC600',  // To change the background
                position: 'top-center',
                hideAfter: 10*1000   // in milli seconds
            })
        }
    }
});

