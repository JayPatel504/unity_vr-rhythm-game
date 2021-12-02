#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BNO055.h>
#include <utility/imumaths.h>

/* Set the delay between fresh samples */
uint16_t BNO055_SAMPLERATE_DELAY_MS = 100;
const int FLEX_A0 = A0; // Pin connected to voltage divider output
const int FLEX_A1 = A1;
const int ledpinG = A3; //Green LED
const int ledpinR = A4; //Red LED
const float VCC = 3.3; // Measured voltage of Ardunio 5V line
const float R_DIV = 10000.0; // Measured resistance of 3.3k resistor
const float STRAIGHT_RESISTANCE = 25196.7; // resistance when straight
const float BEND_RESISTANCE = 48000.0; // resistance at 90 deg
float angle0,angle1; //Flex stuff

Adafruit_BNO055 bno = Adafruit_BNO055(55);
int inputVal=0;
void setup(void)
{
  Serial.begin(9600);
    pinMode(FLEX_A0, INPUT);
    pinMode(FLEX_A1, INPUT);
    pinMode(ledpinG, OUTPUT);
    pinMode(ledpinR, OUTPUT);
    digitalWrite(ledpinG,LOW);
    digitalWrite(ledpinR,LOW);

  /* Initialise the sensor */
  if (!bno.begin())
  {
    /* There was a problem detecting the BNO055 ... check your connections */
    Serial.println("Ooops, no BNO055 detected ... Check your wiring or I2C ADDR!");
    while (1);
  }
}

void loop(void)
{

  if(Serial.available()){
    inputVal=Serial.read();
    if(inputVal=='1'){
      digitalWrite(ledpinG,HIGH);
      delay(1000);
      digitalWrite(ledpinG,LOW);
      inputVal=0;
    }
    else{
      if (inputVal=='2'){
        digitalWrite(ledpinR,HIGH);
        delay(1000);
        digitalWrite(ledpinR,LOW);
        inputVal=0;
      }
    }
  }
  
  //could add VECTOR_ACCELEROMETER, VECTOR_MAGNETOMETER,VECTOR_GRAVITY...
  sensors_event_t orientationData;
  bno.getEvent(&orientationData, Adafruit_BNO055::VECTOR_EULER);

  printEvent(&orientationData);

  int flexADC = analogRead(FLEX_A0);
  float flexV = flexADC * VCC / 1023.0;
  float flexR0 = R_DIV * (VCC / flexV - 1.0);
  
  flexADC = analogRead(FLEX_A1);
  flexV = flexADC * VCC / 1023.0;
  float flexR1 = R_DIV * (VCC / flexV - 1.0);

  // Use the calculated resistance to estimate the sensor's
  // bend angle:
  angle0 = map(flexR0, STRAIGHT_RESISTANCE, BEND_RESISTANCE,0, 90.0);
  angle1 = map(flexR1, STRAIGHT_RESISTANCE, BEND_RESISTANCE,0, 90.0);
//                   
  printEvent(&orientationData);
  delay(BNO055_SAMPLERATE_DELAY_MS);
}

void printEvent(sensors_event_t* event) {
   //dumb values, easy to spot problem
  double x = -1000000, y = -1000000 , z = -1000000;
  if (event->type == SENSOR_TYPE_ORIENTATION) {
    x = event->orientation.x;
    y = event->orientation.y;
    z = event->orientation.z;
    Serial.println(String(x)+","+String(y)+","+String(z)+","+String(angle0)+","+String(angle1));
  }

   //-14 for adjustment
}
