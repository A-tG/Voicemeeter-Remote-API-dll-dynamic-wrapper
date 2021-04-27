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

        private const int ProcedureNotImportedErrorCode = -100;

        private void InitProcedures()
        {
            InitLogin();
            InitGeneralInformation();
            InitGetParameters();
            InitGetLevels();
            InitSetParameters();
            InitDevicesEnumerator();
            InitAudioCallback();
            InitMacroButtons();
        }
    }
}
