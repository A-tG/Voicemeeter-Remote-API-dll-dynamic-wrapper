# Voicemeeter Remote API dll C# dynamic wrapper
 Visual Studio C# **Shared Project**. 
 
 Helps to dynamically load VoicemeeterRemote64.dll or VoicemeeterRemote.dll.
 
 Not tested manually on 32 bit Windows, but should work anyway.
 
 **WIP** - Not all procedures are implemented.
 ### Not implemented yet:
 * VBVMR_AudioCallbackRegister
 * VBVMR_AudioCallbackStart
 * VBVMR_AudioCallbackStop
 * VBVMR_AudioCallbackUnregister

 ### Not tested:
 * VBVMR_GetMidiMessage
 
 [Depends on base class](https://github.com/A-tG/Dynamic-wrapper-for-umanaged-dll/blob/main/dll%20wrapper%20base/DllWrapperBase.cs)
 
 [Extended class with custom types and methods](https://github.com/A-tG/voicemeeter-remote-api-extended)
