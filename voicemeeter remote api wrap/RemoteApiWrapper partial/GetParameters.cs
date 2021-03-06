﻿using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitGetParameters()
        {
            m_isParametersDirty = GetReadyDelegate<VBVMR_IsParametersDirty>();
            m_getParameterFloat = GetReadyDelegate<VBVMR_GetParameterFloat>();
            m_getParameterStringA = GetReadyDelegate<VBVMR_GetParameterStringA>();
            m_getParameterStringW = GetReadyDelegate<VBVMR_GetParameterStringW>();
        }

        private delegate Int32 VBVMR_IsParametersDirty();
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
        public Int32 IsParametersDirty()
        {
            return m_isParametersDirty();
        }

        private delegate Int32 VBVMR_GetParameterFloat([MarshalAs(UnmanagedType.LPStr)] string paramName, out Single value);
        private VBVMR_GetParameterFloat m_getParameterFloat;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="val">The variable receiving the wanted value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public Int32 GetParameter(string paramName, out Single val)
        {
            return m_getParameterFloat(paramName, out val);
        }

        private delegate Int32 VBVMR_GetParameterStringA(
            [MarshalAs(UnmanagedType.LPStr)] string paramName,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder strVal
        );
        private VBVMR_GetParameterStringA m_getParameterStringA;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strVal">The string variable receiving the wanted value. (ANSI)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public Int32 Legacy_GetParameter(string paramName, out string strVal)
        {
            // 512 characters according to DLL documentation
            var strB = new StringBuilder(512);
            var resp = m_getParameterStringA(paramName, strB);
            strVal = strB.ToString();
            return resp;
        }

        private delegate Int32 VBVMR_GetParameterStringW(
            [MarshalAs(UnmanagedType.LPStr)] string paramName,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder strVal
        );
        private VBVMR_GetParameterStringW m_getParameterStringW;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strVal">The string variable receiving the wanted value. (UTF-16)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public Int32 GetParameter(string paramName, out string strVal)
        {
            // 512 characters according to DLL documentation
            var strB = new StringBuilder(512);
            var resp = m_getParameterStringW(paramName, strB);
            strVal = strB.ToString();
            return resp;
        }
    }
}
