using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitLogin()
        {
            GetReadyDelegate(ref m_login);
            GetReadyDelegate(ref m_logout);
            GetReadyDelegate(ref m_runVoicemeeter);
        }

        private delegate Int32 VBVMR_Login();
        private VBVMR_Login m_login;
        /// <summary>
        ///     Open Communication Pipe With Voicemeeter (typically called on software startup).
        /// </summary>
        /// <returns> 
        ///     0: OK (no error).<br/>
        ///     1: OK but Voicemeeter Application not launched.<br/>
        ///     -1: cannot get client (unexpected)<br/>
        ///     -2: unexpected login (logout was expected before).<br/>
        /// </returns>
        public Int32 Login()
        {
            return m_login();
        }

        private delegate Int32 VBVMR_Logout();
        private VBVMR_Logout m_logout;
        /// <summary>
        ///     Close Communication Pipe With Voicemeeter (typically called on software end).
        /// </summary>
        /// <returns>
        ///     0 if ok.<br/>
        /// </returns>
        public Int32 Logout()
        {
            return m_logout();
        }

        private delegate Int32 VBVMR_RunVoicemeeter(Int32 type);
        private VBVMR_RunVoicemeeter m_runVoicemeeter;
        /// <summary>
        ///     Run Voicemeeter Application (get installation directory and run Voicemeeter Application).
        /// </summary>
        /// <param name="type">Voicemeeter type<br/> 
        ///     1 = Standard<br/>
        ///     2 = Banana<br/>
        ///     3 = Potato<br/>
        ///     4 = Standard x64<br/>
        ///     5 = Banana x64<br/>
        ///     6 = Potato x64<br/>
        ///     10 = VBDeviceCheck<br/>
        ///     11 = VoicemeeterMacroButtons<br/>
        ///     12 = VMStreamerView<br/>
        ///     13 = VoicemeeterBUSMatrix8<br/>
        ///     14 = VoicemeeterBUSGEQ15<br/>
        ///     15 = VBAN2MIDI<br/>
        ///     20 = VBCABLE_ControlPanel<br/>
        ///     21 = VBVMAUX_ControlPanel<br/>
        ///     22 = VBVMVAIO3_ControlPanel<br/>
        ///     23 = VBVoicemeeterVAIO_ControlPanel<br/>
        /// </param>
        /// <returns>
        ///     0: Ok.<br/>
		///     -1: not installed<br/>
        /// </returns>
        public Int32 RunVoicemeeter(Int32 type)
        {
            return m_runVoicemeeter(type);
        }
    }
}
