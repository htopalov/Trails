let importedRoutePoints = [];
let importedRouteTotalLength = 0;
let minAltitude = 0;
let maxAltitude = 0;
let altitudes = [];

let yAxisChartData = [];
let xAxisChartData = [];

function drawImportedRoute(routePoints) {
    editableLayers.eachLayer(function (layer) {
            editableLayers.removeLayer(layer);
        });

    importedRouteTotalLength = 0;
    minAltitude = 0;
    maxAltitude = 0;
    importedRoutePoints.length = 0;
    yAxisChartData.length = 0;
    xAxisChartData.length = 0;
    altitudes.length = 0;

    let group = L.layerGroup();

    let pointList = [];
    for (let i = 0; i < routePoints.length; i++) {
        let currentPoint = routePoints[i];
        pointList.push(new L.LatLng(currentPoint[0], currentPoint[1], currentPoint[2]));

        let currentAltitude = currentPoint[2].toFixed(2);
        altitudes.push(currentAltitude);

        if (i % 250 === 0) {
            yAxisChartData.push(currentAltitude);
        }
    }

    minAltitude = Math.min(...altitudes);
    maxAltitude = Math.max(...altitudes);

    let route = new L.Polyline(pointList,
        {
            color: '#c700ac',
            weight: 4,
            smoothFactor: 1,
            fillOpacity: 0.5
        });

    importedRoutePoints = route.getLatLngs().map(el => [el.lat, el.lng, el.alt]);

    let rawCoordinates = route.getLatLngs();
    for (let i = 0; i < rawCoordinates.length - 1; i++) {
        importedRouteTotalLength += rawCoordinates[i].distanceTo(rawCoordinates[i + 1]);
    }

    let routeLengthForChart = 0;

    for (let i = 0; i < rawCoordinates.length - 1; i++) {
        routeLengthForChart += rawCoordinates[i].distanceTo(rawCoordinates[i + 1]);
        if (i % 250 === 0) {
            xAxisChartData.push(`${(routeLengthForChart / 1000).toFixed(2)} km`);
        }
    }

    importedRouteTotalLength /= 1000;

    route.bindPopup(`Route for your event is ${importedRouteTotalLength.toFixed(2)} kilometers long.`);

    group.addLayer(route);

    setupRouteIcons(pointList[0], pointList[pointList.length - 1], route, group);

    let chart = document.getElementById("line-chart");
    if (chart !== null) {
        chart.style.display = 'block';

        new Chart(document.getElementById("line-chart"), {
            type: 'line',
            data: {
                labels: xAxisChartData,
                datasets: [{
                    data: yAxisChartData,
                    label: "Elevation",
                    borderColor: "#fcba03",
                    fill: true
                }]
            },
            options: {
                title: {
                    display: true,
                    text: 'Elevation Profile'
                },
                scales: {
                    xAxes: [{
                        display: true
                    }]
                }
            }
        });
    }
}
