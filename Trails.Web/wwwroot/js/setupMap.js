var center = [42.6194, 25.3930];

var map = L.map('map', {
    fullscreenControl: true, fullscreenControlOptions: {
        position: 'topleft'
    },
    visualClick: true,
    visualClickPane: 'shadowPane'
}).setView(center, 7);


L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 18,
    attribution: '&copy; <a href="https://osm.org/copyright">OpenStreetMap</a> | &copy; <a href="https://opentopomap.org/about">OpenTopoMap</a> | &copy; <a href="/">Trails</a>'
}).addTo(map);

var editableLayers = new L.FeatureGroup();
map.addLayer(editableLayers);

var drawPluginOptions = {
    position: 'topleft',
    draw: {
        polyline: {
            shapeOptions: {
                color: '#c700ac',
                weight: 4,
                smoothFactor: 1,
                fillOpacity: 0.5
            }
        },
        polygon: false,
        circle: false,
        rectangle: false,
        circlemarker: false,
        marker: false
    }
};

var drawControl = new L.Control.Draw(drawPluginOptions);
map.addControl(drawControl);

L.control.mousePosition().addTo(map);
L.control.scale().addTo(map);

L.Control.Watermark = L.Control.extend({
    onAdd: function (map) {
        var img = L.DomUtil.create('img');
        img.src = '/images/watermark.png';
        img.style.width = '130px';
        return img;
    }
});

L.control.watermark = function (opts) {
    return new L.Control.Watermark(opts);
}

L.control.watermark({ position: 'topright' }).addTo(map);

var editableLayers = new L.FeatureGroup();
map.addLayer(editableLayers);

map.on('draw:created', function (e) {
    editableLayers.addLayer(e.layer);
});