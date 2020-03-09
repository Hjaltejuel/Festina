import binascii
import pycom
import socket
import time
from network import LoRa
from machine import I2C, Pin
import mpu6050


lora = LoRa(mode=LoRa.LORAWAN, region=LoRa.EU868)

# Set network keys
app_eui = binascii.unhexlify('70B3D57ED002B89D')
app_key = binascii.unhexlify('83835436F069443896B6E122AD957735')

# Join the network
lora.join(activation=LoRa.OTAA, auth=(app_eui, app_key), timeout=0)

while not lora.has_joined():
    print('Not joined yet...')
    time.sleep(2)

print('Joined')
# pycom.rgbled(blue)

s = socket.socket(socket.AF_LORA, socket.SOCK_RAW)
s.setsockopt(socket.SOL_LORA, socket.SO_DR, 5)
s.setblocking(True)


i2c = I2C()
accelerometer = mpu6050.accel(i2c)
i = 1
while True:
   vals = accelerometer.get_values()
   s.send(vals)
   print('Sent: {}'.format(vals))
   i+=1
   time.sleep(1)