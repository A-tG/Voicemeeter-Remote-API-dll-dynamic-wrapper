# Voicemeeter Remote API dll C# dynamic wrapper
[![nuget](https://img.shields.io/nuget/v/a-tg.VmrapiDynWrap)](https://www.nuget.org/packages/a-tg.VmrapiDynWrap)

Visual Studio C# [**Shared Project**](https://github.com/A-tG/Voicemeeter-Remote-API-dll-dynamic-wrapper/wiki/Useful-Info#how-to-useadd-a-visual-studio-shared-project).

Helps to dynamically load VoicemeeterRemote64.dll or VoicemeeterRemote.dll.

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
