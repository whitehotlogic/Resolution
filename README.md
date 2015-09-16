# Resolution
I updated this csharp project from codeproject so that it supports two monitors instead of one -- which seemed more complicated than it should’ve been. I couldn’t seem to find a full solution for multiple monitors like this anywhere else -- this one supports infinite monitors. Theoretically, it would work on a giant monitor wall. It can even be used as a base class for changing any display configuration setting, not just resolution.

So hopefully it helps someone else. 

Special thanks to original developer, Sreejith SS Nair @ http://www.codeproject.com/Articles/6810/Dynamic-Screen-Resolution

License: Code Project Open License (CPOL) 1.02 (http://www.codeproject.com/info/cpol10.aspx)

How To Use It:
//=================================
int desiredWidth = 1024;
int desiredHeight = 768;
Screen screen = Screen.AllScreens[0];
Resolution.CResolution ChangeRes = new Resolution.CResolution(desiredWidth, desiredHeight, screen);
//=================================

Todo:
Remove redundant constants.
Fix issue where display ordering configuration is lost.
