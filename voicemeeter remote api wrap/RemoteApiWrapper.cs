﻿using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    using AtgDev.Utils.Native;
    // Aliases for correct types from DLL's documentation 
    using vmLong = Int32;
    using vmFloat = Single;

    /// <summary>
    /// List of Voicemeeter fucntions/parameters
    /// <list type="table">
    ///     <listheader>
    ///         <term>Parameter name</term>
    ///         <description>test</description>
    ///     </listheader>
    ///     <listheader>
    ///         <term>Value range</term>
    ///         <description>test</description>
    ///     </listheader>
    ///     <listheader>
    ///         <term>Remark</term>
    ///         <description>test</description>
    ///     </listheader>
    ///     <item>
    ///         <description>"Strip[i].Mono"</description>
    ///         <description>0 (off) or 1 (on)</description>
    ///         <description>Mono Button</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <summary>
    ///     <para>Voicemeeter Remote API</para>
    ///     Wrapper for library that allows communication with Voicemeeter Applications
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
            m_getVoicemeeterType = GetReadyDelegate<VBVMR_GetVoicemeeterType>();
            m_isParametersDirty = GetReadyDelegate<VBVMR_IsParametersDirty>();
            m_getParameterFloat = GetReadyDelegate<VBVMR_GetParameterFloat>();
            m_getParameterString = GetReadyDelegate<VBVMR_GetParameterStringA>();
        }

        private IntPtr ConvertStringToNullTermAscii(in string str)
        {
            return Marshal.StringToHGlobalAnsi(str);
        }


        private delegate vmLong VBVMR_Login();
        private VBVMR_Login m_login;
        ///<summary>
        ///    Open Communication Pipe With Voicemeeter (typically called on software startup).
        ///</summary>
        ///<returns> 
        ///    0: OK (no error).<br/>
        ///    1: OK but Voicemeeter Application not launched.<br/>
        ///    -1: cannot get client (unexpected)<br/>
        ///    -2: unexpected login (logout was expected before).<br/>
        ///</returns>
        public vmLong Login()
        {
            return m_login();
        }

        private delegate vmLong VBVMR_Logout();
        private VBVMR_Logout m_logout;
        ///<summary>
        ///    Close Communication Pipe With Voicemeeter (typically called on software end).
        ///</summary>
        ///<returns>
        ///    0 if ok.<br/>
        ///</returns>
        public vmLong Logout()
        {
            return m_logout();
        }

        private delegate vmLong VBVMR_GetVoicemeeterType(out vmLong type);
        private VBVMR_GetVoicemeeterType m_getVoicemeeterType;
        ///<summary>
        ///    Get Voicemeeter Type.
        ///</summary>
        ///<param name="type">The variable receiving the type (1 = Voicemeeter, 2 = Voicemeeter Banana, 3 = Voicemeeter Potato).</param>
        ///<returns>
        ///    0: OK (no error).<br/>
        ///    -1: cannot get client (unexpected)<br/>
        ///    -2: no server.<br/>
        ///</returns>
        public vmLong GetVoicemeeterType(out vmLong type)
        {
            return m_getVoicemeeterType(out type);
        }

        private delegate vmLong VBVMR_IsParametersDirty();
        private VBVMR_IsParametersDirty m_isParametersDirty;
        ///<summary>
        ///    Check if parameters have changed.
        ///    Call this function periodically (typically every 10 or 20ms).
        ///</summary>
        ///<returns>
        ///    0: no new paramters.<br/>
        ///    1: New parameters -> update your display.<br/>
        ///    -1: error (unexpected)<br/>
        ///    -2: no server.<br/>
        ///</returns>
        public vmLong IsParametersDirty()
        {
            return m_isParametersDirty();
        }

        // using just string for paramName works, 
        // but stil better to convert it to null terminated ASCII according to DLL header just in case
        private delegate vmLong VBVMR_GetParameterFloat(IntPtr paramName, out vmFloat value);
        private VBVMR_GetParameterFloat m_getParameterFloat;
        ///<summary>
        ///    Get parameter value.
        ///</summary>
        ///<param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        ///<param name="val">The float variable receiving the wanted value.</param>
        ///<returns>
        ///    0: OK (no error).<br/>
        ///    -1: error<br/>
        ///    -2: no server.<br/>
        ///    -3: unknown parameter<br/>
        ///    -5: structure mismatch<br/>
        ///</returns>
        public vmLong GetParameter(string paramName, out vmFloat val)
        {
            var paramNamePtr = ConvertStringToNullTermAscii(in paramName);
            return m_getParameterFloat(paramNamePtr, out val);
        }

        private delegate vmLong VBVMR_GetParameterStringA(IntPtr paramName, StringBuilder strVal);
        private VBVMR_GetParameterStringA m_getParameterString;
        ///<summary>
        ///    Get parameter value.
        ///</summary>
        ///<param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        ///<param name="strVal">The string variable receiving the wanted value.</param>
        ///<returns>
        ///    0: OK (no error).<br/>
        ///    -1: error<br/>
        ///    -2: no server.<br/>
        ///    -3: unknown parameter<br/>
        ///    -5: structure mismatch<br/>
        ///</returns>
        public vmLong GetParameter(string paramName, out string strVal)
        {
            var strB = new StringBuilder(512);
            var paramNamePtr = ConvertStringToNullTermAscii(in paramName);
            var resp = m_getParameterString(paramNamePtr, strB);
            strVal = strB.ToString();
            return resp;
        }
    }
}
