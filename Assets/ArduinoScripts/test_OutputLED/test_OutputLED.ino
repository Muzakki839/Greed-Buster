unsigned long last_time = 0;

int led1 = 2;

void setup() {
  Serial.begin(9600);
  while (!Serial) {}

  pinMode(led1, OUTPUT);
  Serial.println("Arduino is alive!!");
}

void loop() {
  // // Print a heartbeat
  // if (millis() > last_time + 2000) {
  //   Serial.println("Arduino is alive!!");
  //   last_time = millis();
  // }

  // Send some message when I receive an 'A' or a 'Z'.
  String message = Serial.readStringUntil('\n');
  Serial.print("uno: ");
  Serial.println(message);  // Debugging di Arduino

  if (message == "l0_on") {
    digitalWrite(led1, HIGH);
  } else if (message == "l0_off") {
    digitalWrite(led1, LOW);
  }
}