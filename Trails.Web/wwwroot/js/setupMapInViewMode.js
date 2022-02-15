﻿let center = [42.6194, 25.3930];

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

L.control.mousePosition().addTo(map);
L.control.scale().addTo(map);

L.Control.Watermark = L.Control.extend({
    onAdd: function () {
        let img = L.DomUtil.create('img');
        img.src = '/images/map/watermark.png';
        img.style.width = '130px';
        return img;
    }
});

L.control.watermark = function (opts) {
    return new L.Control.Watermark(opts);
}

L.control.watermark({ position: 'topright' }).addTo(map);