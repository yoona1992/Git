
/**
* 线段是否相交
* seg: [{ lat: xxx, lng: xxx }, { lat: xxx, lng: xxx }]
* */
function isSegmentsIntersectant(segA, segB) {
    const abc = (segA[0].lat - segB[0].lat) * (segA[1].lng - segB[0].lng) - (segA[0].lng - segB[0].lng) * (segA[1].lat - segB[0].lat);
    const abd = (segA[0].lat - segB[1].lat) * (segA[1].lng - segB[1].lng) - (segA[0].lng - segB[1].lng) * (segA[1].lat - segB[1].lat);
    if (abc * abd >= 0) {
        return false;
    }

    const cda = (segB[0].lat - segA[0].lat) * (segB[1].lng - segA[0].lng) - (segB[0].lng - segA[0].lng) * (segB[1].lat - segA[0].lat);
    const cdb = cda + abc - abd;
    return !(cda * cdb >= 0);
}

/**
 * 判断两多边形边界是否相交
 */
function isPolygonsIntersectant(plyA, plyB) {
    for (let i = 0, il = plyA.length; i < il; i++) {
        for (let j = 0, jl = plyB.length; j < jl; j++) {
            const segA = [plyA[i], plyA[i === il - 1 ? 0 : i + 1]];
            const segB = [plyB[j], plyB[j === jl - 1 ? 0 : j + 1]];
            if (isSegmentsIntersectant(segA, segB)) {
                return true;
            }
        }
    }
    return false;
}

/**
 * 判断两多变形是否存在点与区域的包含关系(A的点在B的区域内或B的点在A的区域内)
 */

var polygon_A="";
var polygon_B="";
function isPointInPolygonBidirectional(plyA, plyB) {
    const [pA, pB] = [[], []];
    plyA.forEach((item) => {
        pA.push(new BMap.Point(item.lng, item.lat));
    });

    plyB.forEach((item) => {
        pB.push(new BMap.Point(item.lng, item.lat));
    });

    //看polygon_A，polygon_A是否为空
    if(polygon_A==""){
        polygon_A=new BMap.Polygon(pA);
    }else{
        polygon_A.setPath(pA);
    }
    if(polygon_B==""){
        polygon_B=new BMap.Polygon(pB);
    }else{
        polygon_B.setPath(pB);
    }
    let [a, b] = [false, false];
    a = pA.some(item => BMapLib.GeoUtils.isPointInPolygon(item,polygon_B ));
    if (!a) {
        b = pB.some(item => BMapLib.GeoUtils.isPointInPolygon(item, polygon_A));
    }

    return a || b;
}
