import pycom
import time
from machine import Pin


# colors = [0xeb34d2, 0x9ceb34, 0x9ceb34, 0x4934eb]

# pycom.heartbeat(False)
# led = Pin('G16', mode=Pin.OUT, value=1)
# led(1)

# for i in range(0, 10_000):
#     pycom.rgbled(colors[i%len(colors)]) 
#     time.sleep(0.1)

# while True:
#     pycom.rgbled(0xFF0000)  # Red
#     time.sleep(1)
#     pycom.rgbled(0x00FF00)  # Green
#     time.sleep(1)
#     pycom.rgbled(0x0000FF)  # Blue
#     time.sleep(1)