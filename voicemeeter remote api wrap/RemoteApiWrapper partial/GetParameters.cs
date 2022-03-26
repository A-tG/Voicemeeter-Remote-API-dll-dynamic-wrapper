using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitGetParameters()
        {
            GetReadyDelegate(ref m_isParametersDirty);
            GetReadyDelegate(ref m_getParameterFloat);
            GetReadyDelegate(ref m_getParameterStringA);
            GetReadyDelegate(ref m_getParameterStringW);
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

        private delegate Int32 VBVMR_GetParameterFloat([In, MarshalAs(UnmanagedType.LPStr)] string paramName, out Single value);
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
            [In, MarshalAs(UnmanagedType.LPStr)] string paramName,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder strVal
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
            [In, MarshalAs(UnmanagedType.LPStr)] string paramName,
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder strVal
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

        /// <summary>
        ///     Get parameter value. Alternative method without string allocation
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strVal">The StringBuilder receiving the wanted value, 512 Capacity. (UTF-16)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException">Thrown if strVal Length is not 512</exception>
        public Int32 GetParameter(string paramName, StringBuilder strVal)
        {
            if (strVal is null) throw new ArgumentNullException($"{nameof(strVal)} is null");

            // 512 characters according to DLL documentation
            if (strVal.Capacity != 512) throw new ArgumentException($"{nameof(strVal)} Capacity have to be 512");

            var resp = m_getParameterStringW(paramName, strVal);
            return resp;
        }
    }
}
