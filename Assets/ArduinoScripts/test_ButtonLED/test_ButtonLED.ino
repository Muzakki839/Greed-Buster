// ====== Konfigurasi ======
#define NUM_MOLES 1
#define DEBOUNCE_TIME 5  // debounce time dalam ms

// Mapping pin LED dan Button
const int ledPins[NUM_MOLES] = {2}; 
const int buttonPins[NUM_MOLES] = {3};

// ====== Variabel Tombol ======
bool lastButtonStates[NUM_MOLES];
bool buttonProcessed[NUM_MOLES];
unsigned long lastDebounceTime[NUM_MOLES] = {0};

// ====== Setup ======
void setup() {
  Serial.begin(9600);

  // Setup LED
  for (int i = 0; i < NUM_MOLES; i++) {
    pinMode(ledPins[i], OUTPUT);
    digitalWrite(ledPins[i], LOW);
  }

  // Setup Button
  for (int i = 0; i < NUM_MOLES; i++) {
    pinMode(buttonPins[i], INPUT_PULLUP);
    lastButtonStates[i] = digitalRead(buttonPins[i]);
    buttonProcessed[i] = false;
  }
}

// ====== Loop ======
void loop() {
  checkButtons();
  receiveSerial();
}

// ====== Cek Tombol ======
void checkButtons() {
  for (int i = 0; i < NUM_MOLES; i++) {
    bool reading = digitalRead(buttonPins[i]);

    if (reading != lastButtonStates[i]) {
      lastDebounceTime[i] = millis(); // ada perubahan, reset debounce timer
    }

    if ((millis() - lastDebounceTime[i]) > DEBOUNCE_TIME) {
      if (reading == LOW && !buttonProcessed[i]) {
        // Tombol ditekan baru
        Serial.print("btn_");
        Serial.println(i + 1);
        buttonProcessed[i] = true;
      }
      else if (reading == HIGH) {
        // Tombol dilepas
        buttonProcessed[i] = false;
      }
    }

    lastButtonStates[i] = reading;
  }
}

// ====== Terima Perintah dari Serial ======
void receiveSerial() {
  if (Serial.available()) {
    String command = Serial.readStringUntil('\n');
    command.trim(); // buang spasi/enter

    if (command.startsWith("led_")) {
      int underscoreIndex = command.indexOf('_');
      int spaceIndex = command.indexOf(' ', underscoreIndex);

      if (spaceIndex != -1) {
        int ledIndex = command.substring(underscoreIndex + 1, spaceIndex).toInt() - 1;
        String action = command.substring(spaceIndex + 1);

        if (ledIndex >= 0 && ledIndex < NUM_MOLES) {
          if (action == "ON") {
            digitalWrite(ledPins[ledIndex], HIGH);
          } else if (action == "OFF") {
            digitalWrite(ledPins[ledIndex], LOW);
          }
        }
      }
    }
  }
}
