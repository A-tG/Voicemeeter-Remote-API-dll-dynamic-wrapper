﻿using System;

namespace AtgDev.Voicemeeter
{
    using AtgDev.Utils.Native;

    /// <summary>
    ///     <para>Voicemeeter Remote API</para>
    ///     Wrapper for the library that allows communication with Voicemeeter Applications
    /// </summary>
    public partial class RemoteApiWrapper : DllWrapperBase
    {
        public const int ProcedureNotImportedErrorCode = -100;

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
            InitDevicesEnumerator();
            InitAudioCallback();
            InitMacroButtons();
        }
    }
}
