async function processGPXAsync(file) {
    let processedRoutePoints = [];
    if (!file.name.endsWith('.gpx')) {
        return;
    }
    let contents = await readFile(file);
    let pointsAsText = contents.match(/<trkpt \w+=\"[0-9]+\.[0-9]+\" \w+=\"[0-9]+\.[0-9]+\"/gm);
    if (pointsAsText.length === 0) {
        return;
    }
    for (var i = 0; i < pointsAsText.length; i++) {
        let pointText = pointsAsText[i];
        let latlng = pointText.match(/([\d.]+)/g);
        let point = [];
        let lat = parseFloat(latlng[0]);
        let lng = parseFloat(latlng[1]);
        point.push(lat);
        point.push(lng);
        processedRoutePoints.push(point);
    }
    return processedRoutePoints;
}



function readFile(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = res => {
            resolve(res.target.result);
        };
        reader.onerror = err => reject(err);

        reader.readAsText(file);
    });
}