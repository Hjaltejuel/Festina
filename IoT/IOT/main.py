from machine import I2C, Pin
import mpu6050

i2c = I2C(0) 
accelerometer = mpu6050.accel(i2c)
while True:
    vals = accelerometer.get_values()
    print(vals)