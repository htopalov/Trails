async function connectParticipantToBeaconAsync(XsrfToken) {
    let participantSelect = document.getElementById('participants');
    let beaconSelect = document.getElementById('beacons');

    if (participantSelect.value === '' || beaconSelect.value === '') {
        window.location = '/Home/Error';
        return;
    }

    await fetch('/Administration/Admin/Connectivity',
            {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': `${XsrfToken}`
                },
                body: JSON.stringify({
                    participantId: participantSelect.value,
                    beaconId: beaconSelect.value
                })
            })
        .then((response) => {
            if (response.ok) {
                participantSelect.remove(participantSelect.selectedIndex);
                beaconSelect.remove(beaconSelect.selectedIndex);
            } else if (response.status === 400) {
                window.location = '/Home/Error';
            }
        })
        .catch(() => {
            window.location.reload();
        });
}