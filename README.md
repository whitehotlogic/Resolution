# Resolution
I found some csharp code on codeproject that changes monitor resolutions, but none of it supported multiple monitors. Getting the code updated to support multiple monitors was way more complex than I thought it would be. But I did it -- and the code now supports infinite monitors. Theoretically, it would work on a giant windows-based monitor wall. Resolution.cs can even be used as a base class for changing any display configuration setting, not just resolution.

So hopefully it helps someone else. 

Special thanks to these developers for their code:
* Sreejith SS Nair @ http://www.codeproject.com/Articles/6810/Dynamic-Screen-Resolution
* Mohammad Elsheimy @ http://www.codeproject.com/Articles/36664/Changing-Display-Settings-Programmatically

License: Code Project Open License (CPOL) 1.02 (http://www.codeproject.com/info/cpol10.aspx)

**How To Use It:**

Load the project in VS2015 and run it. (includes windows forms example of usage)

**OR**

Grab the resolution.cs file from this repository, add it to your project, and make it do like this:
```csharp
int desiredWidth = 1024;
int desiredHeight = 768;
foreach (Screen display in Screen.AllScreens) {
  Resolution.CResolution ChangeRes = new Resolution.CResolution(desiredWidth, desiredHeight, display);
}
```

Known Issues:
* If your monitors are physically positioned out of the driver-determined order, icons can show up in weird places after setting resolution with this code. Workaround: Configure "Auto-Arrange" for desktop icons.

TODO:
* Create gitignore for all the junk in this repository that doesn't need to be there
* Test with a large array of monitors (currently only tested with dual monitors)
