# Voicemeeter Remote API dll C# dynamic wrapper
 Visual Studio C# **Shared Project**. 
 
 Helps to dynamically load VoicemeeterRemote64.dll or VoicemeeterRemote.dll.
 
 Not tested manually on 32 bit Windows, but should work anyway.
 
 **Unsafe code required to use Audio Callback efficiently**

 ### Not tested:
 * VBVMR_GetMidiMessage (no midi device to test)
 
 [Depends on base class](https://github.com/A-tG/Dynamic-wrapper-for-umanaged-dll/blob/main/dll%20wrapper%20base/DllWrapperBase.cs)
 
 [Extended class with custom types and methods](https://github.com/A-tG/voicemeeter-remote-api-extended)
