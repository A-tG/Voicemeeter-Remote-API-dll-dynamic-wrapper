using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitAudioCallback()
        {
            return;
        }

        // typedef long (__stdcall* T_VBVMR_VBAUDIOCALLBACK) (void* lpUser, long nCommand, void* lpData, long nnn);

        // long __stdcall VBVMR_AudioCallbackRegister(long mode, T_VBVMR_VBAUDIOCALLBACK pCallback, void * lpUser, char szClientName[64]);

        // long __stdcall VBVMR_AudioCallbackStart(void);

        // long __stdcall VBVMR_AudioCallbackStop(void);

        // long __stdcall VBVMR_AudioCallbackUnregister(void);
    }
}
