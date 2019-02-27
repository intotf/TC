; (function (win) {
    win.WKT = new function () {
        this.getWkt = function (polygon) {
            if (!polygon || !win.BMap) {
                return null;
            }
                       
            var points = polygon;
            if (polygon.getPath) {
                points = polygon.getPath();
                if (points.length > 0) {
                    points.push(points[0]);
                }
            }

            var array = [];
            for (var i = 0; i < points.length; i++) {
                var pointText = points[i].lng + ' ' + points[i].lat;
                array.push(pointText);
            }
            var text = array.join(',');
            return 'POLYGON((' + text + '))';
        }

        this.toPolygon = function (wkt, opt) {
            if (!win.BMap) {
                return null;
            }

            var points = [];
            var array = wkt.substr(10, wkt.length - 12).split(',');
            for (var i = 0; i < array.length; i++) {
                var lnglat = array[i].trim().split(' ');
                var point = new BMap.Point(Number(lnglat[0]), Number(lnglat[1]));
                points.push(point);
            }

            return new BMap.Polygon(points, opt);
        }
    };

})(window);