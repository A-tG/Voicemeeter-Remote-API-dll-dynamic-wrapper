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

        private delegate Int32 VBVMR_GetParameterFloat(IntPtr paramNamePtr, out Single value);
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
            var paramNamePtr = Marshal.StringToHGlobalAnsi(paramName);
            var res = m_getParameterFloat(paramNamePtr, out val);
            Marshal.FreeHGlobal(paramNamePtr);

            return res;
        }

        /// <summary>
        ///     Get parameter value. Alternative low-level, faster method.
        /// </summary>
        /// <param name="paramNamePtr">Buffer pointer (null terminated ASCII) with the name of the parameter 
        /// (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="val">The variable receiving the wanted value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public Int32 GetParameter(IntPtr paramNamePtr, out Single val)
        {
            return m_getParameterFloat(paramNamePtr, out val);

        }

        private delegate Int32 VBVMR_GetParameterStringA(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_GetParameterStringA m_getParameterStringA;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strVal">The string variable receiving the wanted value. (ASCII)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public Int32 Legacy_GetParameter(string paramName, out string strVal)
        {
            var paramNamePtr = Marshal.StringToHGlobalAnsi(paramName);
            // 512 of char (8 bits) according to DLL documentation
            var strValPtr = Marshal.AllocHGlobal(512);

            var resp = m_getParameterStringA(paramNamePtr, strValPtr);
            strVal = Marshal.PtrToStringAnsi(strValPtr) ?? string.Empty;

            Marshal.FreeHGlobal(paramNamePtr);
            Marshal.FreeHGlobal(strValPtr);

            return resp;
        }

        private delegate Int32 VBVMR_GetParameterStringW(IntPtr paramNamePtr, IntPtr strValPtr);
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
            var paramNamePtr = Marshal.StringToHGlobalAnsi(paramName);
            // 512 of unsigned short (16 bits) according to DLL documentation
            var strValPtr = Marshal.AllocHGlobal(512 * 2);

            var resp = m_getParameterStringW(paramNamePtr, strValPtr);
            strVal = Marshal.PtrToStringUni(strValPtr) ?? string.Empty;

            Marshal.FreeHGlobal(paramNamePtr);
            Marshal.FreeHGlobal(strValPtr);

            return resp;
        }

        /// <summary>
        ///     Get parameter value. Alternative low-level, faster method.
        /// </summary>
        /// <param name="paramBuffPtr">Buffer pointer (null terminated ASCII) with the name of the parameter 
        /// (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strBuffPtr">Buffer pointer receiving the wanted value, 512 size (512 * 2 bytes)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public Int32 GetParameter(IntPtr paramPtr, IntPtr strValPtr)
        {
            return m_getParameterStringW(paramPtr, strValPtr);
        }
    }
}
