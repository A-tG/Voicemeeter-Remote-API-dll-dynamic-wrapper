using System;

namespace AtgDev.Voicemeeter
{
    using AtgDev.Utils.Native;

    /// <summary>
    ///     <para>Voicemeeter Remote API</para>
    ///     Wrapper for the library that allows communication with Voicemeeter Applications
    /// </summary>
    partial class RemoteApiWrapper : DllWrapperBase
    {
        public RemoteApiWrapper(string dllPath) : base(dllPath) 
        {
            InitProcedures();
        }

        private void InitProcedures()
        {
            InitLogin();
            InitGeneralInformation();
            InitGetParameters();
            InitGetLevels();
            InitSetParameters();
            InitMacroButtons();
        }

        // DEVICES ENUMERATOR
        // VBVMR_Output_GetDeviceNumber()

        // VBVMR_Output_GetDeviceDescA()

        // VBVMR_Input_GetDeviceNumber()

        // VBVMR_Input_GetDeviceDescA()

        // VB-AUDIO CALLBACK
        // VBVMR_AudioCallbackRegister()

        // VBVMR_AudioCallbackStart()

        // VBVMR_AudioCallbackStop()

        // VBVMR_AudioCallbackUnregister()
    }
}
