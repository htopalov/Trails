const connection = new signalR.HubConnectionBuilder()
    .withUrl("/live-feed")
    .configureLogging(signalR.LogLevel.None)
    .build();

async function startConnection() {
    try {
        await connection.start();
    } catch (err) {
        setTimeout(() => startConnection(), 5000);
    }
};