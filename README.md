# Voicemeeter Remote API dll C# dynamic wrapper
[![nuget](https://img.shields.io/nuget/v/a-tg.VmrapiDynWrap)](https://www.nuget.org/packages/a-tg.VmrapiDynWrap)

Visual Studio C# [**Shared Project**](https://github.com/A-tG/Voicemeeter-Remote-API-dll-dynamic-wrapper/wiki/Useful-Info#how-to-useadd-a-visual-studio-shared-project).

Helps to dynamically load VoicemeeterRemote64.dll or VoicemeeterRemote.dll.

**Basic return codes, methods and types similar to original DLL. Use extended class for more functionality or you can write your own.**

### Note for 1.2 and later versions:
I optimized marshalling by replacing "MarshalAs" attributes with pointers and then allocating buffers on stack, and by sending pointer to a string with "fixed" statement. So there is a risk of getting stack overflow in some unusual cases, so keep in mind: maximum allocation size is 1024 bytes for out string in
```csharp
GetParameter(string paramName, out string strVal)
``` 
plus for all Get/SetParameter funcitons - parameter's name length * 1 + 1 byte (so "Strip[0].Gain" == 15 bytes (with null character)). 
And there is also overloaded variants without allocation that recieve IntPtr as arguments, so you can use pre-allocated buffers (using something like Marshal.StringToHGlobalAnsi(), Marshal.AllocHGlobal(), or pinning managed array) to achieve native-like performance.

And regarding performance: on AMD Ryzen2600x you can except 66 ns for GetParameter(string, float), and for variant with pre-allocated buffers 55-60 ns for "out string" and "out float" variant. Overall now it is 1.5-2x faster than pre 1.2 version.


### Usage:
```csharp
using AtgDev.Voicemeeter;
using AtgDev.Voicemeeter.Utils;

var vmrApi = new RemoteApi(PathHelper.GetDllPath())
```

All API procedures have been implemented at the moment (March 2021).

Backward compability with older API DLL's versions: no crash if you try to load old DLL without Audio callback and Macro buttons procedures (possible to add backward compability to other methods if needed)

Not tested on 32 bit Windows manually, but supposed to work anyway.

**Unsafe code required to use Audio Callback efficiently**

[Example how to use callback](https://github.com/A-tG/Voicemeeter-AudioCallback-Simple-Example/blob/main/Voicemeeter%20Audio%20Callback%20Simple%20Example/Program.cs)

**.NET 5.0 will be required to support possible cross platform availability in the future**

### Not tested:
* VBVMR_GetMidiMessage (no midi device to test)

### Uses:
* [Base class](https://github.com/A-tG/Dynamic-wrapper-for-unmanaged-dll)

### Extended:
* [Extended class with custom types and methods](https://github.com/A-tG/voicemeeter-remote-api-extended)

## Do you like my projects? Donate
[![Paypal Logo](https://www.paypalobjects.com/webstatic/paypalme/images/pp_logo_small.png)](https://www.paypal.me/atgDeveloperMusician/5)
