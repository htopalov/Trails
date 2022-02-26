async function getParticipantsAndBeaconsAsync() {
    let participantsSelect = document.getElementById('participants');
    let beaconsSelect = document.getElementById('beacons');
    let connectBtn = document.getElementById('connectBtn');
    if (connectBtn) {
        connectBtn.remove();
    }
    participantsSelect.length = 0;
    beaconsSelect.length = 0;

    let eventId = eventsSelect.value;
    if (eventId === null || eventId === '') {
        window.location = '/Home/Error';
        return;
    }

    await fetch(`/Administration/Admin/GetParticipantsForEvent?eventId=${eventId}`)
        .then(async (response) => {
            if (response.ok) {
                let participantsList = await response.json();
                if (participantsList.length > 0) {
                    for (let i = 0; i < participantsList.length; i++) {
                        let newMenuOption = document.createElement('option');
                        newMenuOption.value = participantsList[i]['id'];
                        newMenuOption.textContent = participantsList[i]['fullName'];
                        participantsSelect.appendChild(newMenuOption);
                    }

                    participantsSelect.addEventListener('click', async (e) => {
                        e.stopImmediatePropagation();
                        if (document.getElementById('connectBtn')) {
                            document.getElementById('connectBtn').remove();
                        }
                        beaconsSelect.length = 0;
                        await fetch('/Administration/Admin/GetBeaconsForEvent')
                            .then(async (response) => {
                                if (response.ok) {
                                    let beaconsList = await response.json();

                                    if (beaconsList.length > 0) {
                                        for (let i = 0; i < beaconsList.length; i++) {
                                            let newMenuOption = document.createElement('option');
                                            newMenuOption.value = beaconsList[i]['id'];
                                            newMenuOption.textContent = `Imei: ${beaconsList[i]['imei']}`;
                                            beaconsSelect.appendChild(newMenuOption);
                                        }
                                    }
                                } else if (response.status === 400) {
                                    window.location = '/Home/Error';
                                }
                            })
                            .catch(() => {
                                window.location = '/Home/Error';
                            });
                    });
                }
            } else if (response.status === 400) {
                window.location = '/Home/Error';
            }
        })
        .catch(() => {
            window.location = '/Home/Error';
        });
}