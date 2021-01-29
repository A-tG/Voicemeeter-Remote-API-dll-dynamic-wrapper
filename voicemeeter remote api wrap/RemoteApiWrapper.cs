using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    using AtgDev.Utils.Native;
    // Aliases for correct types from DLL's documentation 
    using vmLong = Int32;
    using vmFloat = Single;

    /// <summary>
    ///     <para>Voicemeeter Remote API</para>
    ///     Wrapper for the library that allows communication with Voicemeeter Applications
    /// </summary>
    class RemoteApiWrapper : DllWrapperBase
    {
        public RemoteApiWrapper(string dllPath) : base(dllPath) 
        {
            InitProcedures();
        }

        private void InitProcedures()
        {
            m_login = GetReadyDelegate<VBVMR_Login>();
            m_logout = GetReadyDelegate<VBVMR_Logout>();
            m_runVoicemeeter = GetReadyDelegate<VBVMR_RunVoicemeeter>();

            m_getVoicemeeterType = GetReadyDelegate<VBVMR_GetVoicemeeterType>();
            m_getVoicemeeterVersion = GetReadyDelegate<VBVMR_GetVoicemeeterVersion>();

            m_isParametersDirty = GetReadyDelegate<VBVMR_IsParametersDirty>();
            m_getParameterFloat = GetReadyDelegate<VBVMR_GetParameterFloat>();
            m_getParameterString = GetReadyDelegate<VBVMR_GetParameterStringA>();

            m_macroButtonIsDirty = GetReadyDelegate<VBVMR_MacroButton_IsDirty>();
            m_macroButtonGetStatus = GetReadyDelegate<VBVMR_MacroButton_GetStatus>();
        }

        // LOGIN
        private delegate vmLong VBVMR_Login();
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
        public vmLong Login()
        {
            return m_login();
        }

        private delegate vmLong VBVMR_Logout();
        private VBVMR_Logout m_logout;
        /// <summary>
        ///     Close Communication Pipe With Voicemeeter (typically called on software end).
        /// </summary>
        /// <returns>
        ///     0 if ok.<br/>
        /// </returns>
        public vmLong Logout()
        {
            return m_logout();
        }
        
        private delegate vmLong VBVMR_RunVoicemeeter(vmLong type);
        private VBVMR_RunVoicemeeter m_runVoicemeeter;
        /// <summary>
        ///     Run Voicemeeter Application (get installation directory and run Voicemeeter Application).
        /// </summary>
        /// <param name="type">Voicemeeter type (1 = Voicemeeter, 2 = Voicemeeter Banana).</param>
        /// <returns>
        ///     0: Ok.<br/>
		/// 	   -1: not installed<br/>
        /// </returns>
        public vmLong RunVoicemeeter(vmLong type)
        {
            return m_runVoicemeeter(type);
        }

        // GENERAL INFORMATION
        private delegate vmLong VBVMR_GetVoicemeeterType(out vmLong type);
        private VBVMR_GetVoicemeeterType m_getVoicemeeterType;
        /// <summary>
        ///     Get Voicemeeter Type.
        /// </summary>
        /// <param name="type">The variable receiving the type (1 = Voicemeeter, 2 = Voicemeeter Banana, 3 = Voicemeeter Potato).</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: cannot get client (unexpected)<br/>
        ///     -2: no server.<br/>
        /// </returns>
        public vmLong GetVoicemeeterType(out vmLong type)
        {
            return m_getVoicemeeterType(out type);
        }

        private delegate vmLong VBVMR_GetVoicemeeterVersion(out vmLong ver);
        private VBVMR_GetVoicemeeterVersion m_getVoicemeeterVersion;
        /// <summary>
        ///     Get Voicemeeter Version
        /// </summary>
        /// <param name="ver">
        ///     Variable receiving the version (v1.v2.v3.v4)<br/>
        ///     <c>
        ///          v1 = (version &amp; 0xFF000000)>>24;<br/>
		/// 	        v2 = (version &amp; 0x00FF0000)>>16;<br/>
		/// 	        v3 = (version &amp; 0x0000FF00)>>8;<br/>
		/// 	        v4 = version &amp; 0x000000FF;<br/>
        /// 	   </c>
        /// </param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: cannot get client (unexpected)<br/>
        ///     -2: no server.<br/>
        /// </returns>
        public vmLong GetVoicemeeterVersion(out vmLong ver)
        {
            return m_getVoicemeeterVersion(out ver);
        }

        // GET PARAMETERS
        private delegate vmLong VBVMR_IsParametersDirty();
        private VBVMR_IsParametersDirty m_isParametersDirty;
        /// <summary>
        ///     Check if parameters have changed.<br/>
        ///     Call this function periodically (typically every 10 or 20ms).
        /// </summary>
        /// <returns>
        ///     0: no new paramters.<br/>
        ///     1: New parameters -> update your display.<br/>
        ///     -1: error (unexpected)<br/>
        ///     -2: no server.<br/>
        /// </returns>
        public vmLong IsParametersDirty()
        {
            return m_isParametersDirty();
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private delegate vmLong VBVMR_GetParameterFloat([MarshalAs(UnmanagedType.LPStr)] string paramName, out vmFloat value);
        private VBVMR_GetParameterFloat m_getParameterFloat;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="val">The float variable receiving the wanted value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public vmLong GetParameter(string paramName, out vmFloat val)
        {
            return m_getParameterFloat(paramName, out val);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private delegate vmLong VBVMR_GetParameterStringA([MarshalAs(UnmanagedType.LPStr)] string paramName, StringBuilder strVal);
        private VBVMR_GetParameterStringA m_getParameterString;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strVal">The string variable receiving the wanted value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public vmLong GetParameter(string paramName, out string strVal)
        {
            var strB = new StringBuilder(512);
            var resp = m_getParameterString(paramName, strB);
            strVal = strB.ToString();
            return resp;
        }

        // GET LEVELS
        // VBVMR_GetLevel()

        // VBVMR_GetMidiMessage()

        // SET PARAMETERS
        // VBVMR_SetParameterFloat()

        // VBVMR_SetParameterStringA()

        // VBVMR_SetParameters()

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

        // MACRO BUTTONS
        public enum MacrobuttonMode
        {
            /// <summary>PUSH or RELEASE state</summary>
            Default = 0,
            /// <summary>change Displayed State only</summary>
            State = 2,
            /// <summary>change Trigger State</summary>
            Trigger = 3
        }

        private delegate vmLong VBVMR_MacroButton_IsDirty();
        private VBVMR_MacroButton_IsDirty m_macroButtonIsDirty;
        /// <summary>
        ///     Check if Macro Buttons states changed.<br/>   
        ///     Call this function periodically (typically every 50 or 500ms) to know if something happen on MacroButton states.<br/>
        ///     (this function must be called from one thread only)<br/>
        /// </summary>
        /// <returns>
        ///      0: no new status.br/>
        ///     >0: last nu logical button status changed.br/>
        ///     -1: error (unexpected)br/>
        ///     -2: no server.
        /// </returns>
        public vmLong MacroButtonIsDirty()
        {
            return m_macroButtonIsDirty();
        }

        // VBVMR_MacroButton_GetStatus()

        private delegate vmLong VBVMR_MacroButton_GetStatus(vmLong buttonIndex, out vmFloat val, MacrobuttonMode mode);
        private VBVMR_MacroButton_GetStatus m_macroButtonGetStatus;
        /// <summary>
        ///     Get current status of a given button.
        /// </summary>
        /// <param name="buttonIndex">button index: 0 to 79</param>
        /// <param name="val">Variable receiving the wanted value (0.0 = OFF / 1.0 = ON)</param>
        /// <param name="mode">Define what kind of value you want to read</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public vmLong MacroButtonGetStatus(vmLong buttonIndex, out vmFloat val, MacrobuttonMode mode)
        {
            return m_macroButtonGetStatus(buttonIndex, out val, mode);
        }

        // VBVMR_MacroButton_SetStatus(long nuLogicalButton, float fValue, long bitmode)
    }
}
