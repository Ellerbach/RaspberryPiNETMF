This project's goal is to facilitate migraiton of .NET Microframework code (NETMF) to mono or .NET Core on RaspberryPi. This allow with very little to your original NETMF code to run it just like this on a RPI. What you have to adjust are the GPIO numbers in your projects and you're good to go! 
Please note that the RPI do not support analogic inputs, so only projects with serial ports or SPI, I2C or digital IO will be able to work.

# Notes
- This is complementary to the Windows 10 IoT approach.
- This project reduces the time to migrate code vs to a Universal Windows App.
- Performance are not the best ones but largemey enough for SPI, I2C, serial port communications

# Prerequirement
- You need to install on your RPI the [WiringPI libraries](http://wiringpi.com/ "WiringPI libraries")
- You need to install either [.NE Core](https://www.microsoft.com/net/core#linuxdebian ".NET Core") (choose the version of Linux you are using) or [Mono](http://www.mono-project.com/docs/getting-started/install/linux/ "Mono") on the Rapsberry

# Known issues
- using serial port many need to remove fully the IO definitions from the code and replace some of your exisintg code. This has been tested very little
- Tri state buttons are emulated, performances are limited
- some limitations with the built in components of RPI but worked for most of my cases 