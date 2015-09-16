# Resolution
I found some csharp code on codeproject that changes monitor resolutions, but none of it supported multiple monitors. Getting the code updated to support multiple monitors was way more complex than I thought it would be. But I did it -- and the code now supports infinite monitors. Theoretically, it would work on a giant windows-based monitor wall. Resolution.cs can even be used as a base class for changing any display configuration setting, not just resolution.

So hopefully it helps someone else. 

Special thanks to these developers for their code:
* Sreejith SS Nair @ http://www.codeproject.com/Articles/6810/Dynamic-Screen-Resolution
* Mohammad Elsheimy @ http://www.codeproject.com/Articles/36664/Changing-Display-Settings-Programmatically

License: Code Project Open License (CPOL) 1.02 (http://www.codeproject.com/info/cpol10.aspx)

**How To Use It:**

1. Load the project in VS2015
2. grab the resolution.cs file from this repository and use it in your project like this:
```csharp
int desiredWidth = 1024;
int desiredHeight = 768;
Screen screen = Screen.AllScreens[0];
Resolution.CResolution ChangeRes = new Resolution.CResolution(desiredWidth, desiredHeight, screen);
```

Todo:
* Remove redundant constants.
* Fix issue where display ordering configuration is lost.
