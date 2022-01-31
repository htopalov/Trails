var center = [42.6194, 25.3930];

var map = L.map('map', {
    fullscreenControl: true, fullscreenControlOptions: {
        position: 'topleft'
    }
}).setView(center, 7);


L.tileLayer('https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png', {
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
                color: '#000000',
                weight: 3
            }
        },
        polygon: false,
        circle: false,
        rectangle: false,
        circlemarker: false,
        marker: false
    },
    edit: {
        featureGroup: editableLayers,
        remove: false
    }
};

var drawControl = new L.Control.Draw(drawPluginOptions);
map.addControl(drawControl);

var editableLayers = new L.FeatureGroup();
map.addLayer(editableLayers);

map.on('draw:created', function (e) {
    var type = e.layerType,
        layer = e.layer;

    editableLayers.addLayer(layer);
});