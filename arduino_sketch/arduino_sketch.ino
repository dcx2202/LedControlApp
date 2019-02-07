#include <FastLED.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
using namespace std;

#define LED_PIN     7
#define NUM_LEDS    30
CRGB leds[NUM_LEDS];
long int inByte;
long int b_inByte;
int* one;
int count = 255;
bool descer = true;
int ms = 2000;
unsigned long time_now;
bool waiting = false;
uint8_t gHue = 0;
int spd = 0;



String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
String commandString = "";
int r = 0;
int g = 0;
int b = 0;


void setup()
{
  //attachInterrupt(digitalPinToInterrupt(2), mudarModo, RISING);
  spd = 90;
  FastLED.addLeds<WS2812, LED_PIN, GRB>(leds, NUM_LEDS);
  Serial.begin(9600);
}

int* getColour() {
  int* colour;
  int i;

  i = 0;

  //for some reason it only works if we put a dud value between the C and
  // the R value
  while (i < 4)
  {
    if (Serial.available() > 0) {
        colour[i] = Serial.read();
        i++;
    }
  }

  return colour;
}

//================================================================================//
//=                                   LOOP                                       =//
//================================================================================//
void loop()
{
  if(stringComplete)
  {
    stringComplete = false;

    getInfo();

    //Color Picker - Leds acendem todos com a cor escolhida
    if(commandString.equals("C"))
    {
      colorPicker(r, g, b);
    }
    
    //BOTAO Breathing - Leds acendem e apagam com fade
    else if(commandString.equals("B"))
    {
      breathing();
    }
  
    //BOTAO Flash - Leds piscam
    else if(commandString.equals("F"))
    {
      flash();
    }
  
    //BOTAO Rainbow - Arco Iris
    else if(commandString.equals("R"))
    {
      rainbow();
    }
  
    //BOTAO Random - Estafeta entre leds com cor aleatoria no primeiro
    else if(commandString.equals("A"))
    {
      allRandom(map(spd, 90, 0, 5000, 50));
    }
  
    //BOTAO Boomerang - Cor aleatoria vai ao fim e volta
    else if(commandString.equals("D"))
    {
      //colorWipe(randomColor(), spd, 0);
      cylon(randomColor(), 7, spd);
    }
  
    //BOTAO Lightning
    else if(commandString.equals("G"))
    {
      lightning(randomColor(), randInRange(1, 5), 1, spd);
    }
  
    else if(commandString.equals("I"))
    {
      theaterChaseRainbow(1, spd);
    }
  
    else if(commandString.equals("K"))
    {
      clearAll();
    }
    //BOTAO SPEED - Mudar velocidade entre 90 e 0
    else if(commandString.equals("E"))
    {
      spd -= 10;
      if(spd < 0)
        spd = 90;
      inByte = b_inByte;
    }

    inputString = "";
    commandString = "";
  }
}

void getInfo()
{
  int first_index = inputString.indexOf("#");
  int second_index = inputString.substring(first_index + 1, inputString.length()).indexOf("#") + first_index + 1;
  int third_index = inputString.substring(second_index + 1, inputString.length()).indexOf("#") + second_index + 1;
  int fourth_index = inputString.substring(third_index + 1, inputString.length()).indexOf("#") + third_index + 1;
  
  commandString = inputString.substring(0, first_index);

  if(commandString == "C")
  {
    r = inputString.substring(first_index + 1, second_index).toInt() + 0;
    g = inputString.substring(second_index + 1, third_index).toInt() + 0;
    b = inputString.substring(third_index + 1, fourth_index).toInt() + 0;
  }
}

//================================================================================//
//=                                   PATTERNS                                   =//
//================================================================================//
void colorPicker(int r, int g, int b)
{
  for(int j = 0 ; j < NUM_LEDS ; j++)
  {
    leds[j].r = r; leds[j].g = g; leds[j].b = b;
  }
  FastLED.show();
}

// Sliding bar across LEDs
void cylon(CRGB c, int width, int speed)
{
  // First slide the leds in one direction
  for(int i = 0; i <= NUM_LEDS-width; i++) {
    for(int j=0; j<width; j++){
      leds[i+j] = c;
    }

    FastLED.show();

    // now that we've shown the leds, reset to black for next loop
    for(int j=0; j<5; j++){
      leds[i+j] = CRGB::Black;
    }
    delay(speed);
  }

  // Now go in the other direction.
  for(int i = NUM_LEDS-width; i >= 0; i--) {
    for(int j=0; j<width; j++){
      leds[i+j] = c;
    }
    FastLED.show();
    for(int j=0; j<width; j++){
      leds[i+j] = CRGB::Black;
    }

    delay(speed);
  }
}

void rainbow()
{
  for(int i = 0 ; i < 4 ; i++)
    {
      int* j = Serial.read();
    }
    static uint8_t hue = 0;
    FastLED.showColor(CHSV(hue++, 255, 255));
    delay(10);
}

void allRandom(int ms)
{
  if(!waiting)
    {
      time_now = millis();
      waiting = true;
    }
    if(millis() > time_now + ms)
    {
      for(int i=0; i<NUM_LEDS; i++)
      {
        leds[i] = randomColor();
      }
      FastLED.show();
      time_now = millis();
      waiting = false;
    }
}

void flash()
{
  for(int i = 0 ; i < 4 ; i++)
  {
    int* j = Serial.read();
  }
  if(!waiting)
  {
    time_now = millis();
    waiting = true;
  }
  if(millis() > time_now + map(spd, 90, 0, 5000, 10))
  {
    if(!descer)
    {
      FastLED.setBrightness(255);
      FastLED.show();
      descer = true;
    }
    else
    {
      FastLED.setBrightness(0);
      FastLED.show();
      descer = false;
    }
    time_now = millis();
    waiting = false;
  }
}

void breathing()
{
  for(int i = 0 ; i < 4 ; i++)
  {
    int* j = Serial.read();
  }
  if(count == 0 && descer)
  {
    if(!waiting)
    {
      time_now = millis();
      waiting = true;
    }
    if(millis() > time_now + map(spd, 90, 0, 5000, 10))
    {
      descer = false;
      time_now = millis();
      waiting = false;
    }
  }
  if(count == 255 && !descer)
  {
    if(!waiting)
    {
      time_now = millis();
      waiting = true;
    }
    if(millis() > time_now + map(spd, 90, 0, 5000, 10))
    {
      descer = true;
      time_now = millis();
      waiting = false;
    }
  }
  if(count > 0 && descer)
  {
    FastLED.setBrightness(count);
    delay(10);
    FastLED.show();
    count--;
  }
  else if(count < 255 && !descer)
  {
    FastLED.setBrightness(count);
    delay(10);
    FastLED.show();
    count++;
  }
}

// Wipes color from end to end
void colorWipe(CRGB c, int speed, int direction)
{
  for(int j = 0; j < NUM_LEDS ; j++)
  {
    leds[j] = CRGB::Black;
  }
  FastLED.show();
  for(int i=0; i<NUM_LEDS; i++){
    if(direction == 0){
      leds[i] = c;
    }
    else{
      leds[NUM_LEDS-1-i] = c;
    }
    FastLED.show();
    delay(speed);
  }
}

// Theater-style crawling lights with rainbow effect
void theaterChaseRainbow(int cycles, int speed)
{
  for (int j=0; j < 256 * cycles; j++) {     // cycle all 256 colors in the wheel
    for (int q=0; q < 3; q++) {
      for (int i=0; i < NUM_LEDS; i=i+3) {
        int pos = i+q;
        leds[pos] = Wheel( (i+j) % 255);    //turn every third pixel on
      }
      FastLED.show();

      delay(speed);

      for (int i=0; i < NUM_LEDS; i=i+3) {
        leds[i+q] = CRGB::Black;  //turn every third pixel off
      }
    }
  }
}

// Random flashes of lightning
void lightning(CRGB c, int simultaneous, int cycles, int speed)
{
  int flashes[simultaneous];

  for(int i=0; i<cycles; i++){
    for(int j=0; j<simultaneous; j++){
      int idx = random(NUM_LEDS);
      flashes[j] = idx;
      leds[idx] = c ? c : randomColor();
    }
    FastLED.show();
    delay(speed);
    for(int s=0; s<simultaneous; s++){
      leds[flashes[s]] = CRGB::Black;
    }
    delay(speed);
  }
}

//================================================================================//
//=                                   SERIAL                                     =//
//================================================================================//
void serialEvent()
{
  while(Serial.available())
  {
    // get the new byte
    char inChar = (char)Serial.read();

    // add it to the input string
    inputString += inChar;

    // if the incoming character is a newline, set a flag
    // so the main loop can act
    if(inChar == '!')
    {
      stringComplete = true;
    }
  }
}

void getCommand()
{
  if(inputString.length() > 0)
  {
    commandString = inputString.substring(0, 1);
  }
}

void mudarModo()
{
  delay(50);
  if (Serial.available() > 0)
  {
    // get incoming byte:
    inByte = Serial.read();
    for(int i = 0 ; i < 4 ; i++)
    {
      one[i] = Serial.read();
    }
  }
}

//================================================================================//
//=                                   MISC                                       =//
//================================================================================//

void clearAll()
{
  delay(50);
  if (Serial.available() > 0)
  {
    // get incoming byte:
    inByte = Serial.read();
    for(int i = 0 ; i < 4 ; i++)
    {
      one[i] = Serial.read();
    }
  }
  for(int i = 0 ; i < NUM_LEDS ; i++)
  {
    leds[i] = CRGB::Black;
  }
  FastLED.setBrightness(255);
  FastLED.show();
  count = 255;
  descer = true;
  waiting = false;
}

// Input a value 0 to 255 to get a color value.
// The colours are a transition r - g - b - back to r.
CRGB Wheel(byte WheelPos)
{
  if(WheelPos < 85) {
    return CRGB(WheelPos * 3, 255 - WheelPos * 3, 0);
  }
  else if(WheelPos < 170) {
    WheelPos -= 85;
    return CRGB(255 - WheelPos * 3, 0, WheelPos * 3);
  }
  else {
    WheelPos -= 170;
    return CRGB(0, WheelPos * 3, 255 - WheelPos * 3);
  }
}

CRGB randomColor()
{
  return Wheel(random(256));
}

// Changes all LEDS to given color
void allColor(CRGB c)
{
  for(int i=0; i<NUM_LEDS; i++){
    leds[i] = c;
  }
  FastLED.show();
}

int randInRange(int min, int max)
{
  return min + (int) (rand() / (double) (RAND_MAX + 1) * (max - min + 1));
}
