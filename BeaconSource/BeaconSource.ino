// GPRS credentials
const char apn[]      = "internet.vivacom.bg"; // APN of mobile network
const char gprsUser[] = "VIVACOM"; // GPRS User
const char gprsPass[] = "VIVACOM"; // GPRS Password
const char simPIN[]   = "xxxx"; // SIM card PIN

// Server details
const char server[] = "https://trailslive.net/"; // Domain
const int  port = 00; // Server port number
const char ip[]= "xx.xx.xx.xx"; //Ip address for connection

const char authKey[]= "xxxxxxxxxxxxx";
const char BeaconImei[] = "xxxxxxxxxxx";

// Controller pins
#define MODEM_RST            5
#define MODEM_PWKEY          4
#define MODEM_POWER_ON       23
#define MODEM_TX             27
#define MODEM_RX             26
#define I2C_SDA              21
#define I2C_SCL              22

// Set serial for debug console (to Serial Monitor, default speed 115200)
#define SerialMon Serial
// Set serial for AT commands (to SIM800 module)
#define SerialAT Serial1

// Configure TinyGSM library
#define TINY_GSM_MODEM_SIM800      // Modem is SIM800
#define TINY_GSM_RX_BUFFER   1024  // Set RX buffer to 1Kb

#include <Wire.h>
#include <TinyGsmClient.h>
#include <TinyGPS++.h>
#include <SoftwareSerial.h>
#include <ArduinoJson.h>

// I2C for SIM800 (to keep it running when powered from battery)
TwoWire I2CPower = TwoWire(0);

TinyGPSPlus gps; // The TinyGPS++ object

TinyGsm modem(SerialAT); // TinyGSM modem object

TinyGsmClient client(modem); // TinyGSM Client for Internet connection(2G GPRS from mobile network)
#define uS_TO_S_FACTOR 1000000UL   // Conversion factor for micro seconds to seconds
#define TIME_TO_SLEEP  20        // Time ESP32 will go to sleep (in seconds) 3600 seconds = 1 hour
#define IP5306_ADDR          0x75
#define IP5306_REG_SYS_CTL0  0x00

void setup() {
  Serial2.begin(9600, SERIAL_8N1, 14, 27); // RX, TX Pins for GPS Serial
  SerialMon.begin(115200); // Set serial monitor debugging window baud rate to 115200
  bool isOk = setPowerBoostKeepOn(1); // Keep power when running from battery
  SerialMon.println(String("IP5306 KeepOn ") + (isOk ? "OK" : "FAIL"));

  // Set modem reset, enable, power pins
  pinMode(MODEM_PWKEY, OUTPUT);
  pinMode(MODEM_RST, OUTPUT);
  pinMode(MODEM_POWER_ON, OUTPUT);
  digitalWrite(MODEM_PWKEY, LOW);
  digitalWrite(MODEM_RST, HIGH);
  digitalWrite(MODEM_POWER_ON, HIGH);

  // Set GSM module baud rate and UART pins
  SerialAT.begin(115200, SERIAL_8N1, MODEM_RX, MODEM_TX);

  SerialMon.println("Initializing modem...");
  modem.restart(); // Restart SIM800 module

  // Unlock your SIM card with a PIN if needed
  if (strlen(simPIN) && modem.getSimStatus() != 3 ) {
    modem.simUnlock(simPIN);
  }

  esp_sleep_enable_timer_wakeup(TIME_TO_SLEEP * uS_TO_S_FACTOR);
}

void loop() {
  double currLatitude = 0;
  double currLongitude = 0;
  double currAltitude = 0;
  double currSpeed = 0;
  
  SerialMon.print("Connecting to mobile network: ");
  if (!modem.gprsConnect(apn, gprsUser, gprsPass)) {
    SerialMon.println(" Fail!");
    esp_deep_sleep_start();
  } else {
    SerialMon.println("Connected to mobile network");
  }

  SerialMon.print("Connecting to server: ");
  if (!client.connect(ip, port)) {
      SerialMon.println(" Fail!");
      esp_deep_sleep_start();
  } else{
    SerialMon.println("Connected to server");
  }
      
  while (Serial2.available() > 0){
       gps.encode(Serial2.read());
       if (gps.location.isUpdated()){ 
           currLatitude = gps.location.lat();
           currLongitude = gps.location.lng();
           currAltitude = gps.altitude.meters();
           currSpeed = gps.speed.kmph();
       }
  }
        
  if(currLatitude == 0 || currLongitude == 0){
    SerialMon.println("Incorrect/Missing GPS data!");
    SerialMon.println("Disconnecting from network!");
    client.stop();
    modem.gprsDisconnect();
    esp_deep_sleep_start();
  }
         
  String result = SerializeData(currLatitude,currLongitude,currAltitude,currSpeed);

      // Making a HTTP POST request to server
      SerialMon.println("HTTP POST request to endpoint");
      client.print(String("POST ") + "https://trailslive.net/beacon/data" + " HTTP/1.1\r\n");
      client.print(String("Host: ") + "https://trailslive.net/" + "\r\n");
      client.println("AuthKey: " + String(authKey));
      client.println("Connection: close");
      client.println("Content-Type: application/json");
      client.print("Content-Length: ");
      client.println(result.length());
      client.println();
      client.println(result);

      unsigned long timeout = millis();
      while (client.connected() && millis() - timeout < 10000L) {
          // Print response from server-have to be 200 OK if event,data,auth key,timestamp are correct
          while (client.available()) {
            char c = client.read();
            SerialMon.print(c);
            timeout = millis();
        }
      }    
  
      // Close client and disconnect
      client.stop();
      SerialMon.println("Server disconnected");
      modem.gprsDisconnect();
      SerialMon.println("GPRS disconnected");
      esp_deep_sleep_start();
}

String SerializeData(double currLatitude, double currLongitude, double currAltitude, double currSpeed){
         String result;
         DynamicJsonDocument doc(110);
         doc["latitude"]   = currLatitude;
         doc["longitude"]  = currLongitude;
         doc["altitude"]   = currAltitude;
         doc["speed"]      = currSpeed;
         doc["beaconImei"]   = BeaconImei;
         serializeJson(doc, result);
         return result;
}

bool setPowerBoostKeepOn(int en){
  I2CPower.beginTransmission(IP5306_ADDR);
  I2CPower.write(IP5306_REG_SYS_CTL0);
  if (en) {
    I2CPower.write(0x37); // Set bit1: 1 enable 0 disable boost keep on
  } else {
    I2CPower.write(0x35); // 0x37 is default reg value
  }
  return I2CPower.endTransmission() == 0;
}
