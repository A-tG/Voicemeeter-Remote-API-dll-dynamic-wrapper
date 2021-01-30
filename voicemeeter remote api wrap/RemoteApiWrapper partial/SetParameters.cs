using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitSetParameters()
        {
            m_setParameterFloat = GetReadyDelegate<VBVMR_SetParameterFloat>();
            m_setParameterStringA = GetReadyDelegate<VBVMR_SetParameterStringA>();
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private delegate Int32 VBVMR_SetParameterFloat([MarshalAs(UnmanagedType.LPStr)] string paramName, Single val);
        private VBVMR_SetParameterFloat m_setParameterFloat;
        /// <summary>
        ///     Set a single parameter.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="val">The variable containing the new value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        public Int32 SetParameter(string paramName, Single val)
        {
            return m_setParameterFloat(paramName, val);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private delegate Int32 VBVMR_SetParameterStringA(
            [MarshalAs(UnmanagedType.LPStr)] string paramName,
            [MarshalAs(UnmanagedType.LPStr)] string strVal
        );
        private VBVMR_SetParameterStringA m_setParameterStringA;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see parameters name table)</param>
        /// <param name="strVal">The variable containing the new value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        public Int32 SetParameter(string paramName, string strVal)
        {
            return m_setParameterStringA(paramName, strVal);
        }

        // VBVMR_SetParameters()
    }
}
