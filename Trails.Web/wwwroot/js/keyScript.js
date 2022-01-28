//create beacon generate key call to endpoint to retrieve rnd key
document.getElementById('gen-btn').addEventListener('click', async () => {
    let keyField = document.getElementById('key-field');
    let infoLabel = document.getElementById('info-label');
    let response = await fetch('/Administration/Beacon/getkey');
    if (response.ok) {
        let result = await response.json();
        keyField.value = result.key;
    } else {
        infoLabel.textContent = 'Error getting key from server! Type strong key by yourself or try again later...';
    }
});

//create beacon copy key 
document.getElementById('copy-key').addEventListener('click', () => {
    navigator.clipboard.writeText(document.getElementById("key-field").value);
    document.getElementById('info-label').textContent = 'Copied!';
});