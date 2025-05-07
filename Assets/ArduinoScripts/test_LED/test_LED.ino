// C++ code
//
int led1=2;

int delay_length = 1000;

void setup()
{
  Serial.begin(9600); // Menginisialisasi komunikasi serial dengan baud rate 9600
  while (!Serial) {}

  pinMode(led1, OUTPUT);
}

void loop()
{
  // Blink  simultanuesly
  digitalWrite(led1, HIGH);
  Serial.println("On");
  delay(delay_length);

  digitalWrite(led1, LOW);
  Serial.println("Off");
  delay(delay_length);
}
