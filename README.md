# Voicemeeter Remote API dll C# dynamic wrapper
 Visual Studio C# **Shared Project**. 
 
 Helps dynamically load VoicemeeterRemote64.dll or VoicemeeterRemote.dll depending on OS. Not tested on 32 bit Windows. 
 
 **WIP** - Not all procedures are implemented and properly tested.
 ### Not implemented yet:
 * VBVMR_GetMidiMessage - *need some research how to marshal as unsinged char pointer*
 * VBVMR_AudioCallbackRegister
 * VBVMR_AudioCallbackStart
 * VBVMR_AudioCallbackStop
 * VBVMR_AudioCallbackUnregister
 
 [Depends on base class](https://github.com/A-tG/Dynamic-wrapper-for-umanaged-dll/blob/main/dll%20wrapper%20base/DllWrapperBase.cs)
 
 [Etended class with custom types and methods](https://github.com/A-tG/voicemeeter-remote-api-extended)
