# SygmaCrypt
Visual Basic.NET desktop app for showing the simplest encryption

## How to run
- Either open `Encryption.sln` in your Visual Studio and press `Start` button
- Or run the executable file `/Encryption/obj/Debug/SygmaCrypt.exe`

## Usage
- You can enter text and password with which this text will be encrypted
- You can also enter already encrypted text and password for it and decrypt it to original unencrypted state
- The encryption technique is very simple:
    - The algorithm just adds corresponding ASCII character values from the text and password and outputs this "summed" character back
- The decryption is very similar to encryption but instead of adding two values, here password value is substracted from text value
