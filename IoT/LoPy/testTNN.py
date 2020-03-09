import binascii
import pycom
import socket
import time
from network import LoRa

# Colors
off = 0x000000
red = 0xff0000
green = 0x00ff00
blue = 0x0000ff

# Turn off heartbeat LED
pycom.heartbeat(False)

# Initialize LoRaWAN radio
lora = LoRa(mode=LoRa.LORAWAN, region=LoRa.EU868)

# Set network keys
app_eui = binascii.unhexlify('70B3D57ED002B89D')
app_key = binascii.unhexlify('83835436F069443896B6E122AD957735')

# Join the network
lora.join(activation=LoRa.OTAA, auth=(app_eui, app_key), timeout=0)
#pycom.rgbled(red)

pycom.rgbled(off)
# Loop until joined
while not lora.has_joined():
    print('Not joined yet...')
    time.sleep(2)
    # lora.join(activation=LoRa.OTAA, auth=(app_eui, app_key), timeout=0)

print('Joined')
pycom.rgbled(blue)

s = socket.socket(socket.AF_LORA, socket.SOCK_RAW)
s.setsockopt(socket.SOL_LORA, socket.SO_DR, 5)
s.setblocking(True)

i = 0
while True:
    count = s.send(bytes([i % 256]))
    print('Sent %s bytes' % count)
    pycom.rgbled(green)
    time.sleep(0.1)
    pycom.rgbled(blue)
    time.sleep(9.9)
    i += 1