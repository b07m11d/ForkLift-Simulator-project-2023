#include <Joystick.h>

Joystick_ Joystick;

void setup() {
    Serial.begin(9600);
  // Initialize Button Pins
  pinMode(2, INPUT_PULLUP);
  pinMode(3, INPUT_PULLUP);
  pinMode(4, INPUT_PULLUP);
  pinMode(5, INPUT_PULLUP);
  pinMode(6, INPUT_PULLUP);
  pinMode(7, INPUT_PULLUP);
  pinMode(8, INPUT_PULLUP);
  pinMode(9, INPUT_PULLUP);
  pinMode(10, INPUT_PULLUP);
  pinMode(11, INPUT_PULLUP);
  pinMode(12, INPUT_PULLUP);
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(A2, INPUT_PULLUP);
  pinMode(A3, INPUT_PULLUP);
  pinMode(A4, INPUT_PULLUP);
  pinMode(A5, INPUT_PULLUP);
  // Initialize Joystick Library
  Joystick.begin();
}

// Constant that maps the phyical pin to the joystick button.
const int pinToButtonMap = 2;

// Last state of the button
int lastButtonState[11] = {0,0,0,0,0,0,0,0,0,0,0};

void loop() {
  int pot1 = analogRead(A0);
  int pot2 = analogRead(A1); 
  int mapped1 = map(pot1,570,800,-1,1);
  //{Joystick.setRyAxis(mapped1);}
  int mapped2 = map(pot2,635,800,-1,1);  
  //{Joystick.setRxAxis(mapped2);}
  Serial.println(String(mapped1)+"@"+String(mapped2));    
 
  // Read pin values
  for (int index = 0; index < 11; index++)
  {
    int currentButtonState = !digitalRead(index + pinToButtonMap );
    if (currentButtonState != lastButtonState[index])
    {
      Joystick.setButton(index, currentButtonState);
      lastButtonState[index] = currentButtonState;
    }
  }
   delay(30);
  //delay(50);
}