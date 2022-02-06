let center = [42.6194, 25.3930];

let map = L.map('map', {
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

let editableLayers = new L.FeatureGroup();
map.addLayer(editableLayers);

let drawPluginOptions = {
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
    },
    edit: {
        featureGroup: editableLayers,
        edit: false
    }
};

L.EditToolbar.Delete.include({
    enable: function() {
        this.options.featureGroup.clearLayers();
        console.log(editableLayers.getLayers().length + ' editable layers delete func');
    }
});

let drawControl = new L.Control.Draw(drawPluginOptions);
map.addControl(drawControl);

L.control.mousePosition().addTo(map);
L.control.scale().addTo(map);

L.Control.Watermark = L.Control.extend({
    onAdd: function (map) {
        let img = L.DomUtil.create('img');
        img.src = '/images/watermark.png';
        img.style.width = '130px';
        return img;
    }
});

L.control.watermark = function (opts) {
    return new L.Control.Watermark(opts);
}

L.control.watermark({ position: 'topright' }).addTo(map);

map.on(L.Draw.Event.CREATED, function (e) {
    let layer = e.layer;
    editableLayers.addLayer(layer);
});