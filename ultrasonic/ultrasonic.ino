int Trig = 8;
int Echo = 9;
int Duration;
int Distance;

void setup() {
  Serial.begin(9600);
  pinMode(Trig,OUTPUT);
  pinMode(Echo,INPUT);
}

void loop() {
  digitalWrite(Trig,LOW);
  delayMicroseconds(1);
  digitalWrite(Trig,HIGH);
  delayMicroseconds(9);
  digitalWrite(Trig,LOW);
  Serial.println(pulseIn(Echo,HIGH) * 0.017);
}
