using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitLogin()
        {
            m_login = GetReadyDelegate<VBVMR_Login>();
            m_logout = GetReadyDelegate<VBVMR_Logout>();
            m_runVoicemeeter = GetReadyDelegate<VBVMR_RunVoicemeeter>();
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
        /// <param name="type">Voicemeeter type (1 = Voicemeeter, 2 = Voicemeeter Banana, 3 = Voicemeeter Potato, 6 = Potato x64).</param>
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
